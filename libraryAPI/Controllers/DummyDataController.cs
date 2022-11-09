using Microsoft.AspNetCore.Mvc;
using libraryAPI.Models;
using libraryAPI.EfCore;
namespace libraryAPI.Controllers;

[ApiController]
[Route("api/library/dummydata")]
public class DummyDataController : ControllerBase
{
    private readonly DbHelper _db;
    public DummyDataController(EF_DataContext eF_DataContext)
    {
        _db = new DbHelper(eF_DataContext);
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var book1 = new Book();
        var book2 = new Book();
        var book3 = new Book();
        var book4 = new Book();
        var book5 = new Book();

        var author1 = new Author();
        var author2 = new Author();
        var author3 = new Author();
        var author4 = new Author();

        var relation1 = new Relation();
        var relation2 = new Relation();
        var relation3 = new Relation();
        var relation4 = new Relation();
        var relation5 = new Relation();
        var relation6 = new Relation();

        //boook1
        book1.name = "Harry Potter and the Philosopher''s Stone";
        book1.date_of_first_publication = new DateTime(2001,12,10);
        book1.edition = 1;
        book1.isbn = "SDFV125667732";
        book1.publisher = "Bloomsbury";
        book1.original_language = "English";

        //book2
        book2.name = "Harry Potter and the Chamber of Secrets";
        book2.date_of_first_publication = new DateTime(1998,07,02);
        book2.edition = 1;
        book2.isbn = "JDHG125786732";
        book2.publisher = "Bloomsbury";
        book2.original_language =  "English";

        //book3
        book3.name = "Game of Thrones";
        book3.date_of_first_publication = new DateTime(1996,08,01);
        book3.edition = 1;
        book3.isbn = "PDJT125SDF732";
        book3.publisher = "Harper Voyager";
        book3.original_language = "English";
        
        //book4
        book4.name = "Time of Contempt";
        book4.date_of_first_publication = new DateTime(1995,03,13);
        book4.edition = 1;
        book4.isbn = "XAXFV12SDR732";
        book4.publisher = "superNOWA";
        book4.original_language = "Polish";

        //book5
        book5.name = "Baptism of Fire";
        book5.date_of_first_publication = new DateTime(1996,11,05);
        book5.edition = 1;
        book5.isbn = "FWEWV2ETDR732";
        book5.publisher = "superNOWA";
        book5.original_language = "Polish";


        //author1 
        author1.name = "J.K. Rowling";
        author1.date_of_birth = new DateTime(1965,07,31);
        author1.country = "United Kingdom";

        //author2 
        author2.name = "George R.R. Martin";
        author2.date_of_birth = new DateTime(1948,09,20);
        author2.country = "United States";

        //author3 
        author3.name = "Stephen Hawking";
        author3.date_of_birth = new DateTime(1942,01,08);
        author3.country = "United Kingdom";
        
        //author4
        author4.name = "Andrzej Sapkowski";
        author4.date_of_birth = new DateTime(1948,06,21);
        author4.country = "Poland";


        try
        {
            ResponseType type = ResponseType.Success;
            _db.PostBook(book1);
            _db.PostBook(book2);
            _db.PostBook(book3);
            _db.PostBook(book4);
            _db.PostBook(book5);
        
            _db.PostAuthor(author1);
            _db.PostAuthor(author2);
            _db.PostAuthor(author3);
            _db.PostAuthor(author4);

            _db.PostEntry(book1,author1);
            _db.PostEntry(book2,author1);
            _db.PostEntry(book3,author2);
            _db.PostEntry(book4,author4);
            _db.PostEntry(book5,author3);
            _db.PostEntry(book5,author4);
            
            return Ok(ResponseHandler.GetAppResponse(type, book1));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
}
