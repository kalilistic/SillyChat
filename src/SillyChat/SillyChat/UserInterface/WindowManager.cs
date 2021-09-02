using System.Numerics;

using CheapLoc;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using ImGuiNET;

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
        public WindowManager(ISillyChatPlugin plugin)
        {
            this.Plugin = plugin;

            // create windows
            this.ConfigWindow = new ConfigWindow(this.Plugin) { Size = new Vector2(180, 130) };
            this.HistoryWindow = new HistoryWindow(this.Plugin)
            {
                Size = new Vector2(200, 200),
                SizeCondition = ImGuiCond.FirstUseEver,
            };

            // setup events
            SillyChatPlugin.PluginInterface.UiBuilder.Draw += this.OnBuildUi;
            SillyChatPlugin.PluginInterface.UiBuilder.OpenConfigUi += this.OnOpenConfigUi;

            // setup window system
            this.WindowSystem = new WindowSystem("SillyChatWindowSystem");
            this.WindowSystem.AddWindow(this.ConfigWindow);
            this.WindowSystem.AddWindow(this.HistoryWindow);

            // setup ui commands
            SillyChatPlugin.CommandManager.AddHandler("/sillyconfig", new CommandInfo(this.ToggleConfig)
            {
                HelpMessage = Loc.Localize("ConfigCommandHelp", "Show SillyChat config window."),
                ShowInHelp = true,
            });
            SillyChatPlugin.CommandManager.AddHandler("/sillyhistory", new CommandInfo(this.ToggleHistory)
            {
                HelpMessage = Loc.Localize("HistoryCommandHelp", "Show SillyChat history window."),
                ShowInHelp = true,
            });
        }

        /// <summary>
        /// Gets config window.
        /// </summary>
        public HistoryWindow HistoryWindow { get; }

        /// <summary>
        /// Gets config window.
        /// </summary>
        public ConfigWindow? ConfigWindow { get; }

        private WindowSystem WindowSystem { get; }

        private ISillyChatPlugin Plugin { get; }

        /// <summary>
        /// Dispose plugin windows.
        /// </summary>
        public void Dispose()
        {
            SillyChatPlugin.PluginInterface.UiBuilder.Draw -= this.OnBuildUi;
            SillyChatPlugin.PluginInterface.UiBuilder.OpenConfigUi -= this.OnOpenConfigUi;
            SillyChatPlugin.CommandManager.RemoveHandler("/sillyconfig");
            SillyChatPlugin.CommandManager.RemoveHandler("/sillyhistory");
        }

        private void ToggleHistory(string command, string args)
        {
            this.HistoryWindow!.IsOpen ^= true;
        }

        private void ToggleConfig(string command, string args)
        {
            this.ConfigWindow!.IsOpen ^= true;
        }

        private void OnOpenConfigUi()
        {
            this.ConfigWindow!.IsOpen ^= true;
        }

        private void OnBuildUi()
        {
            this.WindowSystem.Draw();
        }
    }
}
