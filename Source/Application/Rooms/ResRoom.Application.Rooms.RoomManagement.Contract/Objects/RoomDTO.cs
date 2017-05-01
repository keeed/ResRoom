using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResRoom.Application.Rooms.RoomManagement.Contract.Objects
{
    public class RoomDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}
