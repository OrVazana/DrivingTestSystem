using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Test : ICloneable
    {
        private int passOrNot=0;
        public string TestID { get; set; }
        public string TesterId { get; set; }
        public string TraineeId { get; set; }
        public DateTime PreferedDate { get; set; }
        public Vehicle TestVehicleType { get; set; }
        public Transmission transmission { get; set; }
        public Address PreferdAddress { get; set; }
        public Address EndAddress { get; set; }

            public int PassOrNot {
            get
            {
                return passOrNot;
            }
            set
            {
                if (value == 1 || value==2)
                passOrNot = value;
            }
        }
        public string TesterNotes { get; set; }

        public bool Reverse { get; set; }
        public bool LookInMirrors { get; set; }
        public bool Signaling { get; set; }
        public bool KeepDistance { get; set; }
      


        public override string ToString()
        {
            return "Test date: " + PreferedDate + ", Start in " + PreferdAddress.Street + " " + PreferdAddress.HomeNum + ", " + PreferdAddress.City + ", End in " + EndAddress.Street + " " + EndAddress.HomeNum + ", " + EndAddress.City;

        }

        public Test()
        {

        }
        public Test(string TestID, string TesterId, string TraineeId, DateTime PreferedDate, Vehicle TestVehicleType, Transmission transmission, Address PreferdAddress, Address EndAddress, int PassOrNot, string TesterNotes, bool Reverse, bool LookInMirrors, bool Signaling, bool KeepDistance)
        {
            this.TestID = TestID;
            this.TesterId = TesterId;
            this.TraineeId = TraineeId;
            this.PreferedDate = PreferedDate;
            this.TestVehicleType = TestVehicleType;
            this.transmission = transmission;
            this.PreferdAddress = PreferdAddress;
            this.EndAddress = EndAddress;
            this.PassOrNot = PassOrNot;

            this.TesterNotes = TesterNotes;
            this.Reverse = Reverse;
            this.LookInMirrors = LookInMirrors;
            this.Signaling = Signaling;
            this.KeepDistance = KeepDistance;
        }
        public object Clone()
        {
            return new Test(TestID, TesterId, TraineeId, PreferedDate, TestVehicleType, transmission, PreferdAddress, EndAddress, PassOrNot, TesterNotes, Reverse, LookInMirrors, Signaling, KeepDistance);
        }
    }
}
