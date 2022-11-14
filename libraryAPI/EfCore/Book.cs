using System.ComponentModel.DataAnnotations.Schema;
namespace libraryAPI.EfCore;

[Table("book")]
public class Book
{
	public int id { get; set; }
	public string? isbn { get; set; }
    public string? name { get; set; }
	public DateTime? date_of_first_publication { get; set; }  
	public int edition { get; set; }  
	public string? publisher { get; set; }
	public string? original_language { get; set; }
	public List<Author> authors { get; set; } = new List<Author>();
}