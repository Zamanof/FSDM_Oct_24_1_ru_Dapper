using Dapper;
using System.Data;

namespace Dapper_01._Dapper_ORM;

class AuthorRepository : IAuthorRepository
{
    IDbConnection _db;

    public AuthorRepository(IDbConnection db, string connectionString)
    {
        _db = db;
        _db.ConnectionString = connectionString;
    }

    public Author AddAuthor(Author author)
    {
        var sqlQuery = """
            INSERT INTO Author(FirstName, LastName)
            VALUES (@FirstName, @LastName)
            SELECT CAST(SCOPE_IDENTITY() AS int)
            """;
            
        var id = _db.Query<int>(sqlQuery, new {
        @FirstName = author.FirstName,
        @LastName = author.LastName
        }).FirstOrDefault();

        author.Id = id;
        return author;
    }

    public void AddAuthors(IEnumerable<Author> authors)
    {
        throw new NotImplementedException();
    }

    public Author GetAuthorById(int id)
    {
        var sqlQuery = """
            SELECT *
            FROM Author
            WHERE Id = @id
            """;
        return _db.QueryFirstOrDefault<Author>(sqlQuery, new { @id = id })!;
    }

    public IEnumerable<Author> GetAuthors()
    {
        var sqlQuery = """
            SELECT *
            FROM Author
            """ ;
        return _db.Query<Author>(sqlQuery);
    }

    public void RemoveAuthor(int id)
    {
        var sqlQuery = "DELETE FROM Author WHERE Id=@id";
        _db.Execute(sqlQuery, new { @id = id });
    }

    public void RemoveAuthors(int[] authorIds)
    {
        throw new NotImplementedException();
    }
}
