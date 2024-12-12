using UnityEngine;
using System.Threading;

namespace Momos.Core.RPG
{
    // 柯里化类
    /// <summary> 
    /// Buff运行项: 仅声明了一些基本属性;
    ///     任何时候不要调用这个类,本类未声明且无法声明一些必备的逻辑。
    /// </summary>
    public abstract class BuffControllerItemBase : BuffObject
    {
        private static int RuntimeID = 0;
        /// <summary> 线程安全的调用<see cref="RuntimeID"/>++ </summary>
        protected static int GetRuntimeID => Interlocked.Increment(ref RuntimeID);

        protected int id;
        protected int rumtimeID;
        protected long countLifeMs = 0;
        protected int countLayer = 0;
        /// 推荐使用一次后'余参'的方式, 详见<see cref="UpdateTriggerTime"/>
        protected int countDotTimeMs = 0;
        protected int countSecondTimeMs = 0;

        public int ID => id;
        public int RID => rumtimeID;
        public int CountLayer => countLayer;

        // Dot事件触发时间间隔
        protected abstract int DotIntervalMs { get; }
        // 秒事件触发时间间隔
        protected int SecondIntervalMs => 1000;
        protected BuffConfigItem Config => GetConfig(id);

        /// <summary> 代替构造函数作用 </summary>
        public void Init(int id)
        {
            this.id = id;
            rumtimeID = GetRuntimeID;
        }

        /// <summary> 更新触发时刻 </summary>
        protected bool UpdateTriggerTime(int increMs, ref int countTimeMs, int intervalMs)
        {
            countTimeMs += increMs;
            if (countTimeMs >= intervalMs)
            {
                countTimeMs %= intervalMs;
                return true;
            }
            return false;
        }
    }

    /// <summary> 
    /// Buff运行项: 
    ///     应用'里氏转换'时, 应当调用的类, 拥有完整的逻辑;
    ///     尽管逻辑完整, 但应用时应只读一些变量、属性, 由<see cref="IBuffController{R}"/>控制逻辑链。
    /// </summary>
    /// <typeparam name="R"> 挂载到的'角色的类' </typeparam>
    public abstract class BuffControllerItemBase<R> : BuffControllerItemBase where R : class
    {
        private R giver;
        public R Giver  => giver;

        // 添加
        /// <summary> 控制器添加Buff时调用 </summary>
        public int AddBuff(int layerNum, R giver)
        {
            this.giver = giver;
            return AddLayer(layerNum);
        }
        /// <summary> 添加层数 </summary>
        private int AddLayer(int layerNum)
        {
            int cl = countLayer;
            countLayer = Mathf.Min(Config.limited, countLayer + layerNum);
            // 重置Buff计时, 不重置Dot和计秒
            // countDotTimeMs = countDotTimeMs = countSecondTimeMs = 
            countLifeMs = 0;
            return countLayer - cl;
        }

        // 减少
        /// <summary> 减少层数 </summary>
        public int ReduceBuff(int reduceNum)
        {
            int cl = countLayer;
            countLayer = Mathf.Max(0, countLayer - reduceNum);
            return cl - countLayer;
        }

        public abstract void AddEvt(int layerIncre, IBuffController<R> selfController);

        /// <returns> 更新事件并返回是否生命结束,需要移除 </returns>
        public virtual bool UpdateEvt(int increMs, IBuffController<R> selfController)
        {
            // Buff生命时间 >0 时有计时需求
            if (Config.duration > 0)
            {
                // 时间处理
                countLifeMs += increMs;
                return countLifeMs >= Config.duration;
            }
            return false;
        }

        public abstract void ReduceEvt(int layerReduceIncre, IBuffController<R> selfController);

        public abstract void RemoveEvt(int layerReduceIncre, IBuffController<R> selfController);
    }

    /// <summary> 
    /// Buff运行项: 相比'单泛型'的父类,无新增的方法;
    ///     仅需要被<see cref="BuffControllerBase{CI, R, P}"/>中作为泛型1 及, 做具体项目Buff控制器Item的父类。
    /// </summary>
    /// <typeparam name="R"> 挂载到的'角色的类' </typeparam>
    /// <typeparam name="P"> Buff处理器,继承自<see cref="BuffProcessUnitBase{R}"/> </typeparam>
    public abstract class BuffControllerItemBase<R,P> : BuffControllerItemBase<R>
        where R : class
        where P : BuffProcessUnitBase<R>
    {
        public abstract P BPU { get; }

        public override void AddEvt(int layerIncre, IBuffController<R> selfController)
            => BPU.AddInvoke(layerIncre, this, selfController);

        public override bool UpdateEvt(int increMs, IBuffController<R> selfController)
        {
            if (UpdateTriggerTime(increMs, ref countDotTimeMs, DotIntervalMs))
                BPU.DotInvoke(this, selfController);
            if (UpdateTriggerTime(increMs, ref countSecondTimeMs, SecondIntervalMs))
                BPU.SecondInvoke(this, selfController);
            return base.UpdateEvt(increMs, selfController);
        }

        public override void ReduceEvt(int layerReduceIncre, IBuffController<R> selfController) 
            => BPU.ReduceInvoke(layerReduceIncre, this, selfController);

        public override void RemoveEvt(int layerReduceIncre, IBuffController<R> selfController) 
            => BPU.RemoveInvoke(layerReduceIncre, this, selfController);
    }
}