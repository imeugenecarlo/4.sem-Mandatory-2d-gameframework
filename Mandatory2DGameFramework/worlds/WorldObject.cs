using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.worlds
{
    /// <summary>
    /// Represents an object in the game world.
    /// </summary>
    public class WorldObject
    {
        /// <summary>
        /// Gets or sets the name of the world object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the object is lootable.
        /// </summary>
        public bool Lootable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the object is removable.
        /// </summary>
        public bool Removeable { get; set; }

        /// <summary>
        /// Gets or sets the X position of the world object.
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        /// Gets or sets the Y position of the world object.
        /// </summary>
        public int PositionY { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldObject"/> class.
        /// </summary>
        /// <param name="x">The X position of the world object.</param>
        /// <param name="y">The Y position of the world object.</param>
        /// <param name="name">The name of the world object.</param>
        /// <param name="lootable">A value indicating whether the object is lootable.</param>
        /// <param name="removeable">A value indicating whether the object is removable.</param>
        public WorldObject(int x, int y, string name, bool lootable, bool removeable)
        {
            Name = name;
            Lootable = lootable;
            Removeable = removeable;
            PositionX = x;
            PositionY = y;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(Lootable)}={Lootable.ToString()}, {nameof(Removeable)}={Removeable.ToString()}}}" +
                   $"{nameof(PositionX)}={PositionX}, {nameof(PositionY)}={PositionY}}}";
        }
    }
}
