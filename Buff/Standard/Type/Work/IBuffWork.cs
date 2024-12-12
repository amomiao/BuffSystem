using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momos.Core.RPG
{
    public interface IBuffWork<R> where R : class
    {
        /// <summary> 被动技能,并非生命周期的成员 </summary>
        public void Eternal(R self);

        /// <summary> 附加到对象时 必然发生 </summary>
        public void Awake(int layerIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController);

        /// <summary> 附加到对象时 可以不发生 </summary>
        public void Enable(int layerIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController);

        /// <summary> 移除自对象时 可以不发生 </summary>
        public void Disable(int layerReduceIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController);

        /// <summary> 移除自对象时 必然发生 </summary>
        public void Unactive(int layerReduceIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController);

        /// <summary> 每秒做 </summary>
        public void Second(BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController);

        /// <summary> 每Dot时间间隔做 </summary>
        public void Dot(BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController);
    }
}