namespace OrderNow.WebApp.Models
{
    public class ResponseDTO
    {
        public Object Result { get; set; }

        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; }
    }
}
