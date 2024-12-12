using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momos.Core.RPG.BuffDemo
{
    public interface IDemoBuffWork<R> : IBuffWork<DemoRole> where R : class, new()
    {
        public void Atk(int finalValue, BuffControllerItemBase<R> controllerItem, IBuffController<R> selfController);
    }
}