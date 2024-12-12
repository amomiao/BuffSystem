using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Momos.Core.RPG
{
    public class BuffConfigSetAsset : ScriptableObject
    {
        private static string DirectionPath = "Config/";
        private static string Name = "BuffConfigSet";
        public static string ResPath => Path.Combine(DirectionPath, Name);
        public static BuffConfigSetAsset Load => Resources.Load<BuffConfigSetAsset>(ResPath);

        public List<BuffConfigItem> items;
    }
}