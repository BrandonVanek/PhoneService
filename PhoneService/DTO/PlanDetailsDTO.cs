using PhoneService.Models;

namespace PhoneService.DTO
{
    public class PlanDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Limit { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }
    }
}
