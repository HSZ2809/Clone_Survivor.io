using UnityEngine;

namespace ZUN
{
    public abstract class Item : MonoBehaviour
    {
        [Header("Item Info")]
        [SerializeField] protected string serialNumber;
        [SerializeField] protected Sprite sprite;
        [SerializeField] protected string[] upgradeInfos;
        [SerializeField] protected int level = 1;

        public string SerialNumber { get { return serialNumber; } }
        public Sprite Sprite { get { return sprite; } }
        public string[] UpgradeInfos { get { return upgradeInfos; } }

        public virtual void Upgrade() 
        {
            Debug.LogWarning("Item : Upgrade Mathod Not Set");
            return; 
        }
    }
}