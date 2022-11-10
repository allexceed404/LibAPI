namespace libraryAPI.Models;
public class BookModel
{
	public string? isbn { get; set; }
    public string? name { get; set; }
	public DateTime? date_of_first_publication { get; set; }  
	public int edition { get; set; } = 0;
	public string? publisher { get; set; }
	public string? original_language { get; set; }
	public List<AuthorModel> authors { get; set; } = new List<AuthorModel>();
}