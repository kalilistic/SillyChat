using Xunit;

namespace SillyChat.Test
{
    /// <summary>
    /// Advanced Leet Test.
    /// </summary>
    public class PigLatinTest
    {
        /// <summary>
        /// Test translation.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="translation"></param>
        [Theory]
        [InlineData("cat", "atcay")]
        [InlineData("Hi, how are you?", @"Ihay, owhay areway ouyay?")]
        public void StringsAreTranslatedToPigLatin(string input, string translation)
        {
            var plugin = new SillyChatPluginMock();
            var translator = new PigLatinTranslator(plugin);
            Assert.Equal(translation, translator.Translate(input));
        }
        
    }
}
