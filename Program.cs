using System;
using NLog.Web;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace SeedData
{
    class Program
    {


        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            try
            {

                string choice;
                var db = new SeedDataContext();

                Console.WriteLine("1) Run the seed event \n2) View the seeded data");
                choice = Console.ReadLine();
                Console.Clear();
                logger.Info("Option {choice} selected", choice);

                if (choice == "1")
                {
                    var query = db.Location.OrderBy(b => b.Name);

                    List<int> locations = new List<int>();

                    foreach (var item in query)
                    {
                        Console.WriteLine(item.LocationId);
                        locations.Add(item.LocationId);
                    }

                    Random rnd = new Random();

                    DateTime timeCounter = new DateTime(2020, 7, 1);
                    while (timeCounter <= DateTime.Today)
                    {
                        timeCounter = timeCounter.AddDays(1);
                        Console.WriteLine("month " + timeCounter.Month + " day " + timeCounter.Day);

                        //get 0-5 events for the day - this is where each event will be made and send to the database
                        int numOfEvents = rnd.Next(6);
                        for (int j = 0; j < numOfEvents; j++)
                        {
                            int hour = rnd.Next(0, 24); //0-23 hours 
                            int minute = rnd.Next(0, 60);
                            int second = rnd.Next(0, 60);
                            // Console.WriteLine(hour + " things " + minute + " stuff " + second);
                            DateTime date = new DateTime(timeCounter.Year, timeCounter.Month, timeCounter.Day, hour, minute, second);
                            // Console.WriteLine(date);
                            int eventLocation;
                            int ranLocation = rnd.Next(1, 4);

                            eventLocation = ranLocation;
                            // Console.WriteLine("this is the event: " + eventLocation);

                            SeedEvent seedEventDay = new SeedEvent();

                            seedEventDay.TimeStamp = date;
                            seedEventDay.LocationId = eventLocation;
                            seedEventDay.Flagged = false;
                            db.AddSeedEvent(seedEventDay);
                        }
                    }
                    logger.Info("Data seeded successful");

                }
                else if (choice == "2")
                {

                    // display events
                    // var query = db.SeedEvent.OrderBy(s => s.TimeStamp);
                    var query =
                        from SeedEvent in db.SeedEvent
                        orderby SeedEvent.TimeStamp
                        join Location in db.Location on SeedEvent.LocationId equals Location.LocationId into se
                        from sl in se.DefaultIfEmpty()
                        select new { eventTime = SeedEvent.TimeStamp, locationName = sl.Name};

                    Console.WriteLine($"{query.Count()} events returned");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.eventTime + " - " + item.locationName);
                    }
                }


            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }




    }
}
