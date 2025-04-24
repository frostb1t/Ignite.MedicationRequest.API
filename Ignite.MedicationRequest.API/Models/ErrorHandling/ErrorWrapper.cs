namespace Ignite.MedicationRequest.API.Models.ErrorHandling
{
    public class ErrorWrapper<T>
    {
        public Status Status { get; set; } = Status.Success;
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }

        public ErrorWrapper()
        {
        }

        public ErrorWrapper(T data)
        {
            this.Data = data;
        }
    }
}
