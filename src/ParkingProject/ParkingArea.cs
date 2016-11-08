using System.Collections.Generic;
namespace ParkingProject
{
    public class ParkingArea
    {
        public Dictionary<CarPlace, Car> AreaIndexing;
        public ParkingArea(int numOfLorryTypePlaces, int numOfPassengerTypePlaces)
        {
            AreaIndexing = new Dictionary<CarPlace, Car>();
            for (var i = 1; i <= numOfPassengerTypePlaces+numOfLorryTypePlaces; i++)
            {
                Type parkingType;
                if (i <= numOfLorryTypePlaces) parkingType = Type.Lorry;
                else parkingType = Type.Passenger;
                AreaIndexing.Add(new CarPlace
                {
                    ParkingIndex = i,
                    ParkingType = parkingType
                }, null);
            }
        }
    }

}

