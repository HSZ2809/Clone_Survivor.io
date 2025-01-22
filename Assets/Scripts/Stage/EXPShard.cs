using UnityEngine;

namespace ZUN
{
    public class EXPShard : MonoBehaviour
    {
        public enum Type
        {
            SMALL = 0,
            GREEN = 1,
            BLUE = 2,
            YELLOW = 3
        }

        [SerializeField] private Type shardType;
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private int exp;

        [SerializeField] private Sprite[] sprites;
        [SerializeField] private int[] amount;

        public Type ShardType { get { return shardType; } }

        public int Exp
        {
            get { return exp; }
        }

        public void SetType(Type type)
        {
            shardType = type;
            sr.sprite = sprites[(int)type];
            exp = amount[(int)type];
        }
    }
}