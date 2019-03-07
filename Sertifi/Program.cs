using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

            //Get Data
            Task<List<Student>> students = apiClient.GetAsync(uriStudents);
            //Get Year of highest attendance
            int highestAttendanceYear = stats.GetYearOfHighestAttendance(students.Result);
            //Year with highest overall GPA
            int yearGPA = stats.GetYearWithHighestOverallGPA(students.Result);
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
