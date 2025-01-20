using System.Diagnostics;
using static System.Math;
using System.Numerics;

class SpeedTest
{
    static readonly int[] Factorial = [1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 479001600];
    static readonly long[] FactorialL = [1L, 1L, 2L, 6L, 24L, 120L, 720L, 5040L, 40320L, 362880L, 3628800L, 39916800L,
                                  479001600L, 6227020800L, 87178291200L, 1307674368000L, 20922789888000L,
                                  355687428096000L, 6402373705728000L, 121645100408832000L, 2432902008176640000L];

    public static readonly ushort[] SmallPrimes = [
        2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37,
            41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89,
             97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151,
            157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223,
            227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281,
            283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359,
            367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433,
            439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503,
            509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593,
            599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659,
            661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743,
            751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827,
            829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911,
            919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997,
            1009, 1013, 1019, 1021, 1031, 1033, 1039, 1049, 1051, 1061, 1063, 1069,
            1087, 1091, 1093, 1097, 1103, 1109, 1117, 1123, 1129, 1151, 1153, 1163,
            1171, 1181, 1187, 1193, 1201, 1213, 1217, 1223, 1229, 1231, 1237, 1249,
            1259, 1277, 1279, 1283, 1289, 1291, 1297, 1301, 1303, 1307, 1319, 1321,
            1327, 1361, 1367, 1373, 1381, 1399, 1409, 1423, 1427, 1429, 1433, 1439,
            1447, 1451, 1453, 1459, 1471, 1481, 1483, 1487, 1489, 1493, 1499, 1511,
            1523, 1531, 1543, 1549, 1553, 1559, 1567, 1571, 1579, 1583, 1597, 1601,
            1607, 1609, 1613, 1619, 1621, 1627, 1637, 1657, 1663, 1667, 1669, 1693,
            1697, 1699, 1709, 1721, 1723, 1733, 1741, 1747, 1753, 1759, 1777, 1783,
            1787, 1789, 1801, 1811, 1823, 1831, 1847, 1861, 1867, 1871, 1873, 1877,
            1879, 1889, 1901, 1907, 1913, 1931, 1933, 1949, 1951, 1973, 1979, 1987,
            1993, 1997, 1999, 2003, 2011, 2017, 2027, 2029, 2039, 2053, 2063, 2069,
            2081, 2083, 2087, 2089, 2099, 2111, 2113, 2129, 2131, 2137, 2141, 2143,
            2153, 2161, 2179, 2203, 2207, 2213, 2221, 2237, 2239, 2243, 2251, 2267,
            2269, 2273, 2281, 2287, 2293, 2297, 2309, 2311, 2333, 2339, 2341, 2347,
            2351, 2357, 2371, 2377, 2381, 2383, 2389, 2393, 2399, 2411, 2417, 2423,
            2437, 2441, 2447, 2459, 2467, 2473, 2477, 2503, 2521, 2531, 2539, 2543,
            2549, 2551, 2557, 2579, 2591, 2593, 2609, 2617, 2621, 2633, 2647, 2657,
            2659, 2663, 2671, 2677, 2683, 2687, 2689, 2693, 2699, 2707, 2711, 2713,
            2719, 2729, 2731, 2741, 2749, 2753, 2767, 2777, 2789, 2791, 2797, 2801,
            2803, 2819, 2833, 2837, 2843, 2851, 2857, 2861, 2879, 2887, 2897, 2903,
            2909, 2917, 2927, 2939, 2953, 2957, 2963, 2969, 2971, 2999, 3001, 3011,
            3019, 3023, 3037, 3041, 3049, 3061, 3067, 3079, 3083, 3089, 3109, 3119,
            3121, 3137, 3163, 3167, 3169, 3181, 3187, 3191, 3203, 3209, 3217, 3221,
            3229, 3251, 3253, 3257, 3259, 3271, 3299, 3301, 3307, 3313, 3319, 3323,
            3329, 3331, 3343, 3347, 3359, 3361, 3371, 3373, 3389, 3391, 3407, 3413,
            3433, 3449, 3457, 3461, 3463, 3467, 3469, 3491, 3499, 3511, 3517, 3527,
            3529, 3533, 3539, 3541, 3547, 3557, 3559, 3571, 3581, 3583, 3593, 3607,
            3613, 3617, 3623, 3631, 3637, 3643, 3659, 3671, 3673, 3677, 3691, 3697];

