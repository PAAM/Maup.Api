using Maup.Core.Entities;

namespace Maup.Api.Responses
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public ApiResponse(T data)
        {
            Data = data;
        }

        public Metadata Meta { get; set; }

    }
}
