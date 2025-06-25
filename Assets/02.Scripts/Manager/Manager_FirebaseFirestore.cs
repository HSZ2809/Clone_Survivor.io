using UnityEngine;
using Firebase.Firestore;

namespace ZUN
{
    public class Manager_FirebaseFirestore : MonoBehaviour
    {
        public static Manager_FirebaseFirestore Instance;

        public FirebaseFirestore Firestore { get; private set; }


    }
}