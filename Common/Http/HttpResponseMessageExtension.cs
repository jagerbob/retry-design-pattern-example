using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Http
{
    public static class HttpResponseMessageExtension
    {
        public static void EnsureAPISuccess(this HttpResponseMessage response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new APIResponseException("Invalid API response", (int)response.StatusCode);
        }
    }
}
