using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sertifi
{
    public class Answer
    {
        public string YourName { get { return "Christopher Gunderson"; } }
        public string YourEmail { get { return "mrcgunderson@gmail.com"; } }
        public int YearWithHighestAttendance { get; set; }
        public int YearWithHighestOverallGpa { get; set; }
        public List<long> Top10StudentIdsWithHighestGpa { get; set; }
        public long StudentIdMostInconsistent { get; set; }
        public Answer()
        {

        }
        public Answer(long largestGPASwing, List<long> topTen, int year, int yearGPA)
        {
            StudentIdMostInconsistent = largestGPASwing;
            Top10StudentIdsWithHighestGpa = topTen;
            YearWithHighestAttendance = year;
            YearWithHighestOverallGpa = yearGPA;
        }
    }
}
