namespace DevoraLimeTest.Data.Models
{
    public class History
    {
        public int Id { get; set; }
        public int NumOfFights { get; set; }
        public int ArenaID { get; set; }
        public virtual IEnumerable<Fight> Fights { get; set; }
    }
}
