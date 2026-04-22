using System;

namespace Lab2.App.Task1;

public static class PrimeMath
{
    // Общий метод проверки на простоту для всех версий (по условию он не должен меняться).
    public static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        if (number == 2)
        {
            return true;
        }

        if (number % 2 == 0)
        {
            return false;
        }

        var limit = (int)Math.Sqrt(number);
        for (var divisor = 3; divisor <= limit; divisor += 2)
        {
            if (number % divisor == 0)
            {
                return false;
            }
        }

        return true;
    }
}
