using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee : ICloneable
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender gender { get; set; }
        public DateTime Birth { get; set; }
        public Address Address { get; set; }
        public Vehicle VehicleType { get; set; }
        public Transmission Transmission { get; set; }
        public string PhoneNumber { get; set; }
        public string DriveSchool { get; set; }
        public int NumLesson { get; set; }
        public string password { get; set; }

        public override string ToString()
        {
            return "name:"+FirstName + " " + LastName+", live in "+Address.Street+" " +Address.HomeNum + ", "+Address.City  ;
        }

        public Trainee()
        {

        }
        public Trainee(string Id, string FirstName, string LastName, Gender gender, string PhoneNumber, Address Address, DateTime Birth, Vehicle VehicleType, Transmission Transmission, string DriveSchool, int NumLesson, string password)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.gender = gender;
            this.PhoneNumber = PhoneNumber;
            this.Address = Address;
            this.Birth = Birth;
            this.VehicleType = VehicleType;
            this.Transmission = Transmission;
            this.DriveSchool = DriveSchool;
            this.NumLesson = NumLesson;
            this.password = password;
        }
        public object Clone()
        {
            return new Trainee(Id, FirstName, LastName, gender, PhoneNumber, Address, Birth, VehicleType, Transmission, DriveSchool, NumLesson, password);
        }
    }
}
