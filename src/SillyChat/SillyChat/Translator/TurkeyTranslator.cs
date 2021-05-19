using System.Collections.Generic;
using System.Linq;

namespace SillyChat
{
    /// <summary>
    /// Translate to turkey.
    /// </summary>
    public class TurkeyTranslator : BaseTranslator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TurkeyTranslator"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public TurkeyTranslator(ISillyChatPlugin plugin)
            : base(plugin)
        {
        }

        /// <inheritdoc />
        public override string Translate(string input)
        {
            var words = input.Split(' ');
            var output = new List<string>();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var word in words)
            {
                string gobble = char.IsUpper(word.First()) ? "Gobble" : "gobble";
                if (char.IsPunctuation(word.Last()))
                {
                    gobble += word.Last();
                }

                output.Add(gobble);
            }

            return string.Join(" ", output);
        }
    }
}
