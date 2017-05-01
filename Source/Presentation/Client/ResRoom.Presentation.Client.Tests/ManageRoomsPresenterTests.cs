using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using ResRoom.Presentation.Client.Presenters;
using ResRoom.Presentation.Client.ViewDefinitions;
using ResRoom.Presentation.Client.ServiceModels;
using ResRoom.Presentation.Client.Models;

namespace ResRoom.Presentation.Client.Tests
{
    public class ManageRoomsPresenterTests
    {

        public class OnInitialize_
        {
            // 1. Load Rooms on initialize
            [Fact]
            public void ShouldLoadRooms()
            {
                using (var builder = new ManageRoomsPresenterSUTBuilder())
                {
                    ManageRoomsPresenter sut =
                        builder
                             .UsingMocks()
                             .WithDefaultValues();

                    sut.Initialize();

                    sut.ManageRoomsView.Rooms
                        .Should()
                        .NotBeNull();
                }
            }

            // 2. No rooms loaded if empty rooms from service model.
            [Fact]
            public void ShouldHaveEmptyRooms_WhenNoRoomsReturned()
            {
                using (var builder = new ManageRoomsPresenterSUTBuilder())
                {
                    ManageRoomsPresenter sut =
                        builder
                            .UsingMocks()
                            .WithDefaultView()
                            .WithEmptyRooms();

                    sut.Initialize();

                    sut.ManageRoomsView.Rooms
                        .Should()
                        .BeEmpty();
                }
            }
        }

        public class OnDelete_
        {
            // 1. Delete selected Room if a room is selected
            
            [Fact]
            public void ShouldDeleteSelectedRoom()
            {
                using (var builder = new ManageRoomsPresenterSUTBuilder())
                {
                    ManageRoomsPresenter sut =
                        builder
                            .UsingMocks()
                            .WithServiceModelCommandDelete()
                            .WithViewHavingRooms(3)
                            .WithViewReturningFirstRoomAsSelectedRoom();

                    var selectedRoom = sut.ManageRoomsView.SelectedRoom;

                    sut.DeleteRoom();

                    sut.ManageRoomsView.Rooms
                        .Should()
                        .NotContain(selectedRoom);
                }
            }

            // 2. No Room deleted if no room is selected
            [Fact]
            public void ShouldNotDeleteRoom_WhenNoSelectedRoom()
            {
                using (var builder = new ManageRoomsPresenterSUTBuilder())
                {
                    ManageRoomsPresenter sut =
                        builder
                            .UsingMocks()
                            .WithServiceModelCommandDelete()
                            .WithViewHavingRooms(3);

                    var selectedRoom = sut.ManageRoomsView.SelectedRoom;

                    sut.DeleteRoom();

                    sut.ManageRoomsView.Rooms.Count()
                        .Should()
                        .Be(3);
                }
            }
        }
    }

    public class ManageRoomsPresenterSUTBuilder : IDisposable
    {
        private IManageRoomsView _view;
        private IRoomServiceModelQuery _serviceModelQuery;
        private IRoomServiceModelCommand _serviceModelCommand;

        private ManageRoomsPresenter _instance;

        private Rooms _rooms;

        public ManageRoomsPresenterSUTBuilder()
        {

        }

        public ManageRoomsPresenterSUTBuilder UsingMocks()
        {
            _view = new Mock<IManageRoomsView>().Object;
            _serviceModelQuery = new Mock<IRoomServiceModelQuery>().Object;
            _serviceModelCommand = new Mock<IRoomServiceModelCommand>().Object;

            _rooms = new Rooms();

            return this;
        }

        public ManageRoomsPresenterSUTBuilder WithDefaultValues()
        {
            WithDefaultView();

            WithRooms(3);

            return this;
        }

        public ManageRoomsPresenterSUTBuilder WithDefaultView()
        {
            var mockedView = Mock.Get(_view);
            mockedView.SetupAllProperties();
            mockedView
                .SetupGet(m => m.Rooms)
                .Returns(new Rooms());

            _view = mockedView.Object;

            return this;
        }

        public ManageRoomsPresenterSUTBuilder WithViewHavingRooms(int roomCount)
        {
            var mockedView = Mock.Get(_view);
            mockedView.SetupAllProperties();

            _rooms = createRooms(roomCount);

            mockedView
                .SetupGet(m => m.Rooms)
                .Returns(_rooms);

            _view = mockedView.Object;

            return this;
        }

        public ManageRoomsPresenterSUTBuilder WithViewReturningFirstRoomAsSelectedRoom()
        {
            var mockedView = Mock.Get(_view);
            mockedView
                .SetupGet(m => m.SelectedRoom)
                .Returns(mockedView.Object.Rooms.GetRoom(0));

            return this;
        }

        public ManageRoomsPresenterSUTBuilder WithEmptyRooms()
        {
            var mockedServiceModel = Mock.Get(_serviceModelQuery);
            mockedServiceModel.SetupAllProperties();

            _rooms = new Rooms();

            mockedServiceModel
                .Setup(m => m.GetRooms())
                .Returns(_rooms);

            _serviceModelQuery = mockedServiceModel.Object;

            return this;
        }

        public ManageRoomsPresenterSUTBuilder WithRooms(int roomCount)
        {
            var mockedServiceModelQuery = Mock.Get(_serviceModelQuery);
            mockedServiceModelQuery.SetupAllProperties();

            _rooms = createRooms(roomCount);

            mockedServiceModelQuery
                .Setup(m => m.GetRooms())
                .Returns(_rooms);

            _serviceModelQuery = mockedServiceModelQuery.Object;

            return this;
        }

        public ManageRoomsPresenterSUTBuilder WithServiceModelCommandDelete()
        {
            var mockedServiceModelCommand = Mock.Get(_serviceModelCommand);
            mockedServiceModelCommand.SetupAllProperties();

            mockedServiceModelCommand
                .Setup(m => m.DeleteRoom(It.IsAny<Room>()))
                .Callback(
                    (Room deletedRoom) =>
                    {
                        _rooms.RemoveRoom(deletedRoom);
                    });

            _serviceModelCommand = mockedServiceModelCommand.Object;

            return this;
        }

        public ManageRoomsPresenter Build()
        {
            if (_instance == null)
            {
                return new ManageRoomsPresenter(
                    _view, 
                    _serviceModelQuery, 
                    _serviceModelCommand);
            }

            UsingMocks();

            return _instance;
        }

        public static implicit operator ManageRoomsPresenter(ManageRoomsPresenterSUTBuilder @object)
        {
            return @object.Build();
        }

        public void Dispose()
        {
            // Disposable Stuff
        }

        private Rooms createRooms(int roomCount)
        {
            Rooms rooms = new Rooms();
            for (int i = 0; i < roomCount; i++)
            {
                rooms.AddRoom(
                    new Room()
                    {
                        Id = i,
                        Name = $"Room {i}",
                        Capacity = i
                    });
            }

            return rooms;
        }
    }
}
