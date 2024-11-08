using System;
using System.Linq;
using Mandatory2DGameFramework.configloader;
using Mandatory2DGameFramework.logging;
using Mandatory2DGameFramework.model.Cretures;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;

class Program
{
    static void Main()
    {
        // Use the singleton instance of MyLogger
        MyLogger logger = MyLogger.Instance;
        var world = new World(100, 100);
        var configLoader = new ConfigLoader(logger);
        var gameFramework = new GameFramework(configLoader, logger);

        // Load configuration from XML file
        string filePath = "xmlconfig.txt";
        gameFramework.LoadConfiguration(filePath);

        // Access loaded data
        var creatures = gameFramework.Creatures;
        var attackItems = gameFramework.AttackItems;
        var defenceItems = gameFramework.DefenceItems;
        var worldObjects = gameFramework.WorldObjects;

        // Add loaded creatures to the world
        foreach (var creature in creatures)
        {
            world.AddCreature(creature);
        }

        // Add loaded attack items to the world
        foreach (var attackItem in attackItems)
        {
            world.AddWorldObject(attackItem);
        }

        // Add loaded defence items to the world
        foreach (var defenceItem in defenceItems)
        {
            world.AddWorldObject(defenceItem);
        }

        // Add other loaded world objects to the world
        foreach (var worldObject in worldObjects)
        {
            world.AddWorldObject(worldObject);
        }

        // Example usage of loaded data
        if (creatures.Count > 0)
        {
            Creature hmk = creatures[0];
            Console.WriteLine(hmk);

            if (attackItems.Count > 0)
            {
                hmk.DefaultLoot(attackItems[0]);
            }

            if (defenceItems.Count > 0)
            {
                hmk.DefaultLoot(defenceItems[0]);
            }

            Console.WriteLine(hmk);
        }

        Console.WriteLine();
        Console.WriteLine(world);

        // Add additional properties
        Creature humanKnight = new Creature("human knight", 25, 0, 0);
        AttackItem excelsior = new AttackItem(10, 10, "Excelsior", true, true, 10, 5);
        DefenceItem excelShield = new DefenceItem(10, 10, "ExcelShield", true, true, 7);

        Console.WriteLine(humanKnight);
        world.AddCreature(humanKnight);
        world.AddWorldObject(excelsior);
        world.AddWorldObject(excelShield);

        Console.WriteLine();
        Console.WriteLine(world);

        Console.WriteLine();
        humanKnight.Move(10, 10, world);
        humanKnight.Loot(excelsior);
        humanKnight.Loot(excelShield);
        Console.WriteLine(humanKnight);
        Console.WriteLine();
        Console.WriteLine();

        // Find the troll in the list of creatures
        Creature? troll = creatures.FirstOrDefault(c => c.Name.ToLower() == "troll");
        if (troll != null)
        {
            // Move the human knight to the troll's position
            humanKnight.Move(troll.PositionX - humanKnight.PositionX, troll.PositionY - humanKnight.PositionY, world);

            // Human knight hits the troll once
            humanKnight.Hit(troll);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{humanKnight.Name} hits {troll.Name}. {troll.Name} HP: {troll.HitPoint}");
            Console.ResetColor();

            // Troll hits the human knight once
            if (troll.isAlive)
            {
                troll.Hit(humanKnight);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{troll.Name} hits {humanKnight.Name}. {humanKnight.Name} HP: {humanKnight.HitPoint}");
                Console.ResetColor();
            }
        }
        else
        {
            Console.WriteLine("Troll not found in the loaded creatures.");
        }

        Console.WriteLine();
        Console.WriteLine(world);
    }
}