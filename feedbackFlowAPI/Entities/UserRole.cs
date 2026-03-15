namespace feedbackFlowAPI.Entities
{
    public class UserRole
    {
        public string Name { get; set; } = null!;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
