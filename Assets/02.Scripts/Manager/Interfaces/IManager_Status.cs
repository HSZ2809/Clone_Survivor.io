using System;
using UnityEngine;

namespace ZUN
{
    public interface IManager_Status
    {
        Equipment[] Inventory { get; }

        public float FinalHp { get; }
        public float FinalAtk { get; }
    }
}