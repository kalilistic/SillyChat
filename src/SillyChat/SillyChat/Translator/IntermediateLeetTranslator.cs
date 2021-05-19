namespace SillyChat
{
    /// <summary>
    /// SillyChat service.
    /// </summary>
    public sealed class IntermediateLeetTranslator : BaseLeetTranslator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntermediateLeetTranslator"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public IntermediateLeetTranslator(ISillyChatPlugin plugin)
            : base(plugin)
        {
            this.SetCharacterMapping();
        }

        /// <inheritdoc />
        protected override void SetCharacterMapping()
        {
            this.MappedCharacters.Add('a', @"4");
            this.MappedCharacters.Add('b', @"8");
            this.MappedCharacters.Add('c', @"(");
            this.MappedCharacters.Add('d', @"|)");
            this.MappedCharacters.Add('e', @"3");
            this.MappedCharacters.Add('g', @"6");
            this.MappedCharacters.Add('h', @"|-|");
            this.MappedCharacters.Add('i', @"1");
            this.MappedCharacters.Add('k', @"|<");
            this.MappedCharacters.Add('l', @"|_");
            this.MappedCharacters.Add('m', @"|\/|");
            this.MappedCharacters.Add('n', @"/\/");
            this.MappedCharacters.Add('o', @"0");
            this.MappedCharacters.Add('r', @"|2");
            this.MappedCharacters.Add('s', @"5");
            this.MappedCharacters.Add('t', @"7");
            this.MappedCharacters.Add('u', @"|_|");
            this.MappedCharacters.Add('v', @"\/");
            this.MappedCharacters.Add('w', @"\/\/");
            this.MappedCharacters.Add('y', @"'/");
        }
    }
}
