namespace Order_Management.src.common
{
    public class ConflictException: Exception
    {
        public int StatusCode { get; private set; }

        public ConflictException(string message) : base(message)
        {
            StatusCode = 409;
        }
    }
}
