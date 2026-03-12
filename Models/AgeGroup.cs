namespace Models
{
    public class AgeGroup
    {
        public int Id { get; set; }
        public required int MinAge { get; set; }
        public required int MaxAge { get; set; }
        public string Name {
            get
            {
              if (MinAge == 0 && MaxAge == 2)
                    return "Infant";
                else if (MinAge == 3 && MaxAge == 12)
                    return "Child";
                else if (MinAge == 13 && MaxAge == 19)
                    return "Teenager";
                else if (MinAge == 20 && MaxAge == 64)
                    return "Adult";
                else if (MinAge >= 65)
                    return "Senior";
                else
                    return "Unknown";
            }
        }
        public ICollection<User>? Users { get; set; }
    }
}
