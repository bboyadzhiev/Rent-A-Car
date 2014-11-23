using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using Parse;
using Rent_A_Car.Models;

namespace Rent_A_Car.ViewModels
{
    public class UserVM : ViewModelBase
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public CarModel Car { get; set; }

        public static UserVM FromModel(ParseUser parseUser)
        {
            var car = parseUser["Car"] as CarModel;
            return new UserVM()
            {
                Username = parseUser.Username,
                Car = car
            };
        }
    }
}
