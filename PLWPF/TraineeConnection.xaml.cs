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
    /// Interaction logic for TraineeConnction.xaml
    /// </summary>
    public partial class TraineeConnection : UserControl
    {
        IBL bl = FactoryBL.GetBL();
        Trainee connect = new Trainee();
        Test connect_test = new Test();

        public TraineeConnection(Trainee t)
        {
            InitializeComponent();
            connect = t;
            Rgrid.DataContext = t;
            hours.ItemsSource = Enum.GetNames(typeof(Hours));
            UP_hours.ItemsSource = Enum.GetNames(typeof(Hours));
            vehicle.ItemsSource = Enum.GetNames(typeof(Vehicle));
            transmission.ItemsSource = Enum.GetNames(typeof(Transmission));
            if (bl.findIdTraineeHaveTest(connect.Id))
            {
                succses.Visibility = Visibility.Visible;
                hours.Visibility = Visibility.Hidden;
                housr1.Visibility = Visibility.Hidden;
                date.Visibility = Visibility.Hidden;
                date1.Visibility = Visibility.Hidden;
                b.Visibility = Visibility.Hidden;
                update_test_tab.IsEnabled = true;

                connect_test = bl.FindTest(connect.Id, null).First();
                if (connect_test.PassOrNot == 2)
                {
                    sign_test_tab.IsEnabled = false;
                    update_test_tab.IsEnabled = false;
                    grade_tab.IsEnabled = true;
                    grade_tab.Content = "עבר בהצלחה";
                }
            }
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
                        bl.DeleteTrainee(connect);
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
                if (!IslettersOnly(RdriveSchool.Text))
                    throw new Exception("שם בית הספר יכיל רק אותיות אנגליות");
                if (!IsDigitsOnly(Rlessons.Text))
                    throw new Exception("מספר שיעורים שבוצעו יכיל רק מספרים");

                Trainee t = new Trainee();
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
                    a.City = connect.Address.City;
                else
                    a.City = city.Text;

                if (street.Text == "")
                    a.Street = connect.Address.Street;
                else
                    a.Street = street.Text;

                if (houseN.Text == "")
                    a.HomeNum = connect.Address.HomeNum;
                else
                    a.HomeNum = int.Parse(houseN.Text);

                t.Address = a;

                if (driveSchool.Text == "")
                    t.DriveSchool = connect.DriveSchool;
                else
                    t.DriveSchool = driveSchool.Text;
                if (lessons.Text == "")
                    t.NumLesson = connect.NumLesson;
                else
                    t.NumLesson = int.Parse(lessons.Text);

                if (Password.Text == "")
                    t.password = connect.password;
                else
                    t.password = Password.Text;


                t.Birth = connect.Birth;
                t.Id = connect.Id;
                t.VehicleType = connect.VehicleType;
                t.Transmission = connect.Transmission;
                t.gender = connect.gender;


                bl.UpdateTrainee(t);
                MessageBox.Show("בוחן עודכן בהצלחה!", "בוצע בהצלחה!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                name.Clear();
                family.Clear();
                Password.Clear();
                phone.Clear();
                city.Clear();
                street.Clear();
                houseN.Clear();
                lessons.Clear();
                driveSchool.Clear();

                connect = t;
                Rgrid.DataContext = t;

            }

            catch (Exception m)
            {
                MessageBox.Show(m.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }

        }


        private void TEST(object sender, RoutedEventArgs e)
        {
            try
            {
                Test t = new Test();
                t.TraineeId = connect.Id;
                t.transmission = connect.Transmission;
                t.TestVehicleType = connect.VehicleType;
                t.PreferdAddress = connect.Address;
                double hour = (int)Enum.Parse(typeof(Hours), hours.Text) + 8;
                DateTime temp = new DateTime();
                temp = date.DisplayDate;
                temp = temp.AddHours(hour);
                if(temp<DateTime.Now.Date)
                {
                    throw new Exception("התאריך צריך להיות עתידי");  
                }
                t.PreferedDate = temp;

                bl.AddTest(t);
                connect_test = t;
                succses.Visibility = Visibility.Visible;
                succses.Text = "נרשמת למבחן ";
                hours.Visibility = Visibility.Hidden;
                housr1.Visibility = Visibility.Hidden;
                date.Visibility = Visibility.Hidden;
                date1.Visibility = Visibility.Hidden;
                b.Visibility = Visibility.Hidden;
                update_test_tab.IsEnabled = true;
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

        private void UPDATE_TEST(object sender, RoutedEventArgs e) // חסר התז כדי לעדכן את המבחן הרצוי 
        {
            try
            {
                Test test = new Test();
                test = bl.FindTest(connect.Id,null).First();
                double hour = (int)Enum.Parse(typeof(Hours), UP_hours.Text) + 8;
                DateTime temp = new DateTime();
                temp = UP_date.DisplayDate;
                temp = temp.AddHours(hour);
                if (temp < DateTime.Now.Date)
                {
                    throw new Exception("התאריך צריך להיות עתידי");
                }
                test.PreferedDate = temp;
                
                bl.UpdateTest(test);
                MessageBox.Show("מבחן עודכן בהצלחה", "עדכון מבחן-בוצע!", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
        }
    }

}

