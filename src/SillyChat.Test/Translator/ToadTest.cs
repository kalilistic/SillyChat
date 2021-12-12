using Xunit;

namespace SillyChat.Test
{
    /// <summary>
    /// Toad Test.
    /// </summary>
    public class ToadTest
    {
        /// <summary>
        /// Test translation.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="translation"></param>
        [Theory]
        [InlineData("cat", "ribbit")]
        [InlineData("Hi, how are you?", @"Ribbit, ribbit ribbit ribbit?")]
        public void StringsAreTranslatedToToad(string input, string translation)
        {
            var plugin = new SillyChatPluginMock();
            var translator = new ToadTranslator(plugin);
            Assert.Equal(translation, translator.Translate(input));
        }
        
    }
}
