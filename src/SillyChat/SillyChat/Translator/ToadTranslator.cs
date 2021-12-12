using System.Collections.Generic;
using System.Linq;

namespace SillyChat
{
    /// <summary>
    /// Translate to toad.
    /// </summary>
    public class ToadTranslator : BaseTranslator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToadTranslator"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public ToadTranslator(ISillyChatPlugin plugin)
            : base(plugin)
        {
        }

        /// <inheritdoc />
        public override string Translate(string input)
        {
            var words = input.Split(' ');
            var output = new List<string>();
            foreach (var word in words)
            {
                string ribbit = char.IsUpper(word.First()) ? "Ribbit" : "ribbit";
                if (char.IsPunctuation(word.Last()))
                {
                    ribbit += word.Last();
                }

                output.Add(ribbit);
            }

            return string.Join(" ", output);
        }
    }
}
