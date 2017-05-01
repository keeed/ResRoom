using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResRoom.Presentation.Client.Models
{
    public class Room
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Room)){
                return false;
            }

            Room other = (Room)obj;

            if (Id.Equals(other.Id) &&
                Name.Equals(other.Name) &&
                Capacity.Equals(other.Capacity))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // Use this if field is nullable.
                //int result = (Id != null ? Id.GetHashCode() : 0);
                int result = Id.GetHashCode();
                result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                result = (result * 397) ^ (Capacity.GetHashCode());
                return result;
            }
        }
    }
}
