namespace PersonalFinanceTracker.Services;

public interface IUserService
{
    bool UpdateProfile(int userId, string username);
    bool ChangePassword(int userId, string newPassword);
    bool VerifyPassword(int userId, string password);
}

