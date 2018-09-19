using System;
using System.Linq;
using ORM_Safari_.Models;

namespace ORM_Safari_
{
    class Program
    {

        public static int Greeting()
        {

            Console.WriteLine("Welcome user! You've got a few options available for yourself...");
            // CRUD
            System.Console.WriteLine("1: will create an animal");
            System.Console.WriteLine("2: will read all animals");
            System.Console.WriteLine("3: will update an animal");
            System.Console.WriteLine("4: will delete an animal");
            System.Console.WriteLine("5: Other options");
            var choice = int.Parse(Console.ReadLine());
            return choice;
        }

        public static void AddAnimalToDb()
        {
            // Create animal
            var animalName = "";
            var numberOfSeen = 0;
            var lastSeen = "";
            System.Console.WriteLine("What is the name of the species?");
            animalName = Console.ReadLine();
            System.Console.WriteLine("How many times have you seen this animal?");
            numberOfSeen = int.Parse(Console.ReadLine());
            System.Console.WriteLine("And where did you last see the per-Animal?");
            // Similar to creating object within Js
            var newAnimal = new SeenAnimals
            {
                Species = animalName,
                CountOfTimesSeen = numberOfSeen,
                LocationOfLastSeen = lastSeen
            };
            // Similar to appending to the DOM
            var db = new SafariVacationContext();
            db.SeenAnimalsTable.Add(newAnimal);
            db.SaveChanges();
        }

        public static void ReadAllAnimals()
        {
            // Read all animals in db
            var db = new SafariVacationContext();
            var animals = db.SeenAnimalsTable;
            // SELECT * From SeenAnimals
            foreach (var animal in animals)
            {
                System.Console.WriteLine($"There is a {animal.Species}, last seen at {animal.LocationOfLastSeen}");
            }
        }

        public static void UpdateToAnimals()
        {
            // Update animals in db
            var animalOption = 0;
            System.Console.WriteLine("Would you like to update the number of sightings [1] or last location [2]");
            animalOption = int.Parse(Console.ReadLine());
            if (animalOption == 1)
            {
                var whichSpecies = "";
                var newSightings = 0;
                System.Console.WriteLine("Which species?");
                whichSpecies = Console.ReadLine();
                System.Console.WriteLine("What is the new value? for # of sightings?");
                newSightings = int.Parse(Console.ReadLine());
                // Initialize database
                var db = new SafariVacationContext();
                var CountOfTimesSeenToUpdate = db.SeenAnimalsTable.FirstOrDefault(animal => animal.Species == whichSpecies);
                CountOfTimesSeenToUpdate.CountOfTimesSeen = newSightings;
                db.SaveChanges();
            }
            else
            {
                var whichSpecies = "";
                var newLastSeen = "";
                System.Console.WriteLine("Which species?");
                whichSpecies = Console.ReadLine();
                System.Console.WriteLine("Where is the last location you saw this animal?");
                newLastSeen = Console.ReadLine();
                var db = new SafariVacationContext();
                var LocationOfLastSeenToUpdate = db.SeenAnimalsTable.FirstOrDefault(animal => animal.Species == whichSpecies);
                LocationOfLastSeenToUpdate.LocationOfLastSeen = newLastSeen;
                db.SaveChanges();
            }
        }

        static void Main(string[] args)
        {
            var userInput = 0;
            while (true)
            {
                userInput = Greeting();
                if (userInput == 1)
                {
                    AddAnimalToDb();
                }
                else if (userInput == 2)
                {
                    ReadAllAnimals();
                    System.Console.WriteLine("\n");
                }
                else if (userInput == 3)
                {
                    UpdateToAnimals();
                }
                else if (userInput == 4)
                {
                    // Delete animals
                    var whichSpecies = "";
                    System.Console.WriteLine("Which species would you like to eradicate?");
                    whichSpecies = Console.ReadLine();
                    var db = new SafariVacationContext();
                    var animalToDelete = db.SeenAnimalsTable.FirstOrDefault(animal => animal.Species == whichSpecies);
                    db.SeenAnimalsTable.Remove(animalToDelete);
                    db.SaveChanges();
                }
                else
                {
                    var optionsInput = 0;
                    System.Console.WriteLine("1: Will show you animals in the Jungle");
                    System.Console.WriteLine("2: Will REMOVE animals from the Desert");
                    System.Console.WriteLine("3: Get total number of animal sights");
                    System.Console.WriteLine("4: Get total of lions, tigers, and bears");
                    if (optionsInput == 1)
                    {
                        var db = new SafariVacationContext();
                        var jungleAnimals = db.SeenAnimalsTable.Where(animals => animals.LocationOfLastSeen == "Jungle");
                        foreach (var jungleAnimal in jungleAnimals)
                        {
                            System.Console.WriteLine(jungleAnimal);
                        }
                    }
                    else if (optionsInput == 2)
                    {
                        var db = new SafariVacationContext();
                        var desertAnimals = db.SeenAnimalsTable.Where(animals => animals.LocationOfLastSeen == "Desert");
                        db.SeenAnimalsTable.RemoveRange(desertAnimals);
                        db.SaveChanges();
                    }
                    else if (optionsInput == 3)
                    {
                        var db = new SafariVacationContext();
                        var totalAnimalsSeen = db.SeenAnimalsTable.Sum(c => c.CountOfTimesSeen);
                        System.Console.WriteLine(totalAnimalsSeen);
                    }
                    else if (optionsInput == 4)
                    {
                          var db = new SafariVacationContext();
                        var totalAnimalsSeen = db.SeenAnimalsTable
                            .Where(w => w.Species=="lions" ||w.Species == "bears" || w.Species=="tigers")
                            .Sum(c => c.CountOfTimesSeen);
                        System.Console.WriteLine(totalAnimalsSeen);
                    }
                }
            }
        }
    }
}
