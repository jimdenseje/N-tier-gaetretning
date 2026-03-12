namespace DTOs
{
    public class ScoreDto
    {
        public int Id { get; set; }
        public required int ScoreValue { get; set; }
        public required int UserId { get; set; }
        public required string Username { get; set; }
        public required int AgeGroupId { get; set; }
        public required int DailyChallengeId { get; set; }
        public required DateTime ChallengeDate { get; set; }
    }
}
