using System.Runtime.Serialization;

namespace Common.Exceptions;

[Serializable]
public class APIResponseException : Exception, ISerializable
{
    public int StatusCode { get; }

    public APIResponseException() { }

    public APIResponseException(string message): base(message) { }

    public APIResponseException(string message, int statusCode): base(message) 
    {
        StatusCode = statusCode;
    }
}
