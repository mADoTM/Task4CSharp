using System.Windows;
using Domain;
using Image = System.Drawing.Image;
using Point = System.Drawing.Point;

namespace WpfApp4;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<string> _notifications;
    private ModelPresenter _modelPainter;

    private static readonly string[] DriverNames = { "John", "Michael", "Maxim" };

    private const int ModelGenerationStep = 250;
    private const int MaxModelCount = 3;
    private int _currentModelCount = 0;

    public MainWindow()
    {
        InitializeComponent();

        _notifications = new List<string>();
        var visualElements = new List<VisualElement>();
        _modelPainter = new ModelPresenter(pictureBox, visualElements);
        _currentModelCount = 0;
        // pictureBox.TabIndex = 0;
        // pictureBox.TabStop = false;
        
        _modelPainter.Start();
    }

    void Notification(string message)
    {
        Dispatcher.Invoke((MethodInvoker)delegate
        {
            _notifications.Add(message);
            if (_notifications.Count >= 15)
            {
                _notifications = _notifications.GetRange(_notifications.Count - 5, 5);
                textBoxNotifications.Text = "";
                foreach (var item in _notifications)
                {
                    textBoxNotifications.Text += item + Environment.NewLine + Environment.NewLine;
                }
            }

            textBoxNotifications.Text += message + Environment.NewLine + Environment.NewLine;
        });
        // textBoxNotifications.Invoke((MethodInvoker)delegate
        // {
        //     _notifications.Add(message);
        //     if (_notifications.Count >= 15)
        //     {
        //         _notifications = _notifications.GetRange(_notifications.Count - 5, 5);
        //         textBoxNotifications.Text = "";
        //         foreach (var item in _notifications)
        //         {
        //             textBoxNotifications.Text += item + Environment.NewLine + Environment.NewLine;
        //         }
        //     }
        //
        //     textBoxNotifications.Text += message + Environment.NewLine + Environment.NewLine;
        // });
    }

    private void toolStripAddTrolley_Click(object sender, EventArgs e)
    {
        var trolleys = new List<Trolleybus>();

        var yCoord = 100 + ModelGenerationStep * _currentModelCount;

        var trolleybusStartPoint = new Point(200, yCoord);

        var trolleybus =
            new Trolleybus(_currentModelCount + 1, Notification, trolleybusStartPoint,
                new TrolleyTrace(trolleybusStartPoint, new Point(pictureBox.Width - 200, yCoord)));

        var driver = new Driver(DriverNames[_currentModelCount], trolleybus, Notification);

        trolleys.Add(trolleybus);

        var emergencyService = new EmergencyService(Notification, trolleys,
            new Point(100, yCoord));

        Task.Run(trolleybus.Start);
        Task.Run(driver.Start);
        Task.Run(emergencyService.Start);

        
        _modelPainter.AddVisualElem(new VisualElement(trolleybus, Image.FromFile("C:\\Users\\Maxim Dolzhenko\\RiderProjects\\WpfApp4\\WpfApp4\\images\\trolley.png")));
        _modelPainter.AddVisualElem(new VisualElement(driver, Image.FromFile("C:\\Users\\Maxim Dolzhenko\\RiderProjects\\WpfApp4\\WpfApp4\\images\\driver.png")));
        _modelPainter.AddVisualElem(new VisualElement(emergencyService, Image.FromFile("C:\\Users\\Maxim Dolzhenko\\RiderProjects\\WpfApp4\\WpfApp4\\images\\service.png")));

        _currentModelCount++;

        if (_currentModelCount >= MaxModelCount)
            toolStripAddTrolley.IsEnabled = false;
    }
}