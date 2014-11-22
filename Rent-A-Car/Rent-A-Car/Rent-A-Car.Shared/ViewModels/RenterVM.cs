using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Parse;
using Rent_A_Car.Models;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Rent_A_Car.ViewModels
{
    public class RenterVM : ViewModelBase
    {
        private string id;
        private string name;
        private string address;
        private BitmapImage logo;
        private ParseGeoPoint location;

        public ParseGeoPoint Location
        {
            get { return location; }
            set
            {
                location = value;
                this.RaisePropertyChanged(() => this.Location);
            }
        }


        public BitmapImage Logo
        {
            get { return logo; }
            set
            {
                logo = value;
                this.RaisePropertyChanged(() => this.Logo);
            }
        }


        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                this.RaisePropertyChanged(() => this.Address);
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                this.RaisePropertyChanged(() => this.Name);
            }
        }
              
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                this.RaisePropertyChanged(() => this.Id);
            }
        }

        public static Expression<Func<RenterModel, RenterVM>> FromModel
        {
            get
            {
                return (model) => new RenterVM()
                {
                    Id = model.ObjectId,
                    Name = model.Name,
                    Address = model.Address,
                    Logo = RenterVM.GetImageFromParseUri(model.Logo.Url).Result,
                    Location = model.Location
                };
            }
        }

        //public async Task<RenterVM> GetVMFromModel (RenterModel model)
        //{
        //    var renterVM = new RenterVM();
        //    renterVM.Name = model.Name;
        //    renterVM.Address = model.Address;
        //    renterVM.Logo = RenterVM.GetImageFromBytes(await new HttpClient().GetByteArrayAsync(model.Logo.Url));
        //    return renterVM;
        //}

        public static BitmapImage GetImageFromBytes(byte[] imageBytes)
        {
            InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
            DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0));
            writer.WriteBytes(imageBytes as byte[]);
            writer.StoreAsync().GetResults();
            BitmapImage image = new BitmapImage();
            image.SetSource(ms);
            return image;
        }

        public async static Task<BitmapImage> GetImageFromParseUri(Uri uri)
        {
            var imageBytes =  new HttpClient().GetByteArrayAsync(uri).Result;
            InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
            DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0));
            writer.WriteBytes(imageBytes as byte[]);
            writer.StoreAsync().GetResults();
            BitmapImage image = new BitmapImage();
            image.SetSource(ms);
            return image;
        }
    }
}
