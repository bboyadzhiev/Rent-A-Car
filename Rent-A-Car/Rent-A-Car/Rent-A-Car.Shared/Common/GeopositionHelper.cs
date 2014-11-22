﻿using System;
using System.Collections.Generic;
using System.Text;
//using Windows.Services.Maps;
using Windows.Devices.Geolocation;

namespace Rent_A_Car.Common
{
    public class GeopositionHelper
    {
        //public GeopositionHelper()
        //{
        //    // Nearby location to use as a query hint.
        //    BasicGeoposition queryHint = new BasicGeoposition();
        //    // DALLAS
        //    queryHint.Latitude = 32.7758;
        //    queryHint.Longitude = -96.7967;

        //    Geopoint hintPoint = new Geopoint(queryHint);

        //    MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(
        //                            "street, city, state zip",
        //                            hintPoint,
        //                            3);

        //    if (result.Status == MapLocationFinderStatus.Success)
        //    {
        //        if (result.Locations != null && result.Locations.Count > 0)
        //        {
        //            Uri uri = new Uri("ms-drive-to:?destination.latitude=" + result.Locations[0].Point.Position.Latitude.ToString() +
        //                "&destination.longitude=" + result.Locations[0].Point.Position.Longitude.ToString() + "&destination.name=" + "myhome");

        //            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
        //        }
        //    }
        //}

        //private async void Geocode()
        //{
        //    // Address or business to geocode.
        //    string addressToGeocode = "Microsoft";

        //    // Nearby location to use as a query hint.
        //    BasicGeoposition queryHint = new BasicGeoposition();
        //    queryHint.Latitude = 47.643;
        //    queryHint.Longitude = -122.131;
        //    Geopoint hintPoint = new Geopoint(queryHint);

        //    // Geocode the specified address, using the specified reference point
        //    // as a query hint. Return no more than 3 results.
        //    MapLocationFinderResult result =
        //        await MapLocationFinder.FindLocationsAsync(
        //                            addressToGeocode,
        //                            hintPoint,
        //                            3);

        //    // If the query returns results, display the coordinates
        //    // of the first result.
        //    if (result.Status == MapLocationFinderStatus.Success)
        //    {
        //        tbOutputText.Text = "result = (" +
        //            result.Locations[0].Point.Position.Latitude.ToString() + "," +
        //            result.Locations[0].Point.Position.Longitude.ToString() + ")";
        //    }
        //}

        //private async void ReverseGeocode()
        //{
        //    // Location to reverse geocode.
        //    BasicGeoposition location = new BasicGeoposition();
        //    location.Latitude = 47.643;
        //    location.Longitude = -122.131;
        //    Geopoint pointToReverseGeocode = new Geopoint(location);

        //    // Reverse geocode the specified geographic location.
        //    MapLocationFinderResult result =
        //        await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);

        //    // If the query returns results, display the name of the town
        //    // contained in the address of the first result.
        //    if (result.Status == MapLocationFinderStatus.Success)
        //    {
        //        tbOutputText.Text = "town = " +
        //            result.Locations[0].Address.Town;
        //    }
        //}

        public static double RadToDeg(double radians)
        {
            return radians * (180 / Math.PI);
        }

        public static double DegToRad(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        public static double Bearing(double lat1, double long1, double lat2, double long2)
        {
            //Convert input values to radians  
            lat1 = GeopositionHelper.DegToRad(lat1);
            long1 = GeopositionHelper.DegToRad(long1);
            lat2 = GeopositionHelper.DegToRad(lat2);
            long2 = GeopositionHelper.DegToRad(long2);

            double deltaLong = long2 - long1;

            double y = Math.Sin(deltaLong) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) -
                    Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(deltaLong);
            double bearing = Math.Atan2(y, x);
            return GeopositionHelper.ConvertToBearing(GeopositionHelper.RadToDeg(bearing));
        }

        public static double ConvertToBearing(double deg)
        {
            return (deg + 360) % 360;
        }  

    }
}
