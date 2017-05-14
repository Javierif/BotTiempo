using BotTiempo.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace BotTiempo.Services
{
    public class TiempoServicio
    {
        public static async Task<TiempoItem> GetTiempo(string lugar)
        {
            //El $ delante es una forma de hacer un string.format rápido para asignar el parametro.
            string uri = $"http://api.apixu.com/v1/current.json?key=a27ce9eeba084165a8a104719171405&q={lugar}";
            var tiempoItem = new TiempoItem();
            //Using sirve para crear un bloque de ejecución donde liberará los recursos al salir.
            using (var client = new WebClient())
            {
                //Descargamos el contenido json sin formateo
                var rawData = await client.DownloadStringTaskAsync(new Uri(uri));
                tiempoItem = JsonConvert.DeserializeObject<TiempoItem>(rawData);
                //Añadimos una comprobación de que se ha encontrado correctamente el lugar.
                if (tiempoItem.location.Equals(null)) {
                    tiempoItem.Status = "ERROR";
                } else
                {
                    tiempoItem.Status = "OK";
                }       
            }
            return tiempoItem;
        }
    }
}