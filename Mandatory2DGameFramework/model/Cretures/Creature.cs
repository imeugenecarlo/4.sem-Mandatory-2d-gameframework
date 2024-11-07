using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mandatory2DGameFramework.model.Cretures
{
    public class Creature
    {
        public string Name { get; set; }
        public int HitPoint { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public bool isAlive { get; set; } = true;

        private ICreatureState _currentState;

        public List<IAttackItem?> Attack { get; set; }
        public List<IDefenceItem?> Defence { get; set; }

        public Creature(string name, int hitPoint, int x, int y)
        {
            PositionX = x;
            PositionY = y;
            Name = name;
            HitPoint = hitPoint;

            Attack = new List<IAttackItem?>();
            Defence = new List<IDefenceItem?>();

            _currentState = new AliveState();
        }

        public void SetState(ICreatureState newState)
        {
            _currentState = newState;
        }

        public void Hit(Creature target)
        {
            _currentState.Hit(this, target);
        }

        public void ReceiveHit(int hit)
        {
            _currentState.ReceiveHit(this, hit);
        }

        public void Move(int dx, int dy, World world)
        {
            _currentState.Move(this, dx, dy, world);
        }

        public void Loot(WorldObject obj)
        {
            _currentState.Loot(this, obj);
        }

        // Default actions used by the states
        public void DefaultHit(Creature target)
        {
            int damage = 0;
            foreach (var item in Attack)
                if (item != null)
                {
                    damage += item.Hit;
                }

            target.ReceiveHit(damage);
            if (target.HitPoint <= 0)
            {
                Console.WriteLine($"{target.Name} has been defeated by {Name}");
            }
        }

        public void DefaultReceiveHit(int hit)
        {
            int totalDamageReduction = Defence.Where(item => item != null).Sum(item => item.ReduceHitPoint);

            hit = Math.Max(0, hit - totalDamageReduction);
            HitPoint -= hit;

            if (HitPoint <= 0)
            {
                isAlive = false;
                Console.WriteLine($"{Name} is dead.");
            }
        }

        public void DefaultMove(int dx, int dy, World world)
        {
            int newX = PositionX + dx;
            int newY = PositionY + dy;

            if (world.IsPositionOccupied(newX, newY))
            {
                Console.WriteLine("Position is occupied or object is null.");
                return;
            }

            PositionX = newX;
            PositionY = newY;
            Console.WriteLine($"{Name} moved to ({newX}, {newY})");

            // Check for lootable items at the new position
            var lootableItems = world.GetWorldObjects(newX, newY);
            foreach (var item in lootableItems)
            {
                Loot(item);
            }
        }



        public void DefaultLoot(WorldObject obj)
        {
            if (obj is IAttackItem attackItem)
            {
                if (Attack.Count < 5)
                {
                    Attack.Add(attackItem);
                    Console.WriteLine($"{Name} looted {attackItem.Name}");
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
                    Console.WriteLine($"{Name} looted {defenceItem.Name}");
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

        public override string ToString()
        {
            string attackItems = string.Join(", ", Attack.Where(item => item != null).Select(item => item?.Name));
            string defenceItems = string.Join(", ", Defence.Where(item => item != null).Select(item => item?.Name));

            return $"Name: {Name}, HitPoint: {HitPoint}, Position: ({PositionX}, {PositionY}), " +
                   $"Attack Items: [{attackItems}], Defence Items: [{defenceItems}]";
        }
    }
}

