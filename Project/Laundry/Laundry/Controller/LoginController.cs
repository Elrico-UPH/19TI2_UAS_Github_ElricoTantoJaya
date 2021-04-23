using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.Controller
{
    class LoginController
    {
        public bool loginValidator(string nama, string pass)
        {
            if (string.Equals(nama, pass))
            {
                return true;
            }
            else
            {
                return !true;
            }
        }
    }
}
