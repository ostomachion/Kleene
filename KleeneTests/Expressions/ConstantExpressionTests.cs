using System;
using Xunit;
using Kleene;

namespace KleeneTests
{
    public class ConstantExpressionTests
    {
        public class Constructor
        {
            [Fact]
            public void BasicConstructorChar()
            {
                // When
                var expression = new ConstantExpression<char>('c');

                // Then
                Assert.Equal('c', expression.Value);
            }
        }

        public class Run
        {
            [Theory]
            [InlineData('c')]
            [InlineData('X')]
            [InlineData('9')]
            [InlineData('#')]
            [InlineData(' ')]
            [InlineData('\n')]
            [InlineData('\0')]
            [InlineData('μ')]
            public void ConstantStructureChar_Run(char value)
            {
                // Given
                var expression = new ConstantExpression<char>(value);

                // When
                var result = expression.Run();

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.Equal(value, branch.Value);
                    });
            }

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(-2)]
            [InlineData(Int32.MaxValue)]
            [InlineData(Int32.MinValue)]
            public void ConstantStructureInt_Run(int value)
            {
                // Given
                var expression = new ConstantExpression<int>(value);

                // When
                var result = expression.Run();

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.Equal(value, branch.Value);
                    });
            }

            [Theory]
            [InlineData("")]
            [InlineData("foo")]
            [InlineData("Hello, world!")]
            public void ConstantStructureString_Run(string value)
            {
                // Given
                var expression = new ConstantExpression<string>(value);

                // When
                var result = expression.Run();

                // Then
                Assert.Collection(result,
                    branch =>
                    {
                        Assert.Equal(value, branch.Value);
                    });
            }
        }
    }
}
