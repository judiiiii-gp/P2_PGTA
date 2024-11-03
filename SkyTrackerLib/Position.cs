using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTrackerLib
{
    public class Position
    {
        // Atributes
        // 2D coordintaes will be used to compute
        // the position
        double x, y;
        
        // Structs
        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // Methods
        public double getX() { 
            // Retuns th X atribute
            return x; 
        }
        public double getY()
        {
            // Retuns th Y atribute
            return y;
        }
    }
}
