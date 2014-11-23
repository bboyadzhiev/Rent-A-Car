using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Parse;
using Rent_A_Car.Models;
using Rent_A_Car.ViewModels;

namespace Rent_A_Car.Pages
{
    public class CarTypesPageVM : ViewModelBase
    {
        private ObservableCollection<CarTypeVM> availableTypes;
        private string pageTitle;
        private bool initializing;
        private List<int> counts;



        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                this.RaisePropertyChanged(() => this.PageTitle);
            }
        }



        public IEnumerable<CarTypeVM> AvailableTypes
        {
            get
            {
                if (this.availableTypes == null)
                {
                    this.availableTypes = new ObservableCollection<CarTypeVM>();
                }
                return this.availableTypes;
            }
            set
            {
                if (this.availableTypes == null)
                {
                    this.availableTypes = new ObservableCollection<CarTypeVM>();
                }
                this.availableTypes.Clear();
                foreach (var item in value)
                {
                    this.availableTypes.Add(item);
                }
                this.RaisePropertyChanged(() => this.AvailableTypes);
            }

        }

        public bool Initializing
        {
            get
            {
                return this.initializing;
            }
            set
            {
                this.initializing = value;
                this.RaisePropertyChanged(() => this.Initializing);
            }
        }

        public CarTypesPageVM()
        {
            this.Initializing = true;
            this.PageTitle = string.Format("All renters types");
            this.FetchAvailableCarTypesForRenter(null);
        }

        public CarTypesPageVM(RenterVM renter)
        {
            this.Initializing = true;
            this.PageTitle = string.Format("{0}'s car types", renter.Name);
            // var renterId = renter.Id;
            this.FetchAvailableCarTypesForRenter(renter);
        }

        public string GetCarTypeAssetsUrl(CarTypes type)
        {
            string url;
            switch (type)
            {
                case CarTypes.Sedane:
                    url = "ms-appx:///Assets/CarImages/CarTypes/sedane.jpg";
                    break;
                case CarTypes.HatchBack:
                    url = "ms-appx:///Assets/CarImages/CarTypes/hatchback.jpg";
                    break;
                case CarTypes.Convertible:
                    url = "ms-appx:///Assets/CarImages/CarTypes/convertible.jpg";
                    break;
                case CarTypes.Crossover:
                    url = "ms-appx:///Assets/CarImages/CarTypes/crossover.jpg";
                    break;
                case CarTypes.Limousine:
                    url = "ms-appx:///Assets/CarImages/CarTypes/limousine2.jpg";
                    break;
                case CarTypes.Minivan:
                    url = "ms-appx:///Assets/CarImages/CarTypes/minivan.jpg";
                    break;
                default:
                    url = "ms-appx:///Assets/CarImages/CarTypes/limousine.jpg";
                    break;
            }
            return url;
        }

        private async Task FetchAvailableCarTypesForRenter(RenterVM renter)
        {

            this.Initializing = true;
            if (this.availableTypes == null)
            {
                this.availableTypes = new ObservableCollection<CarTypeVM>();
            }
            IEnumerable<CarModel> parseCars;

            //var query = from gameScore in ParseObject.GetQuery("GameScore")
            //            where gameScore.Get<string>("playerName") == "Dan Stemkoski"
            //            select gameScore;
            var renterId = renter.Id;
            //var query = from cars in CarModel.GetQuery("CarModel")
            //            where cars.Get<string>("renter") == renterId
            //            select cars;
            //IEnumerable<ParseObject> results = await query.FindAsync();

            // parseCars = await new ParseQuery<CarModel>().Where(car => car.Renter.ObjectId == renterId).FindAsync();
            parseCars = await new ParseQuery<CarModel>().FindAsync();
            //  var parseCars = await query.FindAsync();
            var asd = await parseCars.Where(c => c.Renter.ObjectId == renterId).FetchAllAsync();



            var types = asd.AsQueryable()
                   .Select(c => c.CarType).ToList();
            this.counts = new List<int>() { 0, 0, 0, 0, 0, 0 };
            foreach (int item in types)
            {
                this.counts[item]++;
            }



            this.availableTypes.Clear();
            for (int i = 0; i < counts.Count; i++)
            {
                this.availableTypes.Add(new CarTypeVM
                {
                    CarType = ((CarTypes)i),
                    Count = counts[i],
                    Title = ((CarTypes)i).ToString(),
                    Url = GetCarTypeAssetsUrl((CarTypes)i),
                    RenterId = renterId
                });
            }
            this.RaisePropertyChanged(() => this.AvailableTypes);

            this.Initializing = false;
        }


        private async Task FetchAvailableCarTypes()
        {
            this.Initializing = true;

            if (this.availableTypes == null)
            {
                this.availableTypes = new ObservableCollection<CarTypeVM>();
            }
            IEnumerable<CarModel> parseCars;

            parseCars = await new ParseQuery<CarModel>().FindAsync();

            var types = parseCars.AsQueryable()
                   .Select(c => c.CarType).ToList();
            this.counts = new List<int>() { 0, 0, 0, 0, 0, 0 };
            foreach (int item in types)
            {
                this.counts[item]++;
            }



            this.availableTypes.Clear();
            for (int i = 0; i < counts.Count; i++)
            {
                this.availableTypes.Add(new CarTypeVM
                {
                    Count = counts[i],
                    Title = ((CarTypes)i).ToString(),
                    Url = GetCarTypeAssetsUrl((CarTypes)i)
                });
            }
            this.RaisePropertyChanged(() => this.AvailableTypes);

            this.Initializing = false;
        }
    }
}
