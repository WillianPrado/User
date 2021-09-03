using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Models;

namespace User.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
        }

        public static UserModels Get(string name, string password)
        {
            var user = new List<UserModels>();
            return (UserModels)user.Where(x => x.Name.ToLower() == name && x.Password == password);
        }

        public DbSet<UserModels> Users { get; set; }

        public DbSet<CompanyMoldels> Companies { get; set; }

    }
}
