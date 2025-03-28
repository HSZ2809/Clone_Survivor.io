using UnityEngine;

namespace ZUN
{    
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] int    id;
        [SerializeField] string itemName;
        [SerializeField] string tooltip;
        [SerializeField] Sprite iconSprite;

        public int    Id         { get { return id; } }
        public string ItemName   { get { return itemName; } }
        public string Tooltip    { get { return tooltip; } }
        public Sprite IconSprite { get { return iconSprite; } }
    }
}
