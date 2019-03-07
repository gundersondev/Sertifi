using System;
using System.Collections.Generic;
using System.Linq;

namespace Sertifi
{
    public class StudentStatistics
    {
        public int GetYearOfHighestAttendance(List<Student> students)
        {
            int result = 0;
            try
            {
                Dictionary<int, int> attendanceByYear = new Dictionary<int, int>();
                foreach (Student student in students)
                {
                    int startYear = student.StartYear;
                    int endYear = student.EndYear;
                    for (int year = startYear; year <= endYear; year++)
                    {
                        if (attendanceByYear.ContainsKey(year))
                            attendanceByYear[year]++;
                        else
                            attendanceByYear.Add(year, 1);
                    }
                }
                result = !attendanceByYear.Any() ? -1 : attendanceByYear.Aggregate((left, right) => left.Value > right.Value ? left :
                                   (left.Value == right.Value && left.Key < right.Key) ? left : right).Key;

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return -1;
            }
            return result;
        }

        public int GetYearWithHighestOverallGPA(List<Student> students)
        {
            int year = 0;
            try
            {
                if (students.Any())
                {
                    var studentWithMaxGPA = students.OrderByDescending(s => s.MaxGPA).Take(1).FirstOrDefault();
                    int startYear = studentWithMaxGPA.StartYear;
                    int index = Array.FindIndex(studentWithMaxGPA.GPARecord, w => w == studentWithMaxGPA.MaxGPA);
                    year = startYear + index;
                }
                else
                {
                    year = -2;
                }

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return -2;
            }
            return year;
        }

        public List<long> TopTenStudentsOverallGPA(List<Student> students)
        {
            List<long> topTen = new List<long>();
            try
            {
                if (students.Any())
                {
                    topTen = students.OrderByDescending(s => s.AverageGPA).Take(10).Select(i => i.Id).ToList();
                }
                else
                {
                    return new List<long>();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return new List<long>();
            }
            return topTen;
        }

        public long StudentWithLargestGPASwing(List<Student> students)
        {
            Student student = new Student();
            try
            {
                if (students.Any())
                {
                    student = students.OrderByDescending(s => s.DifferenceGPA).FirstOrDefault();
                }
                else
                {
                    return -4;
                }

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return -4;
            }

            return student.Id;
        }
    }
}
