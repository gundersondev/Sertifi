using System;
using System.Collections.Generic;
using System.Linq;

namespace Sertifi
{
    public class StudentStatistics
    {
        public Dictionary<int, YearInformation> PopulateClassInformation(List<Student> students)
        {
            Dictionary<int, YearInformation> attendanceByYear = new Dictionary<int, YearInformation>();
            try
            {
                if (students == null || !students.Any()) return attendanceByYear;

                foreach (Student student in students)
                {
                    int startYear = student.StartYear;
                    int endYear = student.EndYear;
                    int count = 0;
                    for (int year = startYear; year <= endYear; year++)
                    {
                        if (attendanceByYear.ContainsKey(year))
                        {
                            attendanceByYear[year].NumberOfStudents++;
                            attendanceByYear[year].SumOfGPAs += student.GPARecord[count];
                        }
                        else
                        {
                            YearInformation info = new YearInformation();
                            info.SumOfGPAs += student.GPARecord[count];
                            info.NumberOfStudents = 1;
                            attendanceByYear.Add(year, info);
                        }
                    }
                }
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
                return new Dictionary<int, YearInformation>();
            }
            return attendanceByYear;
        }

        public int GetYearOfHighestAttendance(Dictionary<int, YearInformation> info)
        {
            int result = 0;
            try
            {
                //If no data return -1
                //Return largest student class year 
                //If there is a tie return the earliest year
                result = (info == null || !info.Any()) ? -1 : info.Aggregate((left, right) => 
                                            left.Value.NumberOfStudents > right.Value.NumberOfStudents ? left :
                                            (left.Value.NumberOfStudents == right.Value.NumberOfStudents && left.Key < right.Key) ? 
                                            left : right).Key;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return -1;
            }
            return result;
        }

        public int GetYearWithHighestOverallGPA(Dictionary<int,YearInformation> info)
        {
            int result = 0;
            try
            {
                result = (info == null || !info.Any()) ? -2 : info.OrderByDescending(g => g.Value.AverageGPA).FirstOrDefault().Key;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return -2;
            }
            return result;
        }

        public List<long> TopTenStudentsOverallGPA(List<Student> students)
        {
            List<long> topTen = new List<long>();
            try
            {
                if (students != null && students.Any())
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
            long id = 0;
            try
            {
                id = (students == null && !students.Any()) ? -4 : 
                    students.OrderByDescending(s => s.DifferenceGPA).FirstOrDefault().Id;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return -4;
            }
            return id;
        }
    }
}
