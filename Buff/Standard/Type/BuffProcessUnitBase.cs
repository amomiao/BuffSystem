using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momos.Core.RPG
{
    /// <summary> 
    /// Buff处理器: 应当写为一个单例!
    ///     执行方法应当必有参数<see cref="BuffControllerItemBase"/> controllerItem 和<see cref="IBuffController{R}"/> selfController;
    ///     controllerItem是Buff控制器里的一个子项, 能给出一些需要的属性, 比如<see cref="BuffControllerItemBase{R}.Giver"/>。
    ///     selfController是Buff控制器, 能给出<see cref="IBuffController{R}.Self"/>, 以及一些可用操作。
    /// </summary>
    /// <typeparam name="R"> 挂载到的'角色的类' </typeparam>
    public abstract class BuffProcessUnitBase<R> where R : class
    {
        /// <summary> 
        /// 更新'Buff时间及事件';
        /// 触发 Awake和Enable。
        /// </summary>
        public abstract void AddInvoke(int layerIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController);

        /// <summary> 
        /// 更新'Buff时间及事件';
        /// 触发 Dot和Second。
        /// </summary>
        public abstract void DotInvoke(BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController);

        /// <summary> 
        /// 删去'指定Buff'的'指定层数';
        /// 触发 Disable和Unactive。
        /// </summary>
        public abstract void SecondInvoke(BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController);

        /// <summary> 
        /// 删去'指定Buff'的'指定层数';
        /// 触发 Disable和Unactive。
        /// </summary>
        public abstract void ReduceInvoke(int layerReduceIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController);

        /// <summary> 
        /// 删去'指定Buff'的'指定层数';
        /// 触发 Unactive, 而不触发Disable。
        /// </summary>
        public abstract void RemoveInvoke(int layerReduceIncre, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController);
    }
}