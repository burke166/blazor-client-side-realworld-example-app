using Newtonsoft.Json;

namespace BlazorClientSideRealWorld.Models
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public string Token { get; set; }

        public UserModel Clone()
        {
            return new UserModel
            {
                // token will not be cloned.

                Email = Email,
                Username = Username,
                Bio = Bio,
                Image = Image
            };
        }
    }
    public class UserCredentials
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
