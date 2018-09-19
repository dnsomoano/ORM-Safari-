using System;
using ORM_Safari_.Models;

namespace ORM_Safari_
{
    class Program
    {
        static void Main(string[] args)
        {
            var userInput = 0;

            Console.WriteLine("Welcome user! You've got a few options available for yourself...");
            // CRUD
            System.Console.WriteLine("1 will create an animal");
            System.Console.WriteLine("2 will read all animals");
            System.Console.WriteLine("3 will update an animal");
            System.Console.WriteLine("4 will delete an animal");
            userInput = int.Parse(Console.ReadLine());

            // Create animal
            var animalName = "";
            var numberOfSeen = 0;
            var lastSeen = "";
            if (userInput == 1)
            {
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
            else if (userInput == 2)
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
            else if (userInput == 3)
            {
                // Update animals in db
                var animalOption = 0;
                var updateNumberOfSights = "";
                var updateLastSeen = "";
                System.Console.WriteLine("Would you like to update the number of sightings [1] or last location [2]");
                animalOption = int.Parse(Console.ReadLine());
                if (animalOption == 1)
                {
                    
                }

            }
        }
    }
}
