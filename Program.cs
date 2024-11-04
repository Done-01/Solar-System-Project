using System.Text.Json;

class Program
{
   private static void Main()
   {
      Dictionary<string,Planet> PlanetDictionary = Planet.GetPlanets(); // Calls the Get Planets Method to initialise the PlanetDictionary 
      bool running = true;
      Console.WriteLine("Solar System Project\nPress any key");
      Console.ReadKey();
      while(running)
      {
         Console.WriteLine("Enter the name of a planet");
         Planet planetSelection = IsValidPlanetSelection(PlanetDictionary);
         Console.WriteLine($"What would you like to know about {planetSelection.Name}?");
         string? data = IsValidDataSelection(planetSelection);
         Console.WriteLine(data);
         Console.WriteLine("Anything else?");
         
         while(running)
         {
            string? selection = Console.ReadLine();
            switch(selection)
            {
               case "yes":
                  Console.Clear();
                  break; 
               case "no":
                  Console.WriteLine("Bye");
                  running = false;
                  break;
               default:
                  Console.WriteLine("Invalid Selection");
                  continue;
            }
            break;
         }  
      }

   }
   
   private static Planet IsValidPlanetSelection(Dictionary<string,Planet> PlanetDictionary) //Checks a Dictionary for a key, if one exists returns an object
   {
      while(true)
      {
         string? input = Console.ReadLine();
         if(input == null)
         {
            Console.WriteLine("null value enetered");
            continue;
         }
         if(PlanetDictionary.ContainsKey(input))
         {
            return PlanetDictionary[input];
         }
         else
         {
            Console.WriteLine("Selection not in database");
            continue;
         }
      }
   }
   private static string? IsValidDataSelection(Planet planet) // Searches the properties of an object, if a match is found returns the value of the property
   {
      while(true)
      {
         string? input = Console.ReadLine();
         if(input == null)
         {
            Console.WriteLine("Null value entered");
            continue;
         }
         var properties = planet.GetType().GetProperties();
         foreach(var p in properties)
         {
            if(p.Name == input)
            {
               return (string?) p.GetValue(planet);
            }
         }
         Console.WriteLine("Selection not in database");
         continue;
      }

   }
}
class Planet // Planet class to store information and give the json an object to be deserialised into
{
   required public string Name {get;set;}
   required public string Mass {get;set;}
   required public string Distance {get;set;}
   required public string[] Moons {get;set;}
   
   public static Dictionary<string,Planet> GetPlanets() // Reads and deserialises a Json file, returns it as a Dictionary
   {
         string planetsjson = File.ReadAllText("Planets.json");
         List<Planet> planetlist = JsonSerializer.Deserialize<List<Planet>>(planetsjson);
         Dictionary<string,Planet> planetDictionary = new Dictionary<string,Planet>();
         foreach(var p in planetlist)
         {
         planetDictionary.Add(p.Name,p);
         }
         return planetDictionary;
   }
}

