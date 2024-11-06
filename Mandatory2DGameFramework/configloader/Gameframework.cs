using Mandatory2DGameFramework.logging;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.defence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory2DGameFramework.configloader
{
    public class GameFramework
    {
        private readonly IConfigLoader _configLoader;
        private readonly IMyLogger _logger;

        public GameFramework(IConfigLoader configLoader, IMyLogger logger)
        {
            _configLoader = configLoader;
            _logger = logger;
        }

        public void LoadConfiguration()
        {
            // Ensure you are calling the correct method on the IConfigLoader interface
            try
            {
                var attackItems = _configLoader.LoadAttackItems("xmlconfig.txt");
                var defenceItems = _configLoader.LoadDefenceItems("xmlconfig.txt");

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
