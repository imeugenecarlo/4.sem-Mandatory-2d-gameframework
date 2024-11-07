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
        // Initialize world
        World world = new World(100, 100);
        // Initialize logger
        IMyLogger logger = MyLogger.Instance;

        // Initialize config loader with logger
        IConfigLoader configLoader = new ConfigLoader(logger);

        // Initialize game framework with config loader and logger
        GameFramework gameFramework = new GameFramework(configLoader, logger);

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
        AttackItem excelsior = new AttackItem(10, 10, "Excelsior", true, true,10,5);
        DefenceItem excelShield = new DefenceItem(10, 10, "ExcelShield", true, true,7);

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
        Console.ReadLine();
    }
}
