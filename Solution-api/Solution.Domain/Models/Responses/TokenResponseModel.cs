using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Solution.Domain.Models.Responses;

public class TokenResponseModel
{
    [Required]
    [JsonPropertyName("roles")]
    public ICollection<string> Roles { get; set; }

    [Required]
    [JsonPropertyName("token")]
    public string Token { get; set; }

    [Required]
    [JsonPropertyName("tokenExpirationTime")]
    public DateTime TokenExpirationTime { get; set; }
}
