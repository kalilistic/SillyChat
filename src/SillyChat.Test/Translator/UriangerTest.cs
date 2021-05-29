using Xunit;

namespace SillyChat.Test
{
    /// <summary>
    /// Urianger Test.
    /// </summary>
    public class UriangerTest
    {
        /// <summary>
        /// Test translation.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="translation"></param>
        [Theory]
        [InlineData("cat", "Cat.")]
        [InlineData("Hi, how are you?", @"Hail, how art thou?")]
        public void StringsAreTranslatedToUrianger(string input, string translation)
        {
            var plugin = new SillyChatPluginMock();
            var translator = new UriangerTranslator(plugin);
            Assert.Equal(translation, translator.Translate(input));
        }
        
    }
}
