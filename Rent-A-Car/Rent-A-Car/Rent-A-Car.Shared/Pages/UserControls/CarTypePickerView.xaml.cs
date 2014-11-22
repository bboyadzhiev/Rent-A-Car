using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class CarTypePickerView : UserControl
    {
        public CarTypePickerView()
        {
            this.InitializeComponent();
        }

        private void OnCarTypeSelection(object sender, SelectionChangedEventArgs e)
        {
            var typesGridView = (sender as GridView);
            var selectedCarType = typesGridView.SelectedItem;
            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(CarsPage), selectedCarType);
        }
    }
}
