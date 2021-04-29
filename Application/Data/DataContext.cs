using Application.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Application.Data
{
    public class DataContext
    {
        private readonly string _path;
        public DataContext()
        {
            if (Assets == null)
                Assets = new List<Asset>();
            if (Strikes == null)
                Strikes = new List<Strike>();

            // This will get the base path of the application.
            _path = Path
                .GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                    .Replace("\\bin\\Debug\\netcoreapp3.1", string.Empty);

            // This will read the asset data.
            var assetData = File.ReadAllText($"{_path}/Data/Seed/assets.json");
            // This will deserialize the json data.
            dynamic assets = JsonConvert.DeserializeObject(assetData);
            // This will map the deserialized json data to a collection of Asset objects.
            foreach (var item in assets)
            {
                Assets.Add(new Asset
                {
                    AssetName = item["assetName"].ToString(),
                    QuadKey = item["quadKey"].ToString(),
                    AssetOwner = item["assetOwner"].ToString()
                });
            }

            // This will read the lightning data.
            var strikeData = File.ReadAllText($"{_path}/Data/Seed/lightning.json");
            // This will deserialize the json data.
            dynamic strikes = JsonConvert.DeserializeObject(strikeData);
            // This will map the deserialized json data to a collection of Strike objects.
            foreach (var item in strikes)
            {
                Strikes.Add(new Strike
                {
                    FlashType = (FlashType)item["flashType"],
                    StrikeTime = Convert.ToInt64(item["strikeTime"]),
                    Latitude = Convert.ToDouble(item["latitude"]),
                    Longitude = Convert.ToDouble(item["longitude"]),
                    PeakAmps = Convert.ToInt32(item["peakAmps"]),
                    Reserved = item["reserved"].ToString(),
                    ICHeight = Convert.ToInt32(item["icHeight"]),
                    ReceivedTime = Convert.ToInt64(item["receivedTime"]),
                    NumberOfSensors = Convert.ToInt32(item["numberOfSensors"]),
                    Multiplicity = Convert.ToInt32(item["multiplicity"])
                });
            }
        }

        public List<Asset> Assets { get; }

        public List<Strike> Strikes { get; }
    }
}
