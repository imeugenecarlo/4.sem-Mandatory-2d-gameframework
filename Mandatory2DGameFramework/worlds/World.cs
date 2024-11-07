using Mandatory2DGameFramework.model.Cretures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.worlds
{
    public class World
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }


        // world objects
        private List<WorldObject> _worldObjects;
        // world creatures
        private List<Creature> _creatures;

        public World(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
            _worldObjects = new List<WorldObject>();
            _creatures = new List<Creature>();
        }

        public override string ToString()
        {
            var worldObjectsInfo = string.Join(", ", _worldObjects.Select(o => o.ToString()));
            var creaturesInfo = string.Join(", ", _creatures.Select(c => c.ToString()));
            return $"World: MaxX={MaxX}, MaxY={MaxY}\n" +
                   $"Objects: [{worldObjectsInfo}]\n" +
                   $"Creatures: [{creaturesInfo}]";
        }

        public bool IsPositionOccupied(int x, int y)
        {
            return _worldObjects.Any(o => o.PositionX == x && o.PositionY == y) ||
                   _creatures.Any(c => c.PositionX == x && c.PositionY == y);
        }
        //Create world object, checks if position is occupied
        public void AddWorldObject(WorldObject obj)
        {
            if (obj != null && !IsPositionOccupied(obj.PositionX, obj.PositionY))
            {
                _worldObjects.Add(obj);
            }
            else
            {
                Console.WriteLine("Position is occupied or object is null.");
            }
        }
        //Get world object Read Method
        public IEnumerable<WorldObject> GetWorldObjects(int x, int y)
        {
            return _worldObjects.Where(o => o.PositionX == x && o.PositionY == y);
        }

        //Remove world object method
        public void RemoveWorldObject(WorldObject obj)
        {
            _worldObjects.Remove(obj);
        }

        //Add creature method
        public void AddCreature(Creature creature)
        {
            if (creature != null && !IsPositionOccupied(creature.PositionX, creature.PositionY))
            {
                _creatures.Add(creature);
            }
            else
            {
                Console.WriteLine("Position is occupied or creature is null.");
            }
        }
        //Remove Creature method
        public void RemoveCreature(Creature creature)
        {
            _creatures.Remove(creature);
        }




    }
}
