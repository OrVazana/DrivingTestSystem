using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum Hours { eight, nine, ten, eleven, twelve, one, two, three, four, five };
    public enum Gender { male, female };
    public enum Transmission { manual, automatic };
    public enum Vehicle { PrivateCar, Motorbike, Truck, tractor };
    public struct Address
    {
        public string Street { get; set; }
        public int HomeNum { get; set; }
        public string City { get; set; }
    }
}
