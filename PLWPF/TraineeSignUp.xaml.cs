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
    /// Interaction logic for TraineeSignUp.xaml
    /// </summary>
    public partial class TraineeSignUp : UserControl
    {
        IBL bl;
        public TraineeSignUp()
        {
            InitializeComponent();
            gender.ItemsSource = Enum.GetNames(typeof(Gender));
            transmission.ItemsSource = Enum.GetNames(typeof(Transmission));
            vehicle.ItemsSource = Enum.GetNames(typeof(Vehicle));
        }

        private void ADD(object sender, RoutedEventArgs e)
        {
            bl = FactoryBL.GetBL();

            try
            {
                if (!IsDigitsOnly(ID.Text))
                    throw new Exception("תעודת זהות תכיל רק מספרים");
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
                if (!IslettersOnly(driveSchool.Text))
                    throw new Exception("שם בית הספר יכיל רק מספרים");
                if (!IsDigitsOnly(Lessons.Text))
                    throw new Exception("מספר שיעורים יכיל רק מספרים");
                if (Birth.SelectedDate==null)
                    throw new Exception("חסר תאריך לידה");


                Trainee t = new Trainee();
                t.FirstName = name.Text;
                t.LastName = family.Text;
                t.Id = ID.Text;
                t.Birth = (DateTime)Birth.SelectedDate;
                t.gender = (Gender)Enum.Parse(typeof(Gender), gender.Text);
                t.Transmission = (Transmission)Enum.Parse(typeof(Transmission), transmission.Text);
                t.VehicleType = (Vehicle)Enum.Parse(typeof(Vehicle), vehicle.Text);
                t.PhoneNumber = phone.Text;
                Address a = new Address();
                a.City = city.Text;
                a.Street = street.Text;
                a.HomeNum = int.Parse(houseN.Text);
                t.Address = a;
                t.DriveSchool = driveSchool.Text;
                t.NumLesson = int.Parse(Lessons.Text);
                t.password = Password.Text;

                bl.AddTrainee(t);
                MessageBox.Show("תלמיד הוסף בהצלחה!", "בוצע בהצלחה!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                name.Clear();
                family.Clear();
                ID.Clear();
                Password.Clear();
                phone.Clear();
                city.Clear();
                street.Clear();
                houseN.Clear();
                driveSchool.Clear();
                Lessons.Clear();
                Switcher.Switch(new options());
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
    }

}