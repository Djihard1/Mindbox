using Square_Library.Figure;
using System;
using Xunit;

namespace XUnitTest
{
    public class UnitTest
    {
        [Fact]
        public void CircleWIthValue()
        {
            Circle circle = new Circle(99);
            var expectedSquare = Math.PI * Math.Pow(99, 2);
            var actualSquare = circle.Square;
            Assert.Equal(expectedSquare, actualSquare);

        }


        [Fact]
        public void CircleNotValue()
        {
            Circle circle = new Circle();
            var expectedSquare = 0;
            var actualSquare = circle.Square;
            Assert.Equal(expectedSquare, actualSquare);

        }

        [Fact]
        public void TriangleNotValue()
        {
            Triangle triangle = new Triangle();
            var expectedSquare = 0;
            var actualSquare = triangle.Square;
            Assert.Equal(expectedSquare, actualSquare);


        }

        [Fact]
        public void TriangleWIthValues()
        {
            Triangle triangle = new Triangle(5, 4, 2);
            var p = (5 + 4 + 2) / 2;
            var expectedSquare = 3.799671038392666;
            var actualSquare = triangle.Square;
            Assert.Equal(expectedSquare, actualSquare);

        }

        [Fact]
        public void getIsRightTriangle()
        {
            Triangle triangle = new Triangle(5, 4, 2);
            bool isRight = triangle.getIsRightTriangle();
            Assert.False(isRight);

        }

        [Fact]
        public void unknownFigure()
        {
            Figure figure = new Figure(new Circle(66));
            var expectedSquare = Math.PI * Math.Pow(66, 2);
            var actualSquare = figure.Square;
            Assert.Equal(expectedSquare, actualSquare);

        }

    

    }
}
