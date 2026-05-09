using System;
using System.Text;

namespace Records
{
    class Student
    {
        public Student()
        {
            Code = 1;
        }
        public int Age { get; set; }

        // Застосування модифікатора init означає, що для встановлення значення властивості можна використовувати
        // лише ініціалізатор або конструктор. Після ініціалізації властивість доступна тільки для
        // читання, і відповідно, надалі її значення змінити не можна. 
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
            Name = "Чорноморець";
            City = "Одеса";
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
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Student st1 = new Student { Name = "Петро", Surname = "Іванченко", Age = 20, Average = 11.5, Code = 10 };
            Console.WriteLine($"{st1.Code}\t{st1.Name}\t{st1.Surname}\t{st1.Age}\t{st1.Average}");
            st1.Average = 12;
            //st1.Code = 20; // тільки для читання

            Club club1 = new Club { Name = "Динамо", City = "Київ" };
            Console.WriteLine($"{club1.Name} - {club1.City}");
            // club1.Name = "Чорноморець"; // тільки для читання
            // club1.City = "Одеса"; // тільки для читання

            Student st2 = new Student { Name = "Петро", Surname = "Іванченко", Age = 20, Average = 11.5, Code = 10 };
            Club club2 = new Club { Name = "Динамо", City = "Київ" };

            Console.WriteLine(st1.Equals(st2)); // false

            // Під час визначення record компілятор генерує метод Equals() для порівняння з іншим об'єктом.
            // При цьому порівняння двох records здійснюється на основі їхніх значень. 
            Console.WriteLine(club1.Equals(club2)); // true

            Console.WriteLine(st1 == st2);    // false
            Console.WriteLine(club1 == club2); // true

            Club club3 = new Club() { Name = "Реал", City = "Мадрид" };
            Club club4 = club3 with { Name = "Атлетіко" };
            Console.WriteLine($"{club3.Name} - {club3.City}");
            Console.WriteLine($"{club4.Name} - {club4.City}");

            Club club5 = new Club("Шахтар", "Донецьк");
            Console.WriteLine(club5.Name);
            Console.WriteLine(club5.City);

            var (clubName, clubCity) = club5;

            Console.WriteLine(clubName);
            Console.WriteLine(clubCity);

            Team team = new Team("ПСЖ", "Париж");
            Console.WriteLine(typeof(Team).BaseType);
            Console.WriteLine(team.Name);
            Console.WriteLine(team.City);
            //team.Name = "Баварія"; // тільки для читання
            //team.City = "Мюнхен"; // тільки для читання

            var (teamName, teamCity) = team;

            Console.WriteLine(teamName);
            Console.WriteLine(teamCity);

            Movie movie = new Movie("Титанік", "Джеймс Кемерон");
            Console.WriteLine(typeof(Movie).BaseType);
            Console.WriteLine(movie.Name);
            Console.WriteLine(movie.Director);
            movie.Name = "Аватар";

            var (movieName, movieDirector) = movie;

            Console.WriteLine(movieName);
            Console.WriteLine(movieDirector);

            Person teacher = new Teacher("Євген", "Полін", 1);
            Console.WriteLine(teacher); // ToString
        }
    }
}