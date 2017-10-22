namespace DefineAClassPerson
{
    class MathUtilities
    {
        public static double Sum(double firstNum, double secondNum)
        {
            return firstNum + secondNum;
        }

        public static double Subtract(double firstNum, double secondNUm)
        {
            return firstNum - secondNUm;
        }

        public static double Multiply(double firstNum, double secondNUm)
        {
            return firstNum * secondNUm;
        }

        public static double Divide(double dividend, double divisor)
        {
            return dividend / divisor;
        }

        public static double Percentage(double total, double percentage)
        {
            return Divide(Multiply(total, percentage), 100);
        }
    }
}
