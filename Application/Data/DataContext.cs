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
            // var assetData = File.ReadAllText($"D:/source/repos/LightningAlert/Application/Data/Seed/assets.json");
            // This will deserialize the json data.
            dynamic assets = JsonConvert.DeserializeObject(assetData);
            // This will map the deserialized json data to a collection of Asset objects.
            foreach (var asset in assets)
            {
                Assets.Add(new Asset
                {
                    AssetName = asset["assetName"].ToString(),
                    QuadKey = asset["quadKey"].ToString(),
                    AssetOwner = asset["assetOwner"].ToString()
                });
            }

            // This will read the lightning data.
            var strikeData = File.ReadAllText($"{_path}/Data/Seed/lightning.json");
            // var strikeData = File.ReadAllText($"D:/source/repos/LightningAlert/Application/Data/Seed/lightning.json");
            // This will deserialize the json data.
            dynamic strikes = JsonConvert.DeserializeObject(strikeData);
            // This will map the deserialized json data to a collection of Strike objects.
            foreach (var strike in strikes)
            {
                Strikes.Add(new Strike
                {
                    FlashType = (FlashType)strike["flashType"],
                    StrikeTime = Convert.ToInt64(strike["strikeTime"]),
                    Latitude = Convert.ToDouble(strike["latitude"]),
                    Longitude = Convert.ToDouble(strike["longitude"]),
                    PeakAmps = Convert.ToInt32(strike["peakAmps"]),
                    Reserved = strike["reserved"].ToString(),
                    ICHeight = Convert.ToInt32(strike["icHeight"]),
                    ReceivedTime = Convert.ToInt64(strike["receivedTime"]),
                    NumberOfSensors = Convert.ToInt32(strike["numberOfSensors"]),
                    Multiplicity = Convert.ToInt32(strike["multiplicity"])
                });
            }
        }

        public List<Asset> Assets { get; }

        public List<Strike> Strikes { get; }
    }
}
