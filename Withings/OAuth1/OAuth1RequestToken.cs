using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Withings.API.Portable.OAuth1
{
    public class OAuth1RequestToken
    {
        [JsonProperty("request_token")]
        public string Token { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; } // "Bearer" is expected

        [JsonProperty("scope")]
        public string Scope { get; set; }

        //[JsonProperty("expires_in")]
        //public int ExpiresIn { get; set; } //maybe convert this to a DateTime ?

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// This property is NOT set by the library. It is simply provided as a covenience placeholder. The library consumer is responsible for setting up this field.
        /// The library assums this DateTime is UTC for token validation purposes.
        /// </summary>
        public DateTime UtcExpirationDate { get; set; }

        public bool IsFresh()
        {
            if (DateTime.MinValue == UtcExpirationDate)
                throw new InvalidOperationException(
                    $"The {nameof(UtcExpirationDate)} property needs to be set before using this method.");
            return DateTime.Compare(DateTime.UtcNow, UtcExpirationDate) < 0;
        }
    }
}
