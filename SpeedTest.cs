using System;
using System.Diagnostics;
using static System.Math;
using MathNet.Numerics;
using System.Numerics;

class SpeedTest
{
    static readonly int[] Factorial = [1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 479001600];
    static readonly long[] FactorialL = [1L, 1L, 2L, 6L, 24L, 120L, 720L, 5040L, 40320L, 362880L, 3628800L, 39916800L,
                                  479001600L, 6227020800L, 87178291200L, 1307674368000L, 20922789888000L,
                                  355687428096000L, 6402373705728000L, 121645100408832000L, 2432902008176640000L];
    static void Main()
    {
        int N = 30_000_000;
        //MeasureExecutionTime(() => IntMultiDivide(N), nameof(IntMultiDivide));
        //MeasureExecutionTime(() => IntLeftRightShift(N), nameof(IntLeftRightShift));
        //Console.WriteLine();
        //MeasureExecutionTime(() => DoubleDivide(N), nameof(DoubleDivide));
        //MeasureExecutionTime(() => DoubleMulti(N), nameof(DoubleMulti));
        //MeasureExecutionTime(() => FloatMulti(N), nameof(FloatMulti));
        //Console.WriteLine();
        //MeasureExecutionTime(() => DonotStoreResult(N), nameof(DonotStoreResult));
        //MeasureExecutionTime(() => StoreResult(N), nameof(StoreResult));
        //MeasureExecutionTime(() => StoreResultFloat(N), nameof(StoreResultFloat));
        //Console.WriteLine();
        //MeasureExecutionTime(() => FloorCeilRound(N), nameof(FloorCeilRound));
        //MeasureExecutionTime(() => IntFloorCeilRound(N), nameof(IntFloorCeilRound));
        //Console.WriteLine();
        //MeasureExecutionTime(() => TestQuadratic(N), nameof(TestQuadratic));
        //MeasureExecutionTime(() => TestMyQuadratic(N), nameof(TestMyQuadratic));
        //Console.WriteLine();

        int n1 = 541;
        MeasureExecutionTime(() => TestBinomial(n1), nameof(TestBinomial));
        MeasureExecutionTime(() => TestMyBinomial(n1), nameof(TestMyBinomial));
        Console.WriteLine();
        MeasureExecutionTime(() => TestMathExp(N), nameof(TestMathExp));
        MeasureExecutionTime(() => TestFastExp(N), nameof(TestFastExp));

        //for (int i = 1; i < 300; i++)
        //{
        //    for (int j = 0; j <= i; j++)
        //    {
        //        double diff = Math.Abs(MyBinomial(i, j) - SpecialFunctions.Binomial(i, j));
        //        if (diff > 1e-8)
        //        {
        //            Console.WriteLine($"not equal:{i},{j},diff:{diff}");
        //        }
        //    }
        //}

        //Console.WriteLine($"{MyBinomial(297, 5)},{SpecialFunctions.Binomial(297,5)}");
        //Console.WriteLine($"{MyBinomial(297, 6)},{SpecialFunctions.Binomial(297,6)}");
        //Console.WriteLine($"{MyBinomial(297, 7)},{SpecialFunctions.Binomial(297,7)}");
        //Console.WriteLine($"{MyBinomial(297, 8)},{SpecialFunctions.Binomial(297,8)}");
        //Console.WriteLine($"{MyBinomial(297, 9)},{SpecialFunctions.Binomial(297,9)}");
        //Console.WriteLine($"{MyBinomial(297, 10)},{SpecialFunctions.Binomial(297,10)}");
        //Console.WriteLine($"{MyBinomial(297, 11)},{SpecialFunctions.Binomial(297,11)}");
    }

