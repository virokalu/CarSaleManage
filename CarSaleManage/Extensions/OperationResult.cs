namespace CarSaleManage.Extensions
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
    public class ServiceResult<T> : OperationResult
    {
        public T? Data { get; set; }
    }
}
