using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momos.Core.RPG.BuffDemo
{
    public class DemoBuffController : BuffControllerBase<DemoBuffControllerItem, DemoRole, DemoBuffProcessUnit>
    {
        public DemoBuffController(DemoRole self) : base(self) { }
        public int AtkBuff(int finalValue)
        {
            int account = finalValue;
            // 累计所有的增量
            foreach (DemoBuffControllerItem item in this)
                account += (item.AtkEvt(finalValue, item, this) - finalValue);
            return account;
        }
    }
}