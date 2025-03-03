using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

using PubContext _context = new();

var nyaAuthors = new List<Author>
{
        new Author { FirstName = "Lars" , LastName = "Larsson" },
        new Author { FirstName = "Sven" , LastName = "Svensson" },
        new Author { FirstName = "Karl" , LastName = "Karlsson" },
        new Author { FirstName = "Anders" , LastName = "Andersson" }
};

GetAuthors();
//QueryFilters();
//FindIt();
//AddSomeMoreAuthors();
//SkipAndTakeAuthors();
Console.WriteLine("___________________");
//SortAuthors();
//QueryAggregate();
//RetrieveAndUpdateAuthor();
//RetrieveAndUpdateMultipleAuthor();
//VariousOperations();
//DeleteAnAuthor();
InsertMultipleAuthors();
InsertMultipleAuthorsPassedIn(nyaAuthors);

void InsertMultipleAuthorsPassedIn(List<Author> nyaAuthors)
{
    _context.Authors.AddRange(nyaAuthors);
    _context.SaveChanges();
}

void InsertMultipleAuthors()
{
    var newAuthors = new List<Author>
    {
        new Author { FirstName = "Lars" , LastName = "Larsson" },
        new Author { FirstName = "Sven" , LastName = "Svensson" },
        new Author { FirstName = "Karl" , LastName = "Karlsson" },
        new Author { FirstName = "Anders" , LastName = "Andersson" }
    };
    _context.Authors.AddRange(newAuthors);
    _context.SaveChanges();
}

GetAuthors();

void DeleteAnAuthor()
{
    var extraOO = _context.Authors.Find(12);

    if (extraOO !=null)
    {
        _context.Authors.Remove(extraOO);
        _context.SaveChanges();
    }
}

void VariousOperations()
{
    var author = _context.Authors.Find(1); // Detta kommer att returnera
    author.LastName = "Larsson";

    var newAuthor = new Author { LastName = "Olof" , FirstName = "Olofsson" };
    _context.Authors.Add(newAuthor);

    _context.SaveChanges();
}

void RetrieveAndUpdateMultipleAuthor()
{
    var LarssonAuthors = _context.Authors
        .Where(a => a.LastName == "Larsson").ToList();
    foreach (var author in LarssonAuthors)
    {
        author.LastName = "Karlsson";
    }
}

void RetrieveAndUpdateAuthor()
{
    var author = _context.Authors
        .FirstOrDefault(a => a.FirstName == "Sven" && a.LastName == "Svensson");

    if (author != null)
    {
        author.LastName = "Karlsson";
        _context.SaveChanges();
    }
}

void QueryAggregate()
{
    var author = _context.Authors
        .OrderByDescending(a => a.FirstName)
        .FirstOrDefault(a => a.LastName == "Gille");
}

void SortAuthors()
{
    //   var authorsByLastName = _context.Authors
    //       .OrderBy(a => a.LastName)
    //       .ThenBy(a => a.FirstName).ToList();
    //   authorsByLastName.ForEach(a => Console.WriteLine(a.LastName + " " + a.FirstName));


    var authorsDescending = _context.Authors
        .OrderByDescending(a => a.LastName)
        .ThenByDescending(a => a.FirstName).ToList();
    Console.WriteLine("*Descending Last and First name*");
    authorsDescending.ForEach(a => Console.WriteLine(a.LastName + " " + a.FirstName));
}

void SkipAndTakeAuthors()
{
    var groupSize = 2;
    for (var i = 0; i < 5; i++)
    {
        var authors = _context.Authors.Skip(i * groupSize).Take(groupSize).ToList();
        Console.WriteLine("============");
        Console.WriteLine("Group" + (i + 1));
        Console.WriteLine("      ");
        foreach (var author in authors)
        {
            Console.WriteLine(author.FirstName + " " + author.LastName);
        }
    }
}

void AddSomeMoreAuthors()
{
    _context.Authors.Add(new Author { FirstName = "Sven", LastName = "Svensson" });
    _context.Authors.Add(new Author { FirstName = "Anders", LastName = "Andersson" });
    _context.Authors.Add(new Author { FirstName = "Erik", LastName = "Eriksson" });
    _context.Authors.Add(new Author { FirstName = "Lars", LastName = "Larsson" });

    _context.SaveChanges();
}

void FindIt()
{
    var authorIdTwo = _context.Authors.Find(2);
}

void QueryFilters()
{
    //var firstname = "William";
    //var authors = _context.Authors.Where(a => a.FirstName == firstname).ToList();
    var filter = "G%";
    var authors = _context.Authors.Where(a => EF.Functions.Like(a.LastName, filter)).ToList();
}

void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
    }
}