using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using GalaSoft.MvvmLight;
using Rent_A_Car.ViewModels;
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
    public sealed partial class HeaderView : UserControl
    {

        public HeaderView()
            :this (new HeaderVM())
        {

        }

        public HeaderView(HeaderVM viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }

        private void OnSignOutButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
