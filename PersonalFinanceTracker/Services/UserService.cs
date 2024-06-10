using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Services;

public class UserService : IUserService
{
    public bool UpdateProfile(int userId, string username)
    {
        using (var context = new FinanceContext())
        {
            var user = context.FinanceUsers.Find(userId);
            if (user == null) return false;

            user.Username = username;
            context.SaveChanges();
            return true;
        }
    }

    public bool ChangePassword(int userId, string newPassword)
    {
        using (var context = new FinanceContext())
        {
            var user = context.FinanceUsers.Find(userId);
            if (user == null) return false;

            user.Password = newPassword;
            context.SaveChanges();
            return true;
        }
    }

    public bool VerifyPassword(int userId, string password)
    {
        using (var context = new FinanceContext())
        {
            var user = context.FinanceUsers.Find(userId);
            return user != null && user.Password == password;
        }
    }
}