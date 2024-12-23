# C#数学运算相关开发性能优化指导原则

华为公司的[C语言编程规范](https://ilcc.gitbooks.io/wiki/content/StyleGuide/Huawei-C/index.html)在开头就强调了：
> 一般情况下，代码的可阅读性高于性能，只有确定性能是瓶颈时，才应该主动优化。

本文讲述的原则也没有经过大项目和大公司的检验，所以，请批判性地阅读本文。本文中的大部分结论有测试代码支持，参见SpeedTest.cs. 虽然C#的编译器会在release版本中执行一些优化，C#的运行时也有一些优化，但偶尔会遇到debug版本正常，而release版本异常的问题，比如我在github上fork了已停止维护的屏幕录像软件[Captura](https://github.com/FrogGuaGuaGua/Captura)，debug模式能启动，release版本无法启动，我短时间内解决不了这个问题，如果要发布，只能发布debug版本。所以手工执行一些虽然编译器(在release版本中)会做但也简单易读的优化，还是有意义的。同时建议把未经优化的代码作为注释附在旁边，提高可读性。

1. const, readonly, in 这3个关键词在能用的地方要尽量用。这样可以让编译器执行更激进的优化策略，同时提高代码的安全性、可读性和可维护性。
20. 整数乘以或除以$2^n$ (n也是整数)，使用移位来代替。但对乘除非 $2^n$ 的整数不要使用此方法，比如不要把 i * 12 改写成 (i << 2 + i << 3).

30. 除以浮点型常数的除法，改写为乘以这个数的倒数。比如把 x / 2.0 改写为 x * 0.5  .

40. 绝大多数情况下都应该使用double型浮点数，避免使用float型。因为新出的电脑和手机都是64位处理器，都有硬件浮点单元，并针对double型进行了额外的优化，float型的计算速度有时甚至比double型更慢。如果没有大量的浮点数参与运算或需要存储，float型节省存储空间的优势也没有意义。float型的精度太差，可能带来一些难以察觉的bug，比如 for(float i = 0.0f; i < 20000000; i++){} 就是一个难以察觉的死循环，当 i 达到 $2^{24}$=16777216 时，会由于float的精度太低，无法区分16777216和16777217，即无法自增1. 另外，使用float型，所有的字面量都要加 f 后缀，所有的Math库函数前面都要加(float)进行强制类型转换(或者使用MathF库中的函数)，写起来麻烦，看起来丑陋，所以要尽量避免。在以下情形(但不限于)可考虑使用float型：
    * 训练AI模型，数据量巨大但对计算精度要求不高，float型可显著节省存储空间。
    * 程序要在32位处理器上运行，或者要在没有硬件浮点单元的处理器上运行。	

50. 如需计算$x^n$，当$n=2,3$时，不要使用Math.Pow(x,n), 而是直接写成 x * x 和 x * x * x. 当$n=2^k(k\in \bm{N}^+)$时，可用y=x, 再执行k次 y*=y 来代替。当 n 取其它值时才可调用Math.Pow.

60. 引入一些额外的变量来存储函数调用的结果，或者复杂运算过程中的子过程的值，避免重复调用和计算。比如计算二维坐标旋转: $$ x'=x\cos\theta-y\sin\theta \\
y'=x\sin\theta+y\cos\theta $$同一个角的正弦和余弦值都要使用两次。一元二次方程求根，$\frac{\sqrt{\Delta}}{2a}$会使用两次。二元一次方程求根，系数矩阵的行列式值会使用两次。在循环中如果要以同样的参数调用某个函数，或者有一些不随循环变化的子过程，则应提到循环外部，用变量存储。

70. 对浮点数进行取整操作时，如果确定浮点数的大小不超出int(或long)型的范围，以及不会出现NaN，则可以用强制类型转换结合条件语句替代Floor、Ceiling和Round函数，显著提高速度。使用 (int)(t $\pm $ 0.5) 来代替Math.Round(t)则需谨慎，因为当t的小数部分为0.5时，Round(t)的结果取决于中点值舍入模式的设定，默认是MidpointRounding.ToEven，即向最近的偶数舍入。其它模式还有ToZero, AwayFromZero, ToNegativeInfinity, ToPositiveInfinity. 要根据不同的舍入模式选择不同的替代写法。
```C#
	// 替代 a = (int)Math.Floor(t)
	a = (t < 0 ? (int)t - 1 : (int)t);

	// 替代 b = (int)Math.Ceiling(t)
	b = (t < 0 ? (int)t : (int)t + 1);

	// 替代 c = (int)Math.Round(t, MidpointRounding.ToZero)
	c = (t < 0 ? (int)(t - 0.5) : (int)(t + 0.5));
``` 

8. 对于Array of Struct和Struct of Array两种数据结构，

81. 可以考虑用ref struct代替struct，强制结构体存储在栈上，避免装箱操作，同时减少垃圾回收的性能损失。

90. 模式匹配

100. 尽量避免编写含递归调用的函数。比如阶乘函数$n!$，递推数列(斐波那契数列、汉诺塔问题等)，二分查找等，均可以用循环替代递归。

110. 对于那些参数的允许范围比较小的函数，优先考虑用查表法实现。比如阶乘函数$n!$，因为阶乘函数增长太快，在大多数情况下，阶乘函数允许的参数的范围很小，
    $13! = 6227020800 >2^{32} = 4294967296 $ = uint.MaxValue
	$21!=5.109\times 10^{19}>2^{64} = 1.845\times 10^{19} $ = ulong.MaxValue
	$35!=1.033\times 10^{40}>2^{128} = 3.403\times 10^{38} $ = float.MaxValue
	$171!=1.241\times 10^{309}>2^{1024} = 1.798\times 10^{308}$ = double.MaxValue
至多占用171*8=1368Byte的存储空间，就能满足double型计算的需求。不仅速度快，而且没有多次浮点乘法带来的累积误差。
特殊情况下，指数函数的自变量如果只能取正整数，那么自变量的范围一般也不会很大，比如 $ e^{709} < 2^{1024} < e^{710} $，那么可以考虑对不超过某一阈值的整数采用查表法，超过该阈值则调用标准库。或者为自变量取等差数列时的函数值建立数表，然后用少量运算就能得到0~709内任意整数的函数值(参见 https://zhuanlan.zhihu.com/p/5221342896)。
二项式系数(组合数)和阶乘的自然对数$\ln(n!)$也可以采用部分查表法。

120. 以e为底的指数函数有一种快速近似算法：
```C#
public static double FastExp(double x) {  
    long tmp = (long) (1512775 * x + 1072632447);  
    return BitConverter.Int64BitsToDouble(tmp << 32);  
}
```
该方法的速度大致是Math.Exp的5倍，原理参见[A Fast, Compact Approximation of the Exponential Function](https://nic.schraudolph.org/pubs/Schraudolph99.pdf). 对于神经网络中的Sigmoid函数中的指数函数，就可以采用这种近似算法。

13. 以e为底的对数函数有一种快速近似算法：
```C#
public static double FastLn(double x) // 抛弃对x<=0的检查。
{
	long longx = BitConverter.DoubleToInt64Bits(x);
	double k = (longx >> 52) - 1022.5; 
	return k * 0.693147180559945309;  
}
```
该方法实际上就是Math.Log的算法的前半部分，用位运算提取了IEEE 754浮点数的阶码，而抛弃了尾数的对数，速度大致是Math.Log的4倍，其中，-1022.5 = - 1023 + 0.5，0.693147……就是$\ln(2)$，该算法可以保证绝对误差不超过$\frac{\ln(2)}{2}=0.346573\cdots$. 但该算法有一个不可忽视的弊端：设 $n$ 为正整数，则对于区间$[2^{n-1},2^{n})$内的任意实数，该算法会返回完全一样的结果。以2为底或以10为底的对数函数也可以使用该方法，把最后一行与k相乘的常数换掉即可，以2为底就是return k，以10为底就是return k*0.301029995663981196.

14.  免费的数学库推荐ALGLIB免费版，收费的数学库推荐ALGLIB、ILNumerics和Dew.Math. 不推荐 MathNET Numerics，其代码质量低下，原因参见[点评10多个C#的数学库](https://zhuanlan.zhihu.com/p/12783824787).

150. 避免在循环中做以下事情：
> * 创建对象。
> * 使用try catch. 
> * 打开和关闭同一个文件。

16. 避免不加测试地用Parallel.For代替for循环，因为前者需要创建和管理多个线程，会带来额外的开销。当循环次数太少或者单次循环所做的运算太简单时，使用Parallel.For反而会降低性能，而且很可能出现计算结果不正确的问题。比如函数f(x)在某个区间上做数值积分，有sum+=f(xi)*dx这样的累加运算，需要测试Parallel.For的耗时是否更短以及结果是否正确。

 https://www.cnblogs.com/hez2010/p/13724904.html
 https://www.cnblogs.com/blqw/p/3619132.html