    public static readonly uint[] MultiplierForSmallPrimes = [
            2147483648, 1431655765, 858993459, 613566757, 390451573, 330382100, 252645135, 226050911, 186737709, 148102321, 138547333, 116080198,
            104755300, 99882961, 91382283, 81037119, 72796056, 70409300, 64103990, 60492498, 58835169, 54366675, 51746594, 48258060,
            44278014, 42524429, 41698712, 40139882, 39403370, 38008561, 33818641, 32786010, 31350127, 30899046, 28825284, 28443493,
            27356480, 26349493, 25718368, 24826401, 23994231, 23729102, 22486740, 22253717, 21801865, 21582751, 20355296, 19259944,
            18920561, 18755316, 18433337, 17970575, 17821442, 17111424, 16711935, 16330675, 15966422, 15848588, 15505298, 15284582,
            15176563, 14658592, 13990122, 13810185, 13721941, 13548793, 12975733, 12744711, 12377428, 12306497, 12167047, 11963698,
            11702909, 11514658, 11332368, 11214014, 11041048, 10818558, 10710642, 10501143, 10250519, 10201823, 9965122, 9919094,
            9783525, 9695186, 9565629, 9398178, 9316632, 9276388, 9196933, 8966529, 8819235, 8747388, 8607149, 8538703,
            8438050, 8243700, 8212175, 7938942, 7851860, 7710893, 7628717, 7548273, 7521835, 7443618, 7316810, 7242778,
            7170230, 7146369, 7075729, 7006472, 6961050, 6938558, 6806605, 6700417, 6679576, 6638281, 6577286, 6517402,
            6497682, 6381824, 6344118, 6288386, 6215583, 6126915, 6057782, 5973529, 5907796, 5859437, 5811864, 5780576,
            5718998, 5673669, 5643847, 5585134, 5556232, 5457392, 5388918, 5308984, 5295891, 5231386, 5218673, 5193431,
            5180902, 5119151, 5035132, 5011631, 4999962, 4976788, 4897341, 4875105, 4864063, 4842128, 4735356, 4714564,
            4673523, 4623216, 4583744, 4564259, 4535341, 4506787, 4441539, 4423242, 4396078, 4369245, 4333974, 4307891,
            4256658, 4239850, 4214885, 4206629, 4165827, 4157762, 4133752, 4094345, 4086554, 4048038, 4040421, 4017744,
            3951212, 3936726, 3929522, 3915194, 3893897, 3872829, 3845092, 3824548, 3804223, 3731510, 3725037, 3693008,
            3667778, 3636721, 3618339, 3600141, 3576160, 3540781, 3529144, 3511830, 3494685, 3489007, 3472084, 3438725,
            3411412, 3363326, 3358067, 3347598, 3332015, 3326854, 3311463, 3301282, 3296215, 3286127, 3256230, 3251300,
            3236600, 3155744, 3141893, 3128163, 3110042, 3070027, 3048238, 3018249, 3009788, 3005576, 2997186, 2984689,
            2968188, 2960006, 2955931, 2943775, 2919761, 2900046, 2896135, 2888344, 2884465, 2876737, 2865222, 2842467,
            2820071, 2805335, 2783518, 2772736, 2765594, 2754951, 2740886, 2733907, 2720056, 2713183, 2689398, 2682678,
            2672662, 2669340, 2662720, 2652852, 2649579, 2639808, 2623682, 2592015, 2582663, 2576466, 2573378, 2536898,
            2530918, 2527939, 2513147, 2495624, 2492727, 2478343, 2466955, 2458482, 2450067, 2441710, 2416977, 2408844,
            2403452, 2400765, 2384769, 2371600, 2355989, 2345695, 2325375, 2307882, 2300465, 2295547, 2293096, 2288209,
            2285773, 2273673, 2259320, 2252212, 2245148, 2224220, 2221918, 2203678, 2201419, 2176872, 2170272, 2161534,
            2155027, 2150710, 2148558, 2144268, 2135738, 2129384, 2118879, 2116791, 2106409, 2092045, 2081904, 2075867,
            2063896, 2061915, 2057963, 2055993, 2046197, 2034566, 2032640, 2017364, 2015471, 2009812, 2006057, 2004185,
            1994876, 1987491, 1971073, 1949600, 1946066, 1940790, 1933799, 1919968, 1918253, 1914832, 1908027, 1894560,
            1892890, 1889559, 1882932, 1877992, 1873078, 1869816, 1860099, 1858489, 1840964, 1836241, 1834673, 1829982,
            1826869, 1822218, 1811459, 1806886, 1803851, 1802337, 1797810, 1794805, 1790316, 1781405, 1776983, 1772583,
            1762400, 1759512, 1755198, 1746632, 1740968, 1736744, 1733940, 1715928, 1703677, 1696945, 1691598, 1688938,
            1684962, 1683641, 1679690, 1665362, 1657649, 1656370, 1646213, 1641180, 1638676, 1631207, 1622580, 1616473,
            1615257, 1612831, 1608000, 1604396, 1600808, 1598425, 1597236, 1594864, 1591319, 1586616, 1584275, 1583107,
            1579613, 1573825, 1572673, 1566935, 1562375, 1560105, 1552211, 1546622, 1539967, 1538864, 1535563, 1533370,
            1532276, 1523579, 1516050, 1513912, 1510717, 1506478, 1503314, 1501212, 1491827, 1487693, 1482557, 1479493,
            1476442, 1472392, 1467362, 1461371, 1454443, 1452475, 1449534, 1446605, 1445631, 1432134, 1431179, 1426426,
            1422646, 1420764, 1414214, 1412354, 1408648, 1403126, 1400381, 1394923, 1393113, 1390408, 1381463, 1377034,
            1376152, 1369133, 1357878, 1356163, 1355307, 1350195, 1347653, 1345963, 1340921, 1338413, 1335085, 1333427,
            1330124, 1321122, 1320310, 1318689, 1317879, 1313045, 1301900, 1301111, 1298751, 1296399, 1294055, 1292497,
            1290168, 1289393, 1284765, 1283229, 1278645, 1277884, 1274093, 1273338, 1267326, 1266579, 1260631, 1258415,
            1251083, 1245280, 1242398, 1240962, 1240245, 1238814, 1238100, 1230298, 1227485, 1223289, 1221202, 1217740,
            1217050, 1215672, 1213611, 1212925, 1210874, 1207470, 1206791, 1202736, 1199377, 1198708, 1195371, 1190732,
            1188754, 1187440, 1185473, 1182861, 1180910, 1178965, 1173810, 1169973, 1169335, 1168063, 1163633, 1161744  ];

