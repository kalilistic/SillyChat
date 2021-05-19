using System.Collections.Generic;
using System.Linq;

namespace SillyChat
{
    /// <summary>
    /// Translation mode.
    /// </summary>
    public class TranslationMode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationMode"/> class.
        /// </summary>
        /// <param name="code">translation code for config/toggle.</param>
        /// <param name="name">translation name for display.</param>
        /// <param name="translator">translator class.</param>
        public TranslationMode(int code, string name, BaseTranslator translator)
        {
            this.Code = code;
            this.Name = name;
            this.Translator = translator;
        }

        /// <summary>
        /// Gets translator code.
        /// </summary>
        public int Code { get; }

        /// <summary>
        /// Gets translator name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets translator class.
        /// </summary>
        public BaseTranslator Translator { get; }
    }
}
