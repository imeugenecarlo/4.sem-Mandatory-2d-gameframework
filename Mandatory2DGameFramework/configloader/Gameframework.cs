using Mandatory2DGameFramework.logging;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.Cretures;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;

namespace Mandatory2DGameFramework.configloader
{
    public class GameFramework
    {
        private readonly IConfigLoader _configLoader;
        private readonly IMyLogger _logger;

        public List<AttackItem> AttackItems { get; private set; }
        public List<DefenceItem> DefenceItems { get; private set; }
        public List<Creature> Creatures { get; private set; }
        public World World { get; private set; }
        public List<WorldObject> WorldObjects { get; private set; }

        public GameFramework(IConfigLoader configLoader, IMyLogger logger)
        {
            _configLoader = configLoader;
            _logger = logger;
        }

        public void LoadConfiguration(string filePath)
        {
            try
            {
                _logger.LogInfo($"Loading configuration from {filePath}");

                // Load data using the provided filePath
                AttackItems = _configLoader.LoadAttackItems(filePath);
                DefenceItems = _configLoader.LoadDefenceItems(filePath);
                Creatures = _configLoader.LoadCreatures(filePath);
                World = _configLoader.LoadWorld(filePath);
                WorldObjects = _configLoader.LoadWorldObjects(filePath);

                // Log successful loading
                _logger.LogInfo("Configuration loaded successfully.");
            }
            catch (Exception ex)
            {
                // Log error in case the loading fails
                _logger.LogError($"Error loading configuration: {ex.Message}");
            }
        }
    }
}


