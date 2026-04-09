namespace TVSchedulingSystem.Models
{
    public class ProgramItem
    {
        public string ProgramCode { get; set; }
        public string ProgramName { get; set; }
        public string ImagePath { get; set; }

        public override string ToString()
        {
            return $"{ProgramCode} - {ProgramName}";
        }
    }
}