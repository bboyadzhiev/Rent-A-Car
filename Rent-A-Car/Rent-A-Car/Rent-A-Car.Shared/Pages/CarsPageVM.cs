﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Parse;
using Rent_A_Car.Models;
using Rent_A_Car.ViewModels;

namespace Rent_A_Car.Pages
{
    public class CarsPageVM : ViewModelBase
    {
        private ObservableCollection<CarVM> cars;
        private string pageTitle;
        private bool initializing;

        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                this.RaisePropertyChanged(() => this.PageTitle);
            }
        }
        public IEnumerable<CarVM> Cars
        {
            get
            {
                if (this.cars == null)
                {
                    this.cars = new ObservableCollection<CarVM>();
                }
                return this.cars;
            }
            set
            {
                if (this.cars == null)
                {
                    this.cars = new ObservableCollection<CarVM>();
                }
                this.cars.Clear();
                foreach (var item in value)
                {
                    this.cars.Add(item);
                }
                this.RaisePropertyChanged(() => this.Cars);
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

        public CarsPageVM()
        {
            this.FetchCarsForRenterAndType(null, null);
        }
        public CarsPageVM(string renterId)
        {
            this.FetchCarsForRenterAndType(renterId, null);
        }
        public CarsPageVM(string renterId, CarTypes carType)
        {
           
            this.FetchCarsForRenterAndType(renterId, carType);
        }

        private async Task FetchCarsForRenterAndType(string renterId, CarTypes? carType)
        {
            this.Initializing = true;
            int ct = (int)carType;
            var parseCars = await new ParseQuery<CarModel>().FindAsync();
            IEnumerable<CarModel> filtered;
            if (renterId != null && carType != null)
            {
                //int ct = (int) carType;
                // parseCars = await new ParseQuery<CarModel>().WhereContains("renter", renterId).WhereContains("CarType", ct.ToString()).FindAsync();
                // parseCars = await new ParseQuery<CarModel>().Where(c => c.Renter.ObjectId == renterId && c.CarType == carType).FindAsync();
                filtered = await parseCars.Where(c => c.Renter.ObjectId == renterId && c.CarType == carType).FetchAllAsync();
            }
            else if (renterId != null)
            {
                //parseCars = await new ParseQuery<CarModel>().Where(c => c.Renter.ObjectId == renterId).FindAsync();
                filtered = await parseCars.Where(c => c.Renter.ObjectId == renterId).FetchAllAsync();
            }
            else
            {
                //parseCars = await new ParseQuery<CarModel>().FindAsync();
                filtered = await parseCars.FetchAllAsync();
            }

            //this.Cars = parseCars.AsQueryable().Select(CarVM.FromModel);
            this.Cars = filtered.AsQueryable().Select(CarVM.FromModel);

            this.Initializing = false;
        }

        private async Task FetchCarsForRenterAndType2(string renterId, CarTypes carType)
        {
            this.Initializing = true; 
            var query = new ParseQuery<CarModel>().Where(c => (c.Renter.ObjectId == renterId) && (c.CarType == carType));
            //  var query = new ParseQuery<CarModel>().Where(c => c.Renter.ObjectId == "ZNlAgnwMS9");

            var parseCars = await query.FindAsync();
            this.Cars = parseCars.AsQueryable()
                   .Select(CarVM.FromModel);
            this.Initializing = false;
        }
    }
}
