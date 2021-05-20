using Xunit;

namespace SillyChat.Test
{
    /// <summary>
    /// Intermediate Leet Test.
    /// </summary>
    public class IntermediateLeetTest
    {
        /// <summary>
        /// Test translation.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="translation"></param>
        [Theory]
        [InlineData("cat", "(47")]
        [InlineData("Hi, how are you?", @"hai |-|0\/\/ r '/0|_|?")]
        public void StringsAreTranslatedToIntermediateLeet(string input, string translation)
        {
            var plugin = new SillyChatPluginMock();
            var translator = new IntermediateLeetTranslator(plugin);
            Assert.Equal(translation, translator.Translate(input).ToLower());
        }
        
    }
}
