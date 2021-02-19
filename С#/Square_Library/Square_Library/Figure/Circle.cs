using Square_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Square_Library.Figure
{
  public  class Circle : IFigure
    {
        // Вычисление площади круга по радиусу.
        public double Radius { get; set; }
        public double Square => Math.PI * Math.Pow(Radius, 2);

        public Circle()
        {
            Radius = default(int);
        }
        public Circle(double radius)
        {
            Radius = radius;
        }
    }
}
