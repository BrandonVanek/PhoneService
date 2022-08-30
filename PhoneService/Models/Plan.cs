namespace PhoneService.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Limit { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public ICollection<UserPlan> UserPlans { get; set; }
    }
}
