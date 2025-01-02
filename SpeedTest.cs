using System.Diagnostics;
using static System.Math;
using MathNet.Numerics;
using System.Numerics;
using MathNet.Numerics.Random;

class SpeedTest
{
    static readonly int[] Factorial = [1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 479001600];
    static readonly long[] FactorialL = [1L, 1L, 2L, 6L, 24L, 120L, 720L, 5040L, 40320L, 362880L, 3628800L, 39916800L,
                                  479001600L, 6227020800L, 87178291200L, 1307674368000L, 20922789888000L,
                                  355687428096000L, 6402373705728000L, 121645100408832000L, 2432902008176640000L];
    static readonly byte[] PrimesLessThan255 = [2, 3, 5, 7, 11, 13, 17, 19, 23,
        29,  31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101,
        103, 107, 109, 113, 127, 131, 137, 139, 149, 151,157, 163, 167, 173,
        179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251];
    static void Main()
    {
        int N = 610_000_103;
        //MeasureExecutionTime(() => IntMultiDivide(N), nameof(IntMultiDivide));
        //MeasureExecutionTime(() => IntLeftRightShift(N), nameof(IntLeftRightShift));
        //MeasureExecutionTime(() => IntLeftRightShiftParallelFor(N), nameof(IntLeftRightShiftParallelFor));
        //Console.WriteLine();
        MeasureExecutionTime(() => IntMod2n(N), nameof(IntMod2n));
        MeasureExecutionTime(() => IntAnd2n_1(N), nameof(IntAnd2n_1));
        Console.WriteLine();
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

        //int n1 = 541;
        //MeasureExecutionTime(() => TestBinomial(n1), nameof(TestBinomial));
        //MeasureExecutionTime(() => TestMyBinomial(n1), nameof(TestMyBinomial));
        //Console.WriteLine();
        //MeasureExecutionTime(() => TestMathExp(N), nameof(TestMathExp));
        //MeasureExecutionTime(() => TestFastExp(N), nameof(TestFastExp));
        //Console.WriteLine();
        //MeasureExecutionTime(() => TestMathLn(N), nameof(TestMathLn));
        //MeasureExecutionTime(() => TestFastLn(N), nameof(TestFastLn));
        //TestMultiplyMatricesSequential(50);


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

        //for (double i = 1; i < 5; i += 0.1)
        //{
        //    Console.WriteLine($"FastLn({i:F3})={FastLn(i)},Math.Log({i:F3})={Math.Log(i)}");
        //    //Console.WriteLine($"FastLn({i:F3})-Math.Log({i:F3})={FastLn(i) - Math.Log(i)}");
        //}

        //Console.WriteLine($"{MyBinomial(297, 5)},{SpecialFunctions.Binomial(297,5)}");
        //Console.WriteLine($"{MyBinomial(297, 6)},{SpecialFunctions.Binomial(297,6)}");
        //Console.WriteLine($"{MyBinomial(297, 7)},{SpecialFunctions.Binomial(297,7)}");
        //Console.WriteLine($"{MyBinomial(297, 8)},{SpecialFunctions.Binomial(297,8)}");
        //Console.WriteLine($"{MyBinomial(297, 9)},{SpecialFunctions.Binomial(297,9)}");
        //Console.WriteLine($"{MyBinomial(297, 10)},{SpecialFunctions.Binomial(297,10)}");
        //Console.WriteLine($"{MyBinomial(297, 11)},{SpecialFunctions.Binomial(297,11)}");

        //TestPrimeLessThan4294967295();
        //MeasureExecutionTime(() => TestMathSqrt(N), nameof(TestMathSqrt));
        //MeasureExecutionTime(() => TestSqrtDiv(N), nameof(TestSqrtDiv));
        //MeasureExecutionTime(() => TestSqrtMul(N), nameof(TestSqrtMul));

        //Console.WriteLine();
        //MeasureExecutionTime(() => TestDecSqrtDiv(N), nameof(TestDecSqrtDiv));
        //MeasureExecutionTime(() => TestDecSqrtMul(N), nameof(TestDecSqrtMul));

        //GenerateMultiplesOfPrimesLessThan255();
    }

