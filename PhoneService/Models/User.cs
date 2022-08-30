namespace PhoneService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public ICollection<UserPlan> UserPlans { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
