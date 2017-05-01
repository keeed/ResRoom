using ResRoom.Application.Rooms.RoomManagement.Contract.Objects;
using ResRoom.Presentation.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResRoom.Presentation.Client.Translators
{
    public static class RoomTranslator
    {
        public static RoomDTO ToDTO(Room room)
        {
            RoomDTO roomDTO = new RoomDTO();

            roomDTO.Id = room.Id;
            roomDTO.Name = room.Name;
            roomDTO.Capacity = room.Capacity;

            return roomDTO;
        }

        public static Room FromDTO(RoomDTO roomDTO)
        {
            Room room = new Room();

            room.Id = roomDTO.Id;
            room.Name = roomDTO.Name;
            room.Capacity = roomDTO.Capacity;

            return room;
        }
    }
}
