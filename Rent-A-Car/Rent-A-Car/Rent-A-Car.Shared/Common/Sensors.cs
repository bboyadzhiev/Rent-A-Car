using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;
using Windows.Devices.Sensors;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Rent_A_Car.Common
{
    public static class Sensors
    {
        static Geolocator locator = new Geolocator();
        public static event EventHandler LocationChanged;
        public static double lattitude;
        public static double longitude;
        public static double? heading;

        static Sensors()
        {
#if WINDOWS_PHONE_APP
            Accelerometer.ReportInterval = 200;
            Acceleration();
#endif
            locator.DesiredAccuracy = PositionAccuracy.High;
            locator.ReportInterval = 1000; // ms
            locator.MovementThreshold = 100; //meters
            Geolocation();
        }


#if WINDOWS_PHONE_APP
        static Accelerometer Accelerometer = Accelerometer.GetDefault();
        public static event EventHandler AccelerometerChanged;
        public static double accelX;
        public static double accelY;
        public static double accelZ;

        private static void Acceleration()
        {
            Accelerometer.ReadingChanged += (snd, args) =>
            {
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    accelX = args.Reading.AccelerationX;
                    accelY = args.Reading.AccelerationY;
                    accelZ = args.Reading.AccelerationZ;
                    EventHandler handler = AccelerometerChanged;
                    if (handler != null)
                    {
                        handler(null, EventArgs.Empty);
                    }
                });
            };
        }
#else
        public static event EventHandler AccelerometerChanged = null;
        public static double accelX = 0;
        public static double accelY = 0;
        public static double accelZ = 0;
#endif

        private static void Geolocation()
        {
            locator.PositionChanged += (snd, args) =>
           {
               Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
               {
                   var coords = args.Position.Coordinate;
                   longitude = coords.Longitude;
                   lattitude = coords.Longitude;
                   heading = coords.Heading;
                   EventHandler handler = LocationChanged;
                   if (handler != null)
                   {
                       handler(null, EventArgs.Empty);
                   }
               });
           };
            locator.GetGeopositionAsync();  //await
            // await if sometihing dependant on getgeoposition
        }

    }
}
