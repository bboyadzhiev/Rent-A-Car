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
        public static bool enabled { get; set; }

        static Sensors()
        {
#if WINDOWS_PHONE_APP
            accel.ReportInterval = 200;
            Acceleration();

#endif
            locator.DesiredAccuracy = PositionAccuracy.High;
            locator.ReportInterval = 1000; // ms
            locator.MovementThreshold = 3; //meters
            Geolocation();
        }


#if WINDOWS_PHONE_APP
        public static Inclinometer inclinometer = Inclinometer.GetDefault();
        public static event EventHandler InclinationChenged;
        public static InclinometerReading inclinationReadings;


        static Accelerometer accel = Accelerometer.GetDefault();
        public static event EventHandler AccelerometerChanged;
        
        public static double accelX;
        public static double accelY;
        public static double accelZ;

        private static void Acceleration()
        {
            
            accel.ReadingChanged += (snd, args) =>
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

        private static void Inclination()
        {
            inclinometer.ReadingChanged += (snd, args) =>
            {
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    inclinationReadings = args.Reading;
                 
                    EventHandler handler = InclinationChenged;
                    if (handler != null)
                    {
                        handler(null, EventArgs.Empty);
                    }
                });
            };
        }
#else
        public static event EventHandler InclinationChenged  = null;
         public static InclinometerReading inclinationReadings = null;
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
                   lattitude = args.Position.Coordinate.Point.Position.Latitude;//coords.Latitude;
                   longitude = args.Position.Coordinate.Point.Position.Longitude;//coords.Longitude;
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
