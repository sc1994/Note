# 笔记

#### 重载构造器

```C#
public ClassName(){}
public ClassName(string arg) : this() {}
```

当构造器调用另一个构造器时，被调用的构造器先执行。

子类必须声明自己的构造器。派生类可以访问基类的构造器，但是并非自动继承。例如，如果我们定义了如下的Baseclass和Subclass：

```C#
public class Baseclass
{
  public int X;
  public Baseclass () { }
  public Baseclass (int x) { this.X = x; }
}
public class Subclass : Baseclass { }
```

则下面的语句是非法的：

```C#
Subclass s = new Subclass (123);
```

#### 静态构造器

支持两个修饰符：`unsafe` 和  `extern`。

#### 静态属性

静态属性的执行顺序, 依据属性声明的前后顺序

```C#
class Foo
{
	public static int X = Y;     // 0
	public static int Y = 3;     // 3
}

```

```C#
class Program
{
	static void Main() { Console.WriteLine (Foo.X); }    // 3
}
class Foo
{
	public static Foo Instance = new Foo();
	public static int X = 3;
	Foo() { Console.WriteLine (X); }    // 0
}

```

#### 构造器和字段初始化的顺序

当对象实例化时，初始化按照以下的顺序进行：

1．从子类到基类a）初始化字段b）计算被调用的基类构造器中的参数

2．从基类到子类a）构造器方法体的执行

```C#
public class B
{
	int x = 1;                 // Executes 3rd
	public B (int x)
	{
            ...                   // Executes 4th
	}
}
public class D : B
{
	int y = 1;             	      // Executes 1st
	public D (int x)
		: base (x + 1)        // Executes 2nd
	{
		...                   // Executes 5th
	}
}

```

#### 静态类

类可以标记为static，表明它必须只能够由static成员组成，并不能够派生子类。`System.Console `和 `System.Math`类就是静态类的最好示例。

#### 常量

常量是一种值永远不会改变的静态字段。常量会在编译时静态赋值，编译器会在常量使用点上直接替换该值（类似于C++的宏）。常量可以是内置的数据类型：bool、char、string或者枚举类型。

#### GetType 方法和 typeof 运算符

C#中的所有类型在运行时都会维护System.Type类的实例。有两个基本方法可以获得System.Type对象：

-  在类型实例上调用GetType方法

-  在类型名称上使用typeof运算符

GetType在运行时计算而typeof在编译时静态计算（**如果是用泛型类型参数，那么它将由即时编译器解析**）。

#### 结构体

结构体和类相似，不同之处在于：

-  结构体是值类型，而类是引用类型。

-  结构体不支持继承（除了隐式派生自object类型，或**更精确地说，是派生自System. ValueType**）。

除了以下内容，结构体可以包含类的所有成员：

- 无参数的构造器(**隐式包含一个无法重写的无参数构造器，将字段按位置为0**)
- 字段初始化器
- 终结器
- 虚成员或protected成员

#### 友元程序集

在一些高级的场景中，添加System.Runtime.CompilerServices.InternalsVisibleTo程序集特性就可以将internal成员提供给其他的友元程序集访问。可以用如下方法指定友元程序集：

```C#
[assembly: InternalsVisibleTo ("Friend")]
```

如果友元程序集有强名称（见第18章），必须指定其完整的160字节公钥：

[assembly: InternalsVisibleTo ("StrongFriend, PublicKey=0024f000048c...")]可以使用LINQ查询的方式从强命名的程序集中提取完整的公钥值（关于LINQ的介绍请参见第8章）：

```C#
        string key = string.Join ("",
          Assembly.GetExecutingAssembly().GetName().GetPublicKey()
            .Select (b => b.ToString ("x2")));
```

#### 可访问性封顶

类型的可访问性是它内部声明成员可访问性的封顶。关于可访问性封顶，最常用的示例是internal类型中的public成员。例如：

```C#
class C { public void Foo() {} }
```

C的（默认）可访问性是internal，它作为Foo的最高访问权限，使Foo成为internal的。而将Foo指定为public的原因一般是为了将来将C的权限改成public时重构的方便。

#### 访问权限修饰符的限制

当重写基类的函数时，重写函数的可访问性必须一致。例如：

```C#
        class BaseClass             { protected virtual  void Foo() {} }
        class Subclass1 : BaseClass { protected override void Foo() {} }  // OK
        class Subclass2 : BaseClass { public    override void Foo() {} }  // Error
```

（若在另外一个程序集中重写protected internal方法，则重写方法必须为protected。这是上述规则中的一个例外情况。）

编译器会阻止任何不一致的访问权限修饰符。例如，子类可以比基类的访问权限低，但不能比基类的访问权限高：

```C#
        internal class A {}
        public class B : A {}            // Error
```
