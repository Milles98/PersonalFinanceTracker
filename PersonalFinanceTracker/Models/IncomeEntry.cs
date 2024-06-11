namespace PersonalFinanceTracker.Models
{
    public class IncomeEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
