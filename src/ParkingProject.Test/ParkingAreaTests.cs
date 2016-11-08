using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ParkingProject.Test
{
    public class ParkingAreaTests
    {
        [Fact]
        public void CanBeInstantiatedCorrectlyWithTwoPassPlacesAndTwoLorryPlaces()
        {
            var area = new ParkingArea(2, 2);
            Assert.Equal(4, area.AreaIndexing.Count());
            Assert.Equal(2, area.AreaIndexing.Where(x => x.Key.ParkingType == Type.Lorry).Count());
            Assert.Equal(2, area.AreaIndexing.Where(x => x.Key.ParkingType == Type.Passenger).Count());
        }
        [Fact]
        public void CanBeInstantiatedCorrectlyWithThreePassPlacesAndTwoLorryPlaces()
        {
            var area = new ParkingArea(3, 2);
            Assert.Equal(5, area.AreaIndexing.Count());
            Assert.Equal(3, area.AreaIndexing.Where(x => x.Key.ParkingType == Type.Lorry).Count());
            Assert.Equal(2, area.AreaIndexing.Where(x => x.Key.ParkingType == Type.Passenger).Count());
        }
    }
}
