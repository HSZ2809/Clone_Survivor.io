using UnityEngine;

namespace ZUN
{
    public class DropedEXP : MonoBehaviour
    {
        [SerializeField] private int exp = 0;

        public int Exp
        {
            get { return exp; }
        }
    }
}