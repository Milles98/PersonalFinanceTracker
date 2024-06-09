namespace PersonalFinanceTracker.Models;

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; }
}