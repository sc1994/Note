# [ASP.NET Core 3框架揭秘](https://github.com/sc1994/WeRead/tree/main/src/WeRead.Service/wwwroot/ASP.NET%20Core%203%E6%A1%86%E6%9E%B6%E6%8F%AD%E7%A7%98 "ASP.NET Core 3框架揭秘")

## 如何实现的跨平台

#### CLI(公共语言基础)

CLI的英文全称为Common Language Infrastructure，其中Common Language说的是语言，具体描述的是一种通用语言，旨在解决各种高级开发语言的兼容性问题。Infrastructure指的则是运行时环境，旨在弥补不同平台之间执行方式的差异。Common Language 是对承载应用的二进制内容的静态描述，Infrastructure 则表示动态执行应用的引擎，所以 CLI 旨在为可执行代码本身和执行它的引擎确立一个统一的标准。

#### CIL(公共中间语言)

用来描述可执行代码的是一种被称为 CIL（Common Intermediate Language）的语言，这是一种介于高级语言和机器语言之间的中间语言。如图 2-7 所示，虽然程序源文件由不同的编程语言编写，但是我们可以借助相应的编译器将其编译成 CIL 代码。从原则上讲，设计新的编程语言并将其加入.NET 中，只需要配以相应的编译器来生成统一的CIL 代码即可。我们也可以为现有的某种编程语言（如 Java）设计一种以 CIL 为目标语言的编译器，使之成为.NET 语言。CIL 是一门中间语言，也是一门面向对象的语言，所以对于一个CIL程序来说，类型是基本的组成单元和核心要素。微软制定的 CTS（Common Type System）为CLI确立了一个统一的类型系统。

