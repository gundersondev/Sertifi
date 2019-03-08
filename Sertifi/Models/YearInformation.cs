namespace Sertifi.Models
{
    public class YearInformation
    {
        public double SumOfGPAs { get; set; }
        public int NumberOfStudents { get; set; }
        public double AverageGPA { get { return SumOfGPAs / NumberOfStudents; } } 
    }
}
