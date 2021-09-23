using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;

namespace geo
{
  public record Location
  {
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("position")]
    public double[] Position { get; set; }
    public override string ToString() => $"{Name} @ {Position[0]}, {Position[1]}";
  }
  public class Program
  {
    public static async Task<List<Location>> GetJsonData()
    {
      var client = new HttpClient();
      return await client.GetFromJsonAsync<List<Location>>("https://raw.githubusercontent.com/chyld/datasets/main/markers.json");
    }
    public static async Task Main(string[] args)
    {
      var locations = await GetJsonData();

      // simulate input from user
      var name = "Grocery Store";
      var lat = 525.1212;
      var lng = 553.1535;

      locations.Where(loc =>
        loc switch
        {
          Location l when l.Name == name && l.Position[0] == lat && l.Position[1] == lng => true,
          Location l when l.Name == name => true,
          _ => false
        }
      ).ToList().ForEach(loc => Console.WriteLine(loc));
    }
  }
}
