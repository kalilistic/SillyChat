namespace SillyChat
{
    /// <summary>
    /// Translator.
    /// </summary>
    public abstract class BaseTranslator
    {
        /// <summary>
        /// SillyChat plugin.
        /// </summary>
        protected readonly ISillyChatPlugin Plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTranslator"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        protected BaseTranslator(ISillyChatPlugin plugin)
        {
            this.Plugin = plugin;
        }

        /// <summary>
        /// Translate input string.
        /// </summary>
        /// <param name="input">text to translate.</param>
        /// <returns>translated text.</returns>
        public abstract string Translate(string input);
    }
}
