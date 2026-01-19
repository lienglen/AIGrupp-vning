using System.Configuration;
using System.Data;
using System.Windows;

namespace AIGruppÖvning
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration Config { get; private set; }
    }

}
