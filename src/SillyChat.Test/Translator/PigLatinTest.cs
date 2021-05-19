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
        [InlineData("", "")]
        [InlineData("cat", "atcay")]
        [InlineData("Hello, how are you?", @"Ellohay, owhay areway ouyay?")]
        public void StringsAreTranslatedToPigLatin(string input, string translation)
        {
            var plugin = new SillyChatPluginMock();
            var translator = new PigLatinTranslator(plugin);
            Assert.Equal(translation, translator.Translate(input));
        }
        
    }
}
