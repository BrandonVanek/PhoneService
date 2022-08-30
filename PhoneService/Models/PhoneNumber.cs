using System.Text.Json.Serialization;
namespace PhoneService.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
