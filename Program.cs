using System;
using NLog.Web;
using System.IO;
using System.Linq;


namespace SeedData
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            // try
            // {
            //     // Create and save a new Location
            //     Console.Write("Enter a name for a new Location: ");
            //     var name = Console.ReadLine();

            //     var location = new Location { Name = name };

            //     var db = new SeedDataContext();
            //     db.AddLocation(location);
            //     logger.Info("Location added - {name}", name);

            //     // Display all Locations from the database
            //     var query = db.Location.OrderBy(b => b.Name);

            //     Console.WriteLine("All blogs in the database:");
            //     foreach (var item in query)
            //     {
            //         Console.WriteLine(item.Name);
            //     }
            // }
            // catch (Exception ex)
            // {
            //     logger.Error(ex.Message);
            // }

            logger.Info("Program ended");
        }
    }
}