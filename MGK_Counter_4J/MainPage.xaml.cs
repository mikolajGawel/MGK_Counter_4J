using System.Xml;
using System.Xml.Linq;

namespace MGK_Counter_4J
{
    public partial class MainPage : ContentPage
    {
        List<CounterView> counterViews = new List<CounterView>();
        static string filePath = "./counters.xml";
        public void LoadCountersFromFile() {
            if (!File.Exists(filePath)) { return;  }
            var doc = XDocument.Load(filePath);
            foreach (var counter in doc.Descendants("Counter"))
            {
                var name = counter.Attribute("name")?.Value;
                var value = counter.Attribute("value")?.Value;
                if(name == null || value == null) {
                    continue;
                }

                var counterView = new CounterView(name, int.Parse(value),deleteCounter);
                CounterList.Add(counterView);
                counterViews.Add(counterView);
            }
        }
        
        public MainPage()
        {
            InitializeComponent();
            LoadCountersFromFile();
        }

        private async void onAddCounterClicked(object sender, EventArgs e)
        {
            var result = await DisplayPromptAsync("Add Counter", "Counter Name:");

            if (string.IsNullOrWhiteSpace(result) || counterViews.Find(e => e.counterName == result) != null)
            {
                return;
            }

            var value = await DisplayPromptAsync("Add Counter", "Counter Value:",keyboard: Keyboard.Numeric);

            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }
            if(!int.TryParse(value, out _))
            {
                await DisplayAlert("Error", "Please enter a valid number for the counter value.", "OK");
                return;
            }
            var counter = new CounterView(result,count: int.Parse(value),deleteCounter);

            CounterList.Add(counter);
            counterViews.Add(counter);
        }

        private void deleteCounter(object sender, EventArgs e)
        {
            if(sender is CounterView counterView)
            {
                CounterList.Remove(counterView);
                counterViews.Remove(counterView);

                if(File.Exists(filePath))
                {
                    var doc = XDocument.Load(filePath);
                    var counterElement = doc.Descendants("Counter")
                        .FirstOrDefault(c => c.Attribute("name")?.Value == counterView.counterName);
                    if(counterElement != null)
                    {
                        counterElement.Remove();
                        doc.Save(filePath);
                    }
                }
            }
        }
    }
}
