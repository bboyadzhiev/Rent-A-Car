using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using GalaSoft.MvvmLight;
using Parse;
using Rent_A_Car.Models;
using Rent_A_Car.ViewModels;

namespace Rent_A_Car.Pages
{
    public class RentersPageVM : ViewModelBase
    {
        private ObservableCollection<RenterVM> renters;
        private bool initializing;

        public IEnumerable<RenterVM> Renters
        {
             get
            {
                if (this.renters == null)
                {
                    this.renters = new ObservableCollection<RenterVM>();
                }
                return this.renters;
            }
            set
            {
                if (this.renters == null)
                {
                    this.renters = new ObservableCollection<RenterVM>();
                }
                this.renters.Clear();
                foreach (var item in value)
                {
                    this.renters.Add(item);
                }
                this.RaisePropertyChanged(() => this.Renters);
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

        public RentersPageVM()
        {
            this.FetchRenters();
        }

        private async Task FetchRenters()
        {
            this.Initializing = true;

            var rent = await new ParseQuery<RenterModel>().FindAsync();
            this.Renters = rent.AsQueryable()
                    .Select(RenterVM.FromModel);

            this.Initializing = false;
        }
    }
}
