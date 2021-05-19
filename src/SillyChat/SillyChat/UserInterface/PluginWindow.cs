using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace SillyChat
{
    /// <summary>
    /// Plugin window which extends window with sillyChat plugin.
    /// </summary>
    public abstract class PluginWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginWindow"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        /// <param name="windowName">Name of the window.</param>
        /// <param name="flags">ImGui flags.</param>
        protected PluginWindow(ISillyChatPlugin plugin, string windowName, ImGuiWindowFlags flags = ImGuiWindowFlags.None)
            : base(windowName, flags)
        {
            this.Plugin = plugin;
        }

        /// <summary>
        /// Gets sillyChat plugin for window.
        /// </summary>
        protected ISillyChatPlugin Plugin { get; }

        /// <inheritdoc/>
        public override void Draw()
        {
        }
    }
}
