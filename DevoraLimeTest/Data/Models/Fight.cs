namespace DevoraLimeTest.Data.Models
{
    public class Fight
    {
        public int Id { get; set; }
        public int AttackerID { get; set; }
        public virtual Hero Attacker { get; set; }
        public int AttackerStartHP { get; set; }
        public int AttackerEndHP { get; set;}
        public int DeffenderID { get; set; }
        public virtual Hero Deffender { get; set; }
        public int DeffenderStartHP { get; set; }
        public int DeffenderEndHP { get; set; }
    }
}