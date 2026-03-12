namespace Models
{
    public class DailyChallenge
    {
        public int Id { get; set; }
        public required DateTime ChallengeDate { get; set; }
        public required short Direction { get; set; }
        public ICollection<Score>? Scores { get; set; }
    }
}
