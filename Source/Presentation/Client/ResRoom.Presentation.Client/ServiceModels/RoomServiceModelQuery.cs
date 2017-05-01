using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResRoom.Presentation.Client.Models;
using ResRoom.Application.Rooms.RoomManagement.Contract;
using ResRoom.Presentation.Client.Translators;

namespace ResRoom.Presentation.Client.ServiceModels
{
    public class RoomServiceModelQuery : IRoomServiceModelQuery
    {
        private readonly IRoomManagementServiceQuery _roomManagementServiceQuery;

        public RoomServiceModelQuery(IRoomManagementServiceQuery roomManagementServiceQuery)
        {
            if (roomManagementServiceQuery == null)
            {
                throw new ArgumentNullException(nameof(roomManagementServiceQuery));
            }

            _roomManagementServiceQuery = roomManagementServiceQuery;
        }

        public Rooms GetRooms()
        {
            Rooms rooms = new Rooms();

            var roomsDTO = _roomManagementServiceQuery.GetRooms();

            foreach (var roomDTO in roomsDTO)
            {
                rooms.AddRoom(RoomTranslator.FromDTO(roomDTO));
            }

            return rooms;
        }
    }
}
