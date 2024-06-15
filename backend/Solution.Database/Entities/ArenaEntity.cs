namespace Solution.Database.Entities;

[Table("Arena")]
public class ArenaEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }

    [StringLength(64)]
    public string Name { get; set; }

    public virtual ICollection<HeroEntity> Heroes { get; set; }

    public virtual ICollection<FightHistoryEntity> FightHistories { get; set; }
}
