using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SiteWebAssembly.Model
{
    public class Contact
    {
        [Required(AllowEmptyStrings = false)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
