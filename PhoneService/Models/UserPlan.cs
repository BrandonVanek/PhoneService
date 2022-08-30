using System.Text.Json.Serialization;

namespace PhoneService.Models
{
    public class UserPlan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int PlanId { get; set; }
        [JsonIgnore]
        public Plan Plan { get; set; }
    }
}
