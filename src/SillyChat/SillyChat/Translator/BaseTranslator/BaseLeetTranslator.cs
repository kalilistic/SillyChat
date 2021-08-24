using System;
using System.Collections.Generic;

namespace SillyChat
{
    /// <summary>
    /// SillyChat service.
    /// </summary>
    public abstract class BaseLeetTranslator : BaseTranslator
    {
        /// <summary>
        /// Mapped characters to replace.
        /// </summary>
        protected readonly Dictionary<char, string> MappedCharacters = new ();

        private readonly Dictionary<string, string> mappedWords = new ();
        private readonly Random random = new ();

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLeetTranslator"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        protected BaseLeetTranslator(ISillyChatPlugin plugin)
            : base(plugin)
        {
            this.SetWordMapping();
        }

        /// <summary>
        /// Translate.
        /// </summary>
        /// <param name="input">string to translate.</param>
        /// <returns>translated string.</returns>
        public override string Translate(string input)
        {
            input = input.ToLower();
            input = input.Replace(",", string.Empty);
            var words = input.Split(' ');
            var output = new List<string>();

            foreach (var word in words)
            {
                if (this.mappedWords.ContainsKey(word))
                {
                    output.Add(this.mappedWords[word]);
                }
                else
                {
                    var outputWord = string.Empty;
                    foreach (var ch in word)
                    {
                        var outputCh = this.MappedCharacters.ContainsKey(ch) ? this.MappedCharacters[ch] : ch.ToString();
                        if (this.random.Next(0, 2) == 0) outputCh = outputCh.ToUpper();
                        outputWord += outputCh;
                    }

                    output.Add(outputWord);
                }
            }

            return string.Join(" ", output);
        }

        /// <summary>
        /// Set character mapping.
        /// </summary>
        protected abstract void SetCharacterMapping();

        private void SetWordMapping()
        {
            this.mappedWords.Add("leet", @"1337");
            this.mappedWords.Add("bye", @"bai");
            this.mappedWords.Add("and", @"nd");
            this.mappedWords.Add("dude", @"d00d");
            this.mappedWords.Add("girl", @"gurl");
            this.mappedWords.Add("guys", @"guise");
            this.mappedWords.Add("hacks", @"h4x0rz");
            this.mappedWords.Add("hi", @"hai");
            this.mappedWords.Add("cool", @"kewl");
            this.mappedWords.Add("ok", @"kk");
            this.mappedWords.Add("fuck", @"fuq");
            this.mappedWords.Add("the", @"teh");
            this.mappedWords.Add("sucks", @"sux");
            this.mappedWords.Add("owo", @"r4wr");
            this.mappedWords.Add("uwu", @"r4wr");
            this.mappedWords.Add("like", @"liek");
            this.mappedWords.Add("smart", @"smat");
            this.mappedWords.Add("mate", @"m8");
            this.mappedWords.Add("power", @"powwah");
            this.mappedWords.Add("porn", @"pr0n");
            this.mappedWords.Add("what", @"wut");
            this.mappedWords.Add("you", @"u");
            this.mappedWords.Add("are", @"r");
            this.mappedWords.Add("why", @"y");
            this.mappedWords.Add("yes", @"yass");
            this.mappedWords.Add("yeah", @"ya");
            this.mappedWords.Add("rocks", @"roxx0rs");
            this.mappedWords.Add("yay", @"w00t");
            this.mappedWords.Add("whatever", @"w/e");
        }
    }
}
