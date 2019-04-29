using Newtonsoft.Json;

namespace BlazorClientSideRealWorld.Models
{
    public class ErrorsModel
    {

        public string[] Username { get; set; }
        public string[] Email { get; set; }
        public string[] Password { get; set; }
        public string[] Title { get; set; }
        public string[] Body { get; set; }
        public string[] Description { get; set; }

        [JsonProperty(PropertyName = "email or password")]
        public string[] EmailOrPassword { get; set; }

        public string[] Authentication { get; set; }

        public string[] Authorization { get; set; }

        public string[] Other { get; set; }
    }
}