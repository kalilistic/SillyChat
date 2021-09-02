using System;
using System.Collections.Generic;
using System.Linq;

using Dalamud.DrunkenToad;

namespace SillyChat
{
    /// <summary>
    /// Orchestrate translation process.
    /// </summary>
    public class TranslationService
    {
        private readonly List<TranslationMode> translationModes;
        private BaseTranslator translator = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslationService"/> class.
        /// </summary>
        /// <param name="plugin">SillyChat plugin.</param>
        public TranslationService(ISillyChatPlugin plugin)
        {
            this.translationModes = GetModes(plugin);
            this.SetTranslationMode(plugin.Configuration.TranslationMode);
        }

        /// <summary>
        /// Translate.
        /// </summary>
        /// <param name="input">untranslated to text.</param>
        /// <returns>translated text.</returns>
        public string Translate(string input)
        {
            return this.translator.Translate(input);
        }

        /// <summary>
        /// Set translation mode.
        /// </summary>
        /// <param name="code">translation mode code.</param>
        public void SetTranslationMode(int code)
        {
            this.translator = this.GetTranslationMode(code).Translator;
        }

        /// <summary>
        /// Get translation mode names.
        /// </summary>
        /// <returns>array of translation mode names.</returns>
        public string[] GetTranslationModeNames()
        {
            return this.translationModes.Select(x => x.Name).ToArray();
        }

        private static List<TranslationMode> GetModes(ISillyChatPlugin plugin)
        {
            var translationModes = new List<TranslationMode>
            {
                new (0, "Leet (L1)", new BasicLeetTranslator(plugin)),
                new (1, "Leet (L2)", new IntermediateLeetTranslator(plugin)),
                new (2, "Leet (L3)", new AdvancedLeetTranslator(plugin)),
                new (3, "Pig Latin", new PigLatinTranslator(plugin)),
                new (4, "Pirate", new PirateTranslator(plugin)),
                new (5, "Turkey", new TurkeyTranslator(plugin)),
                new (6, "Urianger", new UriangerTranslator(plugin)),
            };
            return translationModes.OrderBy(x => x.Name).ToList();
        }

        private TranslationMode GetTranslationMode(int code)
        {
            try
            {
                return this.translationModes.First(x => x.Code == code);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to get translation mode.");
                return this.translationModes.First();
            }
        }
    }
}
