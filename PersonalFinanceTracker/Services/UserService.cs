using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Repository;
using PersonalFinanceTracker.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool UpdateProfile(int userId, string username)
    {
        var user = _userRepository.GetUserById(userId);
        if (user == null) return false;

        user.Username = username;
        _userRepository.UpdateUser(user);
        return true;
    }

    public bool ChangePassword(int userId, string newPassword)
    {
        var user = _userRepository.GetUserById(userId);
        if (user == null) return false;

        user.Password = newPassword;
        _userRepository.UpdateUser(user);
        return true;
    }

    public bool VerifyPassword(int userId, string password)
    {
        var user = _userRepository.GetUserById(userId);
        return user != null && user.Password == password;
    }
    
    public FinanceUser GetUserByUsername(string username) 
    {
        return _userRepository.GetUserByUsername(username);
    }

    public FinanceUser GetUserByUsernameAndPassword(string username, string password)  
    {
        return _userRepository.GetUserByUsernameAndPassword(username, password);
    }
}
