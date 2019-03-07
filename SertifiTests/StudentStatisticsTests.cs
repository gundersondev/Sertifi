using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sertifi.Tests
{
    [TestClass()]
    public class StudentStatisticsTests
    {
        List<Student> students;
        StudentStatistics stats;

        [TestInitialize]
        public void TestInit()
        {
            string resVal = SertifiTests.Resource.ResourceManager.GetString("Students");
            students = JsonConvert.DeserializeObject<List<Student>>(resVal);
            stats = new StudentStatistics();
        }

        [TestMethod]
        public void WhenStudentDataCountEquals25Test()
        {
            Assert.IsTrue(students.Count == 25);
        }

        [TestMethod]
        public void IsDataOfTypeStudentTest()
        {
            Assert.IsInstanceOfType(students, typeof(List<Student>));
        }

        [TestMethod()]
        public void GetYearOfHighestAttendance_StandardData_Test()
        {
            int year = stats.GetYearOfHighestAttendance(students);
            Assert.AreEqual(2011, year);
        }

        [TestMethod()]
        public void GetYearOfHighestAttendance_NoStudents_Test()
        {
            List<Student> s = new List<Student>();
            int year = stats.GetYearOfHighestAttendance(s);
            Assert.AreEqual(-1, year);
        }

        [TestMethod()]
        public void GetYearWithHighestOverallGPA_StandardData_Test()
        {
            int year = stats.GetYearWithHighestOverallGPA(students);
            Assert.AreEqual(2013, year);
        }

        [TestMethod()]
        public void GetYearWithHighestOverallGPA_NoStudents_Test()
        {
            int year = stats.GetYearWithHighestOverallGPA(new List<Student>());
            Assert.AreEqual(-2, year);
        }

        [TestMethod()]
        public void GetTopTenStudentsOverallGPA_StandardData_Test()
        {
            List<long> ids = stats.TopTenStudentsOverallGPA(students);
            List<long> test = new List<long>() { 4, 10, 11, 20, 18, 13, 24, 6, 22, 9 };
            Assert.IsTrue(ids.Count == 10);
            CollectionAssert.AreEqual(test, ids);
        }

        [TestMethod()]
        public void GetTopTenStudentsOverallGPA_NoStudents_Test()
        {
            List<long> ids = stats.TopTenStudentsOverallGPA(new List<Student>());
            Assert.IsTrue(ids.Count == 0);
            CollectionAssert.AreEqual(new List<long>(), ids);
        }

        [TestMethod()]
        public void StudentWithLargestGPASwing_StandardData_Test()
        {
            long id = stats.StudentWithLargestGPASwing(students);
            Assert.AreEqual(15, id);
        }

        [TestMethod()]
        public void StudentWithLargestGPASwing_NoStudents_Test()
        {
            long id = stats.StudentWithLargestGPASwing(new List<Student>());
            Assert.AreEqual(-4, id);
        }
    }
}