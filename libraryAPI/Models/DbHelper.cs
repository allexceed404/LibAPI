using libraryAPI.EfCore;
namespace libraryAPI.Models;
public class DbHelper
{
    private EF_DataContext _context;
    public DbHelper(EF_DataContext context)
    {
        _context = context;
    }
    public List<Book> GetEntries()
    {
        //GET
        List<Book> response = new List<Book>();
        List<Book> bookList = _context.Books.ToList();
        foreach(Book book in bookList){
            List<Author> authorList = new List<Author>();
            List<Relation> relationList = _context.Relations.Where(
                r=>r.bookid==book.id
            ).ToList();
            foreach(Relation rel in relationList){
                var author = _context.Authors.Where(
                    a=>a.id==rel.authorid
                ).FirstOrDefault();
                authorList.Add(author);
            }
            book.authors = authorList;
            response.Add(book);
        }
        return response;
    }
    public void PostEntry(Book bookIn, Author authorIn)
    {
        //POST
        var book = _context.Books.Where(
            b=>b.name==bookIn.name&&
            b.date_of_first_publication==bookIn.date_of_first_publication
        ).FirstOrDefault();
        if(book==null){
            _context.Books.Add(bookIn);
            _context.SaveChanges();
            
            book = _context.Books.Where(
                b=>b.name==bookIn.name&&
                b.date_of_first_publication==bookIn.date_of_first_publication
            ).FirstOrDefault();
        }

        var author = _context.Authors.Where(
            a=>a.name==authorIn.name&&
            a.date_of_birth==authorIn.date_of_birth
        ).FirstOrDefault();
        if(author==null){
            _context.Authors.Add(authorIn);
            _context.SaveChanges();

            author = _context.Authors.Where(
                a=>a.name==authorIn.name&&
                a.date_of_birth==authorIn.date_of_birth
            ).FirstOrDefault();
        }
        

        var relation = _context.Relations.Where(
            r=>r.bookid==book.id&&
            r.authorid==author.id
        ).FirstOrDefault();
        if(relation==null){
            relation = new Relation(){
                bookid = book.id,
                authorid = author.id
            };
            _context.Relations.Add(relation);
            _context.SaveChanges();
        }
    }
    public void DeleteEntry(Book bookDel, Author authorDel)
    {
        //DELETE
        var book = _context.Books.Where(
            b=>b.name==bookDel.name&&
            b.date_of_first_publication==bookDel.date_of_first_publication
        ).FirstOrDefault();

        var author = _context.Authors.Where(
            a=>a.name==authorDel.name&&
            a.date_of_birth==authorDel.date_of_birth
        ).FirstOrDefault();
        if(book!=null && author!=null){
            var relationDel = _context.Relations.Where(
                r=>r.bookid==book.id&&
                r.authorid==author.id
            ).FirstOrDefault();
            if(relationDel!=null){
                _context.Relations.Remove(relationDel);
                _context.SaveChanges();

                relationDel = _context.Relations.Where(
                    r=>r.bookid==book.id
                ).FirstOrDefault();
                if(relationDel==null){
                    _context.Books.Remove(book);
                    _context.SaveChanges();
                }

                relationDel = _context.Relations.Where(
                    r=>r.authorid==author.id
                ).FirstOrDefault();
                if(relationDel==null){
                    _context.Authors.Remove(author);
                    _context.SaveChanges();
                }
            }
        }
    }
    public List<Book> GetBooks()
    {
        //GET
        List<Book> response = new List<Book>();
        List<Book> bookList = _context.Books.ToList();
        foreach(Book book in bookList){
            List<Author> authorList = new List<Author>();
            List<Relation> relationList = _context.Relations.Where(
                r=>r.bookid==book.id
            ).ToList();
            foreach(Relation rel in relationList){
                var author = _context.Authors.Where(
                    a=>a.id==rel.authorid
                ).FirstOrDefault();
                authorList.Add(author);
            }
            book.authors = authorList;
            response.Add(book);
        }
        return response;
    }
    public List<Book> SearchBook(Book bookSearch)
    {
        //GET-SEARCH
        List<Book> response = new List<Book>();
        List<Book> bookList = _context.Books.Where(
            b=>b.name==bookSearch.name
        ).ToList();
        foreach(Book book in bookList){
            List<Author> authorList = new List<Author>();
            List<Relation> relationList = _context.Relations.Where(
                r=>r.bookid==book.id
            ).ToList();
            foreach(Relation rel in relationList){
                var author = _context.Authors.Where(
                    a=>a.id==rel.authorid
                ).FirstOrDefault();
                authorList.Add(author);
            }
            book.authors = authorList;
            response.Add(book);
        }
        return response;
    }
    public void PostBook(Book bookIn)
    {
        var book = _context.Books.Where(
            b=>b.name==bookIn.name&&
            b.date_of_first_publication==bookIn.date_of_first_publication
        ).FirstOrDefault();
        if(book==null){
            //POST
            book = new Book(){
                isbn = bookIn.isbn,
                name = bookIn.name,
                date_of_first_publication = bookIn.date_of_first_publication,
                edition = bookIn.edition,
                publisher = bookIn.publisher,
                original_language = bookIn.original_language
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            foreach(Author author in bookIn.authors)
            {
                var addAuthor = _context.Authors.Where(
                    a=>a.name==author.name&&
                    a.date_of_birth==author.date_of_birth
                ).FirstOrDefault();
                if(addAuthor==null){
                    _context.Authors.Add(author);
                    _context.SaveChanges();
                }
                PostEntry(bookIn, author);
            }
        }
        else{
            //PUT
            if(bookIn.edition!=0) book.edition = bookIn.edition;
            if(bookIn.publisher!=null) book.publisher = bookIn.publisher;
            if(bookIn.original_language!=null) book.original_language = bookIn.original_language;
            _context.SaveChanges();
            foreach(Author author in bookIn.authors)
            {
                var addAuthor = _context.Authors.Where(
                    a=>a.name==author.name&&
                    a.date_of_birth==author.date_of_birth
                ).FirstOrDefault();
                if(addAuthor==null){
                    addAuthor = new Author(){
                        name = author.name,
                        date_of_birth = author.date_of_birth,
                        country = author.country
                    };
                    _context.Authors.Add(addAuthor);
                    _context.SaveChanges();
                }
                PostEntry(bookIn, author);
            }
        }
    }
    public void DeleteBook(Book bookDel)
    {
        //DELETE
        var book = _context.Books.Where(
            b=>b.name==bookDel.name&&
            b.date_of_first_publication==bookDel.date_of_first_publication
        ).FirstOrDefault();
        if(book!=null){
            _context.Remove(book);
            _context.SaveChanges();
        }
    }
    public List<Author> GetAuthors()
    {
        //GET
        List<Author> response = new List<Author>();
        List<Author> authorList = _context.Authors.ToList();
        foreach(Author author in authorList){
            List<Book> bookList = new List<Book>();
            List<Relation> relationList = _context.Relations.Where(
                r=>r.authorid==author.id
            ).ToList();
            foreach(Relation rel in relationList){
                var book = _context.Books.Where(
                    b=>b.id==rel.bookid
                ).FirstOrDefault();
                bookList.Add(book);
            }
            author.books = bookList;
            response.Add(author);
        }
        return response;
    }
    public List<Author> SearchAuthor(Author authorSearch)
    {
        //GET-SEARCH
        List<Author> response = new List<Author>();
        List<Author> authorList = _context.Authors.Where(
            a=>a.name==authorSearch.name
        ).ToList();
        foreach(Author author in authorList){
            List<Book> bookList = new List<Book>();
            List<Relation> relationList = _context.Relations.Where(
                r=>r.authorid==author.id
            ).ToList();
            foreach(Relation rel in relationList){
                var book = _context.Books.Where(
                    b=>b.id==rel.bookid
                ).FirstOrDefault();
                bookList.Add(book);
            }
            author.books = bookList;
            response.Add(author);
        }
        return response;
    }
    public void PostAuthor(Author authorIn)
    {
        var author = _context.Authors.Where(
            a=>a.name==authorIn.name&&
            a.date_of_birth==authorIn.date_of_birth
        ).FirstOrDefault();
        if(author==null){
            //POST
            author = new Author(){
                name = authorIn.name,
                date_of_birth = authorIn.date_of_birth,
                country = authorIn.country
            };
            _context.Authors.Add(author);
            _context.SaveChanges();
            foreach(Book book in authorIn.books)
            {
                var addBook = _context.Books.Where(
                    b=>b.name==book.name&&
                    b.date_of_first_publication==book.date_of_first_publication
                ).FirstOrDefault();
                if(addBook==null){
                    _context.Books.Add(book);
                    _context.SaveChanges();
                }
                PostEntry(book, authorIn);
            }
        }
        else{
            //PUT
            if(authorIn.country!=null) author.country=authorIn.country;
            _context.SaveChanges();
            foreach(Book book in authorIn.books)
            {
                var addBook = _context.Books.Where(
                    b=>b.name==book.name&&
                    b.date_of_first_publication==book.date_of_first_publication
                ).FirstOrDefault();
                if(addBook==null){
                    _context.Books.Add(book);
                    _context.SaveChanges();
                }
                PostEntry(book, authorIn);
            }
        }
    }
    public void DeleteAuthor(Author authorDel)
    {
        //DELETE
        var author = _context.Authors.Where(
            row=>row.name==authorDel.name&&
            row.date_of_birth==authorDel.date_of_birth
        ).FirstOrDefault();
        if(author!=null){
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}