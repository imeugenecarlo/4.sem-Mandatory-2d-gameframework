using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.defence
{
    public interface IDefenceItem
    {
        string Name { get; set; }
        int ReduceHitPoint { get; set; }
    }
}
