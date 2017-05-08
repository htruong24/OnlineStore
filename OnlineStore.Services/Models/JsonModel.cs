namespace OnlineStore.Services.Models
{
    public class JsonModel<T>
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Result { get; set; }

    }
}
