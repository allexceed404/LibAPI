using libraryAPI.EfCore;
namespace libraryAPI.Models;
public class DbHelper
{
    private EF_DataContext _context;
    public DbHelper(EF_DataContext context)
    {
        _context = context;
    }
    public List<RelationModel> GetEntries()
    {
        //GET
        List<RelationModel> response = new List<RelationModel>();
        List<Relation> relationList = _context.Relations.ToList();
        foreach(Relation rel in relationList){
            var book = _context.Books.Where(
                b=>b.id==rel.bookid
            ).FirstOrDefault();
            var author = _context.Authors.Where(
                a=>a.id==rel.authorid
            ).FirstOrDefault();
            response.Add(new RelationModel(){
                book_name = book.name,
                book_date_of_first_publication  = book.date_of_first_publication ,
                author_name = author.name,
                author_date_of_birth = author.date_of_birth
            });
        }
        return response;
    }
    public void PostEntry(RelationModel relationModel)
    {
        //POST
        var book = _context.Books.Where(
            b=>b.name==relationModel.book_name&&
            b.date_of_first_publication==relationModel.book_date_of_first_publication
        ).FirstOrDefault();
        if(book==null){
            book = new Book();
            book.name=relationModel.book_name;
            book.date_of_first_publication=relationModel.book_date_of_first_publication;
            _context.Books.Add(book);
            _context.SaveChanges();
            
            book = _context.Books.Where(
                b=>b.name==relationModel.book_name&&
                b.date_of_first_publication==relationModel.book_date_of_first_publication
            ).FirstOrDefault();
        }

        var author = _context.Authors.Where(
            a=>a.name==relationModel.author_name&&
            a.date_of_birth==relationModel.author_date_of_birth
        ).FirstOrDefault();
        if(author==null){
            author = new Author();
            author.name = relationModel.author_name;
            author.date_of_birth = relationModel.author_date_of_birth;
            _context.Authors.Add(author);
            _context.SaveChanges();

            author = _context.Authors.Where(
                a=>a.name==relationModel.author_name&&
                a.date_of_birth==relationModel.author_date_of_birth
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
    public void DeleteEntry(RelationModel relationModel)
    {
        //DELETE
        var book = _context.Books.Where(
            b=>b.name==relationModel.book_name&&
            b.date_of_first_publication==relationModel.book_date_of_first_publication
        ).FirstOrDefault();

        var author = _context.Authors.Where(
            a=>a.name==relationModel.author_name&&
            a.date_of_birth==relationModel.author_date_of_birth
        ).FirstOrDefault();
        if(book!=null && author!=null){
            var relation = _context.Relations.Where(
                r=>r.bookid==book.id&&
                r.authorid==author.id
            ).FirstOrDefault();
            if(relation!=null){
                _context.Relations.Remove(relation);
                _context.SaveChanges();

                relation = _context.Relations.Where(
                    r=>r.bookid==book.id
                ).FirstOrDefault();
                if(relation==null){
                    _context.Books.Remove(book);
                    _context.SaveChanges();
                }

                relation = _context.Relations.Where(
                    r=>r.authorid==author.id
                ).FirstOrDefault();
                if(relation==null){
                    _context.Authors.Remove(author);
                    _context.SaveChanges();
                }
            }
        }
    }
    public List<BookModel> GetBooks()
    {
        //GET
        List<BookModel> response = new List<BookModel>();
        List<Book> bookList = _context.Books.ToList();
        foreach(Book book in bookList){
            List<AuthorModel> authorList = new List<AuthorModel>();
            List<Relation> relationList = _context.Relations.Where(
                r=>r.bookid==book.id
            ).ToList();
            foreach(Relation rel in relationList){
                var author = _context.Authors.Where(
                    a=>a.id==rel.authorid
                ).FirstOrDefault();
                authorList.Add(new AuthorModel(){
                    name = author.name,
                    date_of_birth = author.date_of_birth,
                    country = author.country
                });
            }
            response.Add(new BookModel(){
                isbn = book.isbn,
                name = book.name,
                date_of_first_publication = book.date_of_first_publication,
                edition = book.edition,
                publisher = book.publisher,
                original_language = book.original_language,
                authors = authorList
            });
        }
        return response;
    }
    public List<BookModel> SearchBook(BookModel bookModel)
    {
        //GET-SEARCH
        List<BookModel> response = new List<BookModel>();
        List<Book> bookList = _context.Books.Where(
            b=>b.name==bookModel.name
        ).ToList();
        foreach(Book book in bookList){
            List<AuthorModel> authorList = new List<AuthorModel>();
            List<Relation> relationList = _context.Relations.Where(
                r=>r.bookid==book.id
            ).ToList();
            foreach(Relation rel in relationList){
                var author = _context.Authors.Where(
                    a=>a.id==rel.authorid
                ).FirstOrDefault();
                authorList.Add(new AuthorModel(){
                    name = author.name,
                    date_of_birth = author.date_of_birth,
                    country = author.country
                });
            }
            response.Add(new BookModel(){
                isbn = book.isbn,
                name = book.name,
                date_of_first_publication = book.date_of_first_publication,
                edition = book.edition,
                publisher = book.publisher,
                original_language = book.original_language,
                authors = authorList
            });
        }
        return response;
    }
    public void PostBook(BookModel bookModel)
    {
        var book = _context.Books.Where(
            b=>b.name==bookModel.name&&
            b.date_of_first_publication==bookModel.date_of_first_publication
        ).FirstOrDefault();
        if(book==null){
            //POST
            book = new Book(){
                isbn = bookModel.isbn,
                name = bookModel.name,
                date_of_first_publication = bookModel.date_of_first_publication,
                edition = bookModel.edition,
                publisher = bookModel.publisher,
                original_language = bookModel.original_language
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            foreach(AuthorModel author in bookModel.authors)
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
                RelationModel relation = new RelationModel(){
                    book_name = bookModel.name,
                    book_date_of_first_publication = bookModel.date_of_first_publication,
                    author_name = author.name,
                    author_date_of_birth = author.date_of_birth
                };
                PostEntry(relation);
            }
        }
        else{
            //PUT
            book.edition = bookModel.edition;
            book.publisher = bookModel.publisher;
            book.original_language = bookModel.original_language;
            _context.SaveChanges();
            foreach(AuthorModel author in bookModel.authors)
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
                RelationModel relation = new RelationModel(){
                    book_name = bookModel.name,
                    book_date_of_first_publication = bookModel.date_of_first_publication,
                    author_name = author.name,
                    author_date_of_birth = author.date_of_birth
                };
                PostEntry(relation);
            }
        }
    }
    public void DeleteBook(BookModel bookModel)
    {
        //DELETE
        var book = _context.Books.Where(
            b=>b.name==bookModel.name&&
            b.date_of_first_publication==bookModel.date_of_first_publication
        ).FirstOrDefault();
        if(book!=null){
            _context.Remove(book);
            _context.SaveChanges();
        }
    }
    public List<AuthorModel> GetAuthors()
    {
        //GET
        List<AuthorModel> response = new List<AuthorModel>();
        List<Author> authorList = _context.Authors.ToList();
        foreach(Author author in authorList){
            List<BookModel> bookList = new List<BookModel>();
            List<Relation> relationList = _context.Relations.Where(
                r=>r.authorid==author.id
            ).ToList();
            foreach(Relation rel in relationList){
                var book = _context.Books.Where(
                    b=>b.id==rel.bookid
                ).FirstOrDefault();
                bookList.Add(new BookModel(){
                    isbn = book.isbn,
                    name = book.name,
                    date_of_first_publication = book.date_of_first_publication,
                    edition = book.edition,
                    publisher = book.publisher,
                    original_language = book.original_language
                });
            }
            response.Add(new AuthorModel(){
                name = author.name,
                date_of_birth = author.date_of_birth,
                country = author.country,
                books = bookList
            });
        }
        return response;
    }
    public List<AuthorModel> SearchAuthor(AuthorModel authorModel)
    {
        //GET-SEARCH
        List<AuthorModel> response = new List<AuthorModel>();
        List<Author> authorList = _context.Authors.Where(
            a=>a.name==authorModel.name
        ).ToList();
        foreach(Author author in authorList){
            List<BookModel> bookList = new List<BookModel>();
            List<Relation> relationList = _context.Relations.Where(
                r=>r.authorid==author.id
            ).ToList();
            foreach(Relation rel in relationList){
                var book = _context.Books.Where(
                    b=>b.id==rel.bookid
                ).FirstOrDefault();
                bookList.Add(new BookModel(){
                    isbn = book.isbn,
                    name = book.name,
                    date_of_first_publication = book.date_of_first_publication,
                    edition = book.edition,
                    publisher = book.publisher,
                    original_language = book.original_language
                });
            }
            response.Add(new AuthorModel(){
                name = author.name,
                date_of_birth = author.date_of_birth,
                country = author.country,
                books = bookList
            });
        }
        return response;
    }
    public void PostAuthor(AuthorModel authorModel)
    {
        var author = _context.Authors.Where(
            a=>a.name==authorModel.name&&
            a.date_of_birth==authorModel.date_of_birth
        ).FirstOrDefault();
        if(author==null){
            //POST
            author = new Author(){
                name = authorModel.name,
                date_of_birth = authorModel.date_of_birth,
                country = authorModel.country
            };
            _context.Authors.Add(author);
            _context.SaveChanges();
            foreach(BookModel book in authorModel.books)
            {
                var addBook = _context.Books.Where(
                    b=>b.name==book.name&&
                    b.date_of_first_publication==book.date_of_first_publication
                ).FirstOrDefault();
                if(addBook==null){
                    addBook = new Book(){
                        isbn = book.isbn,
                        name = book.name,
                        date_of_first_publication = book.date_of_first_publication,
                        edition = book.edition,
                        publisher = book.publisher,
                        original_language = book.original_language
                    };
                    _context.Books.Add(addBook);
                    _context.SaveChanges();
                }
                RelationModel relation = new RelationModel(){
                    book_name=book.name,
                    book_date_of_first_publication=book.date_of_first_publication,
                    author_name=authorModel.name,
                    author_date_of_birth=authorModel.date_of_birth
                };
                PostEntry(relation);
            }
        }
        else{
            //PUT
            author.country=authorModel.country;
            _context.SaveChanges();
            foreach(BookModel book in authorModel.books)
            {
                var addBook = _context.Books.Where(
                    b=>b.name==book.name&&
                    b.date_of_first_publication==book.date_of_first_publication
                ).FirstOrDefault();
                if(addBook==null){
                    addBook = new Book(){
                        isbn = book.isbn,
                        name = book.name,
                        date_of_first_publication = book.date_of_first_publication,
                        edition = book.edition,
                        publisher = book.publisher,
                        original_language = book.original_language
                    };
                    _context.Books.Add(addBook);
                    _context.SaveChanges();
                }
                RelationModel relation = new RelationModel(){
                    book_name=book.name,
                    book_date_of_first_publication=book.date_of_first_publication,
                    author_name=authorModel.name,
                    author_date_of_birth=authorModel.date_of_birth
                };
                PostEntry(relation);
            }
        }
    }
    public void DeleteAuthor(AuthorModel authorModel)
    {
        //DELETE
        var author = _context.Authors.Where(
            row=>row.name==authorModel.name&&
            row.date_of_birth==authorModel.date_of_birth
        ).FirstOrDefault();
        if(author!=null){
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}