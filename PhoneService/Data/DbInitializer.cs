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
                    new Plan { Name = "Silver Premium", Limit = 24, Price = 100.9, Description = "This is the plan's Description"},
                    new Plan { Name = "Gold Premium", Limit = 24, Price = 100.9, Description = "This is the plan's Description222"},
                    
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
