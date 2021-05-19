namespace SillyChat
{
    /// <summary>
    /// SillyChat plugin configuration.
    /// </summary>
    public abstract class SillyChatConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether fresh install.
        /// </summary>
        public bool FreshInstall { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether plugin is enabled.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets translation mode.
        /// </summary>
        public int TranslationMode { get; set; } = 4;
    }
}
