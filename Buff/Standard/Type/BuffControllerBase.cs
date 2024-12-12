using System.Collections;
using System.Collections.Generic;

namespace Momos.Core.RPG
{
    /// <summary> 
    /// 挂载在角色身上的Buff控制器: 
    ///     仅逻辑, 应用'里氏转换'时,使用接口而非本类。
    /// </summary>
    /// <typeparam name="R"> 挂载到的'角色的类' </typeparam>
    public abstract class BuffControllerBase<R> : BuffObject, IBuffController<R> where R : class
    {
        protected R self;

        public R Self => self;

        protected BuffControllerBase(R self)
        {
            this.self = self;
        }

        // 触发 Awake和Enable
        public abstract void AddBuff(int id, int layerIncre, R giver);
        // 触发 Dot和Second
        public abstract void UpdateBuff(int increMs);
        // 触发 Disable和Unactive
        public abstract void ReduceBuff(int id, int reduceLayerNum);
        public void ReduceBuff(BuffControllerItemBase item) => ReduceBuff(item.ID, item.CountLayer);
        // 触发 Unactive, 而不触发Disable
        public abstract void RemoveBuff(int id, int reduceLayerNum);
        public void RemoveBuff(BuffControllerItemBase item) => RemoveBuff(item.ID, item.CountLayer);
    }

    /// <summary> 
    /// 挂载在角色身上的Buff控制器: 
    ///     逻辑实现, 做具体项目Buff控制器的父类。
    /// </summary>
    /// <typeparam name="CI"> Buff运行项,继承自<see cref="BuffControllerItemBase{R, P}"/> </typeparam>
    /// <typeparam name="R"> 挂载到的'角色的类' </typeparam>
    /// <typeparam name="P"> Buff处理器,继承自<see cref="BuffProcessUnitBase{R}"/> </typeparam>
    public abstract class BuffControllerBase<CI,R,P> : BuffControllerBase<R>, IEnumerable<CI>
        where CI :BuffControllerItemBase<R,P>, new() 
        where R : class
        where P : BuffProcessUnitBase<R>
    {
        protected List<CI> buffList = new List<CI>();

        protected BuffControllerBase(R self) : base(self) { }

        // 触发 Awake和Enable
        public override void AddBuff(int id,int layerIncre,R giver)
        {
            CI b = null;
            foreach (var item in this)
            {
                if (item.ID == id)
                {
                    b = item; 
                    break;
                }
            }
            if (b == null)
            {
                b = new CI();
                b.Init(id);
                buffList.Add(b);
            }
            b.AddEvt(b.AddBuff(layerIncre, giver), this);
        }

        // 触发 Dot和Second
        public override void UpdateBuff(int increMs)
        {
            foreach (var item in this)
            {
                if (item.UpdateEvt(increMs, this))
                {
                    ReduceBuff(item);
                }
            }
        }

        // 触发 Disable和Unactive
        public override void ReduceBuff(int id, int reduceLayerNum)
        { 
            int i = ID2Index(id);
            if (i == -1)
                return;
            buffList[i].ReduceEvt(buffList[i].ReduceBuff(reduceLayerNum), this);
            if (buffList[i].CountLayer == 0)
                buffList.RemoveAt(i);
        }

        // 触发 Unactive, 而不触发Disable
        public override void RemoveBuff(int id, int reduceLayerNum)
        {
            int i = ID2Index(id);
            if (i == -1)
                return;
            buffList[i].RemoveEvt(buffList[i].ReduceBuff(reduceLayerNum), this);
            if (buffList[i].CountLayer == 0)
                buffList.RemoveAt(i);
        }

        // Buff ID 转 索引
        protected int ID2Index(int id)
        {
            for (int i = 0; i < buffList.Count; i++)
            {
                if (buffList[i].ID == id)
                    return i;
            }
            return -1;
        }

        // IEnumerable
        public IEnumerator<CI> GetEnumerator()
        {
            for (int i = buffList.Count - 1; i >= 0; i--)
            {
                yield return buffList[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}