    static void MeasureExecutionTime(Action testFunction, string functionName)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        testFunction();
        stopwatch.Stop();
        Console.WriteLine($"{functionName}: {stopwatch.ElapsedMilliseconds * 0.001:F3}s");
    }

    static void IntMultiDivide(int N)
    {
        long sum = 0L;
        int a, b, c, d, e, f;
        for (int i = 0; i < N; i++)
        {
            a = i * 2;
            b = i * 4;
            c = i * 8;
            d = i / 2;
            e = i / 4;
            f = i / 8;
            sum = sum + a + b - c + d + e + f;
        }
        Console.WriteLine($"sum={sum}");
    }

    static void IntLeftRightShift(int N)
    {
        long sum = 0L;
        int a, b, c, d, e, f;
        for (int i = 0; i < N; i++)
        {
            a = i << 1;
            b = i << 2;
            c = i << 3;
            d = i >> 1;
            e = i >> 2;
            f = i >> 3;
            sum = sum + a + b - c + d + e + f;
        }
        Console.WriteLine($"sum={sum}");
    }

    static void DoubleDivide(double N)
    {
        double sum = 0.0;
        double a, b, c, d, e, f, g, h;
        for (double i = 0.0; i < N; i++)
        {
            a = i / 2.0;
            b = i / 3.0;
            c = i / 4.0;
            d = i / 5.0;
            e = i / 6.0;
            f = i / 7.0;
            g = i / 8.0;
            h = i / 10.0;
            sum = sum + a + b + c - d - e - f - g - h;
        }
        Console.WriteLine($"sum={sum}");
    }

    static void DoubleMulti(double N)
    {
        double sum = 0.0;
        double a, b, c, d, e, f, g, h;
        for (double i = 0.0; i < N; i++)
        {
            //a = i * (1.0 / 2.0);
            //b = i * (1.0 / 3.0);
            //c = i * (1.0 / 4.0);
            //d = i * (1.0 / 5.0);
            //e = i * (1.0 / 6.0);
            //f = i * (1.0 / 7.0);
            //g = i * (1.0 / 8.0);
            //h = i * (1.0 / 10.0);
            a = i * 0.5;
            b = i * 0.33333333333333333;
            c = i * 0.25;
            d = i * 0.2;
            e = i * 0.16666666666666666;
            f = i * 0.14285714285714285;
            g = i * 0.125;
            h = i * 0.1;
            sum = sum + a + b + c - d - e - f - g - h;
        }
        Console.WriteLine($"sum={sum}");
    }
    static void FloatMulti(int N)
    {
        float sum = 0.0f;
        float a, b, c, d, e, f, g, h;
        // 下面for循环中的i如果定义成float，那么达到16777216时，
        // 会由于精度问题，无法自增1，导致死循环
        for (int i = 0; i < N; i++)
        {
            a = i * 0.5f;
            b = i * 0.33333333333333333f;
            c = i * 0.25f;
            d = i * 0.2f;
            e = i * 0.16666666666666666f;
            f = i * 0.14285714285714285f;
            g = i * 0.125f;
            h = i * 0.1f;
            sum = sum + a + b + c - d - e - f - g - h;
        }
        Console.WriteLine($"sum={sum}");
    }

    static void DonotStoreResult(double N)
    {
        double sum = 0.0;
        double x, y, z, a, b, c, d, e, f, delta, det, x1, x2;
        for (double i = 10.0; i < N * 0.2; i++)
        {
            // 二维坐标旋转
            x = 2.0 * Cos(i) - 3.0 * Sin(i);
            y = 2.0 * Sin(i) + 3.0 * Cos(i);
            sum = sum + x + y;

            // 球坐标化直角坐标
            x = 1.5 * Sin(i * 0.1) * Cos(2);
            y = 1.5 * Sin(i * 0.1) * Sin(2);
            z = 1.5 * Cos(i * 0.1);
            sum = sum + x + y + z;

            // 计算二元一次方程组的解
            // a * x1 + b * x2 = e
            // c * x1 + d * x2 = f
            a = Log(i + 1.0) + 1.0;
            b = i;
            c = z;
            d = i / (i + 1.0);
            e = i * 0.001;
            f = 2.0 * e + 1.0;

            det = a * d - b * c; // 行列式
            if (Abs(det) < 1e-8)
            {
                det = det + 1.0; // 此处直接加1是为了方便编程，真实场景不能这样做
            }
            x1 = (d * e - b * f) / det;
            x2 = (-c * e + a * f) / det;
            sum = sum + x1 + x2;

            // 计算二次方程 ax^2+bx+c=0的解，a,b,c沿用前面的值
            delta = b * b - 4.0 * a * c;
            if (delta < 0.0)
            {
                delta = -delta; // 此处直接反号是为了方便编程，真实场景不能这样做
            }
            x1 = (-b + Sqrt(delta)) / (2.0 * a);
            x2 = (-b - Sqrt(delta)) / (2.0 * a);
            sum = sum + 1.1 * x1 + 1.2 * x2;
        }
        Console.WriteLine($"sum={sum}");
    }

    static void StoreResult(double N)
    {
        double sum = 0.0;
        double x, y, z, a, b, c, d, e, f, delta, det, x1, x2;
        double cosi, sini;
        double cos2 = Cos(2), sin2 = Sin(2);
        for (double i = 10.0; i < N * 0.2; i++)
        {
            // 二维坐标旋转
            cosi = Cos(i);
            sini = Sin(i);
            x = 2.0 * cosi - 3.0 * sini;
            y = 2.0 * sini + 3.0 * cosi;
            sum = sum + x + y;

            // 球坐标化直角坐标
            sini = 1.5 * Sin(i * 0.1);
            x = sini * cos2;
            y = sini * sin2;
            z = 1.5 * Cos(i * 0.1);
            sum = sum + x + y + z;

            // 计算二元一次方程组的解
            // a * x1 + b * x2 = e
            // c * x1 + d * x2 = f
            a = Log(i + 1.0) + 1.0;
            b = i;
            c = z;
            d = i / (i + 1.0);
            e = i * 0.001;
            f = 2.0 * e + 1.0;

            det = a * d - b * c; // 行列式
            if (Abs(det) < 1e-8)
            {
                det = det + 1.0; // 此处直接加1是为了方便编程，真实场景不能这样做
            }
            det = 1.0 / det;
            e = e * det;
            f = f * det;
            x1 = d * e - b * f;
            x2 = -c * e + a * f;
            sum = sum + x1 + x2;

            //  计算二次方程 ax^2+bx+c=0的解，a,b,c沿用前面的值
            b = b * 0.5;
            delta = b * b - a * c;
            if (delta < 0.0)
            {
                delta = -delta; // 此处直接反号是为了方便编程，真实场景不能这样做
            }
            a = 1.0 / a;
            b = -b * a;
            c = Sqrt(delta) * a;
            x1 = b + c;
            x2 = b - c;
            sum = sum + 1.1 * x1 + 1.2 * x2;
        }
        Console.WriteLine($"sum={sum}");
    }

    static void StoreResultFloat(int N)
    {
        float sum = 0.0f;
        float x, y, z, a, b, c, d, e, f, delta, det, x1, x2;
        float cosi, sini;
        float cos2 = (float)Cos(2), sin2 = (float)Sin(2);
        for (int i = 10; i < (int)(N * 0.2); i++)
        {
            // 二维坐标旋转
            cosi = (float)Cos(i);
            sini = (float)Sin(i);
            x = 2.0f * cosi - 3.0f * sini;
            y = 2.0f * sini + 3.0f * cosi;
            sum = sum + x + y;

            // 球坐标化直角坐标
            sini = 1.5f * ((float)Sin(i * 0.1f));
            x = sini * cos2;
            y = sini * sin2;
            z = 1.5f * ((float)Cos(i * 0.1f));
            sum = sum + x + y + z;

            // 计算二元一次方程组的解
            // a * x1 + b * x2 = e
            // c * x1 + d * x2 = f
            a = (float)Log(i + 1.0) + 1.0f;
            b = i;
            c = z;
            d = i / (i + 1.0f);
            e = i * 0.001f;
            f = 2.0f * e + 1.0f;

            det = a * d - b * c; // 行列式
            if (Abs(det) < 1e-5)
            {
                det = det + 1.0f; // 此处直接加1是为了方便编程，真实场景不能这样做
            }
            det = 1.0f / det;
            e = e * det;
            f = f * det;
            x1 = d * e - b * f;
            x2 = -c * e + a * f;
            sum = sum + x1 + x2;

            //  计算二次方程 ax^2+bx+c=0的解，a,b,c沿用前面的值
            b = b * 0.5f;
            delta = b * b - a * c;
            if (delta < 0.0f)
            {
                delta = -delta; // 此处直接反号是为了方便编程，真实场景不能这样做
            }
            a = 1.0f / a;
            b = -b * a;
            c = (float)Sqrt(delta) * a;
            x1 = b + c;
            x2 = b - c;
            sum = sum + 1.1f * x1 + 1.2f * x2;
        }
        Console.WriteLine($"sum={sum}");
    }

    static void FloorCeilRound(int N)
    {
        long sum = 0;
        double t;
        int a, b, c;
        for (int i = -N; i < N; i++)
        {
            t = i * 0.1;
            a = (int)Floor(t);
            b = (int)Ceiling(t);
            // (int)Round(t)相当于(int)Round(t, MidpointRounding.ToEven)，与ToZero行为不同
            c = (int)Round(t, MidpointRounding.ToZero);
            sum = sum + a + b - c;
        }
        Console.WriteLine($"sum={sum}");
    }

    static void IntFloorCeilRound(int N)
    {
        long sum = 0;
        double t;
        int a, b, c;
        for (int i = -N; i < N; i++)
        {
            t = i * 0.1;
            // 替代 a = (int)Math.Floor(t)
            a = (t < 0 ? (int)t - 1 : (int)t);

            // 替代 b = (int)Math.Ceiling(t)
            b = (t < 0 ? (int)t : (int)t + 1);

            // 替代 c = (int)Math.Round(t, MidpointRounding.ToZero)
            c = (t < 0 ? (int)(t - 0.5) : (int)(t + 0.5));

            sum = sum + a + b - c;
        }
        Console.WriteLine($"sum={sum}");
    }

    static (Complex, Complex) MyQuadratic(double c, double b, double a)
    {
        Complex x1, x2;
        if (a == 0.0)
        {
            if (b == 0.0)
            {
                x1 = Complex.Zero / Complex.Zero;  // Complex.NaN;
                x2 = x1;
            }
            else
            {
                x1 = new Complex(-c / b, 0.0);
                x2 = x1;
            }
        }
        else
        {
            a = 1.0 / a;
            b = -0.5 * b * a;
            c = c * a;
            double delta = b * b - c;
            double sqrtDelta;
            if (delta < 0.0)
            {
                sqrtDelta = Math.Sqrt(-delta);
                x1 = new Complex(b, sqrtDelta);
                x2 = new Complex(b, -sqrtDelta);
            }
            else
            {
                sqrtDelta = Math.Sqrt(delta);
                x1 = new Complex(b + sqrtDelta, 0.0);
                x2 = new Complex(b - sqrtDelta, 0.0);
            }
        }
        return (x1, x2);
    }

    static void TestQuadratic(int N)
    {
        Complex x1, x2;
        double sum = 0.0;
        double a, b, c;
        for (int i = 11; i < N; i++)
        {
            a = -0.1 * i + 1;
            b = 0.3464 * i + 5;
            c = -0.3 * i + 7;
            (x1, x2) = FindRoots.Quadratic(c, b, a);
            sum = sum + x1.Real + x2.Imaginary;
        }
        Console.WriteLine($"sum={sum}");
    }

    static void TestMyQuadratic(int N)
    {
        Complex x1, x2;
        double sum = 0.0;
        double a, b, c;
        for (int i = 11; i < N; i++)
        {
            a = -0.1 * i + 1;
            b = 0.3464 * i + 5;
            c = -0.3 * i + 7;
            (x1, x2) = MyQuadratic(c, b, a);
            sum = sum + x1.Real + x2.Imaginary;
        }
        Console.WriteLine($"sum={sum}");
    }

    static double MyBinomial(int n, int k)
    {
        if (k < 0 || n < 0 || k > n)
        {
            return double.NaN;
        }
        else if (k == 0 || k == n)
        {
            return 1.0;
        }
        else
        {
            if (k > (n >> 1))
            {
                k = n - k;
            }
            double dblN = n;
            double dblK = k;
            double Cnk = dblN / dblK;
            for (double i = 1.0; i < dblK; i++)
            {
                Cnk = Cnk * (dblN - i) / (dblK - i);
            }
            return Math.Round(Cnk);
        }
    }

    static void TestBinomial(int N)
    {
        double sum = 0.0;
        for (int i = 120; i < N; i++)
        {
            for (int j = 100; j <= i; j++)
            {
                sum = sum + SpecialFunctions.Binomial(i, j);
            }
            sum = sum - Math.Pow(2, i);
        }
        Console.WriteLine($"sum={sum}");
    }

    static void TestMyBinomial(int N)
    {
        double sum = 0.0;
        for (int i = 120; i < N; i++)
        {
            for (int j = 100; j <= i; j++)
            {
                sum = sum + MyBinomial(i, j);
            }
            sum = sum - Math.Pow(2, i);
        }
        Console.WriteLine($"sum={sum}");
    }

    public static double FastExp(double x)
    {
        long tmp = (long)(1512775 * x + 1072632447);
        return BitConverter.Int64BitsToDouble(tmp << 32);
    }

    static void TestMathExp(double N)
    {
        double sum = 0.0, x;
        for (double i = 0.0; i < N; i++)
        {
            x = i * 0.000001;
            sum = sum + Exp(x) - Exp(x - 0.0001);
        }
        Console.WriteLine($"sum={sum}");
    }

    static void TestFastExp(double N)
    {
        double sum = 0.0, x;
        for (double i = 0.0; i < N; i++)
        {
            x = i * 0.000001;
            sum = sum + FastExp(x) - FastExp(x - 0.0001);
        }
        Console.WriteLine($"sum={sum}");
    }

}
