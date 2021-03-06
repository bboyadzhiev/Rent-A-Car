﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Rent_A_Car.Pages.Details;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Rent_A_Car.Pages.UserControls
{
    public sealed partial class CarsPageListView : UserControl
    {
        public CarsPageListView()
        {
            this.InitializeComponent();
        }

        private void OnCarItemSelection(object sender, SelectionChangedEventArgs e)
        {
            var carsListView = (sender as ListView);
            var selectedCar = carsListView.SelectedItem;
            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(CarDetailsPage), selectedCar);
        }
    }
}
