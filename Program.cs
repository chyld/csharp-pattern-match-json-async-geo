using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace geo
{
  public record Location(string name, double[] position)
  {
    public override string ToString() => $"{name} @ {position[0]}, {position[1]}";
  }
  public class Program
  {
    public static async Task<List<Location>> GetJsonData()
    {
      var client = new HttpClient();
      return await client.GetFromJsonAsync<List<Location>>("https://raw.githubusercontent.com/chyld/datasets/main/markers.json");
    }
    public static void Main(string[] args)
    {
      var locations = GetJsonData().GetAwaiter().GetResult();

      // input from user
      var name = "Grocery Store";
      var lat = 525.1212;
      var lng = 553.1535;

      locations.Where(loc =>
        loc switch
        {
          Location l when l.name == name && l.position[0] == lat && l.position[1] == lng => true,
          Location l when l.name == name => true,
          _ => false
        }
      ).ToList().ForEach(loc => Console.WriteLine(loc));
    }
  }
}
