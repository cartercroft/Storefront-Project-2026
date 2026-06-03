namespace Storefront.API.Classes
{
    public class Response<TResult>
    {
        public Response(){}
        public Response(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }
        public Response(TResult result)
        {
            Result = result;
        }
        public TResult? Result { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public bool IsSuccess => !ErrorMessages.Any();
        public string ErrorMessage => string.Join("; ", ErrorMessages);
    }
}
