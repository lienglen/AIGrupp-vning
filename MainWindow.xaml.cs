using AIGruppÖvning.ViewModels;
using System.Windows;

namespace AIGruppÖvning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MessagesViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MessagesViewModel();
            DataContext = viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadAsync(); 
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = InputText.Text.Trim();
            if (inputText.Length == 0)
                return;
            InputText.Text = string.Empty;
            viewModel.AddHumanMessage(inputText);
            viewModel.AddCpuReply();
        }
    }
}