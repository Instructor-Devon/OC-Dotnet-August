using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQLecture
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = {834,2,-23,2,103,-5,42,-1,27};
            int max = numbers.Max();

            //                                 predicate: lemme test something
            int[] evens =  numbers.Where(num => num % 2 == 0).ToArray();


            List<string> names = new List<string>
            {
                "Sharon",
                "Charlie",
                "Barbara",
                "Molly",
                "Ashton",
                "Marcellus",
                "Molly",
                "Zeleg",
                "Zebra",
                "Yvonne",
                "Yoda",
                "Bob",
            };
            var orderedNames = names.OrderBy(name => name.Length);
            //                                      selector: lemme grab something
            int maxstringLength = names.Max(name => name.Length);
            string longestName = names.FirstOrDefault(
                name => name.Length == 266
            );


            List<City> AllCities = Place.GetCities();
            // .Sum(selector)   
            int totalPopulation = AllCities.Sum(city => city.Population);

            // Largest City?
            City biggest = AllCities.FirstOrDefault(
                city => city.Population == AllCities.Max(c => c.Population) // the biggest one?
            );

            // Big Cities
            int bigCityPopValue = 700000;
            int smallCityPopValue = 4000;
            List<City> BigAndSmallCities = AllCities.Where(c => 
                c.Population < smallCityPopValue || c.Population >= bigCityPopValue
            ).ToList();


            List<State> AllStates = Place.GetStates();


        }
    }
}