![](https://camo.githubusercontent.com/ac68e21489d1a2a5bffbc9d816d0bb034cb404cd08a12381be9d7c23b1cd0dc0/68747470733a2f2f7265732e7765726561642e71712e636f6d2f7772657075622f657075625f33333338303938345f3538)

#### VM(虚拟机)

运行环境的差异可以通过虚拟机（Virtual Machine，VM）技术来解决。虚拟机是 CIL的执行容器，能够在执行 CIL代码的过程中采用即时编译的方式将其动态地翻译成与当前执行环境完全匹配的机器指令。如图 2-8 所示，虚拟机屏蔽了不同操作系统之间的差异，使目标程序可以不做任何修改就能运行于不同的底层执行环境中，CIL实际上是一种面向虚拟机的语言。

![](https://camo.githubusercontent.com/85794f38b217c1c136bd224a408aa9695e293a6668a13f9e10d4e3edc8ec8e02/68747470733a2f2f7265732e7765726561642e71712e636f6d2f7772657075622f657075625f33333338303938345f3539)

#### CLR(公共语言运行时)

从本质上讲，按照 CLI规范设计的.NET从其诞生的那一刻就具有跨平台的“基因”，它与Java 没有本质区别。由于采用了统一的中间语言，所以只需要针对不同的平台设计不同的虚拟机（运行时）就能弥补不同操作系统与处理器架构之间的差异. 被称为CoreCLR

#### BCL(基础类库)

为了彻底实现跨平台, 微软重新设计了一套被称为CoreFX的BCL

![](https://camo.githubusercontent.com/07b881616ef32239387b3cf055fe8ab66f9b0bf4599305e5ed69fb231952d500/68747470733a2f2f7265732e7765726561642e71712e636f6d2f7772657075622f657075625f33333338303938345f3836)

上图可以看出, 通过BCL(CoreFx) 提供给上层应用一些标准的类库, 比如 System.IO 等API, 类库最终和CLR(CoreCLR)交付. 而CLR和系统进行交互.

**●** System.Collections：定义了常用的集合类型。

**●** System.Console：提供API完成基本的控制台操作。

**●** System.Data：提供用于访问数据库的API，相当于原来的ADO.NET。

**●** System.Diagnostics：提供基本的诊断、调试和追踪的API。

**●** System.DirectoryServices：提供基于AD（Active Directory）管理的API。

**●** System.Drawing：提供GDI相关的API。

**●** System.Globalization：提供API实现多语言及全球化支持。

**●** System.IO：提供与文件输入和输出相关的API。

**●** System.NET：提供与网络通信相关的API。

**●** System.Numerics：定义一些数值类型作为基元类型的补充，如 BigInteger、Complex 和Plane等。

**●** System.Reflection：提供API以实现与反射相关的操作。

**●** System.Runtime：提供与运行时相关的一些基础类型。

**●** System.Security：提供与数据签名和加密/解密相关的API。

**●** System.Text：提供针对字符串/文本编码与解码相关的API。

**●** System.Threading：提供用于管理线程的API。

**●** System.Xml：提供API用于操作XML结构的数据。

以上列出常用的基础API的命名空间.

对于传统的.NET Framework 来说，承载 BCL 的 API 几乎都定义在 mscorlib.dll 程序集中，这些API并不是全部都转移到组成CoreFX的众多程序集中，那些与运行时（CoreCLR）具有紧密关系的底层 API被定义到 System.Private.CoreLib.dll程序集中

## 依赖注入(Dependence Injection，DI)与控制反转(Inverse of Control，IoC)

#### 框架和类库的区分

类库（Library）和框架（Framework）的不同之处在于：前者往往只是提供实现某种单一功能的 API；而后者则针对一个目标任务对这些单一功能进行编排，以形成一个完整的流程，并利用一个引擎来驱动这个流程自动执行。

#### 框架为什么需要依赖注入

在好莱坞，演员把简历递交给电影公司后就只能回家等待消息。由于电影公司对整个娱乐项目具有完全控制权，演员只能被动地接受电影公司的邀约。“不要给我们打电话，我们会给你打电话”（Don’t call us，we’ll call you）——这就是著名的好莱坞法则，IoC完美地体现了这一法则。

在 IoC 的应用语境中，框架就如同掌握整个电影制片流程的电影公司，由于它是整个工作流程的实际控制者，所以只有它知道哪个环节需要哪些人员。应用程序就像是演员，它只需要按照框架制定的规则注册这些组件即可，因为框架会在适当的时机自动加载并执行注册的组件。

以 ASP.NET MVC 应用开发来说，我们只需要按照约定的规则（如约定的目录结构和文件与类型命名方式等）定义相应的 Controller类型和 View文件即可。ASP.NET MVC框架在处理请求的过程中会根据路由解析生成参数得到目标 Controller的类型，然后自动创建 Controller对象并执行。如果目标Action方法需要呈现一个View，框架就会根据预定义的目录约定找到对应的 View 文件（.cshtml 文件），并对其实施动态编译以生成对应的类型。当目标 View 对象创建之后，它执行之后生成的 HTML 会作为响应回复给客户端。可以看出，整个请求流程处处体现了“框架Call应用”的好莱坞法则。

#### 存疑 ![]()[IoC在设计模式中的应用](https://github.com/sc1994/WeRead/blob/main/src/WeRead.Service/wwwroot/ASP.NET%20Core%203%e6%a1%86%e6%9e%b6%e6%8f%ad%e7%a7%98/15.html.md)

#### 存疑 [一个简易版的依赖注入容器](https://github.com/sc1994/WeRead/blob/main/src/WeRead.Service/wwwroot/ASP.NET%20Core%203%e6%a1%86%e6%9e%b6%e6%8f%ad%e7%a7%98/17.html.md)

#### **生命周期模式**

**●** Singleton：IServiceProvider对象创建的服务实例保存在作为根容器的 IServiceProvider对象中，所以多个同根的IServiceProvider对象提供的针对同一类型的服务实例都是同一个对象。

**●** Scoped：IServiceProvider对象创建的服务实例由自己保存，所以同一个 IServiceProvider对象提供的针对同一类型的服务实例均是同一个对象。

**●** Transient：针对每次服务提供请求，IServiceProvider 对象总是创建一个新的服务实例。

对于asp.net 来说, Singleton在程序启动的时候创建一次, 之后一直复用此实例, 而 Scoped在每一个请求中只存在一个实例, Transient则在每次注入的时候都会创建一个实例.
