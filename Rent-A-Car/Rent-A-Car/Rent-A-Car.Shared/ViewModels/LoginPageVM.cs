
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Parse;
using Rent_A_Car.Models;

namespace Rent_A_Car.ViewModels
{
    public class LoginPageVM : ViewModelBase
    {
        public UserVM User { get; set; }

        public event EventHandler LoginSuccessfull;

        public LoginPageVM()
        {
               // TODO: check for default user
            this.User = new UserVM()
            {
              Username = "Gosho",
              Password = "gogo",
            };
        }

        public async Task<bool> Login()
        {
            try
            {
                await ParseUser.LogInAsync(this.User.Username, this.User.Password);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
