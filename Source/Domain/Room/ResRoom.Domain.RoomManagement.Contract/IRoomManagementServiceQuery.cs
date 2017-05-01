using ResRoom.Domain.RoomManagement.Contract.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResRoom.Domain.RoomManagement.Contract
{
    public interface IRoomManagementServiceQuery
    {
        List<RoomDTO> GetRooms();
    }
}
