using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using Rent_A_Car.ViewModels;
using Rent_A_Car.Common;
using System.Threading.Tasks;
using Parse;
using System.Threading;
using System.Linq;
using Rent_A_Car.Models;

namespace Rent_A_Car.Pages
{
    public class ProfilePageVM  : ViewModelBase
    {
        private UserVM user;
        private CarVM car;
        private bool initializing;

        public ProfilePageVM()
        {
            this.Initializing = true;
            var userInfo = ParseUser.CurrentUser;
            this.User = UserVM.FromModel(userInfo);
            var carId = CarManager.GetUserCarId();
            if (carId != null)
            {
                GetCarDataFromParse(CarManager.GetUserCarId());
            }
            else
            {
                this.Car = new CarVM()
                {
                    Plate = "No car assigned!"

                };
                // no car
            }
            this.Initializing = false;
        }

        public async Task GetCarDataFromParse(string carId)
        {
            var car = await new ParseQuery<CarModel>().Where(c => c.ObjectId == carId).FirstOrDefaultAsync(CancellationToken.None);
            this.Car = CarVM.FromCarModel(car);
            this.Initializing = false;
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
        public CarVM Car
        {
            get { return car; }
            set
            {
                car = value;
                this.RaisePropertyChanged(() => this.Car);
            }
        }
        

        public UserVM User
        {
            get { return user; }
            set
            {
                user = value;
                this.RaisePropertyChanged(() => this.User);
            }
        }
        
    }
}
