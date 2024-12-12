using System;
using UnityEngine;

namespace Momos.Core.RPG
{
    [Serializable]
    public class BuffConfigItem
    {
        /// <summary> buff名称 </summary>
        public string name;
        /// <summary> 图标 </summary>
        public Sprite icon;
        /// <summary> 唯一id </summary>
        public int id;
        /// <summary> 不唯一id </summary>
        public int secID;
        /// <summary> 层数限制 </summary>
        public int limited = 1;
        /// <summary> 持续时长:-1为永久 </summary>
        public int duration = -1;
        /// <summary> 是仅展示的被动加成 </summary>
        public bool isEternal = false;
        /// <summary> 描述 </summary>
        public string description;
    }
}