using Xunit;

namespace SillyChat.Test
{
    /// <summary>
    /// Advanced Leet Test.
    /// </summary>
    public class AdvancedLeetTest
    {
        /// <summary>
        /// Test translation.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="translation"></param>
        [Theory]
        [InlineData("", "")]
        [InlineData("cat", "(@¯|¯")]
        [InlineData("hi, how are you?", @"hai|-|()\/\/r'/()|_|?")]
        public void StringsAreTranslatedToAdvancedLeet(string input, string translation)
        {
            var plugin = new SillyChatPluginMock();
            var translator = new AdvancedLeetTranslator(plugin);
            Assert.Equal(translation, translator.Translate(input).ToLower());
        }
        
    }
}
