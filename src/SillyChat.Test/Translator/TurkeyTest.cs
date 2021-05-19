using Xunit;

namespace SillyChat.Test
{
    /// <summary>
    /// Advanced Leet Test.
    /// </summary>
    public class TurkeyTest
    {
        /// <summary>
        /// Test translation.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="translation"></param>
        [Theory]
        [InlineData("cat", "gobble")]
        [InlineData("Hello, how are you?", @"Gobble, gobble gobble gobble?")]
        public void StringsAreTranslatedToTurkey(string input, string translation)
        {
            var plugin = new SillyChatPluginMock();
            var translator = new TurkeyTranslator(plugin);
            Assert.Equal(translation, translator.Translate(input));
        }
        
    }
}
