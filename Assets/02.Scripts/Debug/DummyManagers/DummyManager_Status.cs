using System;

namespace ZUN
{
    public class DummyManager_Status : IManager_Status
    {
        readonly Equipment[] _inventory;
        readonly float _finalHp;
        readonly float _finalAtk;

        public Equipment[] Inventory => _inventory;
        public float FinalHp => _finalHp;
        public float FinalAtk => _finalAtk;

        public DummyManager_Status(float finalHp, float finalAtk)
        {
            _finalHp = finalHp;
            _finalAtk = finalAtk;
            _inventory = new Equipment[Enum.GetValues(typeof(EquipmentType)).Length];
        }
    }
}
