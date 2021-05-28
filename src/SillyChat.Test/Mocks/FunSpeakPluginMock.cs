namespace SillyChat.Test
{
    /// <inheritdoc />
    public class SillyChatPluginMock : ISillyChatPlugin
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SillyChatPluginMock()
        {
            PluginName = "SillyChat";
            Configuration = new SillyChatConfigMock();
            TranslationService = new TranslationService(this);
            HistoryService = new HistoryService(this);
        }

        /// <inheritdoc />
        public string PluginName { get; set; }

        /// <inheritdoc />
        public SillyChatConfig Configuration { get; }

        /// <inheritdoc />
        public TranslationService TranslationService { get; }

        /// <inheritdoc />
        public HistoryService HistoryService { get; }

        /// <inheritdoc />
        public void SaveConfig()
        {
            throw new System.NotImplementedException();
        }
    }
}
