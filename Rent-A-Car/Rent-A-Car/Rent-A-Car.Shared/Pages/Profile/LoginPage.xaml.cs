﻿using System;
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
using Parse;
using Rent_A_Car.Common;
using Rent_A_Car.ViewModels;
using Windows.UI.Popups;
using Rent_A_Car.Models;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Rent_A_Car.Pages.Profile
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public LoginPage()
            : this(new LoginPageVM())
        {

        }

        public LoginPage(LoginPageVM viewModel)
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
            //this.DataContext = e.Parameter;
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel == null)
            {
                //raise error
                return;
            }
            //MessageDialog dia = new MessageDialog("Username " , "Done!");
            //await dia.ShowAsync();
            //var user = new ParseUser();
            //user.Username = "Gosho";
            //user.Password = "gogo";
            //user["Car"] = new CarModel() { Plate = "XXXXXXXXXXXXX" };
            //await user.SignUpAsync();

            var parseErrorOccured = false;
            
            ParseException.ErrorCode parseErrorCode = ParseException.ErrorCode.OtherCause;
            try
            {
                var isLoggedIn = await this.ViewModel.Login();
                if (isLoggedIn)
                {
                    await CarManager.HasCarAssigned(this.CarCheckComplete);
                    //this.Frame.Navigate(typeof(RentersPage));
                }

            }
            catch (Exception ex)
            {
                if (ex is ParseException)
                {
                    parseErrorOccured = true;
                    parseErrorCode = (ex as ParseException).Code;
                }
            }

            if (parseErrorOccured)
            {
                MessageDialog error = null;
                if (parseErrorCode == ParseException.ErrorCode.ObjectNotFound)
                {
                    error = new MessageDialog("Check your credentials!", "Login Failed!");
                }
                if (parseErrorCode == ParseException.ErrorCode.OtherCause)
                {

                    error = new MessageDialog("Login not successfull\ncheck your connection!", "Sorry!");
                }
                await error.ShowAsync();
            }

        }

        private void CarCheckComplete(object sender, EventArgs e)
        {
            if (sender != null)
            {
                //has car assigned!
                this.Frame.Navigate(typeof(CarPositionPage));
            }
            else
            {
                // no car assigned
                this.Frame.Navigate(typeof(RentersPage));
            }
        }



        public LoginPageVM ViewModel
        {
            get
            {
                return this.DataContext as LoginPageVM;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
