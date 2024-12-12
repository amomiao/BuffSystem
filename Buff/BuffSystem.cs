using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momos.Core.RPG
{
    public class BuffSystem
    {
        private static BuffSystem _instance;
        public static BuffSystem GetInstance()
        {
            return _instance ??= new BuffSystem();
        }

        public BuffConfigItem GetBuffConfig(int id)
        {
            BuffConfigItem item = new BuffConfigItem() { id = id, limited = 1, duration = -1 };
            switch (id)
            {
                case 0:
                    item.limited = 1;
                    break;
                case 1:
                    item.limited = 5;
                    break;
                case 2:
                    item.limited = 3;
                    break;
            }
            return item;
        }
    }

    //public class BuffSystem : BaseManager<BuffSystem>
    //{
    //    public BuffConfigItem GetBuffConfig(int id)
    //    {
    //        BuffConfigItem item = new BuffConfigItem() { id = id, limited = 1,duration = -1 };
    //        switch (id)
    //        {
    //            case 0:
    //                item.limited = 1;
    //                break;
    //            case 1:
    //                item.limited = 5;
    //                break;
    //            case 2:
    //                item.limited = 3;
    //                break;
    //        }
    //        return item;
    //    }
    //}
}