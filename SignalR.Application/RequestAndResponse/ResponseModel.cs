namespace LurkingUnits.Application
{
    public class ResponseModel<T>
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ResponseModel<T> Success(T? data = default(T), string? message = "success")
        {
            return new ResponseModel<T>
            {
                Data = data,
                Ok = true,
                Message = message,
            };
        }

        public static ResponseModel<T> Error(string message = "an error occured",
                                            T? data = default(T))
        {
            return new ResponseModel<T>
            {
                Data = data,
                Ok = false,
                Message = message
            };
        }
    }
}
