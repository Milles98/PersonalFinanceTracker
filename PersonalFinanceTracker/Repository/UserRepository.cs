using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Repository;

public class UserRepository : IUserRepository
{
    public FinanceUser GetUserById(int userId)
    {
        using (var context = new FinanceContext())
        {
            return context.FinanceUsers.Find(userId);
        }
    }

    public FinanceUser GetUserByUsernameAndPassword(string username, string password)
    {
        using (var context = new FinanceContext())
        {
            return context.FinanceUsers.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
    
    public FinanceUser GetUserByUsername(string username)  
    {
        using (var context = new FinanceContext())
        {
            return context.FinanceUsers.FirstOrDefault(u => u.Username == username);
        }
    }

    public void UpdateUser(FinanceUser user)
    {
        using (var context = new FinanceContext())
        {
            context.FinanceUsers.Update(user);
            context.SaveChanges();
        }
    }

    public void AddUser(FinanceUser user)
    {
        using (var context = new FinanceContext())
        {
            context.FinanceUsers.Add(user);
            context.SaveChanges();
        }
    }
}