using System.ComponentModel.DataAnnotations.Schema;
namespace libraryAPI.EfCore;

[Table("relation")]
public class Relation
{
	public int id { get; set; }
    public int bookid { get; set; }
    public Book? book { get; set; }
    public int authorid { get; set; }
    public Author? author { get; set; }
}