namespace feedbackFlowAPI.Entities
{
    public class UserRole
    {
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
