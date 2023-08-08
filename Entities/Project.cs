namespace Module4HW5.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartedDate { get; set; }
        public virtual List<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
