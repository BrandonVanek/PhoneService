using PhoneService.Models;

namespace PhoneService.DTO
{
    public class UserDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
        public List<Plan> Plans { get; set; }
        public List<Device> Devices { get; set; }
    }
}
