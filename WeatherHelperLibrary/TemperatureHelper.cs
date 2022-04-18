using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherHelperLibrary
{
    public class TemperatureHelper
    {
        //Temperature Helper
        public static double C2F(double Celsius, bool round=false)
        {
            //Fahrenheit (F) = (Celsuis x 1.8) + 32 C2F index1

            double result = (Celsius * 1.8) + 32;

            if (round) result = Math.Round(result);

            return result; 
        }

        public static double F2C(double Fahrenheit, bool round=false)
        {
            // Celsius (C) = (Fahrenheit -32) * 5/9 Index2

            double result = (Fahrenheit - 32) * 5/9;

            if (round) result = Math.Round(result);

            return result;

        }
    }
}
