# 面试题

#### `dotnet core` 跨平台的秘密

- `CLI` (公共语言基础)
- `CIL` (公共中间语言)
- `VM` (虚拟机)
- `CLR` (公共语言运行时)
- `BCL` (基础类库)

#### `dotnet standard` 在整个体系中的作用

- TODO

#### `struct` 和 `class` 的在内存占用方面的不同.

- 考察了 对 struct 和 class 的了解

#### `GC` 的回收机制, 如何断言该对象是否可以回收

* TODO

#### `GC` 中三种回收等级分别是什么, 如何理解他们

* 0
* 1
* 2

#### 这段单例代码有什么问题, 如何改进

```java
public class Foo {}

public static readonly Foo FooSingleton = new Foo();
```

#### `DI `中, 三种生命周期的对象将会在什么时候调用 `Dispose`

* `Transient`
* `Scoped`
* `Singleton`

#### `asp.net ` 实现了从接受http请求到调用对应的 `controller `的 `action`需要编写哪些功能

- 监听指定端口
- 分析http url, 并且根据配置得到我需要命中的`controller`和`action`
- 加载(只加载一次)`Assembly` 并找到对应的`controller`
- 从`DI` 中获取实例此`controller`需要的构造参数
- 根据 http header 中的 application-type 和 action 中的配置决定如何实例 action 中的参数
- 执行 action

#### asp.net 中的配置系统 有哪些提供配置的方式

- `appSetting.json`
- 命令行开关
- 环境变量
- TODO

#### 如何设计一个 api 调用次数的计数器

- 中间件
- int 累加的同步锁(使用 lock 关键字 或者 volatile 关键字)

#### HTTP 请求由哪几部分构成

- URL       (http/https)
- Method (Get/Post/Put/Delete)
- Header  (application-type)
- Body

#### HTTP 响应由哪几部分构成

- Status Code
- Response Body

#### HTTP 响应 Status Code 各区间含义

- 100~199 ?
- 200~299 正常响应
- 300~399 重定向?
- 400~499 客户端异常
- 500~599 服务端异常
