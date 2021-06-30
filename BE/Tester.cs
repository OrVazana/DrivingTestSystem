using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class Tester : ICloneable
    {
        private bool[,] ScheduleTable = new bool[5, 10];

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender gender { get; set; }
        public DateTime Birth { get; set; }
        public Address address { get; set; }
        public Vehicle VehicleType { get; set; }
        public Transmission transmission { get; set; }
        public string PhoneNumber { get; set; }
        public int Experience { get; set; }
        public int MaxTestPerWeek { get; set; }
        [XmlIgnore]
        public bool[,] Schedule { get => ScheduleTable; set => ScheduleTable = value; }
        public string change
        {
            get
            {
                string str = "";
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (Schedule[i, j])
                            str += "1";
                        else
                            str += "0";

                    }
                }
                return str;
            }
            set
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Schedule[i, j] = value[i * 10 + j] == '1';
                    }
                }
            }
        }
        public string password { get; set; }

        public override string ToString()
        {
            return "name:" + FirstName + " " + LastName + ", live in " + address.Street + " " + address.HomeNum + ", " + address.City;
        }

        public Tester()
        {
        }
        public Tester(string id, string last, string first, DateTime birth, Gender gen, string phone, Address add, Transmission tran, int exp, int max, Vehicle type, bool[,] schedule, string pass)
        {
            Id = id;
            LastName = last;
            FirstName = first;
            Birth = birth;
            gender = gen;
            PhoneNumber = phone;
            address = add;
            transmission = tran;
            Experience = exp;
            MaxTestPerWeek = max;
            VehicleType = type;
            Schedule = schedule;
            password = pass;
        }
        public object Clone()
        {
            return new Tester(Id, LastName, FirstName, Birth, gender, PhoneNumber, address, transmission, Experience, MaxTestPerWeek, VehicleType, Schedule, password);
        }


    }
}
