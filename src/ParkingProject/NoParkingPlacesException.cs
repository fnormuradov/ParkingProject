using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingProject
{
    public class NoParkingPlacesException : Exception
    {
        public NoParkingPlacesException()
        {
        }

        public NoParkingPlacesException(string message)
        : base(message)
        {
        }

        public NoParkingPlacesException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
