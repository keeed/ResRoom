using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResRoom.Presentation.Client.Models
{
    public class Rooms : IEnumerable<Room>
    {
        private List<Room> _rooms = new List<Room>();

        public Room GetRoom(int index)
        {
            return _rooms[index];
        }

        public void RemoveRoom(Room room)
        {
            _rooms.Remove(room);
        }

        public void AddRoom(Room room)
        {
            _rooms.Add(room);
        }

        public IEnumerator<Room> GetEnumerator()
        {
            return _rooms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
