namespace DevoraLimeTest.Data.Models
{
    public class Arena
    {
        public int Id { get; set; }
        public int NumOfHeroes { get; set; }
        public bool inFight { get; set; }
        public virtual IEnumerable<Hero> Heroes { get; set; }
        public virtual IEnumerable<History> Histories { get; set; }
    }
}
