using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.Cretures
{
    public interface ICreatureState
    {
        void Hit(Creature creature, Creature target);
        void ReceiveHit(Creature creature, int hit);
        void Move(Creature creature, int dx, int dy, World world);
        void Loot(Creature creature, WorldObject obj);
    }

}
