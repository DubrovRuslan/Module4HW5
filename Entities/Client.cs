namespace Module4HW5.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual List<Project> Projects { get; set; } = new List<Project>();
    }
}