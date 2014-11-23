using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Parse;
using Rent_A_Car.Common;
using Rent_A_Car.Models;
using Rent_A_Car.ViewModels;
using System.Linq;
using Windows.Devices.Sensors;


namespace Rent_A_Car.Pages
{
    public class CarPositionPageVM : ViewModelBase
    {
        private CarVM car;
        private string pageTitle;
        private bool initializing;
        private ParseGeoPoint userLocation;
        private ParseGeoPoint northPole;
        private InclinometerReading inclination;
        private double accelX;
        private double accelY;
        private double accelZ;
        private double distanceToCar;
        private string distamceText;
        private double bearing;
        private double northPoleBearing;
        private double pitch;

        public double Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;
                this.RaisePropertyChanged(() => this.Pitch);
            }
        }
        

        public double NorthPoleBearing
        {
            get { return northPoleBearing; }
            set
            {
                northPoleBearing = value;
                this.RaisePropertyChanged(() => this.NorthPoleBearing);
            }
        }
        

        public double Bearing
        {
            get { return bearing; }
            set
            {
                bearing = value;
                this.RaisePropertyChanged(() => this.Bearing);
            }
        }
        

        public string DistanceText
        {
            get { return distamceText; }
            set
            {
                distamceText = value;
                this.RaisePropertyChanged(() => this.DistanceText);
            }
        }
        

        public double AccelZ
        {
            get { return accelZ; }
            set
            {
                accelZ = value;
                this.RaisePropertyChanged(() => this.AccelZ);
            }
        }
        

        public double AccelY
        {
            get { return accelY; }
            set
            {
                accelY = value;
                this.RaisePropertyChanged(() => this.AccelY);
            }
        }
        

        public double AccelX
        {
            get { return accelX; }
            set
            {
                accelX = value;
                this.RaisePropertyChanged(() => this.AccelX);
            }
        }
        

        public InclinometerReading Inclination
        {
            get { return inclination; }
            set
            {
                inclination = value;
                this.RaisePropertyChanged(() => this.DistanceToCar);
            }
        }

        public CarPositionPageVM()
        {
            this.DistanceText = "Positioning...";
            this.Initializing = true;
            Sensors.LocationChanged += this.UpdateLocations;
            Sensors.InclinationChenged += this.UpdateInclination;
            Sensors.AccelerometerChanged += this.UpdatePitch;
            
            GetCarDataFromParse(CarManager.GetUserCarId());
            
           

        }

        private void UpdatePitch(object sender, EventArgs e)
        {
            this.AccelX = Sensors.accelX;
            this.AccelY = Sensors.accelY;
            this.AccelZ = Sensors.accelZ;
            if (Sensors.accelY < 0)
            {
                this.Pitch = 1* Sensors.accelY * (-1);
            }
            else
            {
                this.Pitch = 1 * Sensors.accelY;
            }
            
        }

        public async Task GetCarDataFromParse(string carId)
        {
            var car = await new ParseQuery<CarModel>().Where(c => c.ObjectId == carId).FirstOrDefaultAsync(CancellationToken.None);
            this.Car = CarVM.FromCarModel(car);
            this.Initializing = false;
            this.UpdateLocations(null, null);
        }

        private void UpdateInclination(object sender, EventArgs e)
        {
            this.Inclination = Sensors.inclinationReadings;
        }

        public void UpdateLocations(object sender, EventArgs e)
        {
            var lat = Sensors.lattitude; var lon = Sensors.longitude;
            this.UserLocation = new ParseGeoPoint(lat, lon);
            this.DistanceToCar = userLocation.DistanceTo(this.Car.Location).Kilometers;
            this.Bearing = GeopositionHelper.GetBearing(this.UserLocation, this.Car.Location);
        }

        

        public double DistanceToCar
        {
            get { return distanceToCar; }
            set
            {
                distanceToCar = value;
                if (distanceToCar > 0.1) // 100 m
                {
                    this.DistanceText = string.Format("{0:0.00} km", distanceToCar);
                }
                else
                {
                    this.DistanceText = string.Format("{0:0.00} m", distanceToCar * 1000);
                }
               
                this.RaisePropertyChanged(() => this.DistanceToCar);
            }
        }

        public ParseGeoPoint UserLocation
        {
            get
            {
                return this.userLocation;
            }
            set
            {
                userLocation = value;
                this.RaisePropertyChanged(() => this.UserLocation);
            }
        }

        public CarVM Car
        {
            get { return car; }
            set
            {
                car = value;
                this.RaisePropertyChanged(() => this.Car);
            }
        }

        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                this.RaisePropertyChanged(() => this.PageTitle);
            }
        }

        public bool Initializing
        {
            get { return initializing; }
            set
            {
                initializing = value;
                this.RaisePropertyChanged(() => this.Initializing);
            }

        }
    }
}
