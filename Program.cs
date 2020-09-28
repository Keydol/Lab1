﻿using System;

namespace Lab1
{
    class Program
    {
        delegate double function(double x); 

        static double f1(double x) 
        {
            return Math.Pow(x, 3) + 3 * Math.Pow(x, 2) - 24 * x + 1;
        }

        static double f2(double x) 
        {
            return Math.Tan(1.2 * x) - 2 + 3 * x;
        }

        static void Main(string[] args)
        {
            double x1;
            double x2;
            double E = 1e-3; //точність

            Console.WriteLine("======== Алгебраїчне рiвняння x^3 + 3x^2 - 24x + 1 ========");
            x1 = 3;
            x2 = 4;    
            Console.WriteLine($"======== Уточнення коренiв на вiдрiзку [{x1} ; {x2}] ========\n");
            //methodKombinovanyi(x1, x2, E, f1);
            methodDyhotomii(x1, x2, E, f1);
            methodHord(x1, x2, E, f1);
            


            Console.WriteLine("\n\n======== Трансцендентне рiвняння tg(1,2x) - 2 + 3x ========");
            x1 = 0;
            x2 = 1;
            Console.WriteLine($"======== Уточнення коренiв на вiдрiзку [{x1} ; {x2}] ========\n");
            //methodKombinovanyi(x1, x2, E, f2);
            methodDyhotomii(x1, x2, E, f2);
            methodHord(x1, x2, E, f2);
            Console.ReadLine();
        }

        static void methodKombinovanyi(double x1, double x2, double E, function f)
        {
            Console.WriteLine("\t== Комбiнований метод ==\n");
        }

        static void methodDyhotomii(double x1, double x2, double E, function f)
        {
            Console.WriteLine("\t== Метод дихотомiї ==\n");
            int i = 0;
            double x = x1;
            double xLast = x;
            double xTo = x2;
            double dx = double.MaxValue;

            while (Math.Abs(dx) > 2*E)
            {
                i++;
                x = (xTo + xLast) / 2;
                if (f(xTo) * f(x) < 0) xLast = x;
                else if (f(xTo) * f(x) == 0) break;
                else xTo = x;
                dx = xTo - xLast;
            }

            Console.WriteLine($" x = {Math.Round(x, 3)}");
            Console.WriteLine($" Кiлькiсть iтерацiй = {i}\n");
        }

        static void methodHord(double x1, double x2, double E, function f)
        {
            Console.WriteLine("\t== Метод хорд ==\n");
            int    i = 0;
            double x_next = x1;
            double x_curr = x2;
            double x_last;
            double dx = double.MaxValue;

            while (Math.Abs(dx) > E)
            {
                i++;
                x_last = x_curr;
                x_curr = x_next;
                x_next -= f(x_curr) * (x_curr - x_last) / (f(x_curr) - f(x_last));
                dx = x_next - x_curr;
            }

            Console.WriteLine($" x = {Math.Round(x_next, 3)}");
            Console.WriteLine($" Кiлькiсть iтерацiй = {i}\n");
        }
    }
}