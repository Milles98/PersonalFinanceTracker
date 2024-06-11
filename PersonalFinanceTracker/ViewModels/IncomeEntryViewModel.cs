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
    public class IncomeEntryViewModel : INotifyPropertyChanged
    {
        private IncomeEntry _newEntry;
        private string _successMessage;
        private readonly DispatcherTimer _successMessageTimer;
        private readonly int _currentUserId;

        public IncomeEntry NewEntry
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
        public ICommand AddEntryCommand { get; }

        public IncomeEntryViewModel(int currentUserId)
        {
            _currentUserId = currentUserId;
            NewEntry = new IncomeEntry { Date = DateTime.Now };
            AddEntryCommand = new RelayCommand(AddEntry);
            _successMessageTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            _successMessageTimer.Tick += (sender, args) =>
            {
                SuccessMessage = string.Empty;
                _successMessageTimer.Stop();
            };
        }

        private void AddEntry(object obj)
        {
            using (var context = new FinanceContext())
            {
                NewEntry.UserId = _currentUserId;
                context.IncomeEntries.Add(NewEntry);
                context.SaveChanges();
                NewEntry = new IncomeEntry { Date = DateTime.Now };
                SuccessMessage = "Income entry added successfully!";
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
