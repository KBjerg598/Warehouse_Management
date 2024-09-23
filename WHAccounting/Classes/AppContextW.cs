using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHAccounting.Classes
{
    class AppContextW : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Item> Items { get; set; }

        public AppContextW() : base("Data Source=DESKTOP-JCE248S\\SQLEXPRESS;Initial Catalog=DProgramDB;Integrated Security=True;") { }
    }
}