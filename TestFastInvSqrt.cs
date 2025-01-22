using System.Diagnostics;

class SpeedTest
{
    static float FastInvSqrtF(float x)
    {
        float xHalf = 0.5f * x;
        int i = BitConverter.SingleToInt32Bits(x);
        i = 0x5f375a86 - (i >> 1); // 0x5f3759df
        x = BitConverter.Int32BitsToSingle(i);
        x = x * (1.5f - xHalf * x * x);
        //x = x * (1.5f - xHalf * x * x); //多迭代一次可以提高精度
        return x;
    }

    static double FastInvSqrt(double x)
    {
        double xHalf = 0.5 * x;
        long i = BitConverter.DoubleToInt64Bits(x);
        i = 0x5fe6ec85e7de30daL - (i >> 1);
        x = BitConverter.Int64BitsToDouble(i);
        x = x * (1.5 - xHalf * x * x);
        //x = x * (1.5 - xHalf * x * x); //多迭代一次可以提高精度
        return x;
    }

    static float TestMathFReciprocalSqrtEstimate(int N)
    {
        float sum = 0.0f;
        for (int i = 1; i < N; i++)
        {
            sum += MathF.ReciprocalSqrtEstimate(i) + MathF.ReciprocalSqrtEstimate(i + 0.1f) + MathF.ReciprocalSqrtEstimate(i + 0.2f) +
                   MathF.ReciprocalSqrtEstimate(i + 0.3f) + MathF.ReciprocalSqrtEstimate(i + 0.4f) + MathF.ReciprocalSqrtEstimate(i + 0.5f) +
                   MathF.ReciprocalSqrtEstimate(i + 0.6f) + MathF.ReciprocalSqrtEstimate(i + 0.7f) + MathF.ReciprocalSqrtEstimate(i + 0.8f) +
                   MathF.ReciprocalSqrtEstimate(i + 0.9f);
        }
        return sum;
    }

    static double TestMathReciprocalSqrtEstimate(int N)
    {
        double sum = 0.0;
        for (int i = 1; i < N; i++)
        {
            sum += Math.ReciprocalSqrtEstimate(i) + Math.ReciprocalSqrtEstimate(i + 0.1f) + Math.ReciprocalSqrtEstimate(i + 0.2f) +
                   Math.ReciprocalSqrtEstimate(i + 0.3f) + Math.ReciprocalSqrtEstimate(i + 0.4f) + Math.ReciprocalSqrtEstimate(i + 0.5f) +
                   Math.ReciprocalSqrtEstimate(i + 0.6f) + Math.ReciprocalSqrtEstimate(i + 0.7f) + Math.ReciprocalSqrtEstimate(i + 0.8f) +
                   Math.ReciprocalSqrtEstimate(i + 0.9f);
        }
        return sum;
    }

    static float TestFastInvSqrtF(int N)
    {
        float sum = 0.0f;
        for (int i = 1; i < N; i++)
        {
            sum += FastInvSqrtF(i) + FastInvSqrtF(i + 0.1f) + FastInvSqrtF(i + 0.2f) + FastInvSqrtF(i + 0.3f) + FastInvSqrtF(i + 0.4f) +
                   FastInvSqrtF(i + 0.5f) + FastInvSqrtF(i + 0.6f) + FastInvSqrtF(i + 0.7f) + FastInvSqrtF(i + 0.8f) + FastInvSqrtF(i + 0.9f);
        }
        return sum;
    }

    static double TestFastInvSqrt(int N)
    {
        double sum = 0.0;
        for (int i = 1; i < N; i++)
        {
            sum += FastInvSqrt(i) + FastInvSqrt(i + 0.1) + FastInvSqrt(i + 0.2) + FastInvSqrt(i + 0.3) + FastInvSqrt(i + 0.4) +
                   FastInvSqrt(i + 0.5) + FastInvSqrt(i + 0.6) + FastInvSqrt(i + 0.7) + FastInvSqrt(i + 0.8) + FastInvSqrt(i + 0.9);
        }
        return sum;
    }

    static float Test1_MathFSqrt(int N)
    {
        float sum = 0.0f;
        for (int i = 1; i < N; i++)
        {
            sum += 1.0f / MathF.Sqrt(i) + 1.0f / MathF.Sqrt(i + 0.1f) + 1.0f / MathF.Sqrt(i + 0.2f) + 1.0f / MathF.Sqrt(i + 0.3f) + 1.0f / MathF.Sqrt(i + 0.4f) +
                   1.0f / MathF.Sqrt(i + 0.5f) + 1.0f / MathF.Sqrt(i + 0.6f) + 1.0f / MathF.Sqrt(i + 0.7f) + 1.0f / MathF.Sqrt(i + 0.8f) + 1.0f / MathF.Sqrt(i + 0.9f);
        }
        return sum;
    }

    static double Test1_MathSqrt(int N)
    {
        double sum = 0.0;
        for (int i = 1; i < N; i++)
        {
            sum += 1.0 / Math.Sqrt(i) + 1.0 / Math.Sqrt(i + 0.1) + 1.0 / Math.Sqrt(i + 0.2) + 1.0 / Math.Sqrt(i + 0.3) + 1.0 / Math.Sqrt(i + 0.4) +
                   1.0 / Math.Sqrt(i + 0.5) + 1.0 / Math.Sqrt(i + 0.6) + 1.0 / Math.Sqrt(i + 0.7) + 1.0 / Math.Sqrt(i + 0.8) + 1.0 / Math.Sqrt(i + 0.9);
        }
        return sum;
    }

    static void MeasureExecutionTime(Func<double> testFunction, string functionName)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        double sum = testFunction();
        stopwatch.Stop();
        Console.WriteLine($"{functionName},  sum = {sum:F6}");
        Console.WriteLine($"{functionName}, time = {stopwatch.ElapsedMilliseconds * 0.001:F3}s");
        Console.WriteLine();
    }

    static void Main()
    {
        int N = 9_000_000;

        MeasureExecutionTime(() => TestMathFReciprocalSqrtEstimate(N), nameof(TestMathFReciprocalSqrtEstimate));
        MeasureExecutionTime(() => TestMathReciprocalSqrtEstimate(N), nameof(TestMathReciprocalSqrtEstimate));

        MeasureExecutionTime(() => TestFastInvSqrtF(N), nameof(TestFastInvSqrtF));
        MeasureExecutionTime(() => TestFastInvSqrt(N), nameof(TestFastInvSqrt));

        MeasureExecutionTime(() => Test1_MathFSqrt(N), nameof(Test1_MathFSqrt));
        MeasureExecutionTime(() => Test1_MathSqrt(N), nameof(Test1_MathSqrt));

    }
}