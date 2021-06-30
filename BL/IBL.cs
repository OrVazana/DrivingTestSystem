using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace BL
{
    public interface IBL
    {
        void AddTester(Tester tester);
        void DeleteTester(Tester tester);
        void UpdateTester(Tester tester);
        Tester FindTester(string id);
        void AddTrainee(Trainee trainee);
        void DeleteTrainee(Trainee trainee);
        void UpdateTrainee(Trainee trainee);
        Trainee FindTrainee(string id);
        List<Test> FindTest(string traineeId, string testerId);


        void AddTest(Test test);
        void UpdateTest(Test test);

        List<Tester> GetAllTesters();
        List<Trainee> GetAllTrainees();
        List<Test> GetAllTests();

        List<Tester> XkmAddress(Address address, int x);
        List<Tester> AvailableTester(DateTime dateTime);
        List<Test> Condition(Predicate<Test> p);
        IEnumerable<IGrouping<Vehicle, Tester>> VehicleGrouping(bool sort);
        IEnumerable<IGrouping<string, Tester>> AddressGrouping(bool sort);
        bool findIdTraineeHaveTest(string id);


    }
}
