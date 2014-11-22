using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Rent_A_Car.Common;
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
        public Welcome()
        {
            this.InitializeComponent();

          //  Sensors.AccelerometerChanged += this.UpdateText;
            Sensors.LocationChanged += this.UpdateLocationText;
        }

        private void UpdateLocationText(object sender, EventArgs e)
        {
            this.latTextBlock.Text = string.Format("LAT: {0}", Sensors.lattitude);
            this.lonTextBlock.Text = string.Format("LON: {0}", Sensors.longitude);
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
