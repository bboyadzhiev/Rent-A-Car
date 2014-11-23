using System;
using System.Collections.Generic;
using System.Text;

using Windows.UI.Xaml.Media.Imaging;

using GalaSoft.MvvmLight;

using Parse;

using Rent_A_Car.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Storage.Streams;
using System.Linq.Expressions;


namespace Rent_A_Car.ViewModels
{
    public class CarVM : ViewModelBase
    {

        private CarTypes carType;
        private string plate;
        private string renterId;
        private string description;
        private ParseGeoPoint location;
        private BitmapImage image;
        private int seats;
        private int luggage;
        private bool hasAirConditioner;
        private string title;
        private string id;

        public string Id
        {
            get { return id; }
            set
            {
                id = value; 
                this.RaisePropertyChanged(() => this.Id);
            }
        }


        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                this.RaisePropertyChanged(() => this.Title);
            }
        }


        public string RenterId
        {
            get { return renterId; }
            set
            {
                renterId = value;
                this.RaisePropertyChanged(() => this.RenterId);
            }
        }

        public bool HasAirconditioner
        {
            get { return hasAirConditioner; }
            set
            {
                hasAirConditioner = value;
                this.RaisePropertyChanged(() => this.HasAirconditioner);
            }
        }


        public int Luggage
        {
            get { return luggage; }
            set
            {
                luggage = value;
                this.RaisePropertyChanged(() => this.Luggage);
            }
        }


        public int Seats
        {
            get { return seats; }
            set
            {
                seats = value;
                this.RaisePropertyChanged(() => this.Seats);
            }
        }


        public BitmapImage Image
        {
            get { return image; }
            set
            {
                image = value;
                this.RaisePropertyChanged(() => this.Image);
            }
        }


        public ParseGeoPoint Location
        {
            get { return location; }
            set
            {
                location = value;
                this.RaisePropertyChanged(() => this.Location);
            }
        }


        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                this.RaisePropertyChanged(() => this.Description);
            }
        }


        public string Plate
        {
            get { return plate; }
            set
            {
                plate = value;
                this.RaisePropertyChanged(() => this.Plate);
            }
        }


        public CarTypes CarType
        {
            get { return carType; }
            set
            {
                carType = value;
                this.RaisePropertyChanged(() => this.CarType);
            }
        }

        public static Expression<Func<CarModel, CarVM>> FromModel
        {
            get
            {
                return (model) => new CarVM()
                {
                    Id = model.ObjectId,
                    RenterId = model.Renter.ObjectId,
                    CarType = model.CarType,
                    Plate = model.Plate,
                    Title = model.Title,
                    Description = model.Description,
                    Location = model.Location,
                    Image = CarVM.GetImageFromParseUri(model.Image.Url),
                    Seats = model.Seats,
                    Luggage = model.Luggage,
                    HasAirconditioner = model.HasAirconditioner
                };
            }
        }

        public static CarVM FromCarModel(CarModel model)
        {
            CarVM car = new CarVM();
            car.Id = model.ObjectId;
            car.RenterId = model.Renter.ObjectId;
            car.CarType = model.CarType;
            car.Plate = model.Plate;
            car.Title = model.Title;
            car.Description = model.Description;
            car.Location = model.Location;
            car.Image = CarVM.GetImageFromParseUri(model.Image.Url);
            car.Seats = model.Seats;
            car.Luggage = model.Luggage;
            car.HasAirconditioner = model.HasAirconditioner;
            return car;
        }

        public static BitmapImage GetImageFromParseUri(Uri uri)
        {
            var imageBytes = new HttpClient().GetByteArrayAsync(uri).Result;
            InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
            DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0));
            writer.WriteBytes(imageBytes as byte[]);
            writer.StoreAsync().GetResults();
            BitmapImage image = new BitmapImage();
            image.SetSource(ms);
            return image;
        }
    }
}
