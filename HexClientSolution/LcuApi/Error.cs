using Newtonsoft.Json;

namespace LcuApi
{
    public class Error
    {
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("httpStatus")]
        public int HttpStatus { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
