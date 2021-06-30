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
    /// Interaction logic for Sign_up.xaml
    /// </summary>
    public partial class Sign_up : UserControl
    {
        public Sign_up()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new options());
        }

        private void Tester(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new TesterSignUp());
        }
        private void Trainee(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new TraineeSignUp());
        }
    }
}
