using ResRoom.Presentation.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResRoom.Presentation.Client.ServiceModels
{
    public interface IRoomServiceModelQuery
    {
        Rooms GetRooms();
    }
}
