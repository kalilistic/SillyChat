using System;
using System.Text.RegularExpressions;

namespace SillyChat
{
    /// <summary>
    /// Translate to owo.
    /// </summary>
    public class OwoTranslator : BaseTranslator
    {
        private readonly Random rng = new Random();
        private readonly string[] faces = { "(・`ω´・)", ";;w;;", "owo", "UwU", ">w<", "^w^" };

        /// <summary>
        /// Initializes a new instance of the <see cref="OwoTranslator"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public OwoTranslator(ISillyChatPlugin plugin)
            : base(plugin)
        {
        }

        /// <inheritdoc />
        public override string Translate(string input)
        {
            input = Regex.Replace(input, "(?:r|l)", "w");
            input = Regex.Replace(input, "(?:R|L)", "W");
            input = Regex.Replace(input, "n([aeiou])", "ny$1");
            input = Regex.Replace(input, "N([aeiou])", "Ny$1");
            input = Regex.Replace(input, "N([AEIOU])", "NY$1");
            input = Regex.Replace(input, "ove", "uv");
            input = Regex.Replace(input, "!+", " " + this.RndFace() + " ");

            return input;
        }

        private string RndFace() => this.faces[this.rng.Next(0, this.faces.Length - 1)];
    }
}
