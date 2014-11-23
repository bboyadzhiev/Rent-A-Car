using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Parse;
using Rent_A_Car.Models;
using Windows.Storage;
using Windows.UI.Popups;

namespace Rent_A_Car.Common
{
    public static class CarManager
    {

        public async static void RentCar (string carId)
        {
             var car = await new ParseQuery<CarModel>().Where(c=>c.ObjectId == carId).FirstOrDefaultAsync(CancellationToken.None);
            car.Available = false;
            await car.SaveAsync();

            var user = ParseUser.CurrentUser;
            user["Car"] = car;
            await user.SaveAsync();
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["userCar"] = car.ObjectId;
       
            MessageDialog dia = new MessageDialog("Car " + car.Plate + "\n is now assigned to you!", "Done!");
            await dia.ShowAsync();
        }

        public async static Task<bool> HasCar()
        {
            var result = false;
            var user = ParseUser.CurrentUser;
            var query = new ParseQuery<ParseObject>("_User").Where(u => u.ObjectId == user.ObjectId);
            string id;
            await  query.FirstOrDefaultAsync().ContinueWith(t =>
            {
                var car = t.Result.Get<CarModel>("Car");
                id = car.ObjectId;
                result = true;
            });

            return result;
        }

    }
}
