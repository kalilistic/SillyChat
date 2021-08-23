namespace SillyChat
{
    /// <summary>
    /// SillyChat plugin interface.
    /// </summary>
    public interface ISillyChatPlugin
    {
        /// <summary>
        /// Gets plugin configuration.
        /// </summary>
        SillyChatConfig Configuration { get; }

        /// <summary>
        /// Gets translation service.
        /// </summary>
        TranslationService TranslationService { get; }

        /// <summary>
        /// Gets history service.
        /// </summary>
        HistoryService HistoryService { get; }

        /// <summary>
        /// Save plugin configuration.
        /// </summary>
        void SaveConfig();
    }
}
