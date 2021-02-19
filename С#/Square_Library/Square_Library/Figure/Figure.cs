using Square_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Square_Library.Figure
{
    // Без знания типа фигуры.
public  class Figure : IFigure
    {
        private IFigure figure;

        public double Square => figure.Square;

        public Figure (IFigure area)
        {
            figure = area;

        }
    }
}
