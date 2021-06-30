using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Sign_in.xaml
    /// </summary>
    public partial class Sign_in : UserControl
    {
        public Sign_in()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new options());
        }

        private void tester(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new TesterSignIn());
        }

        private void trainee(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new TraineeSignIn());
        }

        private void admin(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new adminConnection());
        }
    }
}
