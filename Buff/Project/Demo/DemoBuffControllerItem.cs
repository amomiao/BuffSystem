using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momos.Core.RPG.BuffDemo
{
    public sealed class DemoBuffControllerItem : BuffControllerItemBase<DemoRole, DemoBuffProcessUnit>
    {
        protected override int DotIntervalMs => 250;
        public override DemoBuffProcessUnit BPU => DemoBuffProcessUnit.Instance;

        public int AtkEvt(int finalValue, BuffControllerItemBase<DemoRole> controllerItem, IBuffController<DemoRole> selfController) => BPU.AtkInvoke(finalValue, this, selfController);
    }
}