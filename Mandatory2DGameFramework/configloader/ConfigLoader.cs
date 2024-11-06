using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Mandatory2DGameFramework.configloader;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.Cretures;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;

public class ConfigLoader : IConfigLoader
{
    public List<AttackItem> LoadAttackItems(string filePath)
    {
        var attackItems = new List<AttackItem>();

        XDocument doc = XDocument.Load(filePath);
        var items = doc.Descendants("AttackItem");

        // Use LINQ to filter items based on a condition (e.g., only lootable items)
        var lootableItems = items
            .Where(item => bool.Parse(item.Element("Lootable")?.Value ?? "false"))  // Only lootable items
            .Select(item => new AttackItem(
                int.Parse(item.Element("PositionX")?.Value),
                int.Parse(item.Element("PositionY")?.Value),
                item.Element("Name")?.Value,
                bool.Parse(item.Element("Lootable")?.Value ?? "false"),
                bool.Parse(item.Element("Removeable")?.Value ?? "false"))
            {
                Hit = int.Parse(item.Element("Hit")?.Value),
                Range = int.Parse(item.Element("Range")?.Value)
            }).ToList();

        return lootableItems;
    }

    public List<DefenceItem> LoadDefenceItems(string filePath)
    {
        var defenceItems = new List<DefenceItem>();

        XDocument doc = XDocument.Load(filePath);
        var items = doc.Descendants("DefenceItem");

        // Use LINQ to filter items based on a condition (e.g., only items that reduce more than 5 hit points)
        var strongDefences = items
            .Where(item => int.Parse(item.Element("ReduceHitPoint")?.Value ?? "0") > 5)
            .Select(item => new DefenceItem(
                int.Parse(item.Element("PositionX")?.Value),
                int.Parse(item.Element("PositionY")?.Value),
                item.Element("Name")?.Value,
                bool.Parse(item.Element("Lootable")?.Value ?? "false"),
                bool.Parse(item.Element("Removeable")?.Value ?? "false"))
            {
                ReduceHitPoint = int.Parse(item.Element("ReduceHitPoint")?.Value)
            }).ToList();

        return strongDefences;
    }

    public List<Creature> LoadCreatures(string filePath)
    {
        var creatures = new List<Creature>();
        XDocument doc = XDocument.Load(filePath);
        var creatureElements = doc.Descendants("Creatures").Elements("Creature");

        foreach (var creature in creatureElements)
        {
            string name = creature.Element("Name")?.Value ?? string.Empty;
            int hitPoint = int.Parse(creature.Element("HitPoint")?.Value ?? "100");
            int positionX = int.Parse(creature.Element("PositionX")?.Value ?? "0");
            int positionY = int.Parse(creature.Element("PositionY")?.Value ?? "0");

            creatures.Add(new Creature(name, hitPoint, positionX, positionY));
        }

        return creatures;
    }

    public World LoadWorld(string filePath)
    {
        XDocument doc = XDocument.Load(filePath);
        var worldElement = doc.Descendants("World").FirstOrDefault();

        int maxX = int.Parse(worldElement.Element("MaxX")?.Value ?? "0");
        int maxY = int.Parse(worldElement.Element("MaxY")?.Value ?? "0");

        return new World(maxX, maxY);
    }

    public List<WorldObject> LoadWorldObjects(string filePath)
    {
        var worldObjects = new List<WorldObject>();
        XDocument doc = XDocument.Load(filePath);
        var objects = doc.Descendants("WorldObjects").Elements("WorldObject");

        foreach (var obj in objects)
        {
            string name = obj.Element("Name")?.Value ?? string.Empty;
            bool lootable = bool.Parse(obj.Element("Lootable")?.Value ?? "false");
            bool removeable = bool.Parse(obj.Element("Removeable")?.Value ?? "false");
            int positionX = int.Parse(obj.Element("PositionX")?.Value ?? "0");
            int positionY = int.Parse(obj.Element("PositionY")?.Value ?? "0");

            // Check for specific types of WorldObjects, e.g., AttackItem, DefenceItem, etc.
            if (obj.Element("Hit") != null && obj.Element("Range") != null)
            {
                worldObjects.Add(new AttackItem(positionX, positionY, name, lootable, removeable)
                {
                    Hit = int.Parse(obj.Element("Hit")?.Value ?? "0"),
                    Range = int.Parse(obj.Element("Range")?.Value ?? "0")
                });
            }
            else if (obj.Element("ReduceHitPoint") != null)
            {
                worldObjects.Add(new DefenceItem(positionX, positionY, name, lootable, removeable)
                {
                    ReduceHitPoint = int.Parse(obj.Element("ReduceHitPoint")?.Value ?? "0")
                });
            }
            // Add more types like Chest if necessary
        }

        return worldObjects;
    }
}


