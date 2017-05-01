using ResRoom.Application.Rooms.RoomManagement.Contract.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResRoom.Application.Rooms.RoomManagement.Contract
{
    public interface IRoomManagementServiceQuery
    {
        List<RoomDTO> GetRooms();
    }
}
