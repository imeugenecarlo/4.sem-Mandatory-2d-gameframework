using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.attack
{
    public interface IAttackItem
    {
        string Name { get; set; }
        int Hit { get; set; }
        int Range { get; set; }
    }
}
