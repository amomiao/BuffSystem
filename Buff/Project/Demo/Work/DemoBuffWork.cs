using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momos.Core.RPG.BuffDemo
{
    public class DemoBuffWork : BuffWorkBase<DemoRole, DemoBuffWorkItem> ,IDemoBuffWork<DemoRole>
    {
        public DemoBuffWork(DemoBuffWorkItem[] workItems) : base(workItems) { }

        public void Atk(int finalValue, BuffControllerItemBase<DemoRole> controllerItem, IBuffController<DemoRole> selfController)
        {
            foreach (var item in this)
                item.Atk(finalValue, controllerItem, selfController);
        }
    }
}