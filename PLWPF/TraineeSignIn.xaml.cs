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
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TraineeSignIn.xaml
    /// </summary>
    public partial class TraineeSignIn : UserControl
    {
        IBL bl;
        public TraineeSignIn()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Sign_in());
        }

        private void connect(object sender, RoutedEventArgs e)
        {
            bl = FactoryBL.GetBL();
            try
            {
                if (!IsDigitsOnly(id.Text))
                    throw new Exception("תעודת זהות חייבת להיות מורכבת רק ממספרים!");
                Trainee t = new Trainee();
                t = bl.FindTrainee(id.Text);
                if (t.password != password.Password)
                    throw new Exception("הסיסמא שגויה!");
                Switcher.Switch(new TraineeConnection(t));
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }


        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
        bool IslettersOnly(string str)
        {
            foreach (char c in str.ToUpper())
            {
                if (c == ' ')
                    continue;
                if (c < 'A' || c > 'Z')
                    return false;
            }
            return true;

        }
    }
}
