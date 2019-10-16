namespace Brilliance.Models
{
    public class ServiceResponse
    {
        public ServiceResponse()
        {
            IsSuccess = false;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int ErrorCode { get; set; }
    }
}