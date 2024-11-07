using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.Cretures
{
    public class DeadState : ICreatureState
    {
        public void Hit(Creature creature, Creature target)
        {
            Console.WriteLine($"{creature.Name} is dead and cannot attack.");
        }

        public void ReceiveHit(Creature creature, int damage)
        {
            Console.WriteLine($"{creature.Name} is already dead.");
        }

        public void Move(Creature creature, int dx, int dy, World world)
        {
            Console.WriteLine($"{creature.Name} is dead and cannot move.");
        }

        public void Loot(Creature creature, WorldObject obj)
        {
            Console.WriteLine($"{creature.Name} is dead and cannot loot.");
        }
    }

}
