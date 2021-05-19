using System;
using System.Numerics;

using CheapLoc;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;

namespace SillyChat
{
    /// <summary>
    /// Window manager to hold plugin windows and window system.
    /// </summary>
    public class WindowManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowManager"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        /// <param name="pluginInterface">Plugin interface.</param>
        public WindowManager(ISillyChatPlugin plugin, DalamudPluginInterface pluginInterface)
        {
            this.Plugin = plugin;
            this.PluginInterface = pluginInterface;

            // create windows
            this.ConfigWindow = new ConfigWindow(this.Plugin) { Size = new Vector2(180, 130) };

            // setup events
            this.PluginInterface.UiBuilder.OnBuildUi += this.OnBuildUi;
            this.PluginInterface.UiBuilder.OnOpenConfigUi += this.OnOpenConfigUi;

            // setup window system
            this.WindowSystem = new WindowSystem("SillyChatWindowSystem");
            this.WindowSystem.AddWindow(this.ConfigWindow);

            // setup ui commands
            this.PluginInterface.CommandManager.AddHandler("/sillyconfig", new CommandInfo(this.ToggleConfig)
            {
                HelpMessage = Loc.Localize("ConfigCommandHelp", "Show SillyChat config window."),
                ShowInHelp = true,
            });
        }

        /// <summary>
        /// Gets config window.
        /// </summary>
        public ConfigWindow? ConfigWindow { get; }

        private WindowSystem WindowSystem { get; }

        private ISillyChatPlugin Plugin { get; }

        private DalamudPluginInterface PluginInterface { get; }

        /// <summary>
        /// Dispose plugin windows.
        /// </summary>
        public void Dispose()
        {
            this.PluginInterface.UiBuilder.OnBuildUi -= this.OnBuildUi;
            this.PluginInterface.UiBuilder.OnOpenConfigUi -= this.OnOpenConfigUi;
            this.PluginInterface.CommandManager.RemoveHandler("/sillyconfig");
        }

        private void ToggleConfig(string command, string args)
        {
            this.ConfigWindow!.IsOpen ^= true;
        }

        private void OnOpenConfigUi(object sender, EventArgs e)
        {
            this.ConfigWindow!.IsOpen ^= true;
        }

        private void OnBuildUi()
        {
            this.WindowSystem.Draw();
        }
    }
}
