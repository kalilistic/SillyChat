namespace SillyChat
{
    /// <summary>
    /// SillyChat service.
    /// </summary>
    public sealed class AdvancedLeetTranslator : BaseLeetTranslator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedLeetTranslator"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public AdvancedLeetTranslator(ISillyChatPlugin plugin)
            : base(plugin)
        {
            this.SetCharacterMapping();
        }

        /// <inheritdoc />
        public override string Translate(string input)
        {
            var translation = base.Translate(input);
            return translation.Replace(" ", string.Empty);
        }

        /// <inheritdoc />
        protected override void SetCharacterMapping()
        {
            this.MappedCharacters.Add('a', @"@");
            this.MappedCharacters.Add('b', @"|3");
            this.MappedCharacters.Add('c', @"(");
            this.MappedCharacters.Add('d', @"|)");
            this.MappedCharacters.Add('e', @"[-");
            this.MappedCharacters.Add('g', @"(_+");
            this.MappedCharacters.Add('h', @"|-|");
            this.MappedCharacters.Add('i', @"!");
            this.MappedCharacters.Add('k', @"|<");
            this.MappedCharacters.Add('l', @"|_");
            this.MappedCharacters.Add('m', @"|\/|");
            this.MappedCharacters.Add('n', @"/\/");
            this.MappedCharacters.Add('o', @"()");
            this.MappedCharacters.Add('r', @"|2");
            this.MappedCharacters.Add('s', @"$");
            this.MappedCharacters.Add('t', @"¯|¯");
            this.MappedCharacters.Add('u', @"|_|");
            this.MappedCharacters.Add('v', @"\/");
            this.MappedCharacters.Add('w', @"\/\/");
            this.MappedCharacters.Add('y', @"'/");
        }
    }
}
