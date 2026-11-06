namespace Storefront.UI.Classes
{
    //TODO: I was too lazy to separate this out from my package, eventually I will have a package that contains the classes/interfaces that should be exposed separate from the CRUD functionality.
    public class Response
    {
        public Response() { }
        public Response(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public bool IsSuccess => !ErrorMessages.Any();
        public string ErrorMessage => string.Join("; ", ErrorMessages);
    }
    public class Response<TResult> : Response
    {
        public Response() { }
        public Response(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }
        public Response(TResult result)
        {
            Result = result;
        }
        public TResult? Result { get; set; }
    }

}
