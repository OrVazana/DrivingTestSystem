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
using BL;
using BE;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TesterConnection.xaml
    /// </summary>
    public partial class TesterConnection : UserControl
    {
        IBL bl=FactoryBL.GetBL();
        Tester connect = new Tester();
        public List<work_days> listDays { get; set; }
        public converter myconverter = new converter();
        public TesterConnection(Tester t)
        {
            InitializeComponent();
            connect = t;
            Rgrid.DataContext = t;
            listDays =(List<work_days>)myconverter.Convert(t.Schedule, null, null, null);
            RdataGrid_WorkDay.DataContext=listDays;
            dataGrid_WorkDay.DataContext = listDays;
            transmission.ItemsSource = Enum.GetNames(typeof(Transmission));
            vehicle.ItemsSource = Enum.GetNames(typeof(Vehicle));

            List<Test> Tests = bl.GetAllTests();
            testPast.DataContext = Tests.FindAll(item => (item.TesterId==connect.Id &&(DateTime.Now.Date > item.PreferedDate.Date) == true));
            testFuture.DataContext = Tests.FindAll(item => (item.TesterId == connect.Id && (DateTime.Now.Date < item.PreferedDate.Date) == true));
            testToday.DataContext = Tests.FindAll(item => (item.TesterId == connect.Id && DateTime.Now.Date == item.PreferedDate.Date));

        }
        private void logOut(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("האם אתה בטוח שברצונך להתנתק?", "התנתקות", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        Switcher.Switch(new open());
                        break;
                    }
                case MessageBoxResult.No:
                    break;

                default:
                    break;
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("האם אתה בטוח שברצונך למחוק משתמש זה?", "מחיקת משתמש", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        bl = FactoryBL.GetBL();
                        bl.DeleteTester(connect);
                        MessageBox.Show("משתמש נמחק בהצלחה", "מחיקת משתמש-בוצע!", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

                        Switcher.Switch(new open());
                        break;
                    }
                case MessageBoxResult.No:
                    break;

                default:
                    break;
            }
        }

        private void UPDATE(object sender, RoutedEventArgs e)
        {
            bl = FactoryBL.GetBL();
            try
            {

                if (!IslettersOnly(name.Text))
                    throw new Exception("שם פרטי יכיל רק אותיות אנגליות");
                if (!IslettersOnly(family.Text))
                    throw new Exception("שם משפחה יכיל רק אותיות אנגליות");
                if (!IsDigitsOnly(phone.Text))
                    throw new Exception("מספר טלפון יכיל רק מספרים");
                if (!IslettersOnly(city.Text))
                    throw new Exception("עיר תכיל רק אותיות אנגליות");
                if (!IslettersOnly(street.Text))
                    throw new Exception("רחוב יכיל רק אותיות אנגליות");
                if (!IsDigitsOnly(houseN.Text))
                    throw new Exception("מספר בית יכיל רק מספרים");
                if (!IsDigitsOnly(Exp.Text))
                    throw new Exception("שנות ותק יכיל רק מספרים");
                if (!IsDigitsOnly(Max.Text))
                    throw new Exception("מספר מבחנים פר שבוע יכיל רק מספרים");


                Tester t = new Tester();
                if (name.Text == "")
                    t.FirstName = connect.FirstName;
                else
                    t.FirstName = name.Text;

                if (family.Text == "")
                    t.LastName = connect.LastName;
                else
                    t.LastName = family.Text;

                if (phone.Text == "")
                    t.PhoneNumber = connect.PhoneNumber;
                else
                    t.PhoneNumber = phone.Text;

                Address a = new Address();

                if (city.Text == "")
                    a.City = connect.address.City;
                else
                    a.City = city.Text;

                if (street.Text == "")
                    a.Street = connect.address.Street;
                else
                    a.Street = street.Text;

                if (houseN.Text == "")
                    a.HomeNum = connect.address.HomeNum;
                else
                    a.HomeNum = int.Parse(houseN.Text);
                t.address = a;

                if (Exp.Text == "")
                    t.Experience = connect.Experience;
                else
                    t.Experience = int.Parse(Exp.Text);
                if (Max.Text == "")
                    t.MaxTestPerWeek = connect.MaxTestPerWeek;
                else
                    t.MaxTestPerWeek = int.Parse(Max.Text);

                if (Password.Text == "")
                    t.password = connect.password;
                else
                    t.password = Password.Text;

                t.Schedule = (bool[,])myconverter.ConvertBack(listDays, null, null, null);
                t.Birth = connect.Birth;
                t.Id = connect.Id;
                t.VehicleType = connect.VehicleType;
                t.transmission = connect.transmission;
                t.gender = connect.gender;
            
                bl.UpdateTester(t);
                MessageBox.Show("בוחן עודכן בהצלחה!", "בוצע בהצלחה!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                connect = t;
                Rgrid.DataContext = connect;

                name.Clear();
                family.Clear();
                Password.Clear();
                phone.Clear();
                city.Clear();
                street.Clear();
                houseN.Clear();
                Exp.Clear();
                Max.Clear();
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Sign_up());
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
         private void succses_test_update(object sender, RoutedEventArgs e)
        {
            Test test = (Test)testPast.SelectedItem;
            if (test.Reverse && test.LookInMirrors && test.Signaling && test.KeepDistance)
                test.PassOrNot = 2;
            else
                test.PassOrNot = 1;
            bl.UpdateTest(test);
            List<Test> Tests = bl.GetAllTests();
            testPast.DataContext = Tests.FindAll(item => (item.TesterId == connect.Id && (DateTime.Now.Date > item.PreferedDate.Date) == true));
            MessageBox.Show("עודכן בהצלחה!", "עדכון", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

        }
    }
}
