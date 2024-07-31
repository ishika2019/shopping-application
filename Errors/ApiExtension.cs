namespace project.Errors
{
    public class ApiExtension : ApiResponse
    {
        public ApiExtension(int statuscode, string message =null, string details =null) : base(statuscode, message)
        {
            Details = details;
        }

        public string Details { get; set; }


    }
}
