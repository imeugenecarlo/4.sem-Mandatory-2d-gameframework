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
        public int PositionX { get; set; }
        public int PositionY { get; set; }



        // Todo consider how many attack / defence weapons are allowed
        public List<IAttackItem?> Attack { get; set; }
        public List<IDefenceItem?> Defence { get; set; }

        public Creature(int x, int y)
        {
            PositionX = x;
            PositionY = y;
            Name = string.Empty;
            HitPoint = 100;

            Attack = new List<IAttackItem?>(5);
            Defence = new List<IDefenceItem?>(5);

        }

        public int Hit(Creature target)
        {
            int damage = 5;

            target.ReceiveHit(damage);
            if(target.HitPoint<=0)
            {
                Console.WriteLine($"{target.Name} has been defeated by {Name}");
            }
            return damage;
        }

        public void ReceiveHit(int hit)
        {
            int totalDamageReduction = Defence.Where(item => item != null).Sum(item => item.ReduceHitPoint);


            foreach (var item in Defence)
            {
                if (item != null)
                {
                    totalDamageReduction += item.ReduceHitPoint;
                }
            }
            hit = Math.Max(0, hit - totalDamageReduction);
            HitPoint -= hit;

            if (HitPoint <= 0)
            {
                Console.WriteLine("Creature is dead");
            }
        }
        //function to loot an item, if the creature has less than 5 items,
        //it will add the item to the list, otherwise it will ask the user if they want to replace an existing item
        public void Loot(WorldObject obj)
        {
            if (obj is IAttackItem attackItem)
            {
                if (Attack.Count < 5)
                {
                    Attack.Add(attackItem);
                }
                else
                {
                    Console.WriteLine("You already have the maximum number of attack items.");
                    Console.WriteLine("Do you want to replace an existing item? (yes/no)");
                    string response = Console.ReadLine()?.ToLower();

                    if (response == "yes")
                    {
                        ReplaceItem(Attack, attackItem);
                    }
                }
            }
            else if (obj is IDefenceItem defenceItem)
            {
                if (Defence.Count < 5)
                {
                    Defence.Add(defenceItem);
                }
                else
                {
                    Console.WriteLine("You already have the maximum number of defence items.");
                    Console.WriteLine("Do you want to replace an existing item? (yes/no)");
                    string response = Console.ReadLine()?.ToLower();

                    if (response == "yes")
                    {
                        ReplaceItem(Defence, defenceItem);
                    }
                }
            }
        }
        //function to replace an item
        private void ReplaceItem<T>(List<T?> itemList, T newItem) where T : class
        {
            Console.WriteLine("Choose an item to replace:");
            for (int i = 0; i < itemList.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {itemList[i]?.ToString()}");
            }

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= itemList.Count)
            {
                itemList[index - 1] = newItem;
                Console.WriteLine("Item replaced successfully.");
            }
            else
            {
                Console.WriteLine("Invalid choice. No item was replaced.");
            }
        }

        public void Move(int dx, int dy, World world)
        {
            int newX = PositionX + dx;
            int newY = PositionY + dy;

            if(newX >= 0 && newX< world.MaxX && newY >= 0 && newY>world.MaxY )
            {        
                PositionX = newX;
                PositionY = newY;
            }
            {
                Console.WriteLine("Invalid move, movement is out of bounds");
            }
        }


        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(HitPoint)}={HitPoint.ToString()}, {nameof(Attack)}={Attack}, {nameof(Defence)}={Defence}}}";
        }
    }
}
