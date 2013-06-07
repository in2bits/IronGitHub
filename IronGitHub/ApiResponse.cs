using System.Net;

namespace IronGitHub
{
    public class ApiResponse<T> : ApiResponse
    {
        public T Result { get; set; }
    }

    public class ApiResponse
    {
        public HttpWebResponse HttpResponse { get; set; }
    }
}