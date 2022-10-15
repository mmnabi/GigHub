namespace GigHub.Core.Resources
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
            Errors = new List<string>();
        }

        public BaseResponse(List<string> errors)
        {
            Success = false;
            Errors = errors;
        }

        public bool Success { get; private set; }
        public List<string> Errors { get; private set; }
        public dynamic? Result { get; set; }

        public void AddError(string error)
        {
            if (Success) Success = false;
            Errors.Add(error);
        }
    }
}