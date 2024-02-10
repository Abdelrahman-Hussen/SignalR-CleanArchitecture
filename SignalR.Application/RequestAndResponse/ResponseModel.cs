namespace LurkingUnits.Application
{
    public class ResponseModel<T>
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ResponseModel<T> Success(T? data = default(T), string? message = null)
        {
            return new ResponseModel<T>
            {
                Data = data,
                Ok = true,
                Message = message ?? SignalR.Application.Features.System.Resources.Message.Success_General,
            };
        }

        public static ResponseModel<T> Error(string message = null, T? data = default(T))
        {
            return new ResponseModel<T>
            {
                Data = data,
                Ok = false,
                Message = message ?? SignalR.Application.Features.System.Resources.Message.Error_General
            };
        }
    }
}
