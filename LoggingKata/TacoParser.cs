using System;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            string[] cells = line.Split(',');

            if (cells.Length < 3)
            {
                logger.LogError("array length is less than 3");
                return null;
            }

            double latitude = double.Parse(cells[0]);
            double longitude = double.Parse(cells[1]);
            string name = cells[2];

            var latLong = new Point();
            latLong.Latitude = latitude;
            latLong.Longitude = longitude;

            var tacoBell = new TacoBell();
            tacoBell.Name = name;
            tacoBell.Location = latLong;

            return tacoBell;
        }
    }
}
