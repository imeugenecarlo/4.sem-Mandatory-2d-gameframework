using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.defence
{
    public class DefenceItem : WorldObject
    {
        public int ReduceHitPoint { get; set; }

        // Konstruktør
        public DefenceItem(int x, int y, string name, bool lootable, bool removeable)
            : base(x, y, name, lootable, removeable) // Kalder basiskonstruktøren
        {
            ReduceHitPoint = 0; // Standardværdi for ReduceHitPoint
        }

        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(ReduceHitPoint)}={ReduceHitPoint}, " +
                   $"{nameof(PositionX)}={PositionX}, {nameof(PositionY)}={PositionY}}}";
        }
    }

}
