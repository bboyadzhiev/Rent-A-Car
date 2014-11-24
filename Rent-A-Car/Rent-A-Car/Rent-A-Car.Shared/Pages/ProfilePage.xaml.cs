using Rent_A_Car.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Rent_A_Car.Pages.Profile;
using Parse;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Rent_A_Car.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public ProfilePage()
            : this(new ProfilePageVM())
        {

        }

        public ProfilePage(ProfilePageVM viewModel)
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            this.ViewModel = viewModel;
        }



        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void OnRentersAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RentersPage));
        }
        public ProfilePageVM ViewModel
        {
            get
            {
                return (ProfilePageVM)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private async void OnReleaseCarClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Initializing = true;
            await CarManager.ReleaseCar(this.ViewModel.Car);
            this.Frame.Navigate(typeof(RentersPage));
        }

        private async void OnSignOutAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Initializing = true;
            await CarManager.ReleaseCar(this.ViewModel.Car);
            ParseUser.LogOut();
            this.Frame.Navigate(typeof(LoginPage));
        }

        private void OnCarFinderAppBarButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CarPositionPage));
        }

        private void OnManipulation_Completed(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (Math.Abs(e.Cumulative.Translation.X) > this.Frame.ActualWidth/3)
            {
                this.Frame.Navigate(typeof(CarPositionPage));
            }
        }
    }
}
