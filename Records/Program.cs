using System;

namespace Records
{
    class Student
    { 
        public Student()
        {
            Code = 1;
        }
        public int Age { get; set; }

        // Применение модификатора init означает, что для установки значения свойства можно использовать
        // только инициализатор либо конструктор. После инициализации свойства оно доступно только для
        // чтения, и соответственно, в дальнейшем его значение изменить нельзя. 
        public int Code { get; init; } 

        public string Name { get; set; }
        public string Surname { get; set; }
        public double Average { get; set; }
    }

    public record Team(string Name, string City);
    public /*readonly*/ record struct Movie(string Name, string Director);

    public record Club
    {
        public string Name { get; init; }
        public string City { get; init; }
        public Club()
        {
            Name = "Черноморец";
            City = "Одесса";
        }
        public Club(string name, string city)
        {
            Name = name;
            City = city;
        }
        public void Deconstruct(out string clubName, out string clubCity) => (clubName, clubCity) = (Name, City);
    }

    public abstract record Person(string FirstName, string LastName);
    public record Teacher(string FirstName, string LastName, int Grade)
        : Person(FirstName, LastName);

    class Program
    {
        static void Main(string[] args)
        {
            Student st1 = new Student {Name = "Петр", Surname = "Иванченко", Age = 20, Average = 11.5, Code = 10};
            Console.WriteLine($"{st1.Code}\t{st1.Name}\t{st1.Surname}\t{st1.Age}\t{st1.Average}");
            st1.Average = 12;
            //st1.Code = 20; // только для чтения

            Club club1 = new Club { Name = "Динамо", City = "Киев" };
            Console.WriteLine($"{club1.Name} - {club1.City}");
            // club1.Name = "Черноморец"; // только для чтения
            // club1.City = "Одесса"; // только для чтения

            Student st2 = new Student { Name = "Петр", Surname = "Иванченко", Age = 20, Average = 11.5, Code = 10 };
            Club club2 = new Club { Name = "Динамо", City = "Киев" };

            Console.WriteLine(st1.Equals(st2)); // false

            // При определении record компилятор генерирует метод Equals() для сравнения с другим объектом.
            // При этом сравнение двух records производится на основе их значений. 
            Console.WriteLine(club1.Equals(club2)); // true

            Console.WriteLine(st1 == st2);    // false
            Console.WriteLine(club1 == club2); // true

            Club club3 = new Club() { Name = "Реал", City = "Мадрид" };
            Club club4 = club3 with { Name = "Атлетико" };
            Console.WriteLine($"{club3.Name} - {club3.City}");
            Console.WriteLine($"{club4.Name} - {club4.City}");

            Club club5 = new Club("Шахтер", "Донецк");
            Console.WriteLine(club5.Name);
            Console.WriteLine(club5.City);

            var (clubName, clubCity) = club5;

            Console.WriteLine(clubName);    
            Console.WriteLine(clubCity);

            Team team = new Team("ПСЖ", "Париж"); 
            Console.WriteLine(typeof(Team).BaseType);
            Console.WriteLine(team.Name);
            Console.WriteLine(team.City);
            //team.Name = "Бавария"; // только для чтения
            //team.City = "Мюнхен"; // только для чтения

            var (teamName, teamCity) = team;

            Console.WriteLine(teamName);
            Console.WriteLine(teamCity);

            Movie movie = new Movie("Титаник", "Джеймс Кэмерон");
            Console.WriteLine(typeof(Movie).BaseType);
            Console.WriteLine(movie.Name);
            Console.WriteLine(movie.Director);
            movie.Name = "Аватар";

            var (movieName, movieDirector) = movie;

            Console.WriteLine(movieName);
            Console.WriteLine(movieDirector);

            Person teacher = new Teacher("Евгений", "Полин", 1);
            Console.WriteLine(teacher); // ToString

        }
    }
}
