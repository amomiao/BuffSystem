using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momos.Core.RPG.BuffDemo
{
    public class DemoBuffProcessUnit : BuffProcessUnitBase<DemoRole>
    {
        private static DemoBuffProcessUnit _instance;
        public static DemoBuffProcessUnit Instance
        {
            get
            { 
                _instance ??= new DemoBuffProcessUnit();   
                return _instance;
            }
        }

        private DemoBuffWork[] works = new DemoBuffWork[]
        {
            new DemoBuffWork(new DemoBuffWorkItem[]{ new WI_AddAttribute() }),
            new DemoBuffWork(new DemoBuffWorkItem[]{ new WI_AddAttribute() }),
            new DemoBuffWork(new DemoBuffWorkItem[]{ new WI_AddAttribute() })
        };

        private DemoBuffWork GetWork(BuffControllerItemBase<DemoRole> controllerItem)
        {
            switch (controllerItem.ID)
            {
                case 0:
                    return works[0];
                case 1:
                    return works[1];
                case 2:
                    return works[2];
            }
            return null;
        }

        public int AtkInvoke(int finalValue, BuffControllerItemBase<DemoRole> controllerItem, IBuffController<DemoRole> selfController)
        {
            GetWork(controllerItem)?.Atk(finalValue, controllerItem, selfController);
            //Debug.Log($"AtktInvoke: finalValue:{finalValue} id:{controllerItem.ID} giver:{controllerItem.Giver.name} self:{selfController.Self.name}");
            //finalValue = (int)(finalValue * (1 + 0.2f * controllerItem.CountLayer));
            //selfController.ReduceBuff(controllerItem);
            return finalValue;
        }

        public override void AddInvoke(int layerIncre, BuffControllerItemBase<DemoRole> controllerItem, IBuffController<DemoRole> selfController)
        {
            GetWork(controllerItem)?.Awake(layerIncre,controllerItem,selfController);
            GetWork(controllerItem)?.Enable(layerIncre,controllerItem,selfController);
        }

        public override void DotInvoke(BuffControllerItemBase<DemoRole> controllerItem, IBuffController<DemoRole> selfController)
        {
            GetWork(controllerItem)?.Dot(controllerItem, selfController);
        }

        public override void SecondInvoke(BuffControllerItemBase<DemoRole> controllerItem, IBuffController<DemoRole> selfController)
        {
            GetWork(controllerItem)?.Second(controllerItem, selfController);
        }

        public override void ReduceInvoke(int layerReduceIncre, BuffControllerItemBase<DemoRole> controllerItem, IBuffController<DemoRole> selfController)
        {
            GetWork(controllerItem)?.Disable(layerReduceIncre, controllerItem, selfController);
            GetWork(controllerItem)?.Unactive(layerReduceIncre, controllerItem, selfController);
        }

        public override void RemoveInvoke(int layerReduceIncre, BuffControllerItemBase<DemoRole> controllerItem, IBuffController<DemoRole> selfController)
        {
            GetWork(controllerItem)?.Unactive(layerReduceIncre, controllerItem, selfController);
        }
    }
}