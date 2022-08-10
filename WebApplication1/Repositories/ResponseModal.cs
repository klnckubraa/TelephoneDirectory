namespace WebApplication1.Repositories
{
    public class ResponseModal<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public List<string> Results { get; set; }



        public static ResponseModal<T> Success(T data, string message, bool status)
        {
            return new ResponseModal<T> { Data = data, Message = message, Status = status };
        }

        public static ResponseModal<T> Errors(T data, string message, bool status)
        {
            return new ResponseModal<T> { Data = data, Message = message, Status = status };
        }
        public static ResponseModal<T> Error(List<string> results, string message, bool status)
        {
            return new ResponseModal<T> { Results = results, Message = message, Status = status };
        }
    }
}
