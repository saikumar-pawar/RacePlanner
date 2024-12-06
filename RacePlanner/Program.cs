
using System.Globalization;

Console.WriteLine("***************      Racing Calendar App    ***************");
//var races = GetRaces();
var races = GetRacesFromConsole();
PrintRacesInfo(races);
PrintRacesWithDriversInfo(races);

static List<Race> GetRaces()
{
    return
    [
        new Race("Race1", DateOnly.FromDateTime(DateTime.Now.AddDays(8)), "Track1"),
        new Race("Race2", DateOnly.FromDateTime(DateTime.Now.AddDays(2)), "Track2"),
        new Race("Race3", DateOnly.FromDateTime(DateTime.Now.AddDays(20)), "Track3"),
        new Race("Race4", DateOnly.FromDateTime(DateTime.Now.AddDays(10)), "Track4")
    ];
}

static List<Race> GetRacesFromConsole()
{
    var races = new List<Race>();

    while (true)
    {
        Console.Write("To enter race information select (yes): ");
        string? selectedOption = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(selectedOption) && selectedOption.Trim().ToLower() == "yes")
        {
            races.Add(GetRaceFromConsole());
            Console.WriteLine("Race added!");
        }
        else
        {
            break;
        }
    }

    return races;
}

static Race GetRaceFromConsole()
{
    string? raceName;
    DateOnly raceDate;
    string? racreTrackName;

    while (true)
    {
        Console.Write("Enter race name: ");
        raceName = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(raceName))
        {
            raceName = raceName.Trim();
            break;
        }
        else
        {
            Console.WriteLine("Invalid race name");
        }
    }
    while (true)
    {
        Console.Write("Enter race date (yyyy-mm-dd): ");
        string? input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
        {
            if (DateOnly.TryParseExact(input.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out raceDate))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid race date format. Please enter the race date in yyyy-mm-dd format.");
            }
        }
        else
        {
            Console.WriteLine("Invalid race date");
        }
    }
    while (true)
    {
        Console.Write("Enter race track name: ");
        racreTrackName = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(racreTrackName))
        {
            racreTrackName = racreTrackName.Trim();
            break;
        }
        else
        {
            Console.WriteLine("Invalid race name");
        }
    }

    var race = new Race(raceName, raceDate, racreTrackName);
    AddDriversForRace(race);
    return race;
}

static void AddDriversForRace(Race race)
{
    if (race != null)
    {
        while (true)
        {
            Console.Write($"To enter drivers information for {race.Name} at {race.Track} on {race.Date} select (yes): ");
            string? selectedOption = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(selectedOption) && selectedOption.Trim().ToLower() == "yes")
            {
                var driver = GetDriverFromConsole();
                
                if (driver != null)
                {
                    race.AddDriver(driver);
                }
            }
            else
            {
                break;
            }
        }
    }
}

static Driver GetDriverFromConsole()
{
    string? driverName;

    while (true)
    {
        Console.Write("Enter driver name: ");
        driverName = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(driverName))
        {
            driverName = driverName.Trim();
            break;
        }
        else
        {
            Console.WriteLine("Invalid driver name");
        }
    }

    return new Driver(driverName);
}

static void PrintRacesInfo(List<Race> races)
{
    foreach (var race in races)
    {
        if (race != null)
        {
            race.PrintSummary();
        }
    }
}

static void PrintRacesWithDriversInfo(List<Race> races)
{
    foreach (var race in races)
    {
        if (race != null)
        {
            race.PrintSummaryWithDrivers();
        }
    }
}

public class Race
{
    public Race(string name, DateOnly date, string track)
    {
        Name = name;
        Date = date;
        Track = track;
    }

    private const int maxNoOfDriversAllowed = 20;
    private int noOfDriversInRace = 0;
    private List<Driver> drivers = new List<Driver>();
    private Stack<Driver> driversInWaitingList = new Stack<Driver>();
    public string Name { get; init; }
    public DateOnly Date { get; init; }
    public string Track { get; init; }

    public void PrintSummary()
    {
        Console.WriteLine($"{Date:yyyy-MM-dd}: {Name} at {Track}");
    }

    public void PrintSummaryWithDrivers()
    {
        PrintSummary();
        Console.WriteLine("\tDrivers Participating in race:");
        
        if (noOfDriversInRace > 0)
        {
            for (int i = 0; i < noOfDriversInRace; i++)
            {
                Console.WriteLine($"\t\t{drivers[i].Name}");
            }
        }
        else
        {
            Console.WriteLine("\t\tNo drivers participating in this race");
        }

        Console.WriteLine("\tDrivers in waiting list for this race:");

        if (driversInWaitingList.Count > 0)
        {
            foreach (var driver in driversInWaitingList)
            {
                Console.WriteLine($"\t\t{driver.Name}");
            }
        }
        else
        {
            Console.WriteLine("\t\tNo drivers in waiting list for this race");
        }
    }

    public void AddDriver(Driver driver)
    {
        if (noOfDriversInRace < maxNoOfDriversAllowed)
        {
            drivers.Add(driver);
            noOfDriversInRace++;
            Console.WriteLine($"{driver.Name} is added to race {Name} at {Track} on {Date:yyyy-MM-dd}");
        }
        else
        {
            driversInWaitingList.Push(driver);
            Console.WriteLine($"{driver.Name} is added to waiting list of race {Name} at {Track} on {Date:yyyy-MM-dd}");
        }
    }
}


public class Driver
{
    public Driver(string name)
    {
        Name = name;
    }
    public string Name { get; init; }
}
