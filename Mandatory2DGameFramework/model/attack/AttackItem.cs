using Mandatory2DGameFramework.worlds;
using System.Xml.Linq;

namespace Mandatory2DGameFramework.model.attack
{
    /// <summary>
    /// Represents an attack item in the game.
    /// </summary>
    public class AttackItem : WorldObject, IAttackItem
    {
        /// <summary>
        /// Gets or sets the hit value of the attack item.
        /// </summary>
        public int Hit { get; set; }

        /// <summary>
        /// Gets or sets the range of the attack item.
        /// </summary>
        public int Range { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttackItem"/> class.
        /// </summary>
        /// <param name="x">The X position of the attack item.</param>
        /// <param name="y">The Y position of the attack item.</param>
        /// <param name="name">The name of the attack item.</param>
        /// <param name="lootable">A value indicating whether the item is lootable.</param>
        /// <param name="removeable">A value indicating whether the item is removable.</param>
        /// <param name="hit">The hit value of the attack item.</param>
        /// <param name="range">The range of the attack item.</param>
        public AttackItem(int x, int y, string name, bool lootable, bool removeable, int hit, int range)
            : base(x, y, name, lootable, removeable)
        {
            Hit = hit;
            Range = range;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(Hit)}={Hit}, {nameof(Range)}={Range}, " +
                   $"{nameof(PositionX)}={PositionX}, {nameof(PositionY)}={PositionY}}}";
        }
    }
}

