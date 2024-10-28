using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.attack
{
    public class AttackItem : WorldObject
    {
        public int Hit { get; set; }
        public int Range { get; set; }

        // Konstruktør
        public AttackItem(int x, int y, string name, bool lootable, bool removeable)
            : base(x, y, name, lootable, removeable) // Kalder basiskonstruktøren
        {
            Hit = 0; // Standardværdi for Hit
            Range = 0; // Standardværdi for Range
        }

        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(Hit)}={Hit}, {nameof(Range)}={Range}, " +
                   $"{nameof(PositionX)}={PositionX}, {nameof(PositionY)}={PositionY}}}";
        }
    }
}
