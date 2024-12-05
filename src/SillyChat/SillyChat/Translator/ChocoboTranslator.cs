using System;
using System.Collections.Generic;
using System.Linq;

namespace SillyChat
{
    /// <summary>
    /// Translate to toad.
    /// </summary>
    public class ChocoboTranslator : BaseTranslator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChocoboTranslator"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public ChocoboTranslator(ISillyChatPlugin plugin)
            : base(plugin)
        {
        }

        /// <inheritdoc />
        public override string Translate(string input)
        {
            Random random = new Random();
            int rnd = random.Next(1, 3);

            if (input.EndsWith(".") || input.EndsWith(",") || input.EndsWith("!") || input.EndsWith("?"))
            {
                switch (rnd)
                {
                    case 1:
                        input += " Kweh.";
                        break;
                    case 3:
                        input += " Kweh...";
                        break;
                    case 2:
                        input += " Kweh!";
                        break;
                    default:
                        input += " Kweh.";
                        break;
                }
            }
            else
            {
                switch (rnd)
                {
                    case 1:
                        input += ", kweh.";
                        break;
                    case 3:
                        input += ", kweh...";
                        break;
                    case 2:
                        input += ", kweh!";
                        break;
                    default:
                        input += ", kweh.";
                        break;
                }
            }


            return input;
        }
    }
}
