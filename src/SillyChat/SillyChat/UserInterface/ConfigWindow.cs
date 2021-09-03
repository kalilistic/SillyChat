using CheapLoc;
using Dalamud.Interface.Components;
using ImGuiNET;

namespace SillyChat
{
    /// <summary>
    /// Config window for the plugin.
    /// </summary>
    public class ConfigWindow : PluginWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigWindow"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public ConfigWindow(ISillyChatPlugin plugin)
            : base(plugin, Loc.Localize("SettingsTitle", "SillyChat Settings"))
        {
        }

        /// <inheritdoc/>
        public override void Draw()
        {
            // plugin enabled
            var enabled = this.Plugin.Configuration.Enabled;
            if (ImGui.Checkbox(
                Loc.Localize("PluginEnabled", "Enabled") + "###SillyChat_PluginEnabled_Checkbox",
                ref enabled))
            {
                this.Plugin.Configuration.Enabled = enabled;
                this.Plugin.SaveConfig();
            }

            ImGuiComponents.HelpMarker(Loc.Localize("PluginEnabled_HelpMarker", "enable or disable the plugin"));
            ImGui.Spacing();

            // translator
            ImGui.Text(Loc.Localize("Translator", "Translator"));
            ImGuiComponents.HelpMarker(Loc.Localize(
                "Translator_HelpMarker",
                "select translator to use for chat text"));
            ImGui.Spacing();
            ImGui.SetNextItemWidth(ImGui.GetWindowSize().X - 30f);
            var translationMode = this.Plugin.Configuration.TranslationMode;
            if (ImGui.Combo(
                "###SillyChat_TranslationMode_Combo",
                ref translationMode,
                this.Plugin.TranslationService.GetTranslationModeNames(),
                this.Plugin.TranslationService.GetTranslationModeNames().Length))
            {
                this.Plugin.Configuration.TranslationMode = translationMode;
                this.Plugin.SaveConfig();
                this.Plugin.TranslationService.SetTranslationMode(translationMode);
            }
        }
    }
}
