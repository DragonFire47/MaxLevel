using PulsarModLoader;
using PulsarModLoader.MPModChecks;

namespace MaxLevel
{
    public class Mod : PulsarMod
    {
        public override string Version => "0.1.4";

        public override string Author => "Dragon";

        public override string ShortDescription => "Increases max level for components and items. all clients must install";

        public override string Name => "MaxLevel";

        public override int MPRequirements => (int)MPRequirement.All;

        public override string HarmonyIdentifier()
        {
            return "Dragon.MaxLevel";
        }
    }
}
