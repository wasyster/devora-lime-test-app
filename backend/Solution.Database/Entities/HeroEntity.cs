namespace Solution.Database.Entities;

[Table("Hero")]
public class HeroEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ArenaId { get; set; }

    public int Health { get; set; }

    public HeroType Type { get; set; }

    protected Random rnd => new Random();

    public bool IsDead { get; set; }

    public bool IsWinner { get; set; }

    public virtual ArenaEntity Arena { get; set; }
}
