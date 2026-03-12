namespace Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public int AgeGroupId { get; set; }
        public required AgeGroup AgeGroup { get; set; }
        public ICollection<Score>? Scores { get; set; }
    }
}
