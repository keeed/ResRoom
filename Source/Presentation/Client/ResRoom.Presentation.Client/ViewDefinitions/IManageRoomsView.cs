using ResRoom.Presentation.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResRoom.Presentation.Client.ViewDefinitions
{
    public interface IManageRoomsView
    {
        Rooms Rooms { get; set; }

        Room SelectedRoom { get; }
    }
}
