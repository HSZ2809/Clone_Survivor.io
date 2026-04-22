using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZUN
{
    public interface IManager_FirebaseFirestore
    {
        FirebaseFirestore Firestore { get; }
        Task<bool> CreateUserDocumentAsync(string uid, string email);
        Task<UserData> GetUserDataAsync(string uid);
        Task<bool> UpdateFieldAsync<T>(string uid, string fieldName, T value);
        Task<bool> UpdateFieldsAsync(string uid, Dictionary<string, object> updates);
    }
}