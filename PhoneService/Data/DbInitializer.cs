using PhoneService.Models;

namespace PhoneService.Data
{
    public class DbInitializer
    {
        public static void Initialize(PhoneServiceDbContext context)
        {

            // Checks to ensure that Database is even created
            context.Database.EnsureCreated();

            if (!(context.Users.Any()))
            {
                var usersToAdd = new User[]
                {
                    new User { Name = "Kevin Yi", Username = "username", Email = "Kevin@gmail.com", Password = "password"},
                    new User { Name = "Brandon Vanek", Username = "username1", Email = "Brandon@gmail.com", Password = "password1"}
                };
                context.Users.AddRange(usersToAdd);
                context.SaveChanges();
            }
            if (!(context.Plans.Any()))
            {
                var planToAdd = new Plan[]
                {
                    new Plan { Name = "Gold", Limit = 5, Price = 123.9, Description = "Gold plan description"},
                    new Plan { Name = "Silver", Limit = 10, Price = 100.9, Description = "Silver plan description"},
                    new Plan { Name = "Bronze", Limit = 15, Price = 80.9, Description = "Bronze plan description"},

                };
                context.Plans.AddRange(planToAdd);
                context.SaveChanges();
            }
            //if (!context.PhoneNumbers.Any())
            //{
            //    var bookingsToAdd = new PhoneNumber[]
            //    {
            //        new PhoneNumber { Number = "333-333-3333", UserId  = 1},
                    

            //    };
            //    context.PhoneNumbers.AddRange(bookingsToAdd);
            //    context.SaveChanges();
            //}
        }
    
}
}
