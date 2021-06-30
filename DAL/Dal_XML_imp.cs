using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
namespace DAL
{
    public class Dal_XML_imp:IDal
    {
        string traineePath = @"Trainees.xml";

        string testerPath = @"Testers.xml";
        XElement TestRoot;
        string testPath = @"Tests.xml";
        XElement runRoot;
        string runIdPath = @"runningId.xml";



        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source); file.Close();
        }
        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file); file.Close();
            return result;
        }

        private string RunnigID()
        {
            string help = "00000000";
            if (!File.Exists(runIdPath))
            {
                runRoot = new XElement("RunningID");
                XElement id = new XElement("runId", help);
                runRoot.Add(id);
                runRoot.Save(runIdPath);
            }
            else
                runRoot = XElement.Load(runIdPath);
            help = runRoot.Element("runId").Value;

            string c = (GetAllTests().Count()+1).ToString();
            help=help.Substring(0,  8- c.Length);
            help += c;
            XElement ID = (from item in runRoot.Elements()
                          select item).First();
            ID.Value = help;
            runRoot.Save(runIdPath);
            return help;
        }
     

        public void AddTest(Test test)
        {
            List<Test> tests = File.Exists(testPath) ? LoadFromXML<List<Test>>(testPath) : new List<Test>();
            test.TestID = RunnigID();
            var v = (from item in tests
                     where item.TestID == test.TestID
                     select item).ToList();
            if (v.Count() != 0)
                throw new Exception("המבחן כבר קיים!");
            tests.Add(test);
            SaveToXML(tests, testPath);

        //    TestRoot = new XElement("tests",
        //                            from p in tests
        //                            select new XElement("Test",
        //                                new XElement("TestID", p.TestID),
        //                                    new XElement("TesterId", p.TesterId),
        //                                    new XElement("TraineeId", p.TraineeId),
        //                                    new XElement("TestVehicleType", p.TestVehicleType),
        //                                    new XElement("transmission", p.transmission),
        //                                    new XElement("PreferdAddress", p.PreferdAddress),
        //                                    new XElement("PassOrNot", p.PassOrNot),
        //                                    new XElement("TesterNotes", p.TesterNotes),
        //                                    new XElement("Reverse", p.Reverse),
        //                                    new XElement("LookInMirrors", p.LookInMirrors),
        //                                    new XElement("Signaling", p.Signaling),
        //                                    new XElement("KeepDistance", p.KeepDistance)
        //                                    )
        //                            );
        //    TestRoot.Save(testPath);
        }
        public void UpdateTest(Test test)
        {
            List<Test> tests = GetAllTests();
            var v = (from item in tests
                     where item.TestID == test.TestID
                     select item).ToList();
            if (v.Count() != 0)
            {
                tests.Remove(v.First());
                tests.Add(test);
                SaveToXML(tests, testPath);
            }
            else
                throw new Exception("לא קיים מבחן שצריך לעדכן");

        }

        public void AddTester(Tester Tester)
        {

            List<Tester> testers = File.Exists(testerPath) ? LoadFromXML<List<Tester>>(testerPath) : new List<Tester>();
            var v = (from item in testers
                    where item.Id == Tester.Id
                    select item).ToList();
            if (v.Count() > 0)
                throw new Exception("הבוחן כבר קיים!");
            testers.Add(Tester);
            SaveToXML(testers, testerPath);
        }

        public void DeleteTester(Tester tester)
        {
            List<Tester> testers = GetAllTesters();
            var v = (from item in testers
                     where item.Id == tester.Id
                     select item).ToList();
            if (v.Count() != 0)
            {
                testers.Remove(v.First());
                SaveToXML(testers, testerPath);
            }
            else
                throw new Exception("בוחן לא קיים!");
        }

        public void UpdateTester(Tester tester)
        {
            try
            {
                DeleteTester(tester);
                AddTester(tester);
            }
            catch (Exception)
            {
                throw new Exception("לא ניתן לעדכן כי הבוחן לא קיים");
            }
        }



        public void AddTrainee(Trainee Trainee)
        {
            List<Trainee> trainees = File.Exists(traineePath) ? LoadFromXML<List<Trainee>>(traineePath) : new List<Trainee>();
            var v = (from item in trainees
                     where item.Id == Trainee.Id
                     select item).ToList();
            if (v.Count() != 0)
                throw new Exception("הנבחן כבר קיים!");
            trainees.Add(Trainee);
            SaveToXML(trainees, traineePath);
        }

        public void DeleteTrainee(Trainee trainee)
        {
            List<Trainee> trainees = GetAllTrainees();
            var v = (from item in trainees
                     where item.Id == trainee.Id
                     select item).ToList();
            if (v.Count() != 0)
            {
                trainees.Remove(v.First());
                SaveToXML(trainees, traineePath);
            }
            else
                throw new Exception("נבחן לא קיים!");

        }
        
        public void UpdateTrainee(Trainee trainee)
        {
            try
            {
                DeleteTrainee(trainee);
                AddTrainee(trainee);
            }
            catch (Exception)
            {
                throw new Exception("לא ניתן לעדכן כי הנבחן לא קיים");
            }
        }




       

        public List<Tester> GetAllTesters()
        {
            if (!File.Exists(testerPath))
            {
                List<Tester> testers = new List<Tester>();
                SaveToXML(testers, testerPath);
            }
            return LoadFromXML<List<Tester>>(testerPath).DeepCloneByArray().ToList();
        }

        public List<Trainee> GetAllTrainees()
        {
            if (!File.Exists(traineePath))
            {
                List<Tester> trainee = new List<Tester>();
                SaveToXML(trainee, traineePath);
            }
            return LoadFromXML<List<Trainee>>(traineePath).DeepCloneByArray().ToList();
        }

        public List<Test> GetAllTests()
        {
            if (!File.Exists(testPath))
            {
                List<Tester> tests = new List<Tester>();
                SaveToXML(tests, testPath);
            }
            return LoadFromXML<List<Test>>(testPath).DeepCloneByArray().ToList();
        }


        public Trainee FindTrainee(string id)
        {
            var v = (from item in GetAllTrainees()
                     where item.Id == id
                     select item).ToList();
            if (v.Count() == 1)
                return v.First();
            else
                throw new Exception("לא קיים תלמיד כזה");
        }

        public Tester FindTester(string id)
        {

            var v = (from item in GetAllTesters()
                     where item.Id == id
                     select item).ToList();
            if (v.Count() == 1)
                return v.First();
            else
                throw new Exception("לא קיים בוחן כזה");
        }

        public List<Test> FindTest(string TraineeId, string testerId)
        {
            List<Test> temp = GetAllTests();
            if (testerId == null && TraineeId!=null)
            {
                var v = (from item in GetAllTests()
                         where item.TraineeId == TraineeId
                         select item).ToList();
                if (v.Count() == 1)
                    return v;
                else
                    throw new Exception("לא קיים מבחן כזה");
            }
            if (TraineeId == null && testerId!=null)
            {
                var v = (from item in GetAllTests()
                         where item.TesterId == testerId
                         select item).ToList();
                if (v.Count() > 0)
                    return v;
                return null;
            }
            else
                throw new Exception("תקלה");

        }
    }
}