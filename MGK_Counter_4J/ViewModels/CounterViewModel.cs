using MGK_Counter_4J.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MGK_Counter_4J.ViewModels
{
    public class CounterViewModel:INotifyPropertyChanged
    {
        
        public event EventHandler? OnDeleteRequested;
        public event PropertyChangedEventHandler? PropertyChanged;

        private CounterModel _counter;
        public int Value
        {
            get => _counter.Value;
            set
            {
                if (_counter.Value != value)
                {
                    _counter.Value = value;
                    OnPropertyChanged();
                    _counter.Save();
                }
            }
        }
        public string Name { get { return _counter.Name; } }
        public ICommand IncreaseValue { get; }
        public ICommand DecreaseValue { get; }
        public ICommand DeleteCounter { get; }

        public CounterViewModel(int value,string name,EventHandler onDeleteRequested)
        {
            _counter = new CounterModel(name, value);
            OnDeleteRequested = onDeleteRequested;
            IncreaseValue = new Command(() => Value++);
            DecreaseValue = new Command(() => Value++);
            DeleteCounter = new Command(() => { 
                _counter.Delete();
                onDeleteRequested.Invoke(this, EventArgs.Empty); 
            });
            _counter.Save();
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
