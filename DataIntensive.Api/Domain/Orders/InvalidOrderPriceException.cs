using System.Runtime.Serialization;

namespace DataIntensive.Api.Domain.Orders
{
    [Serializable]
    public sealed class InvalidOrderPriceException : Exception
    {
        public InvalidOrderPriceException()
        {
        }

        public InvalidOrderPriceException(string message) : base(message)
        {
        }

        private InvalidOrderPriceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}