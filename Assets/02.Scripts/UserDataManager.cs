using Firebase.Auth;
using Firebase;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace ZUN
{
    public class UserDataManager : MonoBehaviour
    {
        public static UserDataManager instance;

        [SerializeField] Manager_FirebaseAuth auth;
        [SerializeField] Manager_FirebaseFirestore firestore;

        public UserData Cache { get; private set; }
        public event Action<UserData> OnChanged;

        void Awake()
        {
            if (instance != null)
                Destroy(gameObject);
            else
                instance = this;
        }

        public async Task<bool> LoadAsync(string uid)
        {
            Cache = await firestore.GetUserDataAsync(uid);
            // OnChanged?.Invoke(Cache);
            return Cache != null;
        }

        // 시험 코드
        //public async Task<bool> UpdateLevelAsync(string uid, int lvl)
        //{
        //    int oldLevel = Cache.Level;
        //    Cache.Level = lvl;
        //    bool ok = await firestore.UpdateFieldAsync<int>(uid, "Level", lvl);
        //    if (!ok)
        //    {
        //        Cache.Level = oldLevel;
        //        OnChanged?.Invoke(Cache);
        //        Debug.LogWarning("레벨 변경 실패");
        //    }
        //    return ok;
        //}

        // …다른 필드 업데이트 메서드 필요…
    }
}