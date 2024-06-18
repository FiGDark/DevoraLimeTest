namespace DevoraLimeTest.Data.Models
{
    public class Hero
    {
        public int Id { get; set; }
        public int HP { get; set; }
        public int TypeID { get; set; }
        public virtual Type? Type { get; set; } 
        public int ArenaID { get; set; }

    }
}
