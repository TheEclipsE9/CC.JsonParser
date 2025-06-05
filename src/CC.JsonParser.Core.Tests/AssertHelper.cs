namespace CC.JsonParser.Core.Tests
{
    public class AssertHelper
    {
        public static void AssertCountAndEachToken(
            List<Token> expectedTokens,
            List<Token> actualTokens)
        {
            Assert.Equal(expectedTokens.Count, actualTokens.Count);

            for (int i = 0; i < actualTokens.Count; i++)
            {
                var actualToken = actualTokens[i];
                var expectedToken = expectedTokens[i];

                Assert.Equal(expectedToken.TokenType, actualToken.TokenType);
                Assert.Equal(expectedToken.Value, actualToken.Value);
            }
        }
    }
}
