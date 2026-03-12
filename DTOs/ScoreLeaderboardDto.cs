using Models;

namespace DTOs
{
    public class ScoreLeaderboardDto
    {
        public required IEnumerable<ScoreDto> Today { get; set; }
        public required IEnumerable<ScoreDto> Yesterday { get; set; }
    }
}
