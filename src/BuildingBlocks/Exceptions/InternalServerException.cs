using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    internal class InternalServerException : Exception
    {
        public InternalServerException(string message) : base(message)
        {
            
        }

        public InternalServerException(string message, string details)
        {
            Details = details;
        }
        public string? Details { get; }
    }
}
