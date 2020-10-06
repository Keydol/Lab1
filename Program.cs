using System;

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
         static double derivativeFunction(double x)
        {
            return 3 * Math.Pow(x, 2) + 6 * x - 24;
        }
          static double derivativeTranscendentalFunction(double x)
        {
            return 3 +(1.2 / Math.Pow(Math.Cos(1.2 * x),2));
        }
        static double secondDerivativeFunction(double x)
        {
            return 6 * x + 6;
        }
        static double secondDerivativeTranscendentalFunction(double x)
        {
            return 2.88 * (Math.Sin(1.2 * x)/Math.Cos(1.2 * x));
        }

        static void Main(string[] args)
        {
            double x1;
            double x2;
            double E = 1e-3; //точність
            string arithmeticEquation = "x^3 + 3x^2 - 24x + 1 = 0";
            string transcendentalEquation = "tg(1,2x) - 2 + 3x = 0";

            Console.WriteLine("======== Алгебраїчне рiвняння x^3 + 3x^2 - 24x + 1 ========");
            x1 = 3;
            x2 = 4;    
            Console.WriteLine($"======== Уточнення коренiв на вiдрiзку [{x1} ; {x2}] ========\n");
            //methodKombinovanyi(x1, x2, E, f1);
            methodDyhotomii(x1, x2, E, f1);
            methodHord(x1, x2, E, f1);
            methodNewton(x1, x2, E, f1, derivativeFunction, secondDerivativeFunction,arithmeticEquation); 
            methodIterracii(x1, x2, E, f1, derivativeFunction);

            Console.WriteLine("\n\n======== Трансцендентне рiвняння tg(1,2x) - 2 + 3x ========");
            x1 = 0;
            x2 = 1;
            Console.WriteLine($"======== Уточнення коренiв на вiдрiзку [{x1} ; {x2}] ========\n");
            //methodKombinovanyi(x1, x2, E, f2);
            methodDyhotomii(x1, x2, E, f2);
            methodHord(x1, x2, E, f2);
            methodNewton(x1, x2,E, f2, derivativeTranscendentalFunction, secondDerivativeTranscendentalFunction,transcendentalEquation);
            methodIterracii(x1, x2, E, f2, derivativeTranscendentalFunction);
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
        static void methodNewton(double x1, double x2, double E, function functionTask,function derivativeFunction, function secondDerivativeFunction,String equation)
        {
            Console.WriteLine("\t== Метод Ньютона (дотичних) ==\n");
            int iterator = 1;
            double initValueX;
            double iValueX;
            double a = x1;
            double b = x2;
            double e = E;
            if (functionTask(a) * functionTask(b) > 0) // Якщо знаки функції на краях відрізків однакові, то функція коренів немає
            {
                Console.WriteLine("На даному iнтервалi [{0};{1}] рiвняння {2} немає розв'язкiв",a,b,equation);
            }
            else
            {
                initValueX = functionTask(a) * secondDerivativeFunction(a) > 0 ? a : b; // Визначаємо нерухомий кінець та задаємо початкове значення
                iValueX = initValueX - functionTask(initValueX) / derivativeFunction(initValueX); // Визначаємо перше наближення
                Console.WriteLine("Поточна iтерацiя {0} = {1}", iterator, iValueX);
                while (Math.Abs(initValueX - iValueX) > e) // Поки різниця по модулю між коренями не стане меншою за точність e
                {
                    iterator++;
                    initValueX = iValueX;
                    iValueX = initValueX - functionTask(initValueX) / derivativeFunction(initValueX);
                    Console.WriteLine("Поточна iтерацiя {0} = {1}", iterator, iValueX);
                }
                Console.WriteLine($" x = {Math.Round(iValueX, 3)}");
                Console.WriteLine($" Кiлькiсть iтерацiй = {iterator}\n");
            }
        }
        static void methodIterracii(double x1, double x2, double E, function f1, function derivateFunction)
            {
                Console.WriteLine("\t== Метод простої iтерацiї ==\n");
         
                double min = derivateFunction(x1);
                double max = derivateFunction(x2);
                double lmb;
                double X0 = x1;
                int iterator=1;
                
                if (min != 0)
                {
                    lmb = 2 / (min + max);
                }
                else lmb = 1 / max;
                double q = 1 - lmb;
                double X = Phi(X0, lmb,f1);
            
                if (q <= 0.5)
                {
                    Console.WriteLine("Поточна iтерацiя {0} = {1}", iterator, X);
                    while (Math.Abs(X - X0) > ((1 - q) / q) * E)
                    {
                        iterator++;
                        X0 = X;
                        X = Phi(X0, lmb,f1);
                        Console.WriteLine("Поточна iтерацiя {0} = {1}", iterator, X);
                    }
                }
                else
                {
                    Console.WriteLine("Поточна iтерацiя {0} = {1}", iterator, X);
                    if (q > 0.5 && q < 1)
                    {
                        while (Math.Abs(X - X0) > E)
                        {
                            iterator++;
                            X0 = X;
                            X = Phi(X0, lmb, f1);
                            Console.WriteLine("Поточна iтерацiя {0} = {1}", iterator, X);
                        }
                    }
                }
                Console.WriteLine($" x = {Math.Round(X, 3)}");
                Console.WriteLine($" Кiлькiсть iтерацiй = {iterator}\n");

            }   
            
            static double Phi(double x, double lmb, function f)
            {
                 return x - lmb * f(x);
            }
    }
}
