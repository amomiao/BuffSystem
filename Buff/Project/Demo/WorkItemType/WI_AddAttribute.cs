using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momos.Core.RPG.BuffDemo
{
    public class WI_AddAttribute : DemoBuffWorkItem
    {
        public override void Awake(int layerIncre, BuffControllerItemBase<DemoRole> controllerItem, IBuffController<DemoRole> selfController)
        {
            selfController.Self.atk += 10 * layerIncre;
            Debug.Log($"{typeof(WI_AddAttribute)}为{selfController.Self.name}增加了{10 * layerIncre}点攻击力,当前值为{selfController.Self.atk}。");
        }

        public override void Unactive(int layerReduceIncre, BuffControllerItemBase<DemoRole> controllerItem, IBuffController<DemoRole> selfController)
        {
            selfController.Self.atk -= 10 * layerReduceIncre;
            Debug.Log($"{typeof(WI_AddAttribute)}为{selfController.Self.name}增加了{10 * layerReduceIncre}点攻击力的效果结束,当前值为{selfController.Self.atk}。");
        }
    }
}