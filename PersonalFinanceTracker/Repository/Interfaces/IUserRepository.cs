using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Repository;

public interface IUserRepository
{
    FinanceUser GetUserById(int userId);
    FinanceUser GetUserByUsernameAndPassword(string username, string password);
    FinanceUser GetUserByUsername(string username);
    void UpdateUser(FinanceUser user);
    void AddUser(FinanceUser user);
}