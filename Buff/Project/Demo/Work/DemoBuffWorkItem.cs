using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Momos.Core.RPG.BuffDemo
{
    public class DemoBuffWorkItem : BuffWorkItemBase<DemoRole>, IDemoBuffWork<DemoRole>
    {
        public virtual void Atk(int finalValue, BuffControllerItemBase<DemoRole> controllerItem, IBuffController<DemoRole> selfController) { }
    }
}