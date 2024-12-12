<!--TOC-->
- [(一) 命名规则](#-)
  - [1 基本规则](#1-)
  - [2 命名空间命名](#2-)
  - [3 类命名](#3-)
  - [4 接口命名](#4-)
  - [5 方法命名](#5-)
  - [6 变量命名](#6-)
  - [7 控件命名](#7-)
  - [8 常量定义](#8-)
- [(二) 代码格式](#-)
  - [1 缩进](#1-)
  - [2 大括号(花括号)](#2-)
  - [3 换行](#3-)
  - [4 空行](#4-)
  - [5 空格](#5-)
  - [6 排序](#6-)
  - [7 文件定义](#7-)
  - [8 其它](#8-)
- [(三) 注释规则](#-)
  - [1 注释类型](#1-)
    - [(1) 模块注释](#1-)
    - [(2) 属性注释](#2-)
    - [(3) 方法注释](#3-)
    - [(4) 代码间注释](#4-)
  - [2 注释规范](#2-)
- [(四) 设计规则](#-)
  - [1 类设计](#1-)
  - [2 成员设计](#2-)
    - [(1) 字段](#1-)
    - [(2) 属性](#2-)
    - [(3) 事件](#3-)
    - [(4) 构造函数](#4-)
    - [(5) 方法](#5-)
  - [3 异常](#3-)
<!--/TOC-->

术语定义
* Pascal 命名法：将标识符的首字母和后面连接的每个单词的首字母都大写。可以对三字符或更多字符的标识符使用 Pascal 大小写。例如： BackColor。  
* Camel 命名法：标识符的首字母小写，而每个后面连接的单词的首字母都大写。例如： backColor。  
* 匈牙利命名法：匈牙利命名法是一种编程时的命名规范。基本原则是：变量名=属性+类型+对象描述，其中每一对象的名称都要求有明确含义，可以取对象名字全称或名字的一部分。  
* 大写：标识符中的所有字母都大写。仅对于由两个或者更少字母组成的标识符使用该约定。例如：http://System.IO，System.Web.UI  
* 文档的规范分为三种：【强制】【推荐】【参考】，表示规范需要遵循的级别。
    
# (一) 命名规则  

 ## 1 基本规则
【强制】不能使用匈牙利命名法。  
【强制】所有编程相关的命名严禁使用拼音与英文混合的方式，更不允许直接使用中文的方式。  
【推荐】杜绝完全不规范的缩写，避免望文不知义，除非它是众所周知的。  
【推荐】变量名和常量名最多可以包含255个字符，但是，超过30个字符的名称比较笨拙。此外，要想取一个有实际意义的名称，清楚地表达变量或常量的用途，30个字符应当足够。  
 ## 2 命名空间命名
【强制】采用Pascal命名法。  
【推荐】采用有意义的命名空间名，例如产品名称或公司名称。  
 ## 3 类命名
【强制】采用Pascal命名法。  
【推荐】名字可以有两个或三个单词组成，但通常不应多于三个。  
【推荐】使用名词或名词短语命名类。  
【推荐】不要使用下划线字符(_)。  
【推荐】自定义属性类名采用Attribute作为后缀。  
【推荐】自定义异常类名采用Exception作为后缀。  
【推荐】枚举类的成员名称使用Pascal命名法，枚举为特殊的类，成员均为常量。  
【推荐】抽象类命名采用Base作为后缀。  
 ## 4 接口命名
【强制】和类命名相似。  
【强制】在名字前加上"I"前缀。  
【推荐】接口类中的方法和属性不要加任何修饰符号(public也不要加)，保持代码的简洁性。  
	   
```csharp
// 正例：
public interface ICacheService
{
	void Test();
}
```  
【推荐】实现类对应的接口命名应该只在实现类名字前加上"I"前缀。  
> 正例：CacheService实现自ICacheService接口。  

【推荐】如果形容能力的接口名称，取对应的形容词为接口名(一般为-able结尾)。
> 正例：IDisposeable接口。
 ## 5 方法命名
【强制】和类命名相似。  
【推荐】采用动词-对象对命名方法，例如： 
```C#
public void SetEnvelope (double minx, double miny, double maxx, double maxy)
```
【推荐】有返回值的方法应该取名表示其返回值，例如：  
```C#
public string GetEnvelope()
```  
 ## 6 变量命名
程序中变量名称 = 变量的前缀 +代表变量含意的英文单词或单词缩写。  
【强制】类模块级的变量采用Camel命名法，加“_”前缀。  
```C#
public class Hello
{
	private string _name;
	private DateTime _date;
}
```
【强制】属性采用Pascal命名法，属性对应的变量采用Pascal命名法，加“_”前缀。  
```C#
public class Hello
{
	private string _Name;
	public string Name //属性
	{
		get{
			return _Name;
		}
	}
	public int Node { get; private set; } //属性
}
```
【强制】方法的参数和方法内的变量采用Camel命名法。  
```C#
public void Request(string webRequestUri, object userData)
{
	WWWFormInfo wwwFormInfo = (WWWFormInfo)userData;
}
```
【推荐】在变量名中使用互补对，如 min/max、begin/end和 open/close等。
```C#
public void SetEnvelope (double minx, double miny, double maxx, double maxy) 
{
//设置范围 
} 
```
常见的反义词组  
> add/remove begin/end create/destory insert/delete first/last get/release 
increment/decrement put/get add/delete lock/unlock open/close
> min/max old/new start/stop next/previous source/target show/hide 
send/receive cut/paste up/down source/destination  

【推荐】单字符的变量名一般只用于生命期非常短暂的变量。i,j,k,m,n一般用于 integer；c,d,e一般用于character；s用于string
```C#
public class Hello 
{ 
	private const int MAX_COUNT = 10; 
	private void Say(string sayWord) 
	{ 
		for (int i = 0; i < MAX_COUNT; i++) 
		{
			//for 循环
		}
	}
}
```
【推荐】尽可能避免使用var定义变量，尤其是等号右侧无法直接看出类型时。  
## 7 控件命名
【参考】控件命名=控件缩写前缀+变量名  
```C#
private Label lblNote;
private Button btnOk;
private TextBox txtName;
```  
【参考】winform控件缩写
|控件名称	| 缩写         |
|----|----                 |
|Button | Btn              |
|CheckBox | Chk            |
|CheckedListBox | Ckl      |
|ComboBox | Cmb            |
|DateTimePicker | Dtp      |
|Label | Lbl               |
|LinkLabel | Llb           |
|ListBox | Lst             |
|ListView | Lvw            |
|MaskedTextBox | Mtx       |
|MonthCalendar | Cdr       |
|NotifyIcon | Icn          |
|NumeircUpDown | Nud       |
|PictureBox | Pic          |
|ProgressBar | Prg         |
|RadioButton | Rdo         |
|RichTextBox | Rtx         |
|TextBox | Txt             |
|ToolTip | Tip             |
|TreeView | Tvw            |
|WebBrowser | Wbs          |
|FlowLayoutPanel | Flp     |
|GroupBox | Grp            |
|Panel | Pnl               |
|SplitContainer | Spl      |
|TabControl | Tab          |
|TableLayoutPanel | Tlp    |
|ColorDialog | Cld         |
|FolderBrowserDialog | Fbd |
|FontDialog | Fnd          |
|OpenFileDialog | Ofd      |
|SaveFileDialog | Sfd      |
|ImageList | Img           |
|PageSetupDialog | Psd     |
|PrintDialog | Prd         |
|PrintDocument | Pdc       |
|PrintPreviewControl | Prv |
|PrintPreviewDialog | Ppd  |
|CrystalReportViewer | Crv |
|HScrollBar | Hsc          |
|PropertyGrid | Prg        |
|Splitter | Spl            |
|TrackBar | Trb            |
|VScrollBar | Vsc          |
## 8 常量定义
【强制】不允许任何魔法值(即未经预先定义的常量)直接出现在代码中。 
```c# 
//反例：
int x,q;
q = (607 * x) >> 14;
``` 
上面的607和14都是魔法值，当1<=x<=709时，上式与 q = x / 27 等价，但速度比除法更快。
【强制】常量名也应当有一定的意义，采用Pascal 命名法。  
```c# 
public const string kHttpVerbHEAD = "HEAD";
```
【强制】在 long 或者 Long 赋值时，数值后使用大写字母L，不能是小写字母l，小写容易跟数字混淆，造成误解。   
```C#
// 反例：
long a = 2l; //小写l容易被误认为1
```    
【强制】仅对本来就是常量的值使用const修饰符，例如 $\pi, e, \ln2 $ 等数学常数。避免对只读变量使用const修饰符。在此情况下，采用readonly修饰符。例如：  
```c# 
public class MyClass { public readonly int Number;
public MyClass (int someValue) 
{ 
	Number = someValue;
}
public const double M_PI = 3.14159265358979323846;
public const double M_E = 2.7182818284590452354;
public const double M_LN2 = 0.69314718055994530942;
} 
```
【推荐】不要使用一个常量类维护所有常量，要按常量功能进行归类，分开维护。  
> 正例：缓存相关的常量放在类CacheConstants下；系统配置常量放ConfigConstants下。  

【推荐】如果变量值仅在一个固定范围内变化用 enum 类型来定义。  


# (二) 代码格式

 ## 1 缩进
 【强制】采用4个空格缩进，禁止使用Tab字符。配置Visual Studio文字编辑器，以4个空格代替制表符。
 ## 2 大括号(花括号)
【强制】在代码中垂直对齐左括号和右括号，大括号总是放在新行中。
```C#
// 正例：
if (0 == x) 
{
	Console.Write("用户编号必须输入！"); 
}
``` 
 
```C#
// 反例：
if (0 == x){ 
	Console.Write("用户编号必须输入！"); 
}
```
【推荐】类的自动属性花括号与代码合占一行：
```C#
public string Name { get; set; }
```
【推荐】大括号里的内容为空时，花括号与代码合占一行
```C#
private void Test() { }
```
【推荐】if语句总是使用括号，即使它包含一句语句。
```C#
// 正例：
if (0 == x) 
{
	Console.Write("用户编号必须输入！"); 
}
// 反例：
if (0 == x)
	Console.Write("用户编号必须输入！");
```
 ## 3 换行
【强制】不要在同一行内放置一句以上的代码语句，这会使得调试器的单步调试变得更为困难。  
 【推荐】每行代码和注释不要超过 80个字符或屏幕的宽度，如超过则应换行。  
【推荐】当一行被分为几行时，通过将串联运算符放在每一行的末尾而不是开头，清楚地表示没有后面的行是不完整的。  
 ## 4 空行
 【强制】当接口、类、枚举定义在同一文件中时，接口、类和枚举的定义之间使用两个空行。
【强制】以下情况使用一个空行：  
> 方法与方法之间
> 属性和属性之间
> 属性和方法之间
> 方法中变量声明和语句之间  

 【推荐】以下情况使用一个空行： 
> 注释与前面的语句之间
> 不同逻辑、不同语义、不同业务的代码之间
> 方法的返回语句和其他的语句之间
> 语句控制块之后，如if、for、while、switch等。 

 ## 5 空格
【强制】任何二目、三目运算符的左右两边都需要加一个空格。包括赋值运算符=、逻辑运算符&&、加减乘除符号等。  
【强制】关键字和左括号(用空格隔开。  
【强制】方法名和左括号之间没有空格。  
【强制】多个参数之间用逗号隔开，逗号后添加一个空格。  
【强制】语句中的表达式之间用空格隔开。如：  
```C#
for (expr1; expr2; expr3)
bool bol = false;
int a = bol ? 1 : 2;
int b = 2;
while (a > b) {
	b++;
}
Print("test", "test2");
for (int i = 0; i < b; i++)
{
	b++;
}
```
【推荐】没有必要增加若干空格来使变量的赋值等号与上一行对应位置的等号对齐。  
```C#
// 正例：
private int test = 10;
protected string test2 = "222";
public float test3 = 0.3F;
// 反例：
private   int    test = 10;
protected string test2 = "222";
public    float  test3 = 0.3F;
```
 ## 6 排序
【推荐】自定义或第三方的命名空间放在前面，所有System命名空间名一起放在最后面。(但是VS的自动排序功能可能破坏这一规则。)  
【推荐】类中排序：  
> 字段：私有字段，受保护字段(共有字段当作属性对待)  
> 属性：受保护属性，共有属性  
> 事件：私有事件，受保护事件，共有事件  
> 构造函数：根据参数数量，从少到多  
> 方法：方法按照功能分块，可以考虑用#region #endregion将方法块分开。 

 【推荐】局部变量的定义尽可能靠近它的初次使用。
 ## 7 文件定义
【推荐】类型应该与cs文件名称一致。
【推荐】一个cs文件只能定义一个类、接口、枚举、结构体。
【推荐】一个文件应该只对一个命名空间提供类型。避免在同一文件中有多个命名空间。
【推荐】避免文件长度超过500行(除了机器自动产生的代码)。
 ## 8 其它
【强制】不要手工编辑任何机器生成的代码。如果修改机器生成的代码，修改代码格式和风格以符合本编码标准。尽可能采用 partial类以分解出需要维护的部分。  
【推荐】避免方法定义超过25行。  
【推荐】避免超过5个参数的方法，使用结构传递多个参数。  
【推荐】不用提供public或protected成员变量，而是使用属性。 


# (三) 注释规则

 ## 1 注释类型
### (1) 模块注释   
主要用于文件、类、接口等。  
【推荐】模块开始以以下形式书写模块注释，标明模块的用途，使用方法等：
```
/********************************************************************
*
* Copyright (C) 2010-2018, ImageSky Co.,Ltd.,All Rights Reserved.
*
*********************************************************************
* FileName:DBHelper
* Author：xxx xxxx <xxx.xxx@xx.com> 
* Create Date: 2018/7/16 15:44:40
* Version: Version 1.0.0
* Description:数据库操作类
*
*--------------------------------------------------------------------
* Modifier：
* Modify Date:
* Description:
*
*********************************************************************/
```
【推荐】如果模块有修改，则每次修改必须添加以下注释：
```
/********************************************************************
*--------------------------------------------------------------------
* Modifier：
* Modify Date:
* Description:
*
*********************************************************************/
```
### (2) 属性注释
【推荐】类的属性以以下格式编写属性注释：
```
/// <summary>  
/// 属性说明 
/// </summary> 
```
### (3) 方法注释
【推荐】类的方法声明前，以以下格式编写注释
```C#
/// <summary>  
/// 说明：对该方法的说明
/// </summary>  
/// <param name="参数名称"> <参数说明> </param>   
/// <returns>  
/// 对方法返回值的说明，该说明必须明确说明返回的值代表什么含义
/// </returns> 
```
### (4) 代码间注释
代码间注释分为单行注释，多行注释和尾随注释：
单行注释：
> //<单行注释>   

多行注释：  
> /\*多行注释 1 
>    多行注释 2 
>    多行注释 3 \*/  

尾随注释：  
> int a = 10; //尾随注释
 ## 2 注释规范
【强制】注释缩进和其注释的代码在同一层次。  
【强制】所有注释要经过拼写检查。拼写错误的注释表明开发的草率。  
【推荐】保持注释的简介，避免对显而易见的内容作注释，过多的注释反而会影响代码的可读性。代码应该是自解释的。由可读性强的变量和方法组成的好的代码应该不需要注释。  
【推荐】如果语句块(比如循环和条件分枝的代码块)代码太长，嵌套太多，则在其结束“}”要加上注释，标志对应的开始语句。如果分支条件逻辑比较复杂，也要加上注释。  
【推荐】与其“半吊子”英文来注释，不如用中文注释把问题说清楚。专有名词与关键字保持英文原文即可。  
> 反例：“TCP 连接超时”解释成“传输控制协议连接超时”，理解反而费脑筋。  

【推荐】代码修改的同时，注释也要进行相应的修改，尤其是参数、返回值、异常、核心逻辑等的修改。  
【推荐】仅对操作的前提、内在算法等写文档。  
【推荐】避免方法级的文档。对API文档采用大量的外部文档。方法级注释仅作为对其他开发人员的提示。  
【参考】谨慎注释掉代码。在上方详细说明，而不是简单地注释掉，建议用#region #endregion进行折叠，保持代码文件的干净整洁。如果无用，则删除。  
> 说明：代码被注释掉有两种可能性：1)后续会恢复此段代码逻辑。2)永久不用。
前者如果没有备注信息，难以知晓注释动机。后者建议直接删掉即可，假如需要查阅历史代码，登录代码仓库即可。


# (四) 设计规则

 ## 1 类设计
【推荐】类和接口中方法和属性的比例至少是2：1.  
【推荐】避免使用只有一个成员的接口。  
【推荐】努力使每个接口拥有3-5个成员。  
【推荐】每个接口不用超过20个成员。  
【推荐】避免将事件作为接口成员。  
【推荐】避免使用抽象方法，而是使用接口代替。  
【推荐】在类层次中暴露接口。  
【推荐】优先使用明确的接口实现。  
【推荐】永远不要假设一种类型支持某个接口。防护性地检查是否支持该接口。例如：  
```C#
SomeType obj1; 
IMyInterface obj2; 
/* Some code to initialize obj1, then: */ 
obj2 = obj1 as IMyInterface; 
if (obj2 != null)
{ 
	obj2.Method1(); 
} 
else 
{ 
	//Handle error in expected interface 
}
```
【推荐】少用静态类，静态类应该仅用作辅助类、工具类。  
【推荐】不要把静态类当作杂物类，每一个静态类都应该有其明确目的。  
【推荐】要用枚举来强调那些表示值的集合的参数、属性以及返回值的类型性。  
【推荐】优先使用枚举而不是静态常量。例如：  
```C#
//反例：
public static class Color
{
public static int Red = 0;
public static int Green = 1;
public static int Blue = 2;
}
//正例：
public enum Color
{
	Red = 0,
	Green = 1,
	Blue = 2
}
```
【参考】枚举最后一个值不要加逗号。  
 ## 2 成员设计
字段、属性、事件、构造函数和方法等统称为成员  
### (1) 字段  
【推荐】不要提供公有的或受保护的字段。代之以属性来访问字段。  
### (2) 属性  
【推荐】如果不允许调用方法来改变属性值，则创建只读属性。  
【推荐】不要创建只写属性。  
【推荐】属性的获取中尽量不要抛出异常。可以使用方法来代替。  
### (3) 事件  
【推荐】类成员有委托时，使用前将委托复制到局部变量，以避免并发冲突。调用前始终检查委托是否为空。例如  
```C#
public class MySource
{
public event EventHandler MyEvent; 
public void FireEvent() 
{
	EventHandler temp = MyEvent; 
	if (temp != null) 
	{ 
		temp(this, EventArgs.Empty); 
	}
}
}
```
【推荐】不要提供 public 的事件成员变量，而是使用事件访问器。例如：  
```C#
public class MySource 
{ 
	private MyDelegate _SomeEvent; 
	public event MyDelegate SomeEvent 
	{
		add { m_SomeEvent += value; } 
		remove { m_SomeEvent -= value; } 
	}
}
```
### (4) 构造函数  
【推荐】构造函数参数如果仅用来设置属性，应和属性名称相同，仅区分大小写。  
【推荐】 要在类中显示的声明公用的默认构造函数，如果这样的构造函数是必须的。  
说明：如果没有显示默认构造函数，填加有参数构造函数时往往会破坏已有使用默认构造函数的代码。  
### (5) 方法
【推荐】方法重载时，如果两个重载时的某个参数表示相同的输入，则使用相同的名字。  
【推荐】方法重载时，同名参数的参数顺序尽量保持一致。例如：  
```C#
public class MyClass
{
	public void Test();
	public void Test(string logName);
	public void Test(string logName, string machineName);
	public void Test(string logName, string machineName, string source);
}
```
【推荐】方法重载时，较短的重载应该仅仅调用较长的来实现。另外，如果重载需要扩展性，把最长的做成虚函数。例如  
```C#
public class MyClass
{
	public int IndexOf(string s)
	{
		//调用
		return IndexOf(s, 0);
	}
	public int IndexOf(string s, int startIndex)
	{
		//调用
		return IndexOf(s, startIndex, s.Length);
	}
	public virtual int IndexOf(string s, int startIndex, int Count)
	{
		//实际的代码
	}
}
```
## 3 异常  
【强制】不使用异常实现来控制程序流程结构。  
【推荐】优先考虑使用 System 命名空间中已有的异常，避免使用自定义的异常类。  
【推荐】应该使用最合理的，最具针对性的异常。  
【推荐】自定义异常类时： 
> 从 ApplicationException 继承。  
> 提供自定义的序列化。  
> 避免太深的继承层次。  

【推荐】catch语句中，总是抛出最初异常以保持最初错误的堆栈位置。
```C#
catch(Exception exception)
{  
	MessageBox.Show(exception.Message); 
	throw; //Same as throw exception;
}
``` 
【推荐】仅捕获已经显式处理了的异常。  
【推荐】避免将错误代码作为方法的返回值。  
【推荐】对应用程序进行日志和跟踪。  


参考资料：  
[知乎 C# 编码规范手册](https://zhuanlan.zhihu.com/p/663255342)  
[Microsoft C# 标识符命名规则和约定](https://learn.microsoft.com/zh-cn/dotnet/csharp/fundamentals/coding-style/identifier-names)  
[Microsoft C# 代码约定](https://learn.microsoft.com/zh-cn/dotnet/csharp/fundamentals/coding-style/coding-conventions)  
[Microsoft .NET 框架设计准则](https://learn.microsoft.com/zh-cn/dotnet/standard/design-guidelines/)
[万方数据 dotnet开发规范](https://github.com/wanfangdata/guide/blob/master/dotnet%E5%BC%80%E5%8F%91%E8%A7%84%E8%8C%83/%E7%BC%96%E7%A0%81%E8%A7%84%E8%8C%83.md)