    static void Main()
    {
        int N = 610_000_103;
        //MeasureExecutionTime(() => IntMultiDivide(N), nameof(IntMultiDivide));
        //MeasureExecutionTime(() => IntLeftRightShift(N), nameof(IntLeftRightShift));
        //MeasureExecutionTime(() => IntLeftRightShiftParallelFor(N), nameof(IntLeftRightShiftParallelFor));
        //Console.WriteLine();
        //MeasureExecutionTime(() => IntMod2n(N), nameof(IntMod2n));
        //MeasureExecutionTime(() => IntAnd2n_1(N), nameof(IntAnd2n_1));
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
        TestMultiCorrectness();

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
                x1 = Complex.NaN;
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

    //static double TestQuadratic(int N)
    //{
    //    Complex x1, x2;
    //    double sum = 0.0;
    //    double a, b, c;
    //    for (int i = 11; i < N; i++)
    //    {
    //        a = -0.1 * i + 1;
    //        b = 0.3464 * i + 5;
    //        c = -0.3 * i + 7;
    //        (x1, x2) = FindRoots.Quadratic(c, b, a); // Mathnet.Numerics
    //        sum = sum + x1.Real + x2.Imaginary;
    //    }
    //    return sum;
    //}

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

    //static double TestBinomial(int N)
    //{
    //    double sum = 0.0;
    //    for (int i = 120; i < N; i++)
    //    {
    //        for (int j = 100; j <= i; j++)
    //        {
    //            sum = sum + SpecialFunctions.Binomial(i, j); //MathNet.Numerics.SpecialFunctions.Binomial(i, j);
    //        }
    //        sum = sum - Math.Pow(2, i);
    //    }
    //    return sum;
    //}

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
            for (uint j = 0; j < SmallPrimes.Length; j++)
            {
                if (i % SmallPrimes[j] == 0)
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
        for (uint i = 0; i < SmallPrimes.Length; i++)
        {
            uint k = (uint)(4294967296L / SmallPrimes[i]) + 1;
            Console.WriteLine($"{k},");
        }
    }

    static void TestMultiCorrectness()
    {
        for (ushort i = 2; i < 65535; i++)
        {
            int a = i % SmallPrimes[2];
            ulong b = i * MultiplierForSmallPrimes[2];
            int c = (int)((b * SmallPrimes[2]) >> 32);
            if (a != c)
            {
                Console.WriteLine($"i={i},a={a},b={b},c={c}");
            }
        }
    }

    static float InvSqrtF(float x)
    {
        float xHalf = 0.5f * x;
        int i = BitConverter.SingleToInt32Bits(x);
        i = 0x5f375a86 - (i >> 1); // 0x5f3759df
        x = BitConverter.Int32BitsToSingle(i);
        x = x * (1.5f - xHalf * x * x);
        //x = x * (1.5f - xHalf * x * x); //多迭代一次可以提高精度
        return x;
    }

    static double InvSqrt(double x)
    {
        double xHalf = 0.5 * x;
        long i = BitConverter.DoubleToInt64Bits(x);
        i = 0x5fe6ec85e7de30daL - (i >> 1);
        x = BitConverter.Int64BitsToDouble(i);
        x = x * (1.5 - xHalf * x * x);
        //x = x * (1.5 - xHalf * x * x); //多迭代一次可以提高精度
        return x;
    }
    static float TestInvSqrtF(int N)
    {
        float sum = 0.0f;
        for (int i = 0; i < N; i++)
        {
            sum += InvSqrtF(i);
        }
        return sum;
    }

}
