using UnityEngine;

namespace ZUN
{

    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Items Data")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] string id;
        public string Id => id;
        [SerializeField] string itemName;
        public string ItemName => itemName;
        [SerializeField] Sprite itemSprite;
        public Sprite ItemSprite => itemSprite;
        [TextArea(2, 3)]
        [SerializeField] string description;
        public string Description => description;
        [SerializeField] Sprite background;
        public Sprite Background => background;

        public Item Create(int amount)
        {
            return new Item(this, amount);
        }
    }
}