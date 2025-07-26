namespace CarSaleManage.Models.Services.Communication
{
    public class ServiceResult<T>
    {
        public bool Success { get; protected set; }
        public T? Data { get; protected set; }
        public string? Error { get; protected set; }

        public static ServiceResult<T> Ok(T data) => new() { Success = true, Data = data };
        public static ServiceResult<T> Fail(string error) => new() { Success = false, Error = error };
    }
}
