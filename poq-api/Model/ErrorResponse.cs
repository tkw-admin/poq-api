namespace poq_api.Model
{
    public class ErrorResponse
    {
        public int Status { get; set; } = 500;

        public string Message { get; set; }
    }
}
