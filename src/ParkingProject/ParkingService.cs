using System;
using System.Linq;

namespace ParkingProject
{
    public class ParkingService
    {
        private ParkingArea _parkingArea;
        public ParkingService(ParkingArea parkingArea)
        {
            _parkingArea = parkingArea;
        }
        /// <summary>
        /// Returns index when the vacant place was found.
        /// Return 0 when did not.
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public int PlaceCar(Car car)
        {
            int parkingIndex;
            if (car.CarType == Type.Passenger)
            {
                parkingIndex = GetFirstVacantForPassengerCar();
                PlaceCarByIndex(parkingIndex, car);
                return parkingIndex;
            }
            if (car.CarType == Type.Lorry)
            {
                parkingIndex = GetFirstVacantForLorryCar();
                PlaceCarByIndex(parkingIndex, car);
                return parkingIndex;
            }
            return 0;

        }
        public int GetFirstVacantForPassengerCar()
        {
            var result = _parkingArea.AreaIndexing.FirstOrDefault(x => x.Value == null).Key;
            if (result == null) throw new NoParkingPlacesException("No vacant places at all");
            return result.ParkingIndex;
        }
        public int GetFirstVacantForLorryCar()
        {
            var result = _parkingArea.AreaIndexing.FirstOrDefault(x => x.Value == null && x.Key.ParkingType == Type.Lorry).Key;
            if (result == null) throw new NoParkingPlacesException("No vacant places for lorry cars");
            return result.ParkingIndex;
        }
        //Here I assume that parking place with a given index is vacant
        public void PlaceCarByIndex(int index, Car car)
        {
            var carPlaceKey = _parkingArea.AreaIndexing.First(x => x.Key.ParkingIndex == index).Key;
            _parkingArea.AreaIndexing[carPlaceKey] = car;
        }
        //Here I assume that car is on the parking area
        public void LeaveArea(Car car)
        {
            var carPlaceKey = _parkingArea.AreaIndexing.First(x => x.Value == car).Key;
            _parkingArea.AreaIndexing[carPlaceKey] = null;
        }
        public void LeaveAreaByIndex(int index)
        {
            var carPlaceKey = _parkingArea.AreaIndexing.First(x => x.Key.ParkingIndex == index).Key;
            _parkingArea.AreaIndexing[carPlaceKey] = null;
        }
        public ParkingArea GetParkingArea()
        {
            return _parkingArea;
        }
    }
}
