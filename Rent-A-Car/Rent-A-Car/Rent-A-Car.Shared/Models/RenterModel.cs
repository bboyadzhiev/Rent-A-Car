using System;
using System.Collections.Generic;
using System.Text;

using Parse;

namespace Rent_A_Car.Models
{
    [ParseClassName("RenterModel")]
    public class RenterModel : ParseObject
    {
        [ParseFieldName("name")]
        public string Name
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("location")]
        public ParseGeoPoint Location
        {
            get { return GetProperty<ParseGeoPoint>(); }
            set { SetProperty<ParseGeoPoint>(value); }
            //set { SetProperty<ParseGeoPoint>(new ParseGeoPoint(0.0, 0.0)); }
        }

        [ParseFieldName("address")]
        public string Address
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("logo")]
        public ParseFile Logo
        {
            get { return GetProperty<ParseFile>(); }
            set { SetProperty<ParseFile>(value); }
           // set { SetProperty<ParseFile>(new ParseFile("asd.txt", new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 })); }
        }

        public override bool Equals(object obj)
        {
            var other = obj as RenterModel;
            if (other == null)
            {
                return false;
            }
           // return this.Address == other.Address;
            return this.Location.Latitude == other.Location.Latitude
                && this.Location.Longitude == other.Location.Longitude;
        }
    }
}
