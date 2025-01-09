using UnityEngine;

namespace ZUN
{
    public abstract class Item : MonoBehaviour
    {
        [Header("Item Info")]
        [SerializeField] protected string serialNumber;
        [SerializeField] protected Sprite sprite;
        [SerializeField] protected string[] upgradeInfos;

        public string SerialNumber { get { return serialNumber; } }
        public Sprite Sprite { get { return sprite; } }
        public string[] UpgradeInfos { get { return upgradeInfos; } }
    }
}