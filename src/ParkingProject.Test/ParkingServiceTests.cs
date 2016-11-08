using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ParkingProject.Test
{
    public class ParkingServiceTests
    {
        private ParkingService _parkingService;
        public ParkingServiceTests()
        {
            var parkingArea = new ParkingArea(2, 2);
            _parkingService = new ParkingService(parkingArea);
        }
        [Fact]
        public void CanLoadOneCarPassenger()
        {
            var car = new Car { CarId = 1, CarType = Type.Passenger };
            _parkingService.PlaceCar(car);
            var parkingArea = _parkingService.GetParkingArea();
            Assert.Equal(1, parkingArea.AreaIndexing.ElementAt(0).Value.CarId);
        }
        [Fact]
        public void CanLoadOneCarLorry()
        {
            var car = new Car { CarId = 3, CarType = Type.Lorry };
            _parkingService.PlaceCar(car);
            var parkingArea = _parkingService.GetParkingArea();
            Assert.Equal(3, parkingArea.AreaIndexing.ElementAt(0).Value.CarId);
        }
        [Fact]
        public void CanUnloadOneCarPassenger()
        {
            var car_1 = new Car { CarId = 1, CarType = Type.Passenger };
            var car_2 = new Car { CarId = 2, CarType = Type.Passenger };

            _parkingService.PlaceCar(car_1);
            _parkingService.PlaceCar(car_2);
            _parkingService.LeaveArea(car_1);
            var parkingArea = _parkingService.GetParkingArea();
            Assert.Equal(2, parkingArea.AreaIndexing.ElementAt(1).Value.CarId);
            Assert.Null(parkingArea.AreaIndexing.ElementAt(0).Value);
        }
        [Fact]
        public void ThrowsExceptionWhenNoPlaceForLorryOccupiedByPass()
        {
            //Take all places for lorries
            var car_1 = new Car { CarId = 1, CarType = Type.Passenger };
            var car_2 = new Car { CarId = 2, CarType = Type.Passenger };

            _parkingService.PlaceCar(car_1);
            _parkingService.PlaceCar(car_2);

            var car_3 = new Car { CarId = 3, CarType = Type.Lorry };
            Exception ex = Record.Exception((() => { _parkingService.PlaceCar(car_3); }));
            Assert.IsType(typeof(NoParkingPlacesException), ex);
            Assert.Equal("No vacant places for lorry cars", ex.Message);
        }
        [Fact]
        public void ThrowsExceptionWhenNoPlaceForLorryOccupiedByLorries()
        {
            //Take all places for lorries
            var car_1 = new Car { CarId = 1, CarType = Type.Lorry };
            var car_2 = new Car { CarId = 2, CarType = Type.Lorry };

            _parkingService.PlaceCar(car_1);
            _parkingService.PlaceCar(car_2);

            var car_3 = new Car { CarId = 3, CarType = Type.Lorry };
            Exception ex = Record.Exception((() => { _parkingService.PlaceCar(car_3); }));
            Assert.IsType(typeof(NoParkingPlacesException), ex);
            Assert.Equal("No vacant places for lorry cars", ex.Message);
        }
        [Fact]
        public void ThrowsExceptionWhenNoPlaceAtAll()
        {
            //Take all places for lorries
            var car_1 = new Car { CarId = 1, CarType = Type.Passenger };
            var car_2 = new Car { CarId = 2, CarType = Type.Passenger };
            var car_3 = new Car { CarId = 3, CarType = Type.Passenger };
            var car_4 = new Car { CarId = 4, CarType = Type.Passenger };
            var car_5 = new Car { CarId = 5, CarType = Type.Passenger };


            _parkingService.PlaceCar(car_1);
            _parkingService.PlaceCar(car_2);
            _parkingService.PlaceCar(car_3);
            _parkingService.PlaceCar(car_4);


            Exception ex = Record.Exception((() => { _parkingService.PlaceCar(car_5); }));
            Assert.IsType(typeof(NoParkingPlacesException), ex);
            Assert.Equal("No vacant places at all", ex.Message);
        }
        //More or less integration test
        [Fact]
        public void CanLoadAndUnloadAllCars()
        {
            //Take all places for lorries
            var car_1 = new Car { CarId = 1, CarType = Type.Passenger };
            var car_2 = new Car { CarId = 2, CarType = Type.Passenger };
            var car_3 = new Car { CarId = 3, CarType = Type.Passenger };
            var car_4 = new Car { CarId = 4, CarType = Type.Passenger };
            var car_5 = new Car { CarId = 5, CarType = Type.Passenger };


            var index_1 = _parkingService.PlaceCar(car_1);
            var index_2 = _parkingService.PlaceCar(car_2);
            var index_3 = _parkingService.PlaceCar(car_3);
            var index_4 = _parkingService.PlaceCar(car_4);

            Assert.Equal(4, _parkingService.GetParkingArea().AreaIndexing.Where(x => x.Value != null).Count());

            _parkingService.LeaveAreaByIndex(index_1);
            _parkingService.LeaveAreaByIndex(index_2);
            _parkingService.LeaveAreaByIndex(index_3);
            _parkingService.LeaveAreaByIndex(index_4);

            Assert.Equal(0, _parkingService.GetParkingArea().AreaIndexing.Where(x => x.Value != null).Count());
        }
    }
}