    static void MeasureExecutionTime(Func<double> testFunction, string functionName)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        double sum = testFunction();
        Console.WriteLine($"sum={sum}");
        stopwatch.Stop();
        Console.WriteLine($"{functionName}: {stopwatch.ElapsedMilliseconds * 0.001:F3}s");
    }
    static void MeasureExecutionTime(Func<decimal> testFunction, string functionName)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        decimal sum = testFunction();
        Console.WriteLine($"sum={sum}");
        stopwatch.Stop();
        Console.WriteLine($"{functionName}: {stopwatch.ElapsedMilliseconds * 0.001:F3}s");
    }
    static double IntMultiDivide(int N)
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
        return sum;
    }

    static double IntLeftRightShift(int N)
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
        return sum;
    }

    static double IntMod2n(int N)
    {
        long sum = 0L;
        int a, b, c, d, e, f;
        for (int i = 0; i < N; i++)
        {
            a = i % 2;
            b = i % 4;
            c = i % 8;
            d = i % 16;
            e = i % 32;
            f = i % 64;
            sum = sum + a + b - c + d + e + f;
        }
        return sum;
    }

    static double IntAnd2n_1(int N)
    {
        long sum = 0L;
        int a, b, c, d, e, f;
        for (int i = 0; i < N; i++)
        {
            a = i & 1;
            b = i & 3;
            c = i & 7;
            d = i & 15;
            e = i & 31;
            f = i & 63;
            sum = sum + a + b - c + d + e + f;
        }
        return sum;
    }

    static double IntLeftRightShiftParallelFor(int N)
    {
        long sum = 0L;
        int a, b, c, d, e, f;
        Parallel.For(0, N, i =>
        {
            a = i << 1;
            b = i << 2;
            c = i << 3;
            d = i >> 1;
            e = i >> 2;
            f = i >> 3;
            Interlocked.Add(ref sum, a + b - c + d + e + f);
        });
        return sum;
    }

    static double DoubleDivide(double N)
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
        return sum;
    }

    static double DoubleMulti(double N)
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
        return sum;
    }
    static double FloatMulti(int N)
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
        return sum;
    }

    static double DonotStoreResult(double N)
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
        return sum;
    }

    static double StoreResult(double N)
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
        return sum;
    }

    static double StoreResultFloat(int N)
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
        return sum;
    }

    static double FloorCeilRound(int N)
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
        return sum;
    }

    static double IntFloorCeilRound(int N)
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
        return sum;
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

    static double TestQuadratic(int N)
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
        return sum;
    }

    static double TestMyQuadratic(int N)
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
        return sum;
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

    static double TestBinomial(int N)
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
        return sum;
    }

    static double TestMyBinomial(int N)
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
        return sum;
    }

    static double FastExp(double x)
    {
        long tmp = (long)(1512775 * x + 1072632447);
        return BitConverter.Int64BitsToDouble(tmp << 32);
    }

    static double TestMathExp(double N)
    {
        double sum = 0.0, x;
        for (double i = 0.0; i < N; i++)
        {
            x = i * 0.000001;
            sum = sum + Exp(x) - Exp(x - 0.0001);
        }
        return sum;
    }

    static double TestFastExp(double N)
    {
        double sum = 0.0, x;
        for (double i = 0.0; i < N; i++)
        {
            x = i * 0.000001;
            sum = sum + FastExp(x) - FastExp(x - 0.0001);
        }
        return sum;
    }

    static double FastLn(double x) // 抛弃对x<=0的检查。
    {
        long longx = BitConverter.DoubleToInt64Bits(x);
        //     k = (longx >> 52) - 1023 + 0.5
        double k = (longx >> 52) - 1022.5;
        //   ln(2)=0.693147180559945309
        return k * 0.693147180559945309;
    }

    static double TestMathLn(double N)
    {
        double sum = 0.0;
        for (double i = 0.1; i < N; i++)
        {
            sum = sum + Log(i);
        }
        return sum;
    }

    static double TestFastLn(double N)
    {
        double sum = 0.0;
        for (double i = 0.1; i < N; i++)
        {
            sum = sum + FastLn(i);
        }
        return sum;
    }

    static void MultiplyMatricesSequential(double[,] matA, double[,] matB, double[,] result)
    {
        int matACols = matA.GetLength(1);
        int matBCols = matB.GetLength(1);
        int matARows = matA.GetLength(0);

        for (int i = 0; i < matARows; i++)
        {
            for (int j = 0; j < matBCols; j++)
            {
                double temp = 0.0;
                for (int k = 0; k < matACols; k++)
                {
                    temp += matA[i, k] * matB[k, j];
                }
                result[i, j] = temp;
            }
        }
    }

    // https://learn.microsoft.com/zh-cn/dotnet/standard/parallel-programming/how-to-write-a-simple-parallel-for-loop
    static void MultiplyMatricesParallel(double[,] matA, double[,] matB, double[,] result)
    {
        int matACols = matA.GetLength(1);
        int matBCols = matB.GetLength(1);
        int matARows = matA.GetLength(0);

        Parallel.For(0, matARows, i =>
        {
            for (int j = 0; j < matBCols; j++)
            {
                double temp = 0.0;
                for (int k = 0; k < matACols; k++)
                {
                    temp += matA[i, k] * matB[k, j];
                }
                result[i, j] = temp;
            }
        });
    }

    static double[,] InitializeMatrix(int rows, int cols)
    {
        double[,] matrix = new double[rows, cols];

        Random r = new Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = r.NextDouble();
            }
        }
        return matrix;
    }

    static void TestMultiplyMatricesSequential(int N)
    {
        int colCount = 180;
        int rowCount = 2000;
        int colCount2 = 270;
        double[,] m1 = InitializeMatrix(rowCount, colCount);
        double[,] m11 = m1;
        double[,] m2 = InitializeMatrix(colCount, colCount2);
        double[,] m22 = m2;
        double[,] result1 = new double[rowCount, colCount2];
        double[,] result2 = new double[rowCount, colCount2];
        double sum = 0.0;
        Stopwatch stopwatch = new();
        stopwatch.Start();
        for (int i = 0; i < (N >> 2); i++)
        {
            MultiplyMatricesSequential(m1, m2, result1);
            m1[0, 0] += 1.0;
            m2[0, 0] += 1.0;
            sum += result1[0, 0] + result1[1, 1];
        }
        stopwatch.Stop();
        Console.WriteLine($"sum={sum}");
        Console.WriteLine($"Sequential: {stopwatch.ElapsedMilliseconds * 0.001:F3}s");

        sum = 0.0;
        stopwatch.Restart();
        for (int i = 0; i < (N >> 2); i++)
        {
            MultiplyMatricesParallel(m11, m22, result2);
            m11[0, 0] += 1.0;
            m22[0, 0] += 1.0;
            sum += result2[0, 0] + result2[1, 1];
        }
        stopwatch.Stop();
        Console.WriteLine($"sum={sum}");
        Console.WriteLine($"Parallel: {stopwatch.ElapsedMilliseconds * 0.001:F3}s");
    }

    static void TestPrimeLessThan4294967295()
    {
        uint count = 0;
        bool f, g;
        Stopwatch stopwatch = new();
        stopwatch.Start();
        for (uint i = 257; i < 429496; i += 2)
        {
            f = true;
            g = true;
            for (uint j = 0; j < PrimesLessThan255.Length; j++)
            {
                if (i % PrimesLessThan255[j] == 0)
                {
                    f = false; // 有小的素因子
                    break;
                }
            }
            if (f)
            {
                for (uint j = 257; j < Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        g = false; // 是合数
                        break;
                    }
                }
            }
            if (f && !g) //没有小的素因子，但也是合数
            {
                Console.WriteLine(i);
                count++;
            }
        }
        Console.WriteLine($"count={count}");
        stopwatch.Stop();
        Console.WriteLine($"PrimeLessThan4294967295: {stopwatch.ElapsedMilliseconds * 0.001:F3}s");
    }

    static double SqrtUseDiv(double x)
    {
        if (x < 0.0)
        {
            return double.NaN;
        }
        if (x == 0.0)
        {
            return 0.0;
        }
        double x_2 = x * 0.5;
        double sqrtx = 0.1 * x;

        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;

        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;

        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5 + x_2 / sqrtx;

        return sqrtx;
    }

    static double SqrtUseMul(double x)
    {
        if (x < 0.0)
        {
            return double.NaN;
        }
        if (x == 0.0)
        {
            return 0.0;
        }
        double x_2 = x * 0.5;
        double I_sqrtx;
        //if (x < 1.0)
        //{
        //    I_sqrtx = x;
        //}
        //else
        //{
        //    I_sqrtx = 1.0 / x;
        //}
        I_sqrtx = 1.0 / x;
        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);

        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);

        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);

        //I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        //I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        //I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        //I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);
        //I_sqrtx = I_sqrtx * (1.5 - x_2 * I_sqrtx * I_sqrtx);

        return I_sqrtx * x;
    }

    static double TestMathSqrt(double N)
    {
        double sum = 0.0;
        for (double i = 0.0; i < N; i++)
        {
            sum = sum + Math.Sqrt(i);
        }
        return sum;
    }

    static double TestSqrtDiv(double N)
    {
        double sum = 0.0;
        for (double i = 0.0; i < N; i++)
        {
            sum = sum + SqrtUseDiv(i);
        }
        return sum;
    }

    static double TestSqrtMul(double N)
    {
        double sum = 0.0;
        for (double i = 0.0; i < N; i++)
        {
            sum = sum + SqrtUseMul(i);
        }
        return sum;
    }

    ////////////////////////////////
    /// https://www.cnblogs.com/skyivben/archive/2013/02/23/2923582.html
    static decimal DecSqrtUseDiv(decimal x)
    {
        if (x < 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(x), "Cannot compute the square root of a negative number.");
        }
        if (x == 0.0m)
        {
            return 0.0m;
        }
        decimal x_2 = x * 0.5m;
        decimal sqrtx = x * 0.1m;

        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;

        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;

        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;

        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;
        sqrtx = sqrtx * 0.5m + x_2 / sqrtx;

        return sqrtx;
    }

    static decimal DecSqrtUseMul(decimal x)
    {
        if (x < 0.0m)
        {
            throw new ArgumentOutOfRangeException(nameof(x), "Cannot compute the square root of a negative number.");
        }
        if (x == 0.0m)
        {
            return 0.0m;
        }
        decimal x_2 = x * 0.5m;
        decimal I_sqrtx;
        if (x < 1.0m)
        {
            I_sqrtx = x;
        }
        else
        {
            I_sqrtx = 1.0m / x;
        }

        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
                                
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
                                
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);

        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);
        I_sqrtx = I_sqrtx * (1.5m - x_2 * I_sqrtx * I_sqrtx);

        return I_sqrtx * x;
    }

    static decimal TestDecSqrtDiv(int N)
    {
        decimal sum = 0.0m;
        for (int i = 0; i < N; i++)
        {
            sum = sum + DecSqrtUseDiv(i);
        }
        return sum;
    }

    static decimal TestDecSqrtMul(int N)
    {
        decimal sum = 0.0m;
        for (int i = 0; i < N; i++)
        {
            sum = sum + DecSqrtUseMul(i);
        }
        return sum;
    }

    static void GenerateMultiplesOfPrimesLessThan255()
    {
        for (uint i = 0; i < PrimesLessThan255.Length; i++)
        {
            uint k = (uint)(4294967296L / PrimesLessThan255[i]) + 1;
            Console.WriteLine($"{k},");
        }
    }

}
