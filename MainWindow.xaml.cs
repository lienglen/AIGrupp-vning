using AIGruppÖvning.Models;
using AIGruppÖvning.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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