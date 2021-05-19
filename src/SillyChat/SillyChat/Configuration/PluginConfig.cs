using System;

using Dalamud.Configuration;

namespace SillyChat
{
    /// <summary>
    /// Plugin configuration class used for dalamud.
    /// </summary>
    [Serializable]
    public class PluginConfig : SillyChatConfig, IPluginConfiguration
    {
        /// <inheritdoc/>
        public int Version { get; set; } = 0;
    }
}
