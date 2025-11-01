using MGK_Counter_4J.ViewModels;
using System.Xml.Linq;

namespace MGK_Counter_4J;

public partial class CounterView : ContentView
{
    public CounterViewModel CounterViewModel;
    public CounterView(string counterName,int count = 0, EventHandler? onDeleteCounter = null)
    {
        InitializeComponent();
        CounterViewModel = new CounterViewModel(count, counterName, onDeleteCounter);
        BindingContext = CounterViewModel;
    }
    private void onMinusButtonClicked(object sender, EventArgs e)
    {
    }

    private void onPlusButtonClicked(object sender, EventArgs e)
    {

    }

    private void onDeleteCounterClicked(object sender, EventArgs e)
    {
    }
}