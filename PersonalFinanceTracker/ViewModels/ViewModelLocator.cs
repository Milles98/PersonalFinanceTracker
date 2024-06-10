using PersonalFinanceTracker.Repository;
using PersonalFinanceTracker.Services;

namespace PersonalFinanceTracker.ViewModels;

public class ViewModelLocator
{
    private static readonly IUserRepository _userRepository = new UserRepository();
    private static readonly IUserService _userService = new UserService(_userRepository);
    private static readonly ITransactionRepository _transactionRepository = new TransactionRepository();
    private static readonly ITransactionService _transactionService = new TransactionService(_transactionRepository);

    public MainViewModel MainViewModel => new MainViewModel(_userService, _transactionService);
    public LoginViewModel LoginViewModel => new LoginViewModel(_userService, ShowWelcomeView);
    public RegisterViewModel RegisterViewModel => new RegisterViewModel(ShowLoginView);
    public TransactionEntryViewModel TransactionEntryViewModel => new TransactionEntryViewModel(_transactionService, GetCurrentUserId());

    // Implement methods for actions and retrieving current user ID.

    private int GetCurrentUserId()
    {
        // Implement logic to get the current user ID
        return 1; // Example
    }

    private void ShowWelcomeView(string username)
    {
        // Implement navigation to Welcome View
    }

    private void ShowLoginView()
    {
        // Implement navigation to Login View
    }
}

