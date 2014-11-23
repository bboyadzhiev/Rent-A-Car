using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace Rent_A_Car.ViewModels
{
    public class HeaderVM : ViewModelBase
    {
        private String titleText;

        public String TitleText
        {
            get { return titleText; }
            set
            {
                titleText = value; this.RaisePropertyChanged(() => this.TitleText);
            }
        }
        
    }
}
