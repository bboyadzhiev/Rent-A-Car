using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Parse;
using Rent_A_Car.Models;
using Rent_A_Car.ViewModels;
using Windows.Storage;
using System.Linq;
using Windows.UI.Popups;

namespace Rent_A_Car.Common
{
    public static class CarManager
    {

        public async static Task<bool> RentCar(CarVM carVM)
        {
            var car = await new ParseQuery<CarModel>().Where(c => c.ObjectId == carVM.Id).FirstOrDefaultAsync(CancellationToken.None);
            if (car.Available == false)
            {
                MessageDialog error = new MessageDialog("Car " + car.Plate + "\n is not available now!", "Sorry!");
                await error.ShowAsync();
                return false;
            }
            car.Available = false;

            await car.SaveAsync();

            var user = ParseUser.CurrentUser;
            user["Car"] = car;
            await user.SaveAsync();

            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["userCarId"] = car.ObjectId;
            localSettings.Values["userCarLat"] = car.Location.Latitude;
            localSettings.Values["userCarLon"] = car.Location.Longitude;

            MessageDialog dia = new MessageDialog("Car " + car.Plate + "\n is now assigned to you!", "Done!");
            await dia.ShowAsync();
            return true;

        }

        public static async Task HasCarAssigned(EventHandler assignee)
        {
            EventHandler handler = assignee;
            var user = ParseUser.CurrentUser;
            var query = new ParseQuery<ParseObject>("_User").Where(u => u.ObjectId == user.ObjectId).FirstOrDefaultAsync();
            if (query.IsCanceled || query.IsFaulted)
            {
                if (handler != null)
                {
                    handler(null, EventArgs.Empty);
                }
            }
            var res = await query;
            if (res == null)
            {
                if (handler != null)
                {
                    handler(null, EventArgs.Empty);
                }
            }
            CarModel car = null;
            res.TryGetValue<CarModel>("Car", out car);

            if (handler != null)
            {
                handler(car, EventArgs.Empty);
            }
        }

        public static void ReadAssignedCar()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var carId = (string)localSettings.Values["userCarId"];
            var carLat = (double)localSettings.Values["userCarLat"];
            var carLong = (double)localSettings.Values["userCarLon"];

        }

        public static string GetUserCarId()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            return (string)localSettings.Values["userCarId"];
        }
       

   
        #region Deprecated Methods
        //public async static Task<IEnumerable<CarVM>> FetchCarsForRenterAndType(string renterId, CarTypes? carType)
        //{
        //    IEnumerable<CarModel> filtered;
        //    if (renterId != null && carType != null)
        //    {
        //         filtered = await new ParseQuery<CarModel>().Where(c => c.Available && c.CarType == carType && c.Renter.ObjectId == renterId).FindAsync(CancellationToken.None);
        //    }
        //    else if (renterId != null)
        //    {
        //        filtered = await new ParseQuery<CarModel>().Where(c => c.Available && c.Renter.ObjectId == renterId).FindAsync(CancellationToken.None);
        //    }
        //    else
        //    {
        //        filtered = await new ParseQuery<CarModel>().FindAsync(CancellationToken.None);
        //    }
        //    var result =  filtered.AsQueryable().Select(CarVM.FromModel);
        //    return result;
        //}
                  

        //public async static Task<bool> HasCar()
        //{
        //    var result = false;
        //    var user = ParseUser.CurrentUser;
        //    var query = new ParseQuery<ParseObject>("_User").Where(u => u.ObjectId == user.ObjectId);
        //    string id;
        //    await query.FirstOrDefaultAsync().ContinueWith(t =>
        //    {
        //        var car = t.Result.Get<CarModel>("Car");
        //        id = car.ObjectId;
        //        result = true;
        //    });

        //    return result;
        //}

        #endregion

    }
}
