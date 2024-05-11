using System.Net;

namespace Infrastructure.Helper
{
    public class BodyResponse
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
