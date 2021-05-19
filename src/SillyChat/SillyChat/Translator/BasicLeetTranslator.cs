namespace SillyChat
{
    /// <summary>
    /// SillyChat service.
    /// </summary>
    public sealed class BasicLeetTranslator : BaseLeetTranslator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicLeetTranslator"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public BasicLeetTranslator(ISillyChatPlugin plugin)
            : base(plugin)
        {
            this.SetCharacterMapping();
        }

        /// <inheritdoc />
        protected override void SetCharacterMapping()
        {
            this.MappedCharacters.Add('a', @"4");
            this.MappedCharacters.Add('e', @"3");
            this.MappedCharacters.Add('i', @"1");
            this.MappedCharacters.Add('o', @"0");
        }
    }
}
