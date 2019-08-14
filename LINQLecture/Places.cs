using System.Collections.Generic;
using System.Linq;

namespace LINQLecture
{
    public class    City
    {
        public string Name;
        public string StateCode;
        public int Population;
        public State State;
        public City(string name, string stateCode, int pop)
        {
            Name = name;
            StateCode = stateCode;
            Population = pop;
        }
    }
    public class State
    {
        public string Name;
        public string StateCode;
        public List<City> Cities;
        public State(string name, string stateCode)
        {
            Name = name;
            StateCode = stateCode;
            Cities = new List<City>();
        }

    }
    public class Place
    {
        public static List<State> GetStates()
        {
            var states = Cities.Join(States, 
                (c => c.StateCode),
                (s => s.StateCode),
                (joinedCity, joinedState) => {
                    joinedState.Cities.Add(joinedCity);
                    return joinedState;
            }).Distinct();
            return states.ToList();
        }
        public static List<City> GetCities()
        {
            var cities = Cities.Join(States,
                (c => c.StateCode),
                (s => s.StateCode),
                (joinedCity, joinedState) => {
                    joinedCity.State = joinedState;
                    return joinedCity;
            });
                return cities.ToList();
        }
        public static List<State> States
        {
            get 
            {
                return new List<State>()
                {
                    new State("Washington", "WA"),
                    new State("California", "CA"),
                    new State("Texas", "TX")
                };
            }

        }
        public static List<City> Cities
        {
            get
            {
                return new List<City>()
                {
                    new City("Seattle", "WA", 704542),
                    new City("Tacoma", "WA", 211277),
                    new City("Hoquiam", "WA", 8434),
                    new City("Houston", "TX", 2303000),
                    new City("Odessa", "TX", 117871),
                    new City("Corpus Christi", "TX", 325733),
                    new City("Los Angeles", "CA", 3976000),
                    new City("Santa Cruz", "CA", 64465),
                    new City("Barstow", "CA", 23835),
                    new City("Bend", "OR", 13835)
                };
            }
        }
    }
}
