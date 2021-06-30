using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
    /// Interaction logic for adminConnection.xaml
    /// </summary>
    public partial class adminConnection : UserControl
    {
        IBL bl = FactoryBL.GetBL();
        public adminConnection()
        {
            InitializeComponent();
            List<Trainee> Trainees = bl.GetAllTrainees();
            List<Tester> Testers = bl.GetAllTesters();
            List<Test> Tests = bl.GetAllTests();

            trainees.ItemsSource = Trainees;
            testers.ItemsSource = Testers;

            testPast.DataContext = Tests.FindAll(item => (DateTime.Now.Date > item.PreferedDate.Date)==true);
            testFuture.DataContext = Tests.FindAll(item => (DateTime.Now.Date < item.PreferedDate.Date) == true);
            testToday.DataContext = Tests.FindAll(item =>DateTime.Now.Date == item.PreferedDate.Date);
        }

        private void logOut(object sender, RoutedEventArgs e)
        {
            //MessageBoxResult result = MessageBox.Show("האם אתה בטוח שברצונך להתנתק?", "התנתקות", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            //switch (result)
            //{
            //    case MessageBoxResult.Yes:
            //        {
                        Switcher.Switch(new open());
            //            break;
            //        }
            //    case MessageBoxResult.No:
            //        break;

            //    default:
            //        break;
            //}
        }
        private void showButton (object sender,RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel s = (StackPanel)button.Content;
            Popup p = (Popup)s.Children[1];
            if (p.IsOpen == false)
                p.IsOpen = true;
            else
                p.IsOpen = false;
        }
        string whatChoosen = "";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Tester> Testers = bl.GetAllTesters();
            testers.ItemsSource = Testers;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            whatChoosen = "1";
            List<string> add = bl.AddressGrouping(true).Select(t=>t.Key).ToList();
            choose.ItemsSource = add;
            testers.ItemsSource =null;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            whatChoosen = "2";
            List<Vehicle> v = bl.VehicleGrouping(true).Select(t => t.Key).ToList();
            choose.ItemsSource = v;
            testers.ItemsSource = null;
        }

        private void choose_Selected(object sender, RoutedEventArgs e)
        {
            if (choose.SelectedIndex == -1)
                return;
            object selected = choose.Items.GetItemAt(choose.SelectedIndex);
            if (whatChoosen=="1")
            {
                List<Tester> list = new List<Tester>();
                IEnumerable<IGrouping<string, Tester>> l = bl.AddressGrouping(true);
                foreach (var group in l)
                {
                    if (group.Key == selected.ToString())
                    {
                        foreach (var item in group)
                        {
                            list.Add(item);
                        }
                    }
                }
                testers.ItemsSource = list;

            }
            else
            {
                List<Tester> list = new List<Tester>();
                IEnumerable<IGrouping<Vehicle, Tester>> l = bl.VehicleGrouping(true);
                foreach (var group in l)
                {
                    if (group.Key == (Vehicle)selected)
                    {
                        foreach (var item in group)
                        {
                            list.Add(item);
                        }
                    }
                }
                testers.ItemsSource = list;

            }

        }

        private void choose_DropDownClosed(object sender, EventArgs e)
        {
            choose_Selected(sender, new RoutedEventArgs());
        }
    }
}
