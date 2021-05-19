using Dalamud.Plugin;

namespace SillyChat
{
    /// <summary>
    /// Base plugin to register with dalamud.
    /// </summary>
    public class DalamudPlugin : IDalamudPlugin
    {
        private SillyChatPlugin sillyChatPlugin = null!;

        /// <inheritdoc/>
        public string Name { get; } = "SillyChat";

        /// <inheritdoc/>
        public void Dispose()
        {
            this.sillyChatPlugin.Dispose();
        }

        /// <inheritdoc/>
        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.sillyChatPlugin = new SillyChatPlugin(this.Name, pluginInterface);
        }
    }
}
