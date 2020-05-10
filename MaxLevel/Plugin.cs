using PulsarPluginLoader;

namespace MaxLevel
{
    public class Plugin : PulsarPlugin
    {
        public override string Version => "0.0.1";

        public override string Author => "Dragon";

        public override string ShortDescription => "Increases max level for components and items. all clients must install";

        public override string Name => "MaxLevel";

        public override string HarmonyIdentifier()
        {
            return "Dragon.MaxLevel";
        }
    }
}
