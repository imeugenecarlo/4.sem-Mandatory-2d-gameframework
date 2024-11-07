using Mandatory2DGameFramework.worlds;
using System.Xml.Linq;

namespace Mandatory2DGameFramework.model.defence
{
    public class DefenceItem : WorldObject, IDefenceItem
    {
        public int ReduceHitPoint { get; set; }

        public DefenceItem(int x, int y, string name, bool lootable, bool removeable, int reduceHitPoint)
            : base(x, y, name, lootable, removeable)
        {
            ReduceHitPoint = reduceHitPoint;
        }
        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(ReduceHitPoint)}={ReduceHitPoint}, " +
                   $"{nameof(PositionX)}={PositionX}, {nameof(PositionY)}={PositionY}}}";
        }
    }
}

