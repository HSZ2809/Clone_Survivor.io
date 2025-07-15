using UnityEngine;

namespace ZUN
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] Equipment[] initialEquip;

        private void Start()
        {
            if (UserDataManager.instance != null)
            {
                if (!UserDataManager.instance.Cache.IsTutorialCompleted)
                {
                    // 아이템 제공

                    // Cache.IsTutorialCompleted 를 true로 변경
                }
            }
        }
    }
}