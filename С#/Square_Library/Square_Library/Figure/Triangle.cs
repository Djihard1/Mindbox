using Square_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Square_Library.Figure
{
   public class Triangle : IFigure
    {
        public double Side1 { get; set; }
        public double Side2 { get; set; }
        public double Side3 { get; set; }
        public double p { get; set; }


        public double Square
        {
            get
            {
                var p = (Side1 + Side2 + Side3) / 2;
                return Math.Sqrt(p * (p - Side1) * (p - Side2) * (p - Side3));
            }
        }
        public Triangle()
        {
            Side1 = Side2 = Side3 = default(int);
        }

        public Triangle(int side1, int side2, int side3)
        {
            Side1 = side1;
            Side2 = side2;
            Side3 = side3;
        }

        public bool getIsRightTriangle()
        {
           if (Side1 == Math.Sqrt(Math.Pow(Side2, 2) + Math.Pow(Side3, 2))||
                Side2 == Math.Sqrt(Math.Pow(Side1, 2) + Math.Pow(Side3, 2))||
                Side3 == Math.Sqrt(Math.Pow(Side1, 2) + Math.Pow(Side2, 2)))
                {
                return true;
            
            
            }
            else
            {
                return false;
            }
        }






    }





}
