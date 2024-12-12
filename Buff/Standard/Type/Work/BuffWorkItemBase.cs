using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momos.Core.RPG
{
    /// <summary> 
    /// Buff行为单元子项:
    ///     负责解释一个Buff的子条目的文本配置, 并实现运行。
    /// </summary>
    /// <typeparam name="R"> 挂载到的'角色的类' </typeparam>
    public abstract class BuffWorkItemBase<R> : IBuffWork<R> where R : class
    {
        /// <summary> 被动技能,并非生命周期的成员 </summary>
        public virtual void Eternal(R self) { }

        /// <summary> 附加到对象时 必然发生 </summary>
        public virtual void Awake(int layerIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController) { }

        /// <summary> 附加到对象时 可以不发生 </summary>
        public virtual void Enable(int layerIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController) { }

        /// <summary> 移除自对象时 可以不发生 </summary>
        public virtual void Disable(int layerReduceIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController) { }

        /// <summary> 移除自对象时 必然发生 </summary>
        public virtual void Unactive(int layerReduceIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController) { }

        /// <summary> 每秒做 </summary>
        public virtual void Second(BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController) { }

        /// <summary> 每Dot时间间隔做 </summary>
        public virtual void Dot(BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController) { }
    }
}