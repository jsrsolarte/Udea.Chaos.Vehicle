namespace Udea.Chaos.Vehicle.Domain.Exceptions
{
    [Serializable]
    public class AppException : Exception
    {
        public AppException()
        { }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, Exception inner) : base(message, inner)
        {
        }

        protected AppException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public object? InnerMessage { get; set; }
    }
}
