// 
using Dapper;
using Microsoft.Data.SqlClient;

SqlConnection db = new(@"Server=(localdb)\MSSQLLocalDB;Database=Library;Integrated Security=True;Trust Server Certificate=True;");

#region Execute Scalar

//var result = db.ExecuteScalar<int>("SELECT COUNT(*) FROM Books;");
//Console.WriteLine(result);

//Console.WriteLine(db.ExecuteScalar<float>("SELECT AVG(Pages) FROM Books;"));

#endregion

#region Single Row Query - QueryFirst(), QueryFirstOrDefault(), QuerySingle(),  QuerySingleOrDefault() 
//var book = db.QueryFirstOrDefault<Book>("""
//    SELECT *
//    FROM Books
//    WHERE Name LIKE @Name
//    """, new { @Name = "%Viwsual%"});
//if(book is not null) Console.WriteLine(book);
//else Console.WriteLine("Book not found");

//var book2 = db.QuerySingleOrDefault<Book>("""
//    SELECT *
//    FROM Books
//    WHERE Name LIKE @Name
//    """, new { @Name = "%GroupWise%" });
//if (book2 is not null) Console.WriteLine(book2);
//else Console.WriteLine("Book not found");

#endregion

#region Querying Multiple Rows - Query()
//var books = db.Query<Book>("SELECT * FROM Books WHERE Pages > 300").ToList();
//books.ForEach(Console.WriteLine);
#endregion

#region Querying Multiple Results - QueryMultiple(), Read(), ReadFirst(), ReadFirstOrDefault(), ReadSingle(),  ReadSingleOrDefault() 
//var sqlQuery = """
//    SELECT * FROM Authors WHERE FirstName = @FirstName;
//    SELECT * FROM Books WHERE Id > @Id;
//    """;
//var results = db.QueryMultiple(sqlQuery, new { @FirstName = "Mark", @Id = 5 });

//var authors = results.Read<Author>().ToList();
//var books = results.Read<Book>().ToList();

//authors.ForEach(Console.WriteLine);
//Console.WriteLine();
//books.ForEach(Console.WriteLine);
#endregion


#region Querying Specific Columns
//var sqlQuery = "SELECT Id, FirstName FROM Authors";
//var authors = db.Query<Author>(sqlQuery).ToList();

//authors.ForEach(Console.WriteLine);

//foreach (var item in authors)
//{
//    Console.WriteLine($"{item.Id} {item.FirstName}");
//}

#endregion

#region One To Many

var sqlQuery = """
    SELECT *
    FROM Students AS S 
    CROSS JOIN Groups AS G 
    WHERE S.Id_Group = G.Id
    """;

var students = db.Query<Student, Group, Student>(sqlQuery,
    (s, g) =>
    {
        s.Group = g;
        return s;
    }).ToList();

foreach (var student in students)
{
    Console.WriteLine(student);
}

#endregion