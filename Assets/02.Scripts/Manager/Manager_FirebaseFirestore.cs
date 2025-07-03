using Firebase;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ZUN
{
    public class Manager_FirebaseFirestore : MonoBehaviour
    {
        public static Manager_FirebaseFirestore instance;

        public FirebaseFirestore Firestore { get; private set; }

        async void Awake()
        {
            if (instance != null)
                Destroy(gameObject);
            else
                instance = this;

            var depStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

            if (depStatus == DependencyStatus.Available)
                Firestore = FirebaseFirestore.DefaultInstance;
            else
            {
                Application.Quit();
#if UNITY_EDITOR
                Debug.LogError($"Firebase dependency error: {depStatus}");
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }

        /// <summary>
        /// uid에 해당하는 users/{uid} 문서를 생성 후 초기 UserData값 입력.
        /// 생성 실패 시 false 반환.
        /// </summary>
        public async Task<bool> CreateUserDocumentAsync(string uid, string email)
        {
            try
            {
                var docRef = Firestore.Collection("users").Document(uid);

                UserData userData = new (email, "Player000001", 1);

                await docRef.SetAsync(userData);

                Debug.Log("유저 문서 생성 완료");
                return true;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"유저 문서 생성 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// uid에 해당하는 users/{uid} 문서를 읽어서 UserData로 반환.
        /// 문서가 없거나 에러면 null 반환.
        /// </summary>
        public async Task<UserData> GetUserDataAsync(string uid)
        {
            try
            {
                var docRef = Firestore.Collection("users").Document(uid);
                var snap = await docRef.GetSnapshotAsync();
                if (!snap.Exists)
                {
                    Debug.LogWarning($"유저 문서가 없습니다: {uid}");
                    return null;
                }
                var userData = snap.ConvertTo<UserData>();
                Debug.Log($"유저 데이터 읽기 성공: {userData.Name}");
                return userData;
            }
            catch (Exception ex)
            {
                Debug.LogError($"유저 데이터 읽기 실패: {ex.Message}");
                return null;
            }
        }

        // 1) 내부 제네릭 메서드: 어떤 타입 T든 UpdateAsync 호출
        public async Task<bool> UpdateFieldAsync<T>(string uid, string fieldName, T value)
        {
            try
            {
                await Firestore.Collection("users")
                        .Document(uid)
                        .UpdateAsync(fieldName, value);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[{fieldName}] 업데이트 실패: {e.Message}");
                return false;
            }
        }

        // 2) 여러 필드를 한꺼번에 업데이트할 땐 Dictionary 활용
        public async Task<bool> UpdateFieldsAsync(string uid, Dictionary<string, object> updates)
        {
            try
            {
                await Firestore.Collection("users")
                        .Document(uid)
                        .UpdateAsync(updates);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"다중 필드 업데이트 실패: {e.Message}");
                return false;
            }
        }

    }
}