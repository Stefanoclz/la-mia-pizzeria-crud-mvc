using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Ingredienti")]
public class Ingrediente
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Pizza>? listaPizze { get; set; }

}

