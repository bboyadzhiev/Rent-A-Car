using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Parse;
using Rent_A_Car.Common;
using Rent_A_Car.Models;
using Rent_A_Car.Pages;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Rent_A_Car
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Welcome : Page
    {
        private bool booleanvalue;

        public bool Booleanvalue
        {
            get { return booleanvalue; }
            set
            {
                booleanvalue = value;
                if (value == true)
                {
                    this.Frame.Navigate(typeof(RentersPage));
                }
            }
        }

        private event EventHandler CarResult;

        public  Welcome()
        {
            this.InitializeComponent();
            this.ring.IsActive = true;
            var user = ParseUser.CurrentUser;
            this.userName.Text = user.Username;

            this.CarResult += this.MainRouter;
            HasCar();
            Sensors.LocationChanged += this.UpdateLocationText;
        }

        private void MainRouter(object sender, EventArgs e)
        {
            if (sender != null)
            {
                 //has car assigned!
                this.userCar.Text = (sender as CarModel).ObjectId;
            }
            else
            {
                // no car assigned
                this.Frame.Navigate(typeof(RentersPage));
            }
        }


        public async Task HasCar()
        {
            var user = ParseUser.CurrentUser;
            var query = await new ParseQuery<ParseObject>("_User").Where(u => u.ObjectId == user.ObjectId).FirstOrDefaultAsync();
            var car = query.Get<CarModel>("Car");
            
            
            EventHandler handler = this.CarResult;
            if (handler != null)
            {
                handler(car, EventArgs.Empty);
            }
          
        }


        private void UpdateLocationText(object sender, EventArgs e)
        {
            this.latTextBlock.Text = string.Format("LAT: {0}", Sensors.lattitude);
            this.lonTextBlock.Text = string.Format("LON: {0}", Sensors.longitude);
        }

        private void NewMethod()
        {
            var user = ParseUser.CurrentUser;
            // var query = new ParseQuery<ParseObject>("_User").OrderBy("username").Limit(2);//.Where(c => (c.Renter.ObjectId == "ZNlAgnwMS9"));
            //  var query = new ParseQuery<CarModel>().Where(c => c.Renter.ObjectId == "ZNlAgnwMS9");
            var query = new ParseQuery<ParseObject>("_User").Where(u => u.ObjectId == user.ObjectId);

            var parseCars = query.FirstOrDefaultAsync().ContinueWith(t =>
            {
                var car = t.Result.Get<CarModel>("Car");
                this.userCar.Text = car.ObjectId;
                var s = 5;
                //IEnumerable<ParseObject> results = t.Result;
                //foreach (var obj in results)
                //{
                //    var plate = obj.Get<string>("Car");
                //    this.userCar.Text = plate.ToString();
                //}

            });


        }

        public void UpdateText(object sender, EventArgs args)
        {
            this.latTextBlock.Text = Sensors.accelX.ToString();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RentersPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.fromTextBlock.Text = string.Format("FROM: {0}", e.SourcePageType.FullName);

            base.OnNavigatedTo(e);
        }
    }
}
