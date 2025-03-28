using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace TicTacToe;
class Program
{
    static async Task Main()
    {
        using (var client = new HttpClient())
        {
            var comentaris = await client.GetFromJsonAsync <List<string>> (
                      "http://localhost:8080/jugadors"
            );
            
            Dictionary<string, string> jugadors = new Dictionary<string, string>();
            string patternPais = @"representa\w*\s\w*\s(?<country>[\w-]*)";
            string patternPersona = @"participant\w*\s(?<Persona>\w*\s\w*)";
            
            foreach (var comentari in comentaris)
            {
                Match matchPersona = Regex.Match(comentari, patternPersona);
                Match matchPais = Regex.Match(comentari, patternPais);

                if (matchPersona.Success && matchPais.Success)
                {
                    string persona = matchPersona.Groups["Persona"].Value;
                    string pais = matchPais.Groups["country"].Value;
                    jugadors.Add(persona, pais);
                }
            }
            foreach (var jugador in jugadors)
            {
                Console.WriteLine($"Nom: {jugador.Key}, País: {jugador.Value}");
            }
        

            for (int i = 1; i <= 10000; i++ )
            {
                using (var partida = new HttpClient())
                {
                    var partidas = await client.GetFromJsonAsync <Partida>(
                      $"http://localhost:8080/partida/{i}"
                    );
                }

            }
             


        }
       


    }
}


