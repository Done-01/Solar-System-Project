// Solar system project
using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;
class Planet
{

    // Priate variables

    private string name;
    private string mass;
    private string distance;
    private string moons;

    [JsonConstructor]
    private Planet(string name, string mass, string distance, string moons)
    {
        this.name = name;
        this.mass = mass;
        this.distance = distance;
        this.moons = moons;

    }

    public string Name 
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    public string Mass
    {
        get
        {
            return mass;
        }
        set
        {
            mass = value;
        }
    }
    public string Distance
    {
        get
        {
            return distance;
        }
        set
        {
            distance = value;
        }
    }

    public string Moons
    {
        get
        {
            return moons;
        }
        set
        {
            moons = value;
        }
    }

   /* public static void WriteToJson() // used for figuring out how json is formatted / testing
    {
        Planet mercury = new Planet("mercury","heavy","far","probably some");  
        Planet venus = new Planet("venus","heavy","far","probably some");
        Planet earth = new Planet("earth","heavy","far","probably some");
        Planet mars = new Planet("mars","heavy","far","probably some");
        Planet jupiter = new Planet("jupiter","heavy","far","probably some");
        Planet saturn = new Planet("saturn","heavy","far","probably some");
        Planet uranus = new Planet("uranus","heavy","far","probably some");
        Planet neptune = new Planet("neptune","heavy","far","probably some");




        List<Planet> planetlist = new List<Planet>();
        planetlist.Add(mercury);
        planetlist.Add(venus);
        planetlist.Add(earth);
        planetlist.Add(mars);
        planetlist.Add(jupiter);
        planetlist.Add(saturn);
        planetlist.Add(uranus);
        planetlist.Add(neptune);

        string planetstring = JsonSerializer.Serialize(planetlist);
        Console.WriteLine(planetstring);
    }*/
    public static Planet ReadFromJson(string planet) // reads a json file and returnes a Planet object
    {
        Planet unknown = new Planet("n/a","n/a","n/a","n/a");
        StreamReader planetJson = new StreamReader(planet);
        string? jsonString = planetJson.ReadLine();
        try
        {   
            Planet? planetObject = JsonSerializer.Deserialize<Planet>(jsonString);
            
            if(planetObject != null)
            {
                return planetObject;
            }
            else
            {
                return unknown;
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Exception: "+ e.Message);
            return unknown;

        }
        finally
        {
            planetJson.Close();
        }
    }

    public void PrintPlanetName() // just prints "Planet name is "name" "
    {
        Console.WriteLine($"---{name}---");
    }

}
class MenuSystem
{

    public static void TitleMenuPrint() // Prints Welcome and waits for any input to continue
    {
        Console.WriteLine("---Welcome to my solar system project---\n       Press any key to continue");
        Console.ReadLine();
    }
    private static void UserFarewellPrint() // Prints a message to use when user terminates program
    {
        Console.WriteLine("cya l8r");
    }
    private static void PlanetSelectorMenuPrint() // Prints an ordered list of planets used in PlanetSelector method
    {
        Console.WriteLine("--Planets--\n\n1. Mercury\n2. Venus\n3. Earth\n4. Mars\n5. Jupiter\n6. Saturn\n7. Uranus\n8. Neptune\n9. quit");
    }
    private static void PlanetInfoMenuPrint() // Prints an ordered list of planet info options used in PlanetInfoSelector method
    {
        Console.WriteLine("1. Mass\n2. Distance from Sun\n3. Moons\n4. select a different planet\n5. quit");
    }

    public static void PlanetSelector() // first level of the menu system 
    {
        PlanetSelectorMenuPrint();
        while(true)
        {
            int selection = InputParse.IntCheck();

            switch(selection)
            {
                case 1:
                    // code for Mercury
                    Planet mercury = Planet.ReadFromJson("planets/mercury.json");
                    PlanetInfoSelector(mercury);
                    break;
                case 2:
                    //code for Venus
                    Planet venus = Planet.ReadFromJson("planets/venus.json");
                    PlanetInfoSelector(venus);
                    break;
                case 3:
                    //code for Earth
                    Planet earth = Planet.ReadFromJson("planets/earth.json");
                    PlanetInfoSelector(earth);
                    break;
                case 4:
                    //code for Mars
                    Planet mars = Planet.ReadFromJson("planets/mars.json");
                    PlanetInfoSelector(mars);
                    break;
                case 5:
                    //code for Jupiter
                    Planet jupiter = Planet.ReadFromJson("planets/jupiter.json");
                    PlanetInfoSelector(jupiter);
                    break;
                case 6:
                    //code for Saturn
                    Planet saturn = Planet.ReadFromJson("planets/saturn.json");
                    PlanetInfoSelector(saturn);
                    break;
                case 7:
                    //code for Uranus
                    Planet uranus = Planet.ReadFromJson("planets/uranus.json");
                    PlanetInfoSelector(uranus);
                    break;
                case 8:
                    //code for Neptune
                    Planet neptune = Planet.ReadFromJson("planets/neptune.json");
                    PlanetInfoSelector(neptune);
                    break;
                case 9:
                    //code to exit
                    UserFarewellPrint();
                    break;
                default:
                    Console.WriteLine("invalid selection");
                    continue;
            }
            break;

        }
    }
    private static void PlanetInfoSelector(Planet Planet) // second level of the menu system
    {
        Planet.PrintPlanetName();
        PlanetInfoMenuPrint();

        while(true)
        {
            int selection = InputParse.IntCheck();

            switch(selection)
            {
                case 1:
                    //code for mass
                    Console.WriteLine($"{Planet.Name} is {Planet.Mass}");
                    continue;
                case 2:
                    //code for distance
                    Console.WriteLine($"{Planet.Name} is {Planet.Distance}");
                    continue;
                case 3:
                    //code for moons
                    Console.WriteLine($"{Planet.Name} has {Planet.Moons}");
                    continue;
                case 4:
                    //code to go back
                    MenuSystem.PlanetSelector();
                    break;
                case 5:
                    //code to exit
                    UserFarewellPrint();
                    break;
                default:
                    Console.WriteLine("invalid selection");
                    continue;

            }
            break;
        }
    }
}

class InputParse
{
    
    public static string NullCheck(string? input) // Checks if a string is null and loops a user input prompt until a non null string is returned
    {
        while(input == null)
        {
            Console.WriteLine("you managed to eneter a null value");
            input = Console.ReadLine();
        }
        return input;
    }

    public static int IntCheck() // Obtains a non null string from NullCheck method and checks if it can be converted to an int
    {
        int outputInt;
        while(true)
        {
            string input = NullCheck(Console.ReadLine());
            bool isInt = int.TryParse(input, out outputInt);
            if(isInt)
            {
                return outputInt;
            }
            else
            {
                Console.WriteLine("Not an integer. Try again..");
            }
        }            
    }

}

class Program
{
    private static void Main()
    {
        MenuSystem.TitleMenuPrint();
        MenuSystem.PlanetSelector();
    }
}