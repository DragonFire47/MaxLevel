using PulsarModLoader;

namespace MaxLevel
{
    public class Mod : PulsarMod
    {
        public override string Version => "0.1.3";

        public override string Author => "Dragon";

        public override string ShortDescription => "Increases max level for components and items. all clients must install";

        public override string Name => "MaxLevel";

        public override int MPFunctionality => (int)MPFunction.All;

        public override string HarmonyIdentifier()
        {
            return "Dragon.MaxLevel";
        }
    }
}
