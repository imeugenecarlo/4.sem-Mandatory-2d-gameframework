using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.attack
{
    /// <summary>
    /// Interface for attack items.
    /// </summary>
    public interface IAttackItem
    {
        /// <summary>
        /// Gets or sets the name of the attack item.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the hit value of the attack item.
        /// </summary>
        int Hit { get; set; }

        /// <summary>
        /// Gets or sets the range of the attack item.
        /// </summary>
        int Range { get; set; }
    }
}
