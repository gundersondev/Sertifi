using System.Linq;

namespace Sertifi.Models
{
    public class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public double[] GPARecord { get; set; }
        public double MaxGPA { get { return GPARecord.Max(); } }
        public double DifferenceGPA { get { return GPARecord.Max() - GPARecord.Min(); } }
        public double AverageGPA { get { return GPARecord.Average(); } }
    }
}
