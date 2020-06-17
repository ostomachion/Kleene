using System;
using Xunit;
using Kleene;
using System.Linq;
using System.Collections.Generic;

namespace KleeneTests
{
    public class ConstantExpressionTests
    {
        [Fact]
        public void NullInput_Throws() {
            // Given
            var expression = new ConstantExpression<char>('x');
            IEnumerable<Structure> input = null!;

            // Then
            Assert.Throws<ArgumentNullException>(() => {
                expression.Run(input, 0).ToList();
            });
        }
        
        [Theory]
        [InlineData('c')]
        [InlineData('X')]
        [InlineData('9')]
        [InlineData('#')]
        [InlineData(' ')]
        [InlineData('\n')]
        [InlineData('\0')]
        [InlineData('μ')]
        public void RunOnSameChar_ReturnsSameChar(char c)
        {
            // Given
            var expression = new ConstantExpression<char>(c);
            var input = new [] { new ConstantStructure<char>(c) };
            
            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => Assert.Equal(c, ((ConstantStructure<char>)item).Value)));
        }

        [Theory]
        [InlineData('c')]
        [InlineData('X')]
        [InlineData('9')]
        [InlineData('#')]
        [InlineData(' ')]
        [InlineData('\n')]
        [InlineData('\0')]
        [InlineData('μ')]
        public void RunOnStartsWithSameChar_ReturnsSameChar(char c)
        {
            // Given
            var expression = new ConstantExpression<char>(c);
            var input = new [] { c, 'T', 'e', 's', 't', '.' }.Select(x => new ConstantStructure<char>(x));
            
            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Collection(result,
                branch => Assert.Collection(branch,
                    item => Assert.Equal(c, ((ConstantStructure<char>)item).Value)));
        }

        [Theory]
        [InlineData('c')]
        [InlineData('X')]
        [InlineData('9')]
        [InlineData('#')]
        [InlineData(' ')]
        [InlineData('\n')]
        [InlineData('\0')]
        [InlineData('μ')]
        public void RunOnEmpty_ReturnsNothing(char c)
        {
            // Given
            var expression = new ConstantExpression<char>(c);
            var input = Enumerable.Empty<ConstantStructure<char>>();
            
            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Empty(result);
        }

        [Theory]
        [InlineData('c', "foo")]
        [InlineData('o', "foo")]
        [InlineData('X', " X")]
        [InlineData('9', "n")]
        [InlineData('#', "*****")]
        [InlineData(' ', "Hello, world!")]
        [InlineData('\n', " ")]
        [InlineData('\0', " \\ 0 ")]
        [InlineData('μ', "mu")]
        public void RunOnStartsWithDifferentChar_ReturnsNothing(char c, string inputString)
        {
            // Given
            Assert.NotEmpty(inputString);
            Assert.NotEqual(c, inputString.First());
            var input = inputString.Select(c => new ConstantStructure<char>(c));
            var expression = new ConstantExpression<char>(c);
            
            // When
            var result = expression.Run(input, 0);

            // Then
            Assert.Empty(result);
        }
    }
}