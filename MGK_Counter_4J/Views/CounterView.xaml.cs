using MGK_Counter_4J.ViewModels;
using System.Xml.Linq;

namespace MGK_Counter_4J;

public partial class CounterView : ContentView
{
    public CounterViewModel CounterViewModel;
    public event EventHandler? onDeleteRequested;
    public CounterView(string counterName,int count = 0, EventHandler? onDeleteCounter = null)
    {
        InitializeComponent();
        CounterViewModel = new CounterViewModel(count, counterName, (s,e) => {
            onDeleteRequested?.Invoke(this, EventArgs.Empty);
        });
        BindingContext = CounterViewModel;

        if(onDeleteCounter != null)
            onDeleteRequested += onDeleteCounter;
    }
}