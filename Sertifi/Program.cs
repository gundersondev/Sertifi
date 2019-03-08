using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sertifi.Application;
using Sertifi.Models;

namespace Sertifi
{
    public class Program
    {
        private const string uriStudents = "http://apitest.sertifi.net/api/Students";
        private const string uriAggregate = "http://apitest.sertifi.net/api/StudentAggregate";
        private const string baseUrl = "http://apitest.sertifi.net/";

        static void Main(string[] args)
        {
            APIClient apiClient = new APIClient(baseUrl);
            StudentStatistics stats = new StudentStatistics();

            //Get Student Data
            Task<List<Student>> students = apiClient.GetAsync(uriStudents);

            //Get dictionary containing class information by year
            Dictionary<int, YearInformation> info = new Dictionary<int, YearInformation>();
            info = stats.PopulateClassInformation(students.Result);

            //Get Year of highest attendance
            int highestAttendanceYear = stats.GetYearOfHighestAttendance(info);

            //Year with highest overall GPA
            int yearGPA = stats.GetYearWithHighestOverallGPA(info);

            //Top Ten Student GPAs
            List<long> topTen = stats.TopTenStudentsOverallGPA(students.Result);

            //Get Student with largest GPA swing
            long largestGPASwing = stats.StudentWithLargestGPASwing(students.Result);

            //Set answer
            Answer answer = new Answer(largestGPASwing, topTen, highestAttendanceYear, yearGPA);

            //Serialize object to string
            string output = JsonConvert.SerializeObject(answer, Formatting.Indented);

            Task<string> jsonoutput = apiClient.PutAsync(uriAggregate, answer);

            Console.ReadLine();
        }
    }
}
