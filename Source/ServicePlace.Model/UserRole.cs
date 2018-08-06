namespace ServicePlace.Model
{
    public class UserRole
    {
        public int Id { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}