namespace PersonalFinanceTracker.Models;

public class IncomeExpenseEntry
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string Type { get; set; }  // "Income" or "Expense"
}