using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace Momos.Core.RPG
{
    /// <summary> 
    /// Buff行为单元:
    ///     负责接收一个Buff的实际逻辑执行;
    ///     与<see cref="BuffConfigItem"/>是1:1的关系,与<see cref="BuffWorkItemBase{R}"/>是1:n的关系。
    /// </summary>
    /// <typeparam name="R"> 挂载到的'角色的类' </typeparam>
    /// <typeparam name="WI"> 行为单元子项 </typeparam>
    public class BuffWorkBase<R,WI> : IBuffWork<R> , IEnumerable<WI>
        where R : class
        where WI : BuffWorkItemBase<R>
    {
        private WI[] workItems;

        public BuffWorkBase(WI[] workItems)
        {
            this.workItems = workItems;
        }

        /// <summary> 被动技能,并非生命周期的成员 </summary>
        public virtual void Eternal(R self) 
        {
            foreach (var item in workItems)
                item.Eternal(self);
        }

        /// <summary> 附加到对象时 必然发生 </summary>
        public virtual void Awake(int layerIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController)
        {
            foreach (var item in workItems)
                item.Awake(layerIncre, controllerItem, selfController);
        }

        /// <summary> 附加到对象时 可以不发生 </summary>
        public virtual void Enable(int layerIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController)
        {
            foreach (var item in workItems)
                item.Enable(layerIncre, controllerItem, selfController);
        }

        /// <summary> 移除自对象时 可以不发生 </summary>
        public virtual void Disable(int layerReduceIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController)
        {
            foreach (var item in workItems)
                item.Disable(layerReduceIncre, controllerItem, selfController);
        }

        /// <summary> 移除自对象时 必然发生 </summary>
        public virtual void Unactive(int layerReduceIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController)
        {
            foreach (var item in workItems)
                item.Unactive(layerReduceIncre,controllerItem,selfController);
        }

        /// <summary> 每秒做 </summary>
        public virtual void Second(BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController)
        {
            foreach (var item in workItems)
                item.Second(controllerItem, selfController);
        }

        /// <summary> 每Dot时间间隔做 </summary>
        public virtual void Dot(BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController)
        {
            foreach (var item in workItems)
                item.Dot(controllerItem, selfController);
        }

        public IEnumerator<WI> GetEnumerator()
        {
            foreach(var item in workItems)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return  GetEnumerator();
        }
    }
}