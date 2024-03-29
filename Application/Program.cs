﻿using Application.Services;
using System;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // This will initialize the notification service.
                var service = new NotificationService();

                // This will get the list of affected assets.
                var assets = service.ListAffectedAssets();

                // This will output the list of affected assets.
                foreach (var item in assets)
                {
                    Console.WriteLine($"lightnight alert for {item.AssetOwner}:{item.AssetName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops, something went wrong.");
                Console.WriteLine($"Details: {ex.Message}");
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
