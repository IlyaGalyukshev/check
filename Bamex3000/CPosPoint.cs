using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bamex3000
{
    class CPosPoint
    {
        public double startPosX, startPosY, finPosX, finPosY;
        public string name;

        public CPosPoint(double s_startPosX, double s_startPosY, double s_finPosX, double s_finPosY, string s_name)
        {
            startPosX = s_startPosX;
            startPosY = s_startPosY;
            finPosX = s_finPosX;
            finPosY = s_finPosY;
             name = s_name;

        }
        public double getY()
        {
            double result = startPosY - finPosY;
            if (result < 0) result = result * -1;
            return result;
        }
        public double getX()
        {
            if (startPosX < finPosX) return startPosX;
            return finPosX;


        }

    }
}
