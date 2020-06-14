using System;
using Xunit;
using Kleene;

namespace KleeneTests
{
    public class SequenceExpressionTests
    {
        public class Constructor
        {
        }

        public class Run
        {
            [Theory]
            [InlineData('f', 'o')]
            [InlineData(' ', '\t')]
            public void TwoChars_ReturnsChars(char c1, char c2)
            {
                // Given
                var expression = new SequenceExpression(new[] {
                    new ConstantExpression<char>(new ConstantStructure<char>(c1)),
                    new ConstantExpression<char>(new ConstantStructure<char>(c2)),
                });

                var input = new SequenceStructure(new [] {
                    new ConstantStructure<char>(c1),
                    new ConstantStructure<char>(c2),
                });

                // When
                var result = expression.Run<SequenceStructure>(input);

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        // TODO:
                        throw new NotImplementedException(); // TODO:
                    });
            }
        }
    }
}
