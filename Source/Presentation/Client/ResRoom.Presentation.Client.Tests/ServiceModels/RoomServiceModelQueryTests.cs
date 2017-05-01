using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using ResRoom.Presentation.Client.ServiceModels;
using ResRoom.Application.Rooms.RoomManagement.Contract;
using ResRoom.Application.Rooms.RoomManagement.Contract.Objects;

namespace ResRoom.Presentation.Client.Tests.ServiceModels
{
    public class RoomServiceModelQueryTests
    {
        public class OnGetRoom_
        {
            // 1. Make service model query return rooms.
            [Fact]
            public void ShouldReturnRooms()
            {
                using (var builder = new RoomServiceModelQuerySUTBuilder())
                {
                    RoomServiceModelQuery sut
                        = builder
                            .UsingMocks()
                            .WithRooms(3);

                    sut.GetRooms().Count()
                        .Should()
                        .Be(3);
                }
            }
        }
    }

    public class RoomServiceModelQuerySUTBuilder : IDisposable
    {
        private IRoomManagementServiceQuery _serviceQuery;
        private RoomServiceModelQuery _instance;

        private List<RoomDTO> _roomDTOs;

        public RoomServiceModelQuerySUTBuilder()
        {

        }

        public RoomServiceModelQuerySUTBuilder UsingMocks()
        {
            var mockedServiceQuery = new Mock<IRoomManagementServiceQuery>();
            _serviceQuery = mockedServiceQuery.Object;

            return this;
        }

        public RoomServiceModelQuerySUTBuilder WithRooms(int roomCount)
        {
            var mockedServiceQuery = Mock.Get(_serviceQuery);

            _roomDTOs = createRoomDTOs(roomCount);

            mockedServiceQuery
                .Setup(m => m.GetRooms())
                .Returns(_roomDTOs);

            return this;
        }

        public static implicit operator RoomServiceModelQuery(RoomServiceModelQuerySUTBuilder @object)
        {
            return @object.Build();
        }

        public RoomServiceModelQuery Build()
        {
            return new RoomServiceModelQuery(_serviceQuery);
        }

        public void Dispose()
        {
            // Do Some Disposal here
        }

        private List<RoomDTO> createRoomDTOs(int roomCount)
        {
            List<RoomDTO> roomDTOs = new List<RoomDTO>();

            for (int i = 0; i < roomCount; i++)
            {
                roomDTOs.Add(
                    new RoomDTO()
                    {
                        Id = i,
                        Name = $"Room {i}",
                        Capacity = i
                    });
            }

            return roomDTOs;
        }

    }
}
