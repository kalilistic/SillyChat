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

        /// <summary>
        /// Gets or sets timer to process intervals for history view.
        /// </summary>
        public int ProcessTranslationInterval { get; set; } = 300000;

        /// <summary>
        /// Gets or sets max number of translations to keep in history.
        /// </summary>
        public int TranslationHistoryMax { get; set; } = 30;
    }
}
