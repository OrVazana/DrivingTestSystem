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
    /// Interaction logic for options.xaml
    /// </summary>
    public partial class options : UserControl
    {
        public options()
        {
            InitializeComponent();
        }
        private void SignIN(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Sign_in());
        }
        private void SignUP(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Sign_up());
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new open());
        }
    }
}
