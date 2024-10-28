using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.worlds
{
    public class WorldObject
    {
        public string Name { get; set; }
        public bool Lootable { get; set; }
        public bool Removeable { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public WorldObject(int x,int y, string name, bool lootable, bool removeable)
        {
            Name = name;
            Lootable = lootable;
            Removeable = removeable;
            PositionX = x;
            PositionY = y;
        }

        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(Lootable)}={Lootable.ToString()}, {nameof(Removeable)}={Removeable.ToString()}}}"+
                $"{nameof(PositionX)}={PositionX}, {nameof(PositionY)}={PositionY}}}"; 
               
        }
    }
}
