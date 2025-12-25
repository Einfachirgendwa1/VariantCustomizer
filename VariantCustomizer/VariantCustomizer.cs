using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using PluginConfig.API;

namespace VariantCustomizer {
    [BepInPlugin("com.einfachirgendwa1.variantCustomizer", "VariantCustomizer", "1.0.0.0")]
    public class VariantCustomizer : BaseUnityPlugin {
        private readonly Harmony harmony = new Harmony("VariantCustomizer");

        private PluginConfigurator? config;

        private void Awake() {
            string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

            config = PluginConfigurator.Create("Variant Customizer", "com.einfachirgendwa1.variantCustomizer");
            config.SetIconWithURL($"file://{Path.Combine(directory, "icon.png")}");
        }
    }
}