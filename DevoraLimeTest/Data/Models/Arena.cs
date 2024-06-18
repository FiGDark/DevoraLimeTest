namespace DevoraLimeTest.Data.Models
{
    public class Arena
    {
        public int Id { get; set; }
        public int NumOfHeroes { get; set; }
        public bool inFight { get; set; }
        public virtual ICollection<Hero>? Heroes { get; set; }
        public virtual ICollection<History>? Histories { get; set; }
    }
}
