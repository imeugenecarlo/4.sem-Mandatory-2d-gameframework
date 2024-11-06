using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.Cretures;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.worlds;

namespace Mandatory2DGameFramework.configloader
{
    public interface IConfigLoader
    {
        List<AttackItem> LoadAttackItems(string filePath);
        List<DefenceItem> LoadDefenceItems(string filePath);
        List<Creature> LoadCreatures(string filePath);
        World LoadWorld(string filePath);
        List<WorldObject> LoadWorldObjects(string filePath);


    }
}