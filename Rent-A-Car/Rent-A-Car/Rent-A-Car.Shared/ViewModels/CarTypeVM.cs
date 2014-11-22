using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using Rent_A_Car.Models;

using Windows.UI.Xaml.Media.Imaging;

namespace Rent_A_Car.ViewModels
{
    public class CarTypeVM : ViewModelBase
    {
        private CarTypes carType;
        private string url;
        private string title;
        private int count;
        private string renterId;

        public CarTypes CarType
        {
            get { return carType; }
            set { carType = value; }
        }

        public string RenterId
        {
            get { return renterId; }
            set
            {
                renterId = value;
                this.RaisePropertyChanged(() => this.RenterId);
            }
        }
        

        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                this.RaisePropertyChanged(() => this.Count);
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                this.RaisePropertyChanged(() => this.Title);
            }
        }

        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                this.RaisePropertyChanged(() => this.Url);
            }

        }

        
    }
}
