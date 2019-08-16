using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = MusicStore.GetData().AllArtists;
            List<Group> Groups = MusicStore.GetData().AllGroups;

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            Artist mv = Artists.FirstOrDefault(a => a.Hometown == "Mount Vernon");
            Console.WriteLine($"{mv.ArtistName} is from {mv.Hometown}");

            //Who is the youngest artist in our collection of artists?
            
            // orderby on age, pick the first OR on age desc, pick the last
            Artist youngest = Artists.OrderByDescending(a => a.Age)
                .LastOrDefault();

            Artist youngest2 = Artists.FirstOrDefault(
                a => a.Age == Artists.Min(ar => ar.Age)
            );

            

            //Display all artists with 'William' somewhere in their real name
            var williams = Artists
                .Where(a => a.RealName.ToLower()
                    .Contains("william"))
                    .Select(a => new { real = a.RealName, alias = a.ArtistName}).ToList();

            //Display the 3 oldest artist from Atlanta
            var OGs = Artists
                .Where(a => a.Hometown == "Atlanta")
                .OrderByDescending(a => a.Age)
                // LIMIT 3
                .Take(3);


            int[] nums = {1, 4, 6 ,7};
            string[] strNums = nums.Select(num => num.ToString()).ToArray();

            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            var groupsNotFromNy = Groups.Where(
                g => g.Members.Any(m => m.Hometown != "New York City")
            ).ToArray();



            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
	        Console.WriteLine(Groups.Count);
        }
    }
}
