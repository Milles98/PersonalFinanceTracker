using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.ViewModels
{
    public class IncomeExpenseEntryViewModel : INotifyPropertyChanged
    {
        private IncomeExpenseEntry _newEntry;
        private string _successMessage;
        private readonly DispatcherTimer _successMessageTimer;
        private readonly int _currentUserId;

        public IncomeExpenseEntry NewEntry
        {
            get => _newEntry;
            set
            {
                _newEntry = value;
                OnPropertyChanged(nameof(NewEntry));
            }
        }

        public string SuccessMessage
        {
            get => _successMessage;
            set
            {
                _successMessage = value;
                OnPropertyChanged(nameof(SuccessMessage));
                OnPropertyChanged(nameof(IsSuccessMessageVisible));
                if (!string.IsNullOrEmpty(_successMessage))
                {
                    _successMessageTimer.Start();
                }
            }
        }

        public bool IsSuccessMessageVisible => !string.IsNullOrEmpty(SuccessMessage);
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<string> EntryTypes { get; set; }
        public ICommand AddEntryCommand { get; }

        public IncomeExpenseEntryViewModel(int currentUserId)
        {
            _currentUserId = currentUserId;
            NewEntry = new IncomeExpenseEntry { Date = DateTime.Now };
            Categories = new ObservableCollection<Category>();
            EntryTypes = new ObservableCollection<string> { "Income", "Expense" };
            AddEntryCommand = new RelayCommand(AddEntry);
            LoadCategories();
            _successMessageTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            _successMessageTimer.Tick += (sender, args) =>
            {
                SuccessMessage = string.Empty;
                _successMessageTimer.Stop();
            };
        }

        private void LoadCategories()
        {
            using (var context = new FinanceContext())
            {
                var categories = context.Categories.ToList();
                Categories.Clear();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
        }

        private void AddEntry(object obj)
        {
            using (var context = new FinanceContext())
            {
                NewEntry.UserId = _currentUserId;
                context.IncomeExpenseEntries.Add(NewEntry);
                context.SaveChanges();
                NewEntry = new IncomeExpenseEntry { Date = DateTime.Now };
                SuccessMessage = "Entry added successfully!";
                OnPropertyChanged(nameof(NewEntry));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
