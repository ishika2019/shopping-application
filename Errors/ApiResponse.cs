namespace project.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statuscode , string message=null)
        {
            StatusCode = statuscode;
            Message = message ?? getDefaultMessageForStatusCode(statuscode);
        }

        private string getDefaultMessageForStatusCode(int statuscode)
        {
            return statuscode switch
            {

                400=>"A bad request, you have made",
                401=>"Authorized,you are not",
                404=>"Resource found it was not",
                500=>"Errors are path to dark side",
                _=>null
            };
         }
    

        public int StatusCode { get; set; }
        public string Message { get; set; }
             
    }
}
