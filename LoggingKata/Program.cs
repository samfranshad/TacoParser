using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";
        
        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            if (lines.Length == 0)
            {
                logger.LogError("no data was returned");
            }

            if (lines.Length == 1)
            {
                logger.LogWarning("not all data was returned");
            }

            logger.LogInfo($"Lines: {lines[0]}");
            logger.LogInfo("Begin parsing");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable tacoBellOne = null;
            ITrackable tacoBellTwo = null;

            double distance = 0;

            for(int i = 0; i < locations.Length; i++)
            {
                var locA = locations[i];
                var corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);
                
                for(int x = 0; x < locations.Length; x++)
                {
                    var locB = locations[x];
                    var corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);
                    
                    double newDistance = corA.GetDistanceTo(corB);

                    if(newDistance > distance)
                    {
                        distance = newDistance;
                        tacoBellOne = locA;
                        tacoBellTwo = locB;
                    }
                }
            }
            double roundedMiles = Math.Round(distance * 0.00062137, 2);

            var tacoBell = new TacoBell();
            tacoBell.PrintResult(tacoBellOne, tacoBellTwo, roundedMiles);
        }
    }
}
