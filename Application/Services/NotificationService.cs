using Application.Data;
using Application.Entities;
using Microsoft.MapPoint;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class NotificationService
    {
        private readonly DataContext _dataContext;
        public NotificationService()
        {
            _dataContext = new DataContext();
        }

        /// <summary>
        /// This will list all the affected assets affected by lightning strikes.
        /// </summary>
        /// <returns>A collection of Asset objects.</returns>
        public List<Asset> ListAffectedAssets()
        {
            // This will get the data of lightning strikes.
            var strikes = _dataContext.Strikes
                .Where(x => x.FlashType == FlashType.CloudToGround);

            // This will initialize the quad key collection.
            var quadKeys = new List<string>();

            // This will iterate through the collection of lightning strikes.
            foreach (var item in strikes)
            {
                // This will initialize the required values
                int pixelX, pixelY, tileX, tileY;

                // This will convert the inital longitude and latitude value to pixel x and y values.
                TileSystem.LatLongToPixelXY(item.Latitude, item.Longitude, 12, out pixelX, out pixelY);

                // This will convert the pixel x and y values to tile x and y values.
                TileSystem.PixelXYToTileXY(pixelX, pixelY, out tileX, out tileY);

                // This will convert the tile x and y values to a usable quad key.
                var quadKey = TileSystem.TileXYToQuadKey(tileX, tileY, 12);

                // This will determine if the resulting quad key already exists in the collection.
                if (!quadKeys.Contains(quadKey))
                    // This will add the quad key to the quad key collection.
                    quadKeys.Add(quadKey);
            }
            // This will extract the affected assets by their quad key.
            var affectedAssets = _dataContext.Assets.Where(x => quadKeys.Contains(x.QuadKey));
            return affectedAssets.ToList();
        }
    }
}
