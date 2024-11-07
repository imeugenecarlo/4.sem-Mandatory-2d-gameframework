using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Mandatory2DGameFramework.configloader;
using Mandatory2DGameFramework.logging;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.Cretures;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;

public class ConfigLoader : IConfigLoader
{
    private readonly IMyLogger _logger;

    public ConfigLoader(IMyLogger logger)
    {
        _logger = logger;
    }

    public List<AttackItem> LoadAttackItems(string filePath)
    {
        var attackItems = new List<AttackItem>();

        try
        {
            XDocument doc = XDocument.Load(filePath);
            var items = doc.Descendants("AttackItem");

            var lootableItems = items
                .Where(item => bool.Parse(item.Element("Lootable")?.Value ?? "false"))
                .Select(item => new AttackItem(
                    int.Parse(item.Element("PositionX")?.Value),
                    int.Parse(item.Element("PositionY")?.Value),
                    item.Element("Name")?.Value,
                    bool.Parse(item.Element("Lootable")?.Value ?? "false"),
                    bool.Parse(item.Element("Removeable")?.Value ?? "false"),
                    int.Parse(item.Element("Hit")?.Value),
                    int.Parse(item.Element("Range")?.Value)
                )).ToList();

            _logger.LogInfo("Attack items loaded successfully from XML.");
            return lootableItems;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error loading Attack items from {filePath}: {ex.Message}");
            return attackItems; // Return an empty list on error
        }
    }

    public List<DefenceItem> LoadDefenceItems(string filePath)
    {
        var defenceItems = new List<DefenceItem>();

        try
        {
            XDocument doc = XDocument.Load(filePath);
            var items = doc.Descendants("DefenceItem");

            var strongDefences = items
                .Where(item => int.Parse(item.Element("ReduceHitPoint")?.Value ?? "0") > 5)
                .Select(item => new DefenceItem(
                    int.Parse(item.Element("PositionX")?.Value),
                    int.Parse(item.Element("PositionY")?.Value),
                    item.Element("Name")?.Value,
                    bool.Parse(item.Element("Lootable")?.Value ?? "false"),
                    bool.Parse(item.Element("Removeable")?.Value ?? "false"),
                    int.Parse(item.Element("ReduceHitPoint")?.Value)
                )).ToList();

            _logger.LogInfo("Defence items loaded successfully from XML.");
            return strongDefences;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error loading Defence items from {filePath}: {ex.Message}");
            return defenceItems; // Return an empty list on error
        }
    }

    public List<Creature> LoadCreatures(string filePath)
    {
        var creatures = new List<Creature>();
        try
        {
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

            _logger.LogInfo("Creatures loaded successfully from XML.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error loading Creatures from {filePath}: {ex.Message}");
        }

        return creatures;
    }

    public World LoadWorld(string filePath)
    {
        try
        {
            XDocument doc = XDocument.Load(filePath);
            var worldElement = doc.Descendants("World").FirstOrDefault();

            if (worldElement != null)
            {
                int maxX = int.Parse(worldElement.Element("MaxX")?.Value ?? "0");
                int maxY = int.Parse(worldElement.Element("MaxY")?.Value ?? "0");

                _logger.LogInfo("World loaded successfully from XML.");
                return new World(maxX, maxY);
            }
            else
            {
                _logger.LogError("World element not found in XML.");
                return new World(0, 0); // Return default world if no world element found
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error loading World from {filePath}: {ex.Message}");
            return new World(0, 0); // Return default world on error
        }
    }

    public List<WorldObject> LoadWorldObjects(string filePath)
    {
        var worldObjects = new List<WorldObject>();
        try
        {
            XDocument doc = XDocument.Load(filePath);
            var objects = doc.Descendants("WorldObjects").Elements();

            foreach (var obj in objects)
            {
                string name = obj.Element("Name")?.Value ?? string.Empty;
                bool lootable = bool.Parse(obj.Element("Lootable")?.Value ?? "false");
                bool removeable = bool.Parse(obj.Element("Removeable")?.Value ?? "false");
                int positionX = int.Parse(obj.Element("PositionX")?.Value ?? "0");
                int positionY = int.Parse(obj.Element("PositionY")?.Value ?? "0");

                if (obj.Name == "AttackItem")
                {
                    worldObjects.Add(new AttackItem(
                        positionX, positionY, name, lootable, removeable,
                        int.Parse(obj.Element("Hit")?.Value ?? "0"),
                        int.Parse(obj.Element("Range")?.Value ?? "0")
                    ));
                }
                else if (obj.Name == "DefenceItem")
                {
                    worldObjects.Add(new DefenceItem(
                        positionX, positionY, name, lootable, removeable,
                        int.Parse(obj.Element("ReduceHitPoint")?.Value ?? "0")
                    ));
                }
                else
                {
                    worldObjects.Add(new WorldObject(positionX, positionY, name, lootable, removeable));
                }
            }

            _logger.LogInfo("World objects loaded successfully from XML.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error loading World objects from {filePath}: {ex.Message}");
        }

        return worldObjects;
    }
}
