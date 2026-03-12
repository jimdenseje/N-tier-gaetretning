namespace Models
{
    public class Score
    {
        public int Id { get; set; }
        public int DailyChallengeId { get; set; }
        public required DailyChallenge DailyChallenge { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public required int ScoreValue { get; set; }
    }
}
