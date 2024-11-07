using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mandatory2DGameFramework.model.Cretures
{
    public class AliveState : ICreatureState
    {
        public void Hit(Creature creature, Creature target)
        {
            creature.DefaultHit(target);
        }

        public void ReceiveHit(Creature creature, int hit)
        {
            creature.DefaultReceiveHit(hit);
        }

        public void Move(Creature creature, int dx, int dy, World world)
        {
            creature.DefaultMove(dx, dy, world);
        }

        public void Loot(Creature creature, WorldObject obj)
        {
            creature.DefaultLoot(obj);
        }
    }


}
