using Microsoft.EntityFrameworkCore;

namespace DatingApp_API.Data
{

    //can be also written as follows as Primary constructor instead of traditional method used 
    // public DataContext(DbContextOptions options) : base(options)
    //        {

    //        }

    //protected DataContext()
    //{
    //}


    public class DataContext(DbContextOptions options) : DbContext(options)
    {

        //here AppUser is model/entity we created and Users is the name will be created in DB for AppUser entity
        public DbSet<AppUser> Users { get; set; }

    }
}
