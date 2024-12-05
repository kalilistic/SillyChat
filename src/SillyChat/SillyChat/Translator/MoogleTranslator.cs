using System;
using System.Collections.Generic;
using System.Linq;

namespace SillyChat
{
    /// <summary>
    /// Translate to toad.
    /// </summary>
    public class MoogleTranslator : BaseTranslator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoogleTranslator"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public MoogleTranslator(ISillyChatPlugin plugin)
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
                        input += " Kupo.";
                        break;
                    case 3:
                        input += " Kupo...";
                        break;
                    case 2:
                        input += " Kupo!";
                        break;
                    default:
                        input += " Kupo.";
                        break;
                }
            }
            else
            {
                switch (rnd)
                {
                    case 1:
                        input += ", kupo.";
                        break;
                    case 3:
                        input += ", kupo...";
                        break;
                    case 2:
                        input += ", kupo!";
                        break;
                    default:
                        input += ", kupo.";
                        break;
                }
            }


            return input;
        }
    }
}
