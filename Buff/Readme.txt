依赖:
ProjectBase
	BaseManager<T>: 弱依赖
EditorTools
	EditorToolPack: 提供一些绘制封装。
	ScrollViewGrid: 提供一个冻结首行的滚动视图。

目录说明:
	./Standard 目录下是基础的依赖, 应该可以应付多数情况, 谨慎修改。
		1.Standard/Config是ScriptOjbect配置文件相关。
			1) 声明了菜单条目: [MenuItem("Tools/BuffSystem/BuffConfigSet", priority = 150)]
		2.Standard/Type是运行时的'基础依赖'。
			1) Type/Internal 中的内容非必要不继承
				BuffObject 封装一些内部接口, 无需在意。
				IBuffController 控制器的接口
			2） Type/Work 作业单元, 提供运行实现的一个子系统, 在Standard中无与主系统未耦合。
				IBuffWork 集合-子项行为规范
				BuffWorkBase 集合: 拥有子项, 外部调用时, 内部foreach子项
				BuffWorkItemBase 子项: 声明一些子类, 声明逻辑并装载信息, 提供实现。
			3)	BuffControllerBase 挂载在角色身上的Buff控制器
			4） BuffControllerItemBase Buff运行项
			5） BuffProcessUnitBase Buff处理器: 应当写为一个单例!

具体详见Demo
快速入手: 使用继承构建项目系统。
	1.自行声明一个作为Buff载体的类, 其将作为'基础依赖'中的泛型'R'。
	2.BuffSystem自行配置一些测试信息。
	3.新建一个BuffProcessUnitBase<R>子类, 下称'P'。
		实现抽象方法, 可以写一些Deb.log()做测试。
	4.新建一个BuffControllerItemBase<R,P>子类, 下称'CI'。
	5.新建一个BuffControllerBase<CI,R,P>子类, 下称'C'。
	6.为自定义的角色R, 增加一个C类型的变量, 下称'c'。
		此时运行Role.c可以运行IBuffController中的方法。
	此时Buff量比较小的游戏可以直接在'P'的方法中，使用Switch id去实现。

增加一个新的'事件触发',以造成攻击为例。
	如Demo:
	1. 让处理器拥有'事件处理'能力: 
		在'P'中声明:
		int AtkInvoke(int, BuffControllerItemBase<R>, IBuffController<R>);
		可以写入任意参数, 
		但BuffControllerItemBase<R>, IBuffController<R>的存在是必要的,
		BuffControllerItemBase<R>是Buff控制器里的一个子项, 能给出Giver以及一些需要的属性,
		IBuffController<R>是Buff控制器, Self以及一些可用操作
	2. 让控制器子项链接到处理器:
		在'CI'中声明 与'P.AtkInvoke'结构一致的方法:
		int AtkEvt(int, BuffControllerItemBase<R>, IBuffController<R>) 
			=> BPU.AtkInvoke(finalValue, this, selfController);
	3. 让控制器调用子项方法,实现给角色提供的'事件接口':
		在'C'中声明 与'CI.AtkEvt'结构一致的方法: 
		public int AtkBuff(int finalValue)	// 引用Demo中的逻辑
		{
		    int account = finalValue;
		    // 累计所有的增量
		    foreach (DemoBuffControllerItem item in this)
		        account += (item.AtkEvt(finalValue, item, this) - finalValue);
		    return account;
		}
	此时'R'就拥有了'c.AtkBuff(int)'的事件可以触发。

使用'Work'进行更深度的Buff作业。
	1. 声明一个新的接口, 继承IBuffWork<R>, 增写在'P'中新添加的'事件触发', 下称'IW'。
		若无新增'事件触发', 也应当声明, 因为不可能保证未来是否有需求;
		IW和IW<R>的声明方式皆可。
	2. 声明一个类, 继承BuffWorkItemBase<R>与IW, 下称'WI'。
		对接口方法使用'空的虚方法'实现即可。
	3. 声明一个类, 继承BuffWorkBase<R, WI>与IW, 下称'W'。
		对每一个新增方法,写 foreach (var item in this)=>Item.Evt即可。
	4. 在'P'中接入'W', 'P'中的方法接入'W'中的对应方法。
		详见Demo中的'DemoBuffProcessUnit'。
	5. 对详细的Buff行为细节声明类, 即声明一个继承'WI'的子类。
		构造'W':'WI'(1:n),
		'P'中收容所有'W', 并根据id调用对应的'W', 处理作业。
		详情查看Demo的'WI_AddAttribute'的调用情况。