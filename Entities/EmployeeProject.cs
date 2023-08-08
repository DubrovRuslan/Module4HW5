namespace Module4HW5.Entities
{
    public class EmployeeProject
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public DateTime StartDate { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
