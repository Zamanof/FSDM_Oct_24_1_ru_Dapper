// Dapper ORM

using Dapper_01._Dapper_ORM;
using Microsoft.Data.SqlClient;

string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Authors;Integrated Security=True;Trust Server Certificate=True;";

IAuthorRepository repository = new AuthorRepository(new SqlConnection(), connectionString);

#region AddData
//Author author = new() { FirstName = "Ben", LastName = "Albahari" };
//author = repository.AddAuthor(author);
//Console.WriteLine(author);
#endregion

#region Read Datas
var authors = repository.GetAuthors().ToList();
authors.ForEach(Console.WriteLine);
#endregion

#region Read Data
//var author = repository.GetAuthorById(4);
//Console.WriteLine(author);
#endregion

#region Remove Data
//repository.RemoveAuthor(1);
//var authors = repository.GetAuthors().ToList();
//authors.ForEach(Console.WriteLine);
#endregion