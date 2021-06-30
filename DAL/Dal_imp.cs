using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    public class Dal_imp : IDal
    {
        private string RunnigID()
        {
            string help = "00000000";
            int c = GetAllTests().Count().ToString().Length + 1;
            help.Substring(0, 7 - c);
            help += c;
            return help;
        }
        public void AddTest(Test test)
        {
            test.TestID = RunnigID();
            DS.DataSource.TestsList.Add((Test)test.Clone());
        }
        public void UpdateTest(Test test)
        {
            var v = from item in GetAllTests()
                    where item.TestID == test.TestID
                    select item;
            if (v != null)
            {
                DS.DataSource.TestsList.Remove(v as Test);
                DS.DataSource.TestsList.Add((Test)test.Clone());
            }
            else
                throw new Exception("לא קיים מבחן שצריך לעדכן");
        }


        public void AddTester(Tester tester)
        {
            var v = from item in GetAllTesters()
                    where item.Id == tester.Id
                    select item;
            if (v.Count() > 0)
                throw new Exception("הבוחן כבר קיים!");
            else
                DS.DataSource.TestersList.Add((Tester)tester.Clone());
        }
        public void DeleteTester(Tester tester)
        {
            var v = (from item in GetAllTesters()
                     where item.Id == tester.Id
                     select item).ToList();
            if (v.Count() != 0)
                DS.DataSource.TestersList.Remove(v.First());
            else
                throw new Exception("הבוחן לא קיים!");
        }
        public void UpdateTester(Tester tester)
        {
            var v = from item in GetAllTesters()
                    where item.Id == tester.Id
                    select item;
            if (v.Count() > 0)
            {
                DS.DataSource.TestersList.Remove(v.First());
                DS.DataSource.TestersList.Add((Tester)tester.Clone());
            }
            else
                throw new Exception("לא ניתן לעדכן כי לא קיים תלמיד כזה");
        }


        public void AddTrainee(Trainee trainee)
        {
            var v = (from item in GetAllTrainees()
                     where item.Id == trainee.Id
                     select item).ToList();
            if (v.Count() != 0)
                throw new Exception("הנבחן כבר קיים!");
            else
                DS.DataSource.TraineesList.Add((Trainee)trainee.Clone());
        }
        public void DeleteTrainee(Trainee trainee)
        {
            var v = from item in GetAllTrainees()
                    where item.Id == trainee.Id
                    select item;
            if (v != null)
                DS.DataSource.TraineesList.Remove(v.First());
            else
                throw new Exception("נבחן לא קיים!");
        }
        public void UpdateTrainee(Trainee trainee)
        {
            var v = from item in GetAllTrainees()
                    where item.Id == trainee.Id
                    select item;
            if (v != null)
            {
                DS.DataSource.TraineesList.Remove(v.First());
                DS.DataSource.TraineesList.Add((Trainee)trainee.Clone());
            }
            else
                throw new Exception("לא ניתן לעדכן כי הנבחן לא קיים");
        }

        public List<Test> GetAllTests()
        {
            return DataSource.TestsList.DeepCloneByArray().ToList();
        }
        public List<Tester> GetAllTesters()
        {
            return DataSource.TestersList.DeepCloneByArray().ToList();
        }
        public List<Trainee> GetAllTrainees()
        {
            return DataSource.TraineesList.DeepCloneByArray().ToList();
        }
        public IEnumerable<Test> GetAllTests(Func<Test, bool> predicat = null)
        {
            if (predicat == null)
                return DataSource.TestsList.AsEnumerable();
            return DataSource.TestsList.Where(predicat).DeepCloneByArray();
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

        public List<Test> FindTest(string TraineeId, string testerId)
        {
            List<Test> temp = GetAllTests();
            if (testerId == null && TraineeId != null)
            {
                var v = (from item in GetAllTests()
                         where item.TraineeId == TraineeId
                         select item).ToList();
                if (v.Count() == 1)
                    return v;
                else
                    throw new Exception("לא קיים מבחן כזה");
            }
            if (TraineeId == null && testerId != null)
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







