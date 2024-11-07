using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mandatory2DGameFramework.model.Cretures
{
    /// <summary>
    /// Represents a creature in the game.
    /// </summary>
    public class Creature
    {
        /// <summary>
        /// Gets or sets the name of the creature.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the hit points of the creature.
        /// </summary>
        public int HitPoint { get; set; }

        /// <summary>
        /// Gets or sets the X position of the creature.
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        /// Gets or sets the Y position of the creature.
        /// </summary>
        public int PositionY { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the creature is alive.
        /// </summary>
        public bool isAlive { get; set; } = true;

        private ICreatureState _currentState;

        /// <summary>
        /// Gets or sets the list of attack items the creature has.
        /// </summary>
        public List<IAttackItem?> Attack { get; set; }

        /// <summary>
        /// Gets or sets the list of defense items the creature has.
        /// </summary>
        public List<IDefenceItem?> Defence { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Creature"/> class.
        /// </summary>
        /// <param name="name">The name of the creature.</param>
        /// <param name="hitPoint">The hit points of the creature.</param>
        /// <param name="x">The X position of the creature.</param>
        /// <param name="y">The Y position of the creature.</param>
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

        /// <summary>
        /// Sets the state of the creature.
        /// </summary>
        /// <param name="newState">The new state of the creature.</param>
        public void SetState(ICreatureState newState)
        {
            _currentState = newState;
        }

        /// <summary>
        /// Hits the specified target creature.
        /// </summary>
        /// <param name="target">The target creature.</param>
        public void Hit(Creature target)
        {
            _currentState.Hit(this, target);
        }

        /// <summary>
        /// Receives a hit with the specified damage.
        /// </summary>
        /// <param name="hit">The damage to be received.</param>
        public void ReceiveHit(int hit)
        {
            _currentState.ReceiveHit(this, hit);
        }

        /// <summary>
        /// Moves the creature by the specified delta values.
        /// </summary>
        /// <param name="dx">The delta X value.</param>
        /// <param name="dy">The delta Y value.</param>
        /// <param name="world">The world in which the creature moves.</param>
        public void Move(int dx, int dy, World world)
        {
            _currentState.Move(this, dx, dy, world);
        }

        /// <summary>
        /// Loots the specified world object.
        /// </summary>
        /// <param name="obj">The world object to be looted.</param>
        public void Loot(WorldObject obj)
        {
            _currentState.Loot(this, obj);
        }

        /// <summary>
        /// Default action for hitting a target creature.
        /// </summary>
        /// <param name="target">The target creature.</param>
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

        /// <summary>
        /// Default action for receiving a hit.
        /// </summary>
        /// <param name="hit">The damage to be received.</param>
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

        /// <summary>
        /// Default action for moving the creature.
        /// </summary>
        /// <param name="dx">The delta X value.</param>
        /// <param name="dy">The delta Y value.</param>
        /// <param name="world">The world in which the creature moves.</param>
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

        /// <summary>
        /// Default action for looting a world object.
        /// </summary>
        /// <param name="obj">The world object to be looted.</param>
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

        /// <summary>
        /// Replaces an item in the specified list with a new item.
        /// </summary>
        /// <typeparam name="T">The type of the items in the list.</typeparam>
        /// <param name="itemList">The list of items.</param>
        /// <param name="newItem">The new item to replace with.</param>
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

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            string attackItems = string.Join(", ", Attack.Where(item => item != null).Select(item => item?.Name));
            string defenceItems = string.Join(", ", Defence.Where(item => item != null).Select(item => item?.Name));

            return $"Name: {Name}, HitPoint: {HitPoint}, Position: ({PositionX}, {PositionY}), " +
                   $"Attack Items: [{attackItems}], Defence Items: [{defenceItems}]";
        }
    }
}


