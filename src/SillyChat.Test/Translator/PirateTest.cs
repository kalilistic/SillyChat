using Xunit;

namespace SillyChat.Test
{
    /// <summary>
    /// Advanced Leet Test.
    /// </summary>
    public class PirateTest
    {
        /// <summary>
        /// Test translation.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="translation"></param>
        [Theory]
        [InlineData("cat", "parrot")]
        [InlineData("Hi, how are you?", @"Ahoy, how be ye?")]
        public void StringsAreTranslatedToPirate(string input, string translation)
        {
            var plugin = new SillyChatPluginMock();
            var translator = new PirateTranslator(plugin);
            Assert.Equal(translation, translator.Translate(input));
        }
        
    }
}
