using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Pizze")]
public class Pizza
{
    [Key]
    public int id { get; set; }

    [Required(ErrorMessage = "Il campo è obbligatorio")]
    [StringLength(25, ErrorMessage = "Il nome non può avere più di 25 caratteri")]
    public string name { get; set; }

    [Required(ErrorMessage = "Il campo è obbligatorio")]
    [StringLength(400, ErrorMessage = "La descrizione non può avere più di 400 caratteri")]
    [MoreThanFiveWordsValidationAttribute]
    [Column(TypeName = "text")]
    public string description { get; set; }

    [Required(ErrorMessage = "Il campo è obbligatorio")]
    [Url(ErrorMessage = "Deve essere un link valido")]
    public string fotoLink { get; set; }

    [Required(ErrorMessage = "Il campo è obbligatorio")]
    [Range(1, 15, ErrorMessage = "Il prezzo deve essere compreso tra 1 e 15")]
    [Column(TypeName = "decimal(6, 2)")]
    public decimal prezzo { get; set; }

    public List<Ingrediente>? listaIngredienti { get; set; }

    public int? CategoryId { get; set; }

    public Category? Categoria { get; set; }

    public Pizza(string name, string description, string fotoLink, decimal prezzo)
    {
        this.name = name;
        this.description = description;
        this.fotoLink = fotoLink;
        this.prezzo = prezzo;
    }

    public Pizza()
    {

    }
}

