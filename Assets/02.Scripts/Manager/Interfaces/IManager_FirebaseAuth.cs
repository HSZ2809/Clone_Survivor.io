using System.Threading.Tasks;
using Firebase.Auth;

namespace ZUN
{
    public interface IManager_FirebaseAuth
    {
        string UserId { get; }
        
        Task<bool> SignUpAsync(string email, string password);
        Task<bool> SignInAsync(string email, string password);
    }
}