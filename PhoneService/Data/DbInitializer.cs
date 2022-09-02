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
            if (!context.Devices.Any())
            {
                var devicesToAdd = new Device[]
                {
                    new Device { Name = "Gold", PhoneNumber = "123-456-7890", UserId = 1},
                    new Device { Name = "Silver", PhoneNumber = "222-333-4444", UserId = 1},
                    new Device { Name = "Bronze", PhoneNumber = "123-123-1234", UserId = 1},
                    new Device { Name = "Gold", PhoneNumber = "111-111-1111", UserId = 2},
                    new Device { Name = "Silver", PhoneNumber = "222-222-2222", UserId = 2},
                    new Device { Name = "Bronze", PhoneNumber = "333-333-3333", UserId = 2},
                };
                context.Devices.AddRange(devicesToAdd);
                context.SaveChanges();
            }
            if (!context.UserPlans.Any())
            {
                var userPlansToAdd = new UserPlan[]
                {
                    new UserPlan { UserId = 1, PlanId = 1},
                    new UserPlan { UserId = 1, PlanId = 2},
                    new UserPlan { UserId = 1, PlanId = 3},
                    new UserPlan { UserId = 2, PlanId = 1},
                    new UserPlan { UserId = 2, PlanId = 2},
                    new UserPlan { UserId = 2, PlanId = 3},
                };
                context.UserPlans.AddRange(userPlansToAdd);
                context.SaveChanges();
            }            
        }    
}
}
