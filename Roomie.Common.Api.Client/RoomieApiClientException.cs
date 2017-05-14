using Roomie.Common.Api.Models;
using System;

namespace Roomie.Common.Api.Client
{
    public class RoomieApiClientException : Exception
    {
        public Error Error { get; private set; }

        public RoomieApiClientException(Error error)
            : base(error.Message)
        {
            Error = error;
        }
    }
}
