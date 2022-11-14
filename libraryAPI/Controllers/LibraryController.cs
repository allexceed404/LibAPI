using Microsoft.AspNetCore.Mvc;
using libraryAPI.Models;
using libraryAPI.EfCore;
namespace libraryAPI.Controllers;

[ApiController]
[Route("api/library")]
public class LibraryController : ControllerBase
{
    private readonly DbHelper _db;
    public LibraryController(EF_DataContext eF_DataContext)
    {
        _db = new DbHelper(eF_DataContext);
    }

    [HttpGet]
    public IActionResult Get()
    {
        ResponseType type = ResponseType.Success;
        try
        {
            IEnumerable<Book> data = _db.GetEntries();
            if(!data.Any())
            {
                type = ResponseType.NotFound;
            }
            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex)
        {
            type = ResponseType.Faliure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }

    [HttpPost]
    public IActionResult Post(Tuple<Book, Author> input)
    {
        Book book = (Book)input.Item1;
        Author author = (Author)input.Item2;
        try
        {
            ResponseType type = ResponseType.Success;
            _db.PostEntry(book, author);
            return Ok(ResponseHandler.GetAppResponse(type, "Added Successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpDelete]
    public IActionResult Delete(Tuple<Book, Author> input)
    {
        Book book = (Book)input.Item1;
        Author author = (Author)input.Item2;
        try
        {
            ResponseType type = ResponseType.Success;
            _db.DeleteEntry(book, author);
            return Ok(ResponseHandler.GetAppResponse(type, "Deleted Successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
}

[ApiController]
[Route("api/library/books")]
public class BookController : ControllerBase
{
    private readonly DbHelper _db;
    public BookController(EF_DataContext eF_DataContext)
    {
        _db = new DbHelper(eF_DataContext);
    }

    [HttpGet]
    public IActionResult Get()
    {
        ResponseType type = ResponseType.Success;
        try
        {
            IEnumerable<Book> data = _db.GetBooks();
            if(!data.Any())
            {
                type = ResponseType.NotFound;
            }
            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex)
        {
            type = ResponseType.Faliure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpGet("search")]
    public IActionResult Search(Book bookSearch)
    {
        ResponseType type = ResponseType.Success;
        try
        {
            IEnumerable<Book> data = _db.SearchBook(bookSearch);
            if(!data.Any())
            {
                type = ResponseType.NotFound;
            }
            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex)
        {
            type = ResponseType.Faliure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpPost]
    public IActionResult Post(Book bookIn)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.PostBook(bookIn);
            return Ok(ResponseHandler.GetAppResponse(type, "Added Successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpPut]
    public IActionResult Put(Book bookUpdate)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.PostBook(bookUpdate);
            return Ok(ResponseHandler.GetAppResponse(type, "Updated Successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpDelete]
    public IActionResult Delete(Book bookDel)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.DeleteBook(bookDel);
            return Ok(ResponseHandler.GetAppResponse(type, "Deleted Successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
}

[ApiController]
[Route("api/library/authors")]
public class AuthorController : ControllerBase
{
    private readonly DbHelper _db;
    public AuthorController(EF_DataContext eF_DataContext)
    {
        _db = new DbHelper(eF_DataContext);
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        ResponseType type = ResponseType.Success;
        try
        {
            IEnumerable<Author> data = _db.GetAuthors();
            if(!data.Any())
            {
                type = ResponseType.NotFound;
            }
            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex)
        {
            type = ResponseType.Faliure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpGet("search")]
    public IActionResult Search(Author authorSearch)
    {
        ResponseType type = ResponseType.Success;
        try
        {
            IEnumerable<Author> data = _db.SearchAuthor(authorSearch);
            if(!data.Any())
            {
                type = ResponseType.NotFound;
            }
            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex)
        {
            type = ResponseType.Faliure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpPost]
    public IActionResult Post(Author authorIn)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.PostAuthor(authorIn);
            return Ok(ResponseHandler.GetAppResponse(type, "Added Successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpPut]
    public IActionResult Put(Author authorUpdate)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.PostAuthor(authorUpdate);
            return Ok(ResponseHandler.GetAppResponse(type, "Updated Successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpDelete]
    public IActionResult Delete(Author authorDel)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.DeleteAuthor(authorDel);
            return Ok(ResponseHandler.GetAppResponse(type, "Deleted Successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
}