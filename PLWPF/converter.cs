using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;


namespace PLWPF
{
    public class converter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool[,] w = (bool[,])value;
            List<work_days> listsDays = new List<work_days>();
            for (int i = 0; i < 5; i++)
            {
                listsDays.Add(new work_days(((Enum.GetValues(typeof(DayOfWeek))).GetValue(i)).ToString()));
                listsDays[i].eight = w[i, 0];
                listsDays[i].nine = w[i, 1];
                listsDays[i].ten = w[i, 2];
                listsDays[i].eleven = w[i, 3];
                listsDays[i].twelve = w[i, 4];
                listsDays[i].one = w[i, 5];
                listsDays[i].two = w[i, 6];
                listsDays[i].three = w[i, 7];
                listsDays[i].four = w[i, 8];
                listsDays[i].five = w[i, 9];
            }
            return listsDays;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<work_days> listsDays = (List<work_days>)value;
            bool[,] w = new bool[5, 10];
            for (int i = 0; i < 5; i++)
            {
                w[i, 0] = listsDays[i].eight;
                w[i, 1] = listsDays[i].nine;
                w[i, 2] = listsDays[i].ten;
                w[i, 3] = listsDays[i].eleven;
                w[i, 4] = listsDays[i].twelve;
                w[i, 5] = listsDays[i].one;
                w[i, 6] = listsDays[i].two;
                w[i, 7] = listsDays[i].three;
                w[i, 8] = listsDays[i].four;
                w[i, 9] = listsDays[i].five;
            }
            return w;
        }
    }
    public class work_days // One day
    {
        private readonly string days_hours;
        public string Days_hours => days_hours;
        public work_days(string name)
        {
            days_hours = name;
            eight = false;
            nine = false;
            ten = false;
            eleven = false;
            twelve = false;
            one = false;
            two = false;
            three = false;
            four = false;
            five = false;
        }
        public bool eight { get; set; }
        public bool nine { get; set; }
        public bool ten { get; set; }
        public bool eleven { get; set; }
        public bool twelve { get; set; }
        public bool one { get; set; }
        public bool two { get; set; }
        public bool three { get; set; }
        public bool four { get; set; }
        public bool five { get; set; }
    }
}
