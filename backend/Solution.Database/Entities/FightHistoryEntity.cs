namespace Solution.Database.Entities;

[Table("FightHistory")]
public class FightHistoryEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }

    public int ArenaId { get; set; }

    [StringLength(1024)]
    public string Log {  get; set; }

    public virtual ArenaEntity Arena { get; set; }
}
