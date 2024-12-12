using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momos.Core.RPG.BuffDemo
{
    public class DemoRole : MonoBehaviour
    {
        public int atk = 100;
        public DemoBuffController buffController;

        private void Awake()
        {
            buffController = new DemoBuffController(this);
        }

        private void Start()
        {
            //buffController.AddBuff(1, 2, this);
            //buffController.AddBuff(1, 2, this);
            //buffController.AddBuff(1, 2, this);
            //buffController.ReduceBuff(1, 5);

            //buffController.AddBuff(1, 2, this);
            //buffController.RemoveBuff(1, 2);

            //buffController.AddBuff(1, 2, this);

            buffController.AddBuff(1, 2, this);
            buffController.AddBuff(2, 2, this);
            Debug.Log($"Buff加成后的攻击值:" + atk);
            buffController.ReduceBuff(1, 2);
            buffController.ReduceBuff(2, 2);
            Debug.Log($"攻击Buff移除后的攻击值:" + atk);
        }

        private void Update()
        {
            buffController.UpdateBuff((int)(Time.deltaTime * 1000));
        }
    }
}