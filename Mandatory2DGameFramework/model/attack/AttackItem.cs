using Mandatory2DGameFramework.worlds;
using System.Xml.Linq;

namespace Mandatory2DGameFramework.model.attack
{
    public class AttackItem : WorldObject, IAttackItem
    {
        public int Hit { get; set; }
        public int Range { get; set; }

        public AttackItem(int x, int y, string name, bool lootable, bool removeable, int hit, int range)
            : base(x, y, name, lootable, removeable)
        {
            Hit = hit;
            Range = range;
        }
        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(Hit)}={Hit}, {nameof(Range)}={Range}, " +
                   $"{nameof(PositionX)}={PositionX}, {nameof(PositionY)}={PositionY}}}";
        }
    }
}

