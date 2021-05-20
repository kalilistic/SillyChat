using Xunit;

namespace SillyChat.Test
{
    /// <summary>
    /// Basic Leet Test.
    /// </summary>
    public class BasicLeetTest
    {
        /// <summary>
        /// Test translation.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="translation"></param>
        [Theory]
        [InlineData("cat", "c4t")]
        [InlineData("Hi, how are you?", "hai h0w r y0u?")]
        public void StringsAreTranslatedToBasicLeet(string input, string translation)
        {
            var plugin = new SillyChatPluginMock();
            var translator = new BasicLeetTranslator(plugin);
            Assert.Equal(translation, translator.Translate(input).ToLower());
        }
        
    }
}
