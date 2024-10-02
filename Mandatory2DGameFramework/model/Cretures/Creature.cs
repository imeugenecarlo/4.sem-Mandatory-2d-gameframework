using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.model.Cretures
{
    public class Creature
    {
        public string Name { get; set; }
        public int HitPoint { get; set; }


        // Todo consider how many attack / defence weapons are allowed
        public List<AttackItem?> Attack { get; set; }
        public List<DefenceItem?> Defence { get; set; }

        public Creature()
        {
            Name = string.Empty;
            HitPoint = 100;

            Attack = null;
            Defence = null;

            Attack = new List<AttackItem?>(5);
            Defence = new List<DefenceItem?>(5);

        }

        public int Hit(Creature target)
        {
            int damage = 5;

            target.ReceiveHit(damage);
            return damage;
        }

        public void ReceiveHit(int hit)
        {
            int totalDamageReduction = 0;

            foreach (var item in Defence)
            {
                if (item != null)
                {
                    totalDamageReduction += item.ReduceHitPoint;
                }
            }
            hit -= totalDamageReduction;
            HitPoint -= hit;

            if (HitPoint <= 0)
            {
                Console.WriteLine("Creature is dead");
            }
        }

        public void Loot(WorldObject obj)
        {
            if (obj is AttackItem)
            {
                if (Attack.Count < 5)
                {
                    Attack.Add(obj as AttackItem);
                }
                else
                {
                    Console.WriteLine("Cannot carry more than 5 attack items");
                }

            }
            else if (obj is DefenceItem)
            {

                if (Defence.Count < 5)
                {
                    Defence.Add(obj as DefenceItem);
                }
                else
                {
                    Console.WriteLine("Cannot carry more than 5 defence items");
                }
            }
        }

        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(HitPoint)}={HitPoint.ToString()}, {nameof(Attack)}={Attack}, {nameof(Defence)}={Defence}}}";
        }
    }
}
