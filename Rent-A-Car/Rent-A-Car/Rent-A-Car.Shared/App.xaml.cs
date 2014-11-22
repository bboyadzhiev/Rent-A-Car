using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Rent_A_Car.Common;
using Parse;
using Rent_A_Car.Models;
using System.Threading.Tasks;
using Windows.Storage;

// The Universal Hub Application project template is documented at http://go.microsoft.com/fwlink/?LinkID=391955

namespace Rent_A_Car
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif

        /// <summary>
        /// Initializes the singleton instance of the <see cref="App"/> class. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
            this.InitParse();

          //  this.SeedRenters();
        }

        private void InitParse()
        {
            ParseObject.RegisterSubclass<CarModel>();
            ParseObject.RegisterSubclass<RenterModel>();
            //ParseObject.RegisterSubclass<UserModel>();
            ParseClient.Initialize("pb0ca7B5uQgBo0i8IUmgxJ2IYi4NfLUHEIOb4Juu", "Zz6jDgmZqgV9eNPp7JdjdKpDSWutIfzMqDWoCtVz");

            //  await TestObject();

            //  var cars = await this.GetCars();
        }

        private async void SeedRenters()
        {
            var renters = new List<RenterModel>();
            var renter = new RenterModel();

            renter.Name = "Get-you-there Inc.";
            renter.Address = "boulevard 'Tsarigradsko shose' 163, 1784 Sofia";
            //42.646219, 23.398265
            var point = new ParseGeoPoint();
            point.Latitude = 42.646219;
            point.Longitude = 23.398265;
            renter.Location = point;
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/CarImages/CarTypes/limousine2.jpg"));

            Stream stream = (await file.OpenReadAsync()).AsStreamForRead();
            renter.Logo = new ParseFile("logo.jpg", stream);

            renters.Add(renter);

            renter = new RenterModel();
            renter.Name = "You'll never get there Inc.";
            renter.Address = "Lyulin, Sofia";
            //42.653192, 23.195399
            point = new ParseGeoPoint();
            point.Latitude = 42.653192;
            point.Longitude = 23.195399;
            renter.Location = point;
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/CarImages/CarTypes/limousine2.jpg"));

            stream = (await file.OpenReadAsync()).AsStreamForRead();
            renter.Logo = new ParseFile("logo.jpg", stream);
            renters.Add(renter);

            var rentersInDb = await new ParseQuery<RenterModel>().FindAsync();
            var missingRenters = renters.Where(rent => !rentersInDb.Any(rentDb => rentDb.Equals(rent)));

            foreach (var missingRenter in missingRenters)
            {
                await missingRenter.SaveAsync();
            }

            this.SeedCars();
        }

        private async void SeedCars()
        {
            var rentersInDb = await new ParseQuery<RenterModel>().FindAsync();
            foreach (var renter in rentersInDb)
            {

                var carModels = new List<CarModel>();
                var i = 1;
                foreach (CarTypes carType in (CarTypes[])Enum.GetValues(typeof(CarTypes)))
                {
                    var model = new CarModel();
                    if (carType == CarTypes.Sedane)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                        model = await GetSeedCar(renter, i, carType);
                        carModels.Add(model);
                        i++;
                        }
                    }
                    else
                    {
                        model = await GetSeedCar(renter, i, carType);
                        carModels.Add(model);
                        i++;
                    }
                   
                }
                var carsInDb = await new ParseQuery<CarModel>().FindAsync();
                var missingCars = carModels.Where(catModel => !carsInDb.Any(catDb => catDb.Equals(catModel)));

                foreach (var missingCar in missingCars)
                {
                    await missingCar.SaveAsync();
                }
            }


        }

        private static async Task<CarModel> GetSeedCar(RenterModel renter, int i, CarTypes carType)
        {
            var model = new CarModel();
            model.Available = true;
            model.CarType = carType;
            // model.CarTypeForParse = carType;
            model.HasAirconditioner = (i % 2 == 0 ? true : false);
            // photo
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(model.GetCarTypeAssetsUri());

            Stream stream = (await file.OpenReadAsync()).AsStreamForRead();
            //BinaryReader reader = new BinaryReader(stream);
            //byte[] data = reader.ReadBytes((int)stream.Length);
            model.Plate = "CA " + i + " " + renter.ObjectId;
            model.Image = new ParseFile(carType + " " + model.Plate + ".jpg", stream);
            model.Title = model.CarType.ToString() + "-" + model.Plate;
            model.Location = renter.Location;
            model.Luggage = 3;
            model.Price = i * 3.14;
            model.Seats = i > 4 ? 5 : 4;
            model.Renter = renter;
            model.Description = string.Format("This is a {0} with {1} seats and {2} airconditioner.", carType, model.Seats, model.HasAirconditioner ? "has" : "has no");

            return model;
        }

        

        private static async Task TestObject()
        {
            var testObject = new ParseObject("TestObject");
            testObject["foo"] = "bar";
            await testObject.SaveAsync();
        }

        private async Task<IEnumerable<CarModel>> GetCars()
        {
            var cars = await new ParseQuery<CarModel>()
            .FindAsync();
            return cars;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                //Associate the frame with a SuspensionManager key                                
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                // TODO: change this value to a cache size that is appropriate for your application
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        // Something went wrong restoring state.
                        // Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
#if WINDOWS_PHONE_APP
                // Removes the turnstile navigation for startup.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;
#endif

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                // if (!rootFrame.Navigate(typeof(HubPage), e.Arguments))
                if (!rootFrame.Navigate(typeof(Welcome), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }
#endif

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }
    }
}