using System.Xml.Linq;

namespace MGK_Counter_4J;

public partial class CounterView : ContentView
{
    public event EventHandler? OnDeleteCounter;

    private int counterValue = 0;
    public String counterName {get;}
    public static string configPath = "counters.xml";
    public CounterView(string counterName,int count = 0, EventHandler? onDeleteCounter = null)
    {
        InitializeComponent();
        this.counterName = counterName;
        TitleLabel.Text = counterName;
        counterValue = count;
        updateCounter();
        OnDeleteCounter = onDeleteCounter;
    }
    private void updateCounter()
    {
        CounterLabel.Text = counterValue.ToString();
        if (File.Exists(configPath))
        {
            var xml = XDocument.Load(configPath);
            var counterXML = xml.Descendants("Counter").Where(e => (string)e.Attribute("name") == counterName).FirstOrDefault();
            if (counterXML == null) {
                var newCounter = new XElement("Counter",
                    new XAttribute("name", counterName), new XAttribute("value", counterValue));
                xml.Root?.Add(newCounter);
            }
            else {
                counterXML.SetAttributeValue("value", counterValue);
            }   
            xml.Save(configPath);
        }
    }
    private void onMinusButtonClicked(object sender, EventArgs e)
    {
        counterValue--;
        updateCounter();
    }

    private void onPlusButtonClicked(object sender, EventArgs e)
    {
        counterValue++;
        updateCounter();
    }

    private void onDeleteCounterClicked(object sender, EventArgs e)
    {
        OnDeleteCounter?.Invoke(this, EventArgs.Empty);
    }
}