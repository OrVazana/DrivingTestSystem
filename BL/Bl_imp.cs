using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BE;
using DAL;
namespace BL
{
    public class Bl_imp : IBL
    {
        IDal factory = FactoryDal.GetDAL();

        public void AddTest(Test test)
        {
            #region past month
            var v = GetAllTests().FindLast(item => test.TraineeId == item.TraineeId);
            if (v != null)
            {
                TimeSpan month = test.PreferedDate - v.PreferedDate;
                if (month.Days < 30)
                    throw new Exception("it's too early to re-test ");
            }
            #endregion
            #region available testers
            List<Tester> available = AvailableTester(test.PreferedDate);
            if (available.Count() == 0)
                throw new Exception("There is no available tester");
            #endregion
            #region 15km from trainee
            List<Tester> xkm = XkmAddress(test.PreferdAddress, 15);
            if (xkm.Count() == 0)
                throw new Exception("There is no closer tester in 10km");
            #endregion
            #region both
            var both = (from item in available
                        from item2 in xkm
                        where item.Id == item2.Id
                        select item).ToList();
            #endregion
            #region Max test per week
            bool flag = false;
            do
            {
                if (both.Count() == 0)
                    throw new Exception("their is no tester who is available ");
                Tester tester = both.First();
                both.Remove(tester);

                var s = (from item in GetAllTests()
                         where item.TesterId == tester.Id
                         where item.PreferedDate.AddDays(-(int)item.PreferedDate.DayOfWeek) == test.PreferedDate.AddDays(-(int)test.PreferedDate.DayOfWeek)
                         select item).ToList();
                if (s.Count() >= tester.MaxTestPerWeek)
                    flag = true;
                else
                    flag = false;
                test.TesterId = tester.Id;
                Tester temp = FindTester(test.TesterId);
                test.EndAddress = temp.address;

            } while (flag);
            #endregion
            try
            {
                factory.AddTest(test);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateTest(Test test)
        {
            var v = GetAllTests().Find(t => t.TestID == test.TestID);

            if (v != null)
            {
                try
                {
                    factory.UpdateTest(test);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
                throw new Exception("");

        }
        public void AddTester(Tester tester)
        {
            if (DateTime.Now.Year - tester.Birth.Year < 40)
            {
                throw new Exception("the tester is too young");
            }
            else
            {
                try
                {
                    factory.AddTester(tester);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void DeleteTester(Tester tester)
        {
            try
            {
                factory.DeleteTester(tester);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateTester(Tester tester)
        {
            try
            {
                factory.UpdateTester(tester);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AddTrainee(Trainee trainee)
        {
            if (DateTime.Now.Year - trainee.Birth.Year < 18)
            {
                throw new Exception("the trainee is too young");
            }
            else
            {
                try
                {
                    factory.AddTrainee(trainee);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public void DeleteTrainee(Trainee trainee)
        {
            try
            {
                factory.DeleteTrainee(trainee);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateTrainee(Trainee trainee)
        {
            try
            {
                factory.UpdateTrainee(trainee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Tester> GetAllTesters()
        {
            List<Tester> temp = new List<Tester>();
            temp = factory.GetAllTesters();
            return temp;
        }
        public List<Test> GetAllTests()
        {
            List<Test> temp = new List<Test>();
            temp = factory.GetAllTests();
            return temp;
        }
        public List<Trainee> GetAllTrainees()
        {
            List<Trainee> temp = new List<Trainee>();
            temp = factory.GetAllTrainees();
            return temp;
        }

        public List<Tester> XkmAddress(Address addressTrainee, int x)
        {
            Random r = new Random();
            var v = from item in GetAllTesters()
                    where Distance(addressTrainee, item.address) < x
                    select item;
            return v.ToList();
        }
        double Distance(Address addressTrainee, Address addressTester)
        {
            string API_KEY = @"pcNsEXrtoCyYnfAQFBGTTHCx1cUDnMT3";
            Address addressA = new Address();
            Address addressB = new Address();
            addressA.City = addressTrainee.City;
            addressA.HomeNum = addressTrainee.HomeNum;
            addressA.Street = addressTrainee.Street;
            addressB.City = addressTester.City;
            addressB.HomeNum = addressTester.HomeNum;
            addressB.Street = addressTester.Street;
            double result;
            List<Address> addr = new List<Address> { addressA, addressB };
            string origin = addr[0].Street + " " + addr[0].HomeNum + " st. " + addr[0].City+ " israel";
            string destination = addr[1].Street + " " + addr[1].HomeNum + " st. " + addr[1].City+ " israel";
            string KEY = API_KEY;
            string url = @"https://www.mapquestapi.com/directions/v2/route" +
            @"?key=" + KEY + @"&from=" + origin + @"&to=" + destination + @"&outFormat=xml" + @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" + @"&enhancedNarrative=false&avoidTimedConditions=false";
            //request from MapQuest service the distance between the 2 addresses
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);
            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
            {
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);

                result = (distInMiles * 1.609344);

                return result;

            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
            {
                throw new Exception("an error occurred, one of the addresses is not found. try again.");
            }
            else //busy network or other error...
            {
                throw new Exception("We have'nt got an answer, maybe the net is busy...");
            }
        }
        public List<Tester> AvailableTester(DateTime dateTimeTest)
        {
            int day = (int)dateTimeTest.DayOfWeek - 1;
            int hour = dateTimeTest.Hour - 8;
            var v = (GetAllTesters().FindAll(item => item.Schedule[day, hour] == true)).ToList();
            if (GetAllTests().Count() > 0)
            {
                var t = (from item2 in GetAllTests()
                         from item in v
                         where check(item,item2,dateTimeTest)
                         select item).ToList();
                return t;
            }
            else
                return v;
        }
        private bool check(Tester tester,Test test, DateTime dateTimeTest)
        {
            if (tester.Id == test.TesterId)
            {
                if (dateTimeTest != test.PreferedDate)
                {
                    return true;
                }
                return false;
            }
            else
                return true;
        }
        public List<Test> Condition(Predicate<Test> predicate)
        {
            var v = from item in GetAllTests()
                    where predicate(item)
                    select item;
            return v.ToList();
        }
        public IEnumerable<IGrouping<Vehicle, Tester>> VehicleGrouping(bool sort)
        {
            if (sort)
            {
                return from item in GetAllTesters()
                       orderby item.LastName, item.FirstName
                       group item by item.VehicleType;
            }
            else
                return from item in GetAllTesters()
                       group item by item.VehicleType; ;
        }
        public IEnumerable<IGrouping<string, Tester>> AddressGrouping(bool sort)
        {
            if (sort)
            {
                return from item in GetAllTesters()
                       orderby item.LastName, item.FirstName
                       group item by item.address.City;

            }
            else return from item in GetAllTesters()
                        group item by item.address.City;
        }

        public Tester FindTester(string id)
        {
            try
            {
                return factory.FindTester(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Trainee FindTrainee(string id)
        {
            try
            {
                return factory.FindTrainee(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Test> FindTest(string traineeid, string testerId)
        {
            try
            {
                return factory.FindTest(traineeid, testerId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool findIdTraineeHaveTest(string id)
        {
            var v = (from item in GetAllTests()
                     where item.TraineeId == id
                     select item).ToList();
            if (v.Count != 0)
                return true;
            else
                return false;
        }
    }
}
