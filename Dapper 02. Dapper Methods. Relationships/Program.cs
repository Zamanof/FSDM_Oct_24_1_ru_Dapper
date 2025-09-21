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

//var sqlQuery = """
//    SELECT *
//    FROM Students AS S 
//    JOIN Groups AS G 
//    ON S.Id_Group = G.Id
//    """;

//var students = db.Query<Student, Group, Student>(sqlQuery,
//    (s, g) =>
//    {
//        s.Group = g;
//        return s;
//    }).ToList();

//foreach (var student in students)
//{
//    Console.WriteLine(student);
//}

//var groupDict = new Dictionary<int, Group>();

//var groups = db.Query<Student, Group, Group>(sqlQuery,
//    (s, g) =>
//    {
//        if (!groupDict.TryGetValue(g.Id, out var existingGroup))
//        {
//            existingGroup = g;
//            existingGroup.Students = new List<Student>();
//            groupDict.Add(g.Id, existingGroup);
//        }
//        existingGroup.Students.Add(s);
//        return existingGroup;

//    }).Distinct().ToList();

//foreach (var group in groups)
//{
//    Console.WriteLine(group);
//    foreach (var student in group.Students)
//    {
//        Console.WriteLine($"    {student.FirstName} {student.LastName}");
//    }
//}


#endregion

#region Many To Many
var sqlQuery = """
    SELECT *
    FROM Students AS S
    JOIN S_Cards AS SC ON S.Id = SC.Id_Student
    JOIN Books AS B ON B.Id = SC.Id_Book
    """;
var results = db.Query<Student, Book, Student>(sqlQuery,
    (s, b) =>
    {
        s.Books.Add(b);
        return s;
    }
    );
foreach (var r in results)
{
    Console.WriteLine($"{r.FirstName} {r.LastName}");
    foreach (var b in r.Books)
    {
        Console.WriteLine($"        {b.Name}");
    }
}

#endregion
