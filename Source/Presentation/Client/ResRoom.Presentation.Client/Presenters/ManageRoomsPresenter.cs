using ResRoom.Presentation.Client.Models;
using ResRoom.Presentation.Client.ServiceModels;
using ResRoom.Presentation.Client.ViewDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResRoom.Presentation.Client.Presenters
{
    public class ManageRoomsPresenter
    {
        private IManageRoomsView _manageRoomsView;
        private IRoomServiceModelQuery _roomServiceModelQuery;
        private IRoomServiceModelCommand _roomServiceModelCommand;
        private Rooms _rooms;

        public IManageRoomsView ManageRoomsView
        {
            get { return _manageRoomsView; }
            set { _manageRoomsView = value; }
        }

        public IRoomServiceModelQuery RoomServiceModelQuery
        {
            get { return _roomServiceModelQuery; }
            set { _roomServiceModelQuery = value; }
        }

        public IRoomServiceModelCommand RoomServiceModelCommand
        {
            get { return _roomServiceModelCommand; }
            set { _roomServiceModelCommand = value; }
        }

        public Rooms Rooms
        {
            get { return _rooms; }
            set { _rooms = value; }
        }

        public ManageRoomsPresenter(
            IManageRoomsView manageRoomsView,
            IRoomServiceModelQuery roomServiceModelQuery,
            IRoomServiceModelCommand roomServiceModelCommand)
        {
            if (manageRoomsView == null)
            {
                throw new ArgumentNullException(nameof(manageRoomsView));
            }

            _manageRoomsView = manageRoomsView;

            if (roomServiceModelQuery == null)
            {
                throw new ArgumentNullException(nameof(roomServiceModelQuery));
            }

            _roomServiceModelQuery = roomServiceModelQuery;

            if (roomServiceModelCommand == null)
            {
                throw new ArgumentNullException(nameof(roomServiceModelCommand));
            }

            _roomServiceModelCommand = roomServiceModelCommand;
        }

        public void Initialize()
        {
            Rooms rooms = RoomServiceModelQuery.GetRooms();

            ManageRoomsView.Rooms = rooms;
        }

        public void DeleteRoom()
        {
            Room selectedRoom = ManageRoomsView.SelectedRoom;

            RoomServiceModelCommand.DeleteRoom(selectedRoom);
        }
    }
}
