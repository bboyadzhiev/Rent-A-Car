using System;
using System.Collections.Generic;
using System.Text;
using Parse;

namespace Rent_A_Car.Models
{
      [ParseClassName("UserModel")]
    public class UserModel   : ParseObject
    {
          [ParseFieldName("avatar")]
          public ParseFile Avatar
          {
              get { return GetProperty<ParseFile>(); }
              set { SetProperty<ParseFile>(value); }
          }
    }
}
