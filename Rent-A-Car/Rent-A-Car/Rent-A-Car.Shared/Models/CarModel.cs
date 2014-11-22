using System;
using System.Collections.Generic;
using System.Text;
using Parse;

namespace Rent_A_Car.Models
{
    public enum CarTypes
    {
        Sedane = 0,
        HatchBack = 1,
        Convertible = 2,
        Crossover = 3,
        Limousine = 4,
        Minivan = 5
    }

    [ParseClassName("CarModel")]
    public class CarModel : ParseObject
    {
        public CarTypes CarType
        {
            get { return (CarTypes)this.CarTypeForParse; }
            set { this.CarTypeForParse = (int)value; }
        }

        [ParseFieldName("CarType")]
        public int CarTypeForParse
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        [ParseFieldName("renter")]
        public RenterModel Renter
        {
            get { return GetProperty<RenterModel>(); }
            set { SetProperty<RenterModel>(value); }
        }

        [ParseFieldName("plate")]
        public string Plate
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("plate")]
        public string Title
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("available")]
        public bool Available
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("description")]
        public string Description
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("location")]
        public ParseGeoPoint Location
        {
            get { return GetProperty<ParseGeoPoint>(); }
            set { SetProperty<ParseGeoPoint>(value); }
        }

        [ParseFieldName("image")]
        public ParseFile Image
        {
            get { return GetProperty<ParseFile>(); }
            set { SetProperty<ParseFile>(value); }
        }

        [ParseFieldName("seats")]
        public int Seats
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        [ParseFieldName("price")]
        public double Price
        {
            get { return GetProperty<double>(); }
            set { SetProperty<double>(value); }
        }

        [ParseFieldName("luggage")]
        public int Luggage
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        [ParseFieldName("airconditioner")]
        public bool HasAirconditioner
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        public Uri GetCarTypeAssetsUri()
        {
            Uri uri;
            switch (this.CarType)
            {
                case CarTypes.Sedane:
                    uri = new Uri("ms-appx:///Assets/CarImages/CarTypes/sedane.jpg");
                    break;                                     
                case CarTypes.HatchBack:
                    uri = new Uri("ms-appx:///Assets/CarImages/CarTypes/hatchback.jpg");
                    break;                                     
                case CarTypes.Convertible:
                    uri = new Uri("ms-appx:///Assets/CarImages/CarTypes/convertible.jpg");
                    break;                                     
                case CarTypes.Crossover:
                    uri = new Uri("ms-appx:///Assets/CarImages/CarTypes/crossover.jpg");
                    break;                                     
                case CarTypes.Limousine:
                    uri = new Uri("ms-appx:///Assets/CarImages/CarTypes/limousine2.jpg");
                    break;                                     
                case CarTypes.Minivan:
                    uri = new Uri("ms-appx:///Assets/CarImages/CarTypes/minivan.jpg");
                    break;                                     
                default:
                    uri = new Uri("ms-appx:///Assets/CarImages/CarTypes/limousine.jpg");
                    break;
            }
            return uri;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CarModel;
            if (other == null)
            {
                return false;
            }
            return this.Plate == other.Plate;
        }
    }
}
