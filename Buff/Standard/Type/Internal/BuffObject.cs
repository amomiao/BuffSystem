using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momos.Core.RPG
{
    public class BuffObject
    {
        protected BuffSystem System => BuffSystem.GetInstance();
        protected BuffConfigItem GetConfig(int id) => System.GetBuffConfig(id);
    }
}