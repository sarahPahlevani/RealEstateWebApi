using Newtonsoft.Json;

namespace RealEstateAgency.Shared.BaseModels
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
