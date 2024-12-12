using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momos.Core.RPG
{
    /// <summary> Buff控制器接口,使调用者只需要<see cref="BuffControllerBase{T, R, P}"/>中的一个通用泛型<see cref="R"/>。  </summary>
    /// <typeparam name="R"> 挂载到的'角色的类' </typeparam>
    public interface IBuffController<R> where R : class
    {
        public R Self { get; }

        /// <summary> 
        /// 更新'Buff时间及事件';
        /// 触发 Awake和Enable。
        /// </summary>
        public void AddBuff(int id, int layerIncre, R giver);

        /// <summary> 
        /// 更新'Buff时间及事件';
        /// 触发 Dot和Second。
        /// </summary>
        public void UpdateBuff(int increMs);

        /// <summary> 
        /// 删去'指定Buff'的'指定层数';
        /// 触发 Disable和Unactive。
        /// </summary>
        public void ReduceBuff(int id, int reduceLayerNum);
        /// <summary> 
        /// 删去'整个Buff';
        /// 触发 Disable和Unactive。
        /// </summary>
        public void ReduceBuff(BuffControllerItemBase item);

        /// <summary> 
        /// 删去'指定Buff'的'指定层数';
        /// 触发 Unactive, 而不触发Disable。
        /// </summary>
        public void RemoveBuff(int id, int reduceLayerNum);
        /// <summary> 
        /// 删去'整个Buff';
        /// 触发 Unactive, 而不触发Disable。
        /// </summary>
        public void RemoveBuff(BuffControllerItemBase item);
    }
}