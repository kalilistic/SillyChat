using Xunit;

namespace SillyChat.Test
{
    /// <summary>
    /// Owo Test.
    /// </summary>
    public class OwoTest
    {
        /// <summary>
        /// Test translation.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="translation"></param>
        [Theory]
        [InlineData("Hi, how are you?", @"Hi, how awe you?")]
        public void StringsAreTranslatedToOwo(string input, string translation)
        {
            var plugin = new SillyChatPluginMock();
            var translator = new OwoTranslator(plugin);
            Assert.Equal(translation, translator.Translate(input));
        }
        
    }
}
