using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using BE;
namespace DAL
{
    public interface IDal
    {
        void AddTester(Tester tester);
        void DeleteTester(Tester tester);
        void UpdateTester(Tester tester);
        void AddTrainee(Trainee trainee);
        void DeleteTrainee(Trainee trainee);
        void UpdateTrainee(Trainee trainee);

        void AddTest(Test test);
        void UpdateTest(Test test);

        List<Tester> GetAllTesters();
        List<Trainee> GetAllTrainees();
        List<Test> GetAllTests();
        Trainee FindTrainee(string id);
        Tester FindTester(string id);
        List<Test> FindTest(string traineeId, string testerId);
    }

    static class stt
    {
        public static IEnumerable<T> DeepCloneByArray<T>(this IEnumerable<T> obj)
        {
            T[] arr = new T[obj.Count()];
            obj.ToList().CopyTo(arr);
            return arr.ToList();
        }
    }
}
