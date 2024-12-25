# C#数学运算相关开发性能优化方法

本文Github地址：https://github.com/FrogGuaGuaGua/CSharpCodePrinciple/blob/master/CSharp-MathOptimization.md

华为公司的[C语言编程规范](https://ilcc.gitbooks.io/wiki/content/StyleGuide/Huawei-C/index.html)在开头就强调了：
> 一般情况下，代码的可阅读性高于性能，只有确定性能是瓶颈时，才应该主动优化。

本文讲述的方法没有经过大项目和大公司的检验，所以，请批判性地阅读本文。本文中的大部分结论有测试代码支持，参见[SpeedTest.cs](https://github.com/FrogGuaGuaGua/CSharpCodePrinciple/blob/master/SpeedTest.cs). 虽然C#的编译器会在release版本中执行一些优化，C#的运行时也有一些优化，但偶尔会遇到debug版本正常，而release版本异常的问题，比如我在github上fork了已停止维护的屏幕录像软件[Captura](https://github.com/FrogGuaGuaGua/Captura)，debug模式能启动，release版本无法启动，我短时间内解决不了这个问题，如果要发布，只能发布debug版本。所以手工执行一些虽然编译器(在release版本中)会做但也简单易读的优化，还是有意义的。同时建议把未经优化的代码作为注释附在旁边，提高可读性。


1. const, readonly, in 这3个关键词在能用的地方要尽量用。这样可以让编译器执行更激进的优化策略，同时提高代码的安全性、可读性和可维护性。

2. 正整数乘以或除以 $2^n$ (n也是整数)，使用移位来代替。但对乘以非 $2^n$ 的整数不要使用此方法，比如不要把 i * 12 改写成 (i << 2 + i << 3). 对于负整数的乘除运算，也不要用移位，很容易出错。 对于int型整数，除法耗时是乘法的13倍，是移位的17倍。对于long型整数，除法耗时是乘法的1.4倍，是移位的9.8倍。([数据来源](https://learn.microsoft.com/en-us/previous-versions/dotnet/articles/ms973852(v=msdn.10)))

3. 除以浮点型常数的除法，改写为乘以这个数的倒数。比如把 x / 2.0 改写为 x * 0.5  .对于条件语句if(a/b > c)，可以改写为if( (b > 0 && a > b * c) || (b < 0 && a < b * c) ). 因为浮点除法的耗时是浮点乘法耗时的13倍。([数据来源](https://learn.microsoft.com/en-us/previous-versions/dotnet/articles/ms973852(v=msdn.10)))

4. 绝大多数情况下都应该使用double型浮点数，避免使用float型。因为新出的电脑和手机都是64位处理器，都有硬件浮点单元，并针对double型进行了额外的优化，float型的计算速度有时甚至比double型更慢。如果没有大量的浮点数参与运算或需要存储，float型节省存储空间的优势也没有意义。float型的精度太差，可能带来一些难以察觉的bug，比如 for(float i = 0.0f; i < 20000000; i++) 就是一个难以察觉的死循环，当 i 达到 $2^{24}$=16777216 时，会由于float的精度太低，无法区分 16777216 和 16777217，即无法自增1. 另外，使用float型，所有的字面量都要加 f 后缀，所有的Math库函数前面都要加(float)进行强制类型转换(或者使用MathF库中的函数)，写起来麻烦，看起来丑陋，所以要尽量避免。在以下情形(但不限于)可考虑使用float型：
    * 训练AI模型，数据量巨大但对计算精度要求不高，float型可显著节省存储空间。
    * 程序要在32位处理器上运行，或者要在没有硬件浮点单元的处理器上运行。	

5. 如需计算 $x^n$ ，当 $n=2,3$ 时，不要使用Math.Pow(x,n), 而是直接写成 x * x 和 x * x * x. 当 $n=2^k(k\in N^+)$ 时，可用y=x, 再执行k次 y*=y 来代替。当 n 取其它值时才可调用Math.Pow.

6. 引入一些额外的变量来存储函数调用的结果，或者复杂运算过程中的子过程的值，避免重复调用和计算。比如计算二维坐标旋转: 
```
    x1=x*cos(a)-y*sin(a) 
    y1=x*sin(a)+y*cos(a)
```
 同一个角的正弦和余弦值都要使用两次。一元二次方程求根， $\frac{\sqrt{\Delta}}{2a}$ 会使用两次。二元一次方程求根，系数矩阵的行列式值会使用两次。在循环中如果要以同样的参数调用某个函数，或者有一些不随循环变化的子过程，则应提到循环外部，用变量存储。

7. 对浮点数进行取整操作时，如果确定浮点数的大小不超出int(或long)型的范围，以及不会出现NaN，则可以用强制类型转换结合条件语句替代Floor、Ceiling和Round函数，显著提高速度。使用 (int)(t ± 0.5) 来代替Math.Round(t)则需谨慎，因为当t的小数部分为0.5时，Round(t)的结果取决于中点值舍入模式的设定，默认是MidpointRounding.ToEven，即向最近的偶数舍入。其它模式还有ToZero, AwayFromZero, ToNegativeInfinity, ToPositiveInfinity. 要根据不同的舍入模式选择不同的替代写法。
```C#
	// 替代 a = (int)Math.Floor(t)
	a = (t < 0 ? (int)t - 1 : (int)t);

	// 替代 b = (int)Math.Ceiling(t)
	b = (t < 0 ? (int)t : (int)t + 1);

	// 替代 c = (int)Math.Round(t, MidpointRounding.ToZero)
	c = (t < 0 ? (int)(t - 0.5) : (int)(t + 0.5));
``` 

8. 对于Array of Struct(AoS)和Struct of Array(SoA)两种数据结构，  
>  * 内存布局：  
> AoS：每个结构体实例的所有字段在内存中是连续存储的。  
> SoA：每个字段的所有值在内存中是连续存储的，但不同字段的值分开存储。  
>  * 性能：  
> AoS：在需要频繁访问单个结构体实例的所有字段时性能较好。  
> SoA：在需要频繁访问所有实例的单个字段时性能较好，特别是在SIMD(单指令多数据)优化中表现更佳。 

对于有限元程序，需要存储大量节点的编号、坐标和位移，需要视情况选择AoS或SoA. 

9. 对于较小的结构体，可以考虑用ref struct代替struct，强制结构体存储在栈上(注意防范栈溢出)，避免装箱操作，同时减少垃圾回收的性能损失。

10. 对于局部变量，使用 Span\<T\>, ReadOnlySpan\<T\> 和 stackalloc 在栈上分配连续的小段内存(注意防范栈溢出)，比使用数组(存储在堆上)速度更快。

11. 对于分支较多的流程，优先使用[模式匹配](https://learn.microsoft.com/zh-cn/dotnet/csharp/fundamentals/functional/pattern-matching)而不是大量的if else，既能提高程序可读性，又能提高运行速度。比如分段函数就应该使用模式匹配。
```C#
    static double Foo(double x) => x switch
    {
        < 0 => -x,                // 当 x < 0 时，f(x) = -x
        >= 0 and <= 1 => x * x,   // 当 0 <= x <= 1 时，f(x) = x^2
        > 1 and <= 2 => 2 * x,    // 当 1 < x <= 2 时，f(x) = 2x
        > 2 => x + 1,             // 当 x > 2 时，f(x) = x + 1
        _ => throw new ArgumentOutOfRangeException(nameof(x), "Invalid input")
    };
```

12. 尽量避免编写含递归调用的函数。比如阶乘函数 n!，递推数列(斐波那契数列、汉诺塔问题等)，二分查找等，均可以用循环替代递归。

13. 对于那些参数的允许范围比较小的函数，优先考虑用查表法实现。比如阶乘函数 n!，因为阶乘函数增长太快，在大多数情况下，阶乘函数允许的参数的范围很小，  
 $ 13! = 6227020800 >2^{32} = 4294967296 $ = uint.MaxValue  
 $ 21!=5.109\times 10^{19}>2^{64} = 1.845\times 10^{19} $ = ulong.MaxValue  
 $ 35!=1.033\times 10^{40}>2^{128} = 3.403\times 10^{38} $ = float.MaxValue  
 $ 171!=1.241\times 10^{309}>2^{1024} = 1.798\times 10^{308} $ = double.MaxValue  
至多占用171*8=1368Byte的存储空间，就能满足double型计算的需求。不仅速度快，而且没有多次浮点乘法带来的累积误差。  
特殊情况下，指数函数的自变量如果只能取正整数，那么自变量的范围一般也不会很大，比如 $ e^{709} < 2^{1024} < e^{710} $ ，那么可以考虑对不超过某一阈值的整数采用查表法，超过该阈值则调用标准库。或者为自变量取等差数列时的函数值建立数表，然后用少量运算就能得到0~709内任意整数的函数值(参见 https://zhuanlan.zhihu.com/p/5221342896 )。  
二项式系数(组合数)和阶乘的自然对数ln(n!)也可以采用部分查表法。

14. 小于255的素数(质数)一共有54个，如下：
```C#
    static readonly byte[] PrimesLessThan255 = [2, 3, 5, 7, 11, 13, 17, 19, 23,
        29,  31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 
        103, 107, 109, 113, 127, 131, 137, 139, 149, 151,157, 163, 167, 173, 
        179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251];
```   
对正整数n进行素性测试时，可以先用上表的素数进行试除(比使用255以内的奇数试除要快)，若都不能整除，就可以继续用从257到 $\sqrt{n}$ 之间的奇数进行试除，从257开始是因为253(=11*23)和255都是合数。试除法是最简单但并不高效的素性测试方法，比较高效的方法是 <a href="http://www.matrix67.com/blog/archives/234">Miller-Rabin测试法</a>。但因为绝大多数正整数都有一个不大的素因子，比如：88％的正整数有一个小于100的素因子，92％的正整数有一个小于1000的素因子(数据来源： <a href="https://www.docin.com/p-1037060136.html">大数因子分解算法综述.刘新星</a>)。大于255但不超过1024的素数有118个。同时，不超过n的正整数中，素数占的比例大致是1/ln(n)(素数定理). 因此，构建一张比较大的素数表，采用先除素数再除奇数的试除法，对于不太大的整数，是一种勉强能用的素性测试方法，同时也是寻找素因子的方法。

15. 以e为底的指数函数有一种快速近似算法：
```C#
    public static double FastExp(double x) {  
        long tmp = (long) (1512775 * x + 1072632447);  
        return BitConverter.Int64BitsToDouble(tmp << 32);  
    }
```
该方法的速度大致是Math.Exp的5倍，原理参见《 <a href="https://nic.schraudolph.org/pubs/Schraudolph99.pdf"> A Fast, Compact Approximation of the Exponential Function</a>》. 对于神经网络中的Sigmoid函数中的指数函数，就可以采用这种近似算法。

16. 以e为底的对数函数有一种快速近似算法：
```C#
    public static double FastLn(double x) // 抛弃对x<=0的检查。
    {
        long longx = BitConverter.DoubleToInt64Bits(x);
        double k = (longx >> 52) - 1022.5; 
        return k * 0.693147180559945309;  
    }
```
该方法实际上就是Math.Log的算法的前半部分，用位运算提取了IEEE 754浮点数的阶码，而抛弃了尾数的对数，速度大致是Math.Log的4倍，其中，-1022.5 = - 1023 + 0.5，0.693147……就是ln(2)，该算法可以保证绝对误差不超过ln(2)/2=0.346573... 但该算法有一个不可忽视的弊端：设 n 为正整数，则对于区间 $[2^{n-1},2^n)$ 内的任意实数，该算法会返回完全一样的结果。以2为底或以10为底的对数函数也可以使用该方法，把最后一行与k相乘的常数换掉即可，以2为底就是return k，以10为底就是return k*0.301029995663981196. 

17. 免费的数学库推荐ALGLIB免费版，收费的数学库推荐ALGLIB、ILNumerics和Dew.Math. 不推荐 MathNET Numerics，其代码质量低下，原因参见[点评10多个C#的数学库](https://zhuanlan.zhihu.com/p/12783824787).

18. 避免在循环中做以下事情：
> * 创建对象。  
> * 使用try catch.   
> * 打开和关闭同一个文件、数据库等。  
> * 创建和断开对同一个URI的链接。  

19. 避免不加测试地用Parallel.For代替for循环，因为前者需要创建和管理多个线程，会带来额外的开销。当循环次数太少或者单次循环所做的运算太简单时，使用Parallel.For反而会降低性能，而且很可能出现计算结果不正确的问题。比如函数f(x)在某个区间上做数值积分，有sum+=f(xi)*dx这样的累加运算，需要测试Parallel.For的耗时是否更短以及结果是否正确。

20. 考虑使用[[SkipLocalsInit](https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/attributes/general#skiplocalsinit-attribute)]属性，省略CLR将方法中声明的所有局部变量初始化为其默认值的操作，提高速度。注意：此属性需要 AllowUnsafeBlocks 编译器选项，同时要重点检查代码中是否存在访问未初始化的变量的行为。

21. 绝大多数时候，矩阵求逆都是非必须的(而且计算代价很大的)，除非就是要得到逆矩阵本身。比如对于线性方程组 $Ax=b,\ x=A^{-1}b$ ，使用逆矩阵表达方程组的解只具有形式意义，不可直接用于计算。请使用高斯消去法、LU分解法、Jacobi迭代法、Gauss-Seidel迭代法、Cholesky分解法等。矩阵的逆几乎不会单独出现，几乎总是会和其它矩阵做乘法，总有不求逆的替代方案。

22. 多项式求值优先使用秦九韶算法， 
$ a_nx^n +a_{n-1}x^{n-1}+\cdots+a_1x+a_0 = (\cdots ((a_nx+a_{n-1})x+a_{n-2})x+\cdots+a_1)x+a_0 $   
对于阶数不太高的多项式(比如小于10阶)，不要使用循环语句来实现这个算法，而应该手工进行循环展开。还可以使用融合乘加指令[Fma.MultiplyAdd](https://learn.microsoft.com/zh-cn/dotnet/api/system.runtime.intrinsics.x86.fma.multiplyadd?view=net-9.0)进行进一步加速。秦九韶算法是一个串行的算法，无法并行。如果某个n次多项式的全部根均为实数(设为 $x_1$ , $x_2$ , $\cdots$ , $ x_n $ ，需要提前计算出来)，那就可以使用SIMD指令进行并行计算： $ a_n(x-x_1)(x-x_2)\cdots (x-x_n) $ 

23. 利用泰勒级数计算double型函数值时，多项式阶数通常不应该超过17阶，太高的阶数没有意义(因为浮点运算的累积误差)。泰勒级数具有局部性，离展开点越远，精度越差。所以如果要提高计算精度，首先应考虑更换展开点，而不是提高多项式的阶数。


参考文章：
 * [Writing Faster Managed Code: Know What Things Cost](https://learn.microsoft.com/en-us/previous-versions/dotnet/articles/ms973852(v=msdn.10))  
 * [新版C#高效率编程指南](https://www.cnblogs.com/hez2010/p/13724904.html)  
 * [C#中那些举手之劳的性能优化](https://www.cnblogs.com/blqw/p/3619132.html)  


