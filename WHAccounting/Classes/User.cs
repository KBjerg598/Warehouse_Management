using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHAccounting.Classes
{
    public class User
    {
        public int id { get; set; }
        public string ulogin, uphoneNumber, upassword;

        public string ULogin
        {
            get { return ulogin; }
            set { ulogin = value; }
        }

        public string UPhoneNumber
        {
            get { return uphoneNumber; }
            set { uphoneNumber = value; }
        }

        public string UPassword
        {
            get { return upassword; }
            set { upassword = value; }
        }

        public User() { }
        public User(string login, string phoneNumber, string password)
        {
            ulogin = login;
            uphoneNumber = phoneNumber;
            upassword = password;
        }
    }
}
