using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace Drugi_zadatak
{
    internal class Program
    {

        static int Main(string[] args)
        {
            int choice;
            Console.WriteLine("Dobar dan!\n");
            //TRYPARSE UMJEATO PARSE

            var books = new Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)>
            {
                {0, ("Hamlet", "William Shakespeare", new DateTime(2026, 10, 1), new DateTime(2026, 11, 12)) },
                {1, ("Patnje mladog Werthera", "J. W. Goethe", new DateTime(2026, 11, 19), new DateTime()) },
                {2, ("Gavran", "Edgar Allan Poe", new DateTime(2026, 10, 30), new DateTime()) },
                {3, ("Ponos i predrasude", "Jane Austen", new DateTime(2026, 11, 3), new DateTime(2026, 11, 17)) },
                {4, ("No longer human", "Osamu Dazai", new DateTime(2026, 11, 16), new DateTime()) },
                {5, ("1984", "George Orwell", new DateTime(2026, 11, 12), new DateTime(2026, 11, 20)) }
            };

            var nums = books.Keys.ToList();

            var members = new Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)>
            {
                {0, ("Angelina", "Jolie", new DateTime(1975, 6, 4), new List<int>{ 1, 4 }) },
                {1, ("Brad", "Pitt", new DateTime(1963, 12, 18), new List<int>{ 0, 3 }) },
                {2, ("Edward", "Norton", new DateTime(1969, 8, 18), new List<int>{ 5 }) },
                {3, ("Anne", "Bolton", new DateTime(2010, 3, 9), new List<int>{ 2 }) }
            };

            bool repeat = true;

            while (repeat)
            {
                Console.WriteLine("Molimo odaberite željeno:\n");
                Console.WriteLine("1 - Članovi\n2 - Posudbe\n0 - Izlaz iz aplikacije\n");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Zbogom!\n\n");
                        repeat = false;
                        Console.Clear();

                        return 0;

                    case 1:
                        Console.Clear();
                        Console.WriteLine("ČLANOVI\n");
                        MenuMember(members, books);
                        Console.Clear();

                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("POSUDBE\n");
                        MenuBook(members, books);
                        break;

                    default:
                        Console.WriteLine("Krivi unos");
                        Console.Clear();
                        break;
                }
            }
            return 0;
        }

        
        static void MenuMember(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members,
            Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)> books)
        {
            Console.WriteLine("Odaberite:\n");
            Console.WriteLine("1 - Unos novog člana\n2 - Brisanje člana\n3 - Uređivanje člana\n4 - Pregled svih članova\n0 - Povratak na glavni izbornik\n");

            int memChoice = int.Parse(Console.ReadLine());
            Console.Clear();

            switch (memChoice)
            {
                case 0:
                    Console.WriteLine("Vracanje...\n");
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    Console.Clear();

                    break;

                case 1:
                    Console.WriteLine("UNOS NOVOG ČLANA\n");
                    InputNewMember(members);
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    Console.Clear();

                    break;

                case 2:
                    Console.WriteLine("BRISANJE ČLANA\n");
                    DeleteMember(members);
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    Console.Clear();

                    break;

                case 3:
                    Console.WriteLine("UREĐIVANJE ČLANA\n");
                    EditMember(members, books);
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    Console.Clear();

                    break;

                case 4:
                    Console.WriteLine("PREGLED SVIH ČLANOVA\n");
                    PrintMember(members, books);
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    Console.Clear();

                    break;

                default:
                    Console.WriteLine("Krivi unos");
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    Console.Clear();

                    break;
            }
        }

        static void InputNewMember(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members)
        {
            Console.WriteLine("Ime i prezime: ");
            string firstLastTog = Console.ReadLine();
            string[] firstLast = firstLastTog.Split(" ");
            if (firstLast.Length != 2)
            {
                Console.WriteLine("Krivi unos imena\n");
                return;
            }
            Console.WriteLine("Datum rođenja (DD/MM//YYYY): ");
            string date = Console.ReadLine();

            string[] dates = date.Split('/');
            int day = int.Parse(dates[0]);
            int month = int.Parse(dates[1]);
            int year = int.Parse(dates[2]);
            

            DateTime dateOfBirth = new DateTime(year, month, day);
            members.Add(members.Count, (firstLast[0], firstLast[1], dateOfBirth, new List<int>() ));
            Console.WriteLine($"Korisnik {firstLast[0]} {firstLast[1]}, rođen {dateOfBirth.ToString("dd/MM/yyyy")} je uspješno dodan!");
        }

        static void DeleteMember(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members)
        {
            Console.WriteLine("Unesite ime i prezime ili id člana kojeg želite obrisati: ");
            var inputDel = Console.ReadLine();
            int memberId = -1;
            int id;
            if (int.TryParse(inputDel, out id))
            {
                if (members.ContainsKey(id))
                    memberId = id;
            }
            else
            {
                string firstLastTog = inputDel;
                string[] firstLast = inputDel.Split(" ");
                if (firstLast.Length == 2)
                {
                    foreach (var member in members)
                    {
                        if (member.Value.firstName == firstLast[0] && member.Value.lastName == firstLast[1])
                        {
                            memberId = member.Key;
                            break;
                        }
                    }
                }
            }
            if (memberId == -1)
            {
                Console.WriteLine("Krivi upis");
                return;
            }

            bool check = AskDelete(members, memberId);
            if (check)
                members.Remove(memberId);
            else
                Console.WriteLine("Član nije nađen");
            
        }
        static bool AskDelete(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members, int id)
        {
            Console.WriteLine($"Član {members[id].firstName} {members[id].lastName} će biti obrisan.\nŽelite li nastaviti?(Y/N)\n");
            string answer = Console.ReadLine();
            switch (answer)
            {
                case "Y":
                case "y":
                    Console.WriteLine("Član obrisan!\n");
                    return true;

                case "N":
                case "n":
                    Console.WriteLine("Vraćanje na glavni izbornik\n");
                    return false;

                default:
                    Console.WriteLine("Krivi unos.");
                    return false;
            }
        }
        static void EditMember(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members,
            Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)> books)
        {
            Console.WriteLine("Unesite id člana kojeg želite promijeniti: ");
            int idEdit = int.Parse(Console.ReadLine());

            foreach (var member in members)
            {
                if (member.Key == idEdit)
                {
                    ChooseEdit(members, books, idEdit);
                }
            }

        }

        static void ChooseEdit(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members,
            Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)> books, int id)
        {

            Console.WriteLine($"Član {members[id].firstName} {members[id].lastName}\n");
            Console.WriteLine("Što želite urediti?\n1 - Ime i prezime\n2 - Datum rođenja\n3 - Popis posuđenih knjiga\n");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Trenutno ime i prezime: {members[id].firstName} {members[id].lastName}\nUnesite novo ime i prezime: ");

                    string firstLastTog = Console.ReadLine();
                    string[] firstLast = firstLastTog.Split(" ");

                    if (firstLast[0] != null && firstLast[1] != null)
                    {
                        var temp = members[id];
                        temp.firstName = firstLast[0];
                        temp.lastName = firstLast[1];
                        members[id] = temp;
                    }
                    break;

                case 2:
                    Console.WriteLine("Trenutni datum: " + members[id].dateOfBirth.ToString("dd/MM/yyyy") + "\nUnesite novi datum (DD/MM/YYYY): ");

                    string date = Console.ReadLine();

                    string[] dates = date.Split('/');
                    int day = int.Parse(dates[0]);
                    int month = int.Parse(dates[1]);
                    int year = int.Parse(dates[2]);

                    if (day > 1 && month > 1 && year > 1)
                    {
                        var newdate = new DateTime(year, month, day);
                        var temp = members[id];
                        temp.dateOfBirth = newdate;
                        members[id] = temp;
                    }
                    break;

                case 3:
                    EditBook(books);
                    break;

                default:
                    Console.WriteLine("Krivi unos.");
                    break;

            }
            Console.WriteLine($"Uređeni član: {members[id].firstName} {members[id].lastName}, datum rođenja {members[id].dateOfBirth}\n");
        }

        static void PrintMember(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members,
            Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)> books)
        {
            Console.WriteLine("\nIspis svih članova (abecedno): ");
            var numbers = members.Keys.ToList();
            numbers.Sort();

            foreach( var number in numbers )
            {
                var member = members[number];
                int age = DateTime.Today.Year - member.dateOfBirth.Year;
                if (member.dateOfBirth.Date > DateTime.Today.AddYears(-age))
                    age--;
                int count = member.nums.Count;
                Console.WriteLine($"[{number}] {member.lastName}, {member.firstName} ({age}) - broj posudbi: {count}");
            }

            Console.WriteLine("Ispis članova mlađih od 18: ");
            foreach (var number in numbers)
            {
                var member = members[number];
                int age = DateTime.Today.Year - member.dateOfBirth.Year;
                if (member.dateOfBirth.Date > DateTime.Today.AddYears(-age))
                    age--;
                int count = member.nums.Count;
                if(age <= 18)
                    Console.WriteLine($"[{number}] {member.lastName}, {member.firstName} ({age}) - broj posudbi: {count}");
            }

            Console.WriteLine("Ispis svih članova koji imaju barem jednu nevraćenu knjigu: ");
            foreach (var number in numbers)
            {
                var member = members[number];
                int age = DateTime.Today.Year - member.dateOfBirth.Year;
                if (member.dateOfBirth.Date > DateTime.Today.AddYears(-age))
                    age--;
                int count = member.nums.Count;
                int check = 0;
                foreach(var book in books)
                {
                    if (member.nums.Contains(book.Key) == true && book.Value.returnDate.Year == 1)
                        check++;
                }

                if (check > 0) 
                    Console.WriteLine($"[{number}] {member.lastName}, {member.firstName} ({age}) - broj posudbi: {count}");
            }
        }


        static void MenuBook(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members,
            Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)> books)
        {
            Console.WriteLine("Odaberite:\n");
            Console.WriteLine("1 - Unos nove posudbe\n2 - Vraćanje knjige\n3 - Brisanje posudbe\n4 - Uređivanje postojeće posudbe" +
                "\n5 - Pregled svih posudbi\n6 - Izvještaji i analize\n0 - Povratak na glavni izbornik");

            int memChoice = int.Parse(Console.ReadLine());
            Console.Clear();

            switch (memChoice)
            {
                case 0:
                    Console.WriteLine("Vracanje...\n");
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();


                    break;

                case 1:
                    Console.WriteLine("UNOS NOVE POSUDBE\n");
                    FindMember(members, books);
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    Console.Clear();

                    break;

                case 2:
                    Console.WriteLine("VRAĆANJE KNJIGE\n");
                    bool check = ReturnBook(books);
                    if(!check)
                        Console.WriteLine("Krivi upis");
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    Console.Clear();

                    break;

                case 3:
                    Console.WriteLine("BRISANJE POSUDBE\n");
                    EraseBook(members, books);
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    Console.Clear();

                    break;

                case 4:
                    Console.WriteLine("UREĐIVANJE POSTOJEĆE POSUDBE\n");
                    EditBook(books);
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    Console.Clear();

                    break;

                case 5:
                    Console.WriteLine("PREGLED SVIH POSUDBI\n");
                    PrintBooks(members, books);
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    Console.Clear();

                    break;

                case 6:
                    Console.WriteLine("IZVJEŠTAJI I ANALIZE\n");
                    Statistics(members, books);
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    Console.Clear();

                    break;

                default:
                    Console.WriteLine("Krivi unos\n");
                    Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    Console.Clear();

                    break;
            }
        }

        static void FindMember(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members,
            Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)> books)
        {
            Console.WriteLine("Unesite ime i prezime ili id člana: ");
            string input = (Console.ReadLine() ?? "").Trim();
            int memberId = -1;
            int id;
            if (int.TryParse(input, out id))
            {
                if (members.ContainsKey(id)) 
                    memberId = id;
            }
            else
            {
                string firstLastTog = input;
                string[] firstLast = input.Split(" ");
                if (firstLast.Length == 2)
                {
                    foreach (var member in members)
                    {
                        if (member.Value.firstName ==  firstLast[0] && member.Value.lastName == firstLast[1])
                        {
                            memberId = member.Key;
                            break;
                        }
                    }
                }
            }

            if (memberId == -1)
            {
                Console.WriteLine("Krivi upis");
                return;
            }

            int newId = InputNewNumber(books);
            if (newId != -1)
                members[memberId].nums.Add(newId);

        }

        static int InputNewNumber(Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)> books)
        {
            Console.WriteLine("Upiši ime knjige: ");
            string bookTitle = (Console.ReadLine() ?? "").Trim();
            Console.WriteLine("Upiši autora knjige: ");
            string bookAuthor = (Console.ReadLine() ?? "").Trim();

            if (bookTitle == "" || bookAuthor == "")
            {
                Console.WriteLine("Naslov i autor ne mogu biti prazni.");
                return -1;
            }

            Console.WriteLine("Unesite datum posudbe (YYYY-MM-DD): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out DateTime loanDate))
            {
                Console.WriteLine("Neispravan datum.");
                return -1;
            }

            int newId = books.Count == 0 ? 0 : books.Keys.Max() + 1;
            books.Add(newId, (bookTitle, bookAuthor, loanDate, new DateTime()));
            Console.WriteLine($"Posudba uspješno dodana! Rok za vraćanje: {loanDate.AddDays(21):yyyy-MM-dd}");
            return newId;
        }

        static bool ReturnBook(Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)> books)
        {
            Console.WriteLine("Nevraćene knjige:");
            foreach (var book in books)
            {
                if(book.Value.returnDate.Year == 1)
                    Console.WriteLine($"[{book.Key}] '{book.Value.title}', {book.Value.author}, posuđena: {book.Value.loanDate}");
            }
            Console.WriteLine("Unesite naslov knjige koju želite vratiti: ");
            var name = Console.ReadLine();
            bool check = false;
            foreach (var book in books)
            {
                if(book.Value.title == name)
                {
                    check = true;
                    Console.WriteLine($"[{book.Key}] '{book.Value.title}', {book.Value.author}, posuđena: {book.Value.loanDate} i vraćena: " +
                        $"{DateTime.Today.ToString("dd/MM/yyyy")}");
                    int daysLate = NmbDays(book.Value.loanDate, DateTime.Today);
                    if(daysLate <= 0)
                    {
                        Console.WriteLine("Posudba ne kasni.");
                        var temp1 = books[book.Key];
                        temp1.returnDate = DateTime.Today;
                        books[book.Key] = temp1;

                        break;
                    }
                    Console.WriteLine($"Kasnite {daysLate} dana!");
                    double toPay = daysLate * 0.5;
                    Console.WriteLine($"Molimo platite {toPay} eura");
                    var temp = books[book.Key];
                    temp.returnDate = DateTime.Today;
                    books[book.Key] = temp;

                    break;
                }
            }
            return check;
        }

        static int NmbDays(DateTime loan, DateTime returnDate)
        {
            if (returnDate.Year == 1)
            {
                TimeSpan diff1 = DateTime.Today - loan;
                int daysDiff1 = diff1.Days;
                int daysLate1 = daysDiff1 - 21;
                if (daysLate1 > 0)
                    return daysLate1;
                return 0;

            }

            TimeSpan diff = returnDate - loan;
            int daysDiff = diff.Days;
            int daysLate = daysDiff - 21;
            if (daysLate > 0)
                return daysLate;
            return 0;

        }

        static void EraseBook(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members, 
            Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)> books)
        {
            Console.WriteLine("Odaberite željeno: ");
            Console.WriteLine("1 - po ID-u\n2 - sve vraćene posudbe odabranog člana\n3 - sve posudbe sa zakasninom većom od željenog broja");
            int choice;
            bool a = int.TryParse(Console.ReadLine(), out choice);
            if (!a)
            {
                Console.WriteLine("Krivi unos");
                return;
            }
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Unesite željeni id: ");
                    int answer;
                    bool b = int.TryParse(Console.ReadLine(), out answer);
                    if (!b)
                    {
                        Console.WriteLine("Krivi unos");
                        return;
                    }
                    foreach (var book in books)
                    {
                        if (book.Key == answer)
                        {
                            Console.WriteLine($"Knjiga {book.Value.title} će biti izbrisana. Jeste li sigurni? (Y/N)");
                            string answerYN2 = Console.ReadLine();
                            switch (answerYN2)
                            {
                                case "Y":
                                case "y":
                                    books.Remove(book.Key);
                                    Console.WriteLine("Knjiga obrisana!\n");
                                    return;

                                case "N":
                                case "n":
                                    Console.WriteLine("Vraćanje na glavni izbornik\n");
                                    return;

                                default:
                                    Console.WriteLine("Krivi unos.");
                                    return;
                            }
                        }
                    }
                    break;

                case 2:
                    Console.WriteLine("Unesite id željenog člana");
                    int ansMember;
                    bool c = int.TryParse(Console.ReadLine(),out ansMember);
                    if (!c)
                    {
                        Console.WriteLine("Krivi unos");
                        return;
                    }

                    foreach(var member in members)
                    {
                        if(member.Key == ansMember)
                        {
                            Console.WriteLine("Posudbe koje će biti izbrisane: ");
                            foreach(var book in books)
                            {
                                if (member.Value.nums.Contains(book.Key) == true)
                                    Console.WriteLine($"[{book.Key}] '{book.Value.title}', {book.Value.author}, posuđena: {book.Value.loanDate} i vraćena: {book.Value.returnDate}");
                            }
                            Console.WriteLine("Jeste li sigurni? (Y/N)");
                            string answerYN1 = Console.ReadLine();
                            switch (answerYN1)
                            {
                                case "Y":
                                case "y":
                                    foreach (var book in books)
                                    {
                                        if (member.Value.nums.Contains(book.Key) == true)
                                            books.Remove(book.Key);
                                    }
                                    member.Value.nums.Clear();
                                    Console.WriteLine("Posudbe obrisane!\n");
                                    return;

                                case "N":
                                case "n":
                                    Console.WriteLine("Vraćanje na glavni izbornik\n");
                                    return;

                                default:
                                    Console.WriteLine("Krivi unos.");
                                    return;
                            }

                        }
                    }

                    break;

                case 3:
                    Console.WriteLine("Unesite granicu zakasnine: ");
                    double limit;
                    bool d = double.TryParse(Console.ReadLine(),out limit);
                    if(!d)
                    {
                        Console.WriteLine("Krivi unos");
                        return;
                    }
                    Console.WriteLine("Posudbe koje će biti obrisane: ");
                    foreach(var book in books)
                    {
                        if (book.Value.returnDate.Year == 1)
                            continue;
                        int daysLate = NmbDays(book.Value.loanDate, book.Value.returnDate);
                        double toPay = daysLate * 0.5;
                        if(toPay >= limit)
                        {
                            Console.WriteLine($"[{book.Key}] '{book.Value.title}', {book.Value.author}, posuđena: " +
                                $"{book.Value.loanDate} i vraćena: {book.Value.returnDate}");     
                        }

                    }

                    Console.WriteLine("Jeste li sigurni? (Y/N)");
                    string answerYN = Console.ReadLine();
                    switch (answerYN)
                    {
                        case "Y":
                        case "y":
                            foreach (var book in books)
                            {
                                if (book.Value.returnDate.Year == 1)
                                    continue;
                                int daysLate = NmbDays(book.Value.loanDate, book.Value.returnDate);
                                double toPay = daysLate * 0.5;
                                if (toPay >= limit)
                                {
                                    Console.WriteLine($"[{book.Key}] '{book.Value.title}', {book.Value.author}, posuđena: " +
                                        $"{book.Value.loanDate} i vraćena: {book.Value.returnDate}");
                                }
                            }
                            Console.WriteLine("Posudbe obrisane!\n");
                            return;

                        case "N":
                        case "n":
                            Console.WriteLine("Vraćanje na glavni izbornik\n");
                            return;

                        default:
                            Console.WriteLine("Krivi unos.");
                            return;
                    }

                    break;

                default:
                    Console.WriteLine("Krivi unos.");
                    break;


            }
        }

        static void EditBook(Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)> books)
        {
            Console.WriteLine("Unesite id člana kojeg želite promijeniti: ");
            int idEdit = int.Parse(Console.ReadLine());

            foreach (var book in books)
            {
                if (book.Key == idEdit)
                {
                    ChooseEditBook(books, idEdit);
                }
            }
        }

        static void ChooseEditBook(Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)> books, int id)
        {

            Console.WriteLine($"Knjiga {books[id].title}\n");
            Console.WriteLine("Što želite urediti?\n1 - Naslov\n2 - Autor\n3 - Datum posudbe\n4 - Datum vraćanja");
            int choice;
            bool a = int.TryParse(Console.ReadLine(), out choice);

            if(!a)
            {
                Console.WriteLine("Krivi unos");
                return;
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Trenutni naslov: {books[id].title}\nUnesite novi naslov: ");
                    string newTitle = Console.ReadLine();
                    var book = books[id];
                    book.title = newTitle;
                    books[id] = book;
                    break;

                case 2:
                    Console.WriteLine($"Trenutni autor: {books[id].author}\nUnesite novog autora: ");
                    string newAuthor = Console.ReadLine();
                    var book1 = books[id];
                    book1.author = newAuthor;
                    books[id] = book1;
                    break;

                case 3:
                    Console.WriteLine("Trenutni datum posudbe: " + books[id].loanDate + "\nUnesite novi datum (DD/MM/YYYY): ");

                    string date = Console.ReadLine();

                    string[] dates = date.Split('/');
                    int day = int.Parse(dates[0]);
                    int month = int.Parse(dates[1]);
                    int year = int.Parse(dates[2]);

                    if (day > 1 && month > 1 && year > 1)
                    {
                        var newdate = new DateTime(year, month, day);
                        var temp = books[id];
                        temp.loanDate = newdate;
                        books[id] = temp;
                    }
                    break;

                case 4:
                    Console.WriteLine("Trenutni datum vraćanja: " + books[id].returnDate + "\nUnesite novi datum (DD/MM/YYYY): ");

                    string retDate = Console.ReadLine();

                    string[] retDates = retDate.Split('/');
                    int dayRet = int.Parse(retDates[0]);
                    int monthRet = int.Parse(retDates[1]);
                    int yearRet = int.Parse(retDates[2]);

                    if (dayRet > 1 && monthRet > 1 && yearRet > 1)
                    {
                        var newdate = new DateTime(yearRet, monthRet, dayRet);
                        var temp = books[id];
                        temp.returnDate = newdate;
                        books[id] = temp;
                    }
                    break;

                default:
                    Console.WriteLine("Krivi unos.");
                    break;

            }
            int daysLate = NmbDays(books[id].loanDate, books[id].returnDate);
            double toPay = daysLate * 0.5;
            Console.WriteLine($"Uređeni član: {books[id].title} {books[id].author}, " +
                $"datum posudbe {books[id].loanDate}, datum vraćanja {books[id].returnDate}\nNova zakasnina: {toPay} eura");
        }

        static void PrintBooks(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members,
            Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)> books)
        {
            Console.WriteLine("Odaberite željeno:");
            Console.WriteLine("1 - Sve posudbe kako su spremljene\n2 - Sve posudbe po datumu\n3 - sve posudbe po zakasnini\n" +
                "4 - sve trenutno nevraćene posudbe\n 5 - posudbe po dijelu autora, naziva\n6 - posudbe grupirane po mjesecu");
            int choice;
            bool e = int.TryParse(Console.ReadLine(), out choice);
            if(!e)
            {
                Console.WriteLine("Krivi unos");
                return;
            }

            switch(choice)
            {
                case 1:
                    foreach (var book in books)
                    {
                        int dayLate = NmbDays(book.Value.loanDate, book.Value.returnDate);
                        double toPay = dayLate * 0.5;
                        if(dayLate == 0)
                            Console.WriteLine($"[{book.Key}] - '{book.Value.title}' {book.Value.author}, datum posudbe {book.Value.loanDate}, " +
                                $"datum vraćanja {book.Value.returnDate}. Ne kasni");
                        else
                            Console.WriteLine($"[{book.Key}] - '{book.Value.title}' {book.Value.author}, datum posudbe {book.Value.loanDate}, " +
                                $"datum vraćanja {book.Value.returnDate}. Zakasnina: {toPay}");
                    }
                    break;

                case 2:
                    var nums = books.Keys.ToList();
                    Console.WriteLine("Ulazno(1) ili silazno(2)");
                    int ansSort;
                    bool f = int.TryParse(Console.ReadLine(), out ansSort);
                    if (!f)
                    {
                        Console.WriteLine("Krivi unos");
                        return;
                    }
                    if (ansSort == 1)
                    {
                        nums.Sort();
                        foreach (var num in nums)
                        {
                            int dayLate = NmbDays(books[num].loanDate, books[num].returnDate);
                            double toPay = dayLate * 0.5;
                            if (dayLate == 0)
                                Console.WriteLine($"[{num}] - '{books[num].title}' {books[num].author}, datum posudbe {books[num].loanDate}, " +
                                    $"datum vraćanja {books[num].returnDate}. Ne kasni");
                            else
                                Console.WriteLine($"[{num}] - '{books[num].title}' {books[num].author}, datum posudbe {books[num].loanDate}, " +
                                    $"datum vraćanja {books[num].returnDate}. Zakasnina: {toPay}");
                        }
                    }    
                        
                    else if (ansSort == 2)
                    {
                        nums.Reverse();
                        foreach (var num in nums)
                        {
                            int dayLate = NmbDays(books[num].loanDate, books[num].returnDate);
                            double toPay = dayLate * 0.5;
                            if (dayLate == 0)
                                Console.WriteLine($"[{num}] - '{books[num].title}' {books[num].author}, datum posudbe {books[num].loanDate}, " +
                                    $"datum vraćanja {books[num].returnDate}. Ne kasni");
                            else
                                Console.WriteLine($"[{num}] - '{books[num].title}' {books[num].author}, datum posudbe {books[num].loanDate}, " +
                                    $"datum vraćanja {books[num].returnDate}. Zakasnina: {toPay}");
                        }
                    }
                    else
                        Console.WriteLine("Krivi unos");

                    break;
                case 3:
                    Dictionary<int, double> toSort = new Dictionary<int, double>();
                    var nums1 = books.Keys.ToList();
                    foreach (var num in nums1)
                    {
                        int dayLate = NmbDays(books[num].loanDate, books[num].returnDate);
                        double toPay = dayLate * 0.5;
                        toSort.Add(num, toPay);
                    }
                    Console.WriteLine("Ulazno(1) ili silazno(2)");

                    var ascSort = toSort.OrderBy(p => p.Value);
                    var descSort = toSort.OrderByDescending(p => p.Value);

                    int ansSort1;
                    bool g = int.TryParse(Console.ReadLine(), out ansSort1);
                    if (!g)
                    {
                        Console.WriteLine("Krivi unos");
                        return;
                    }
                    
                    if (ansSort1 == 1)
                    {
                        foreach(var num in ascSort)
                        {
                            int dayLate = NmbDays(books[num.Key].loanDate, books[num.Key].returnDate);
                            double toPay = dayLate * 0.5;
                            if (dayLate == 0)
                                Console.WriteLine($"[{num.Key}] - '{books[num.Key].title}' {books[num.Key].author}, datum posudbe {books[num.Key].loanDate}, " +
                                    $"datum vraćanja {books[num.Key].returnDate}. Ne kasni");
                            else
                                Console.WriteLine($"[{num.Key}] - '{books[num.Key].title}' {books[num.Key].author}, datum posudbe {books[num.Key].loanDate}, " +
                                    $"datum vraćanja {books[num.Key].returnDate}. Zakasnina: {toPay}");
                        }
                    }

                    else if (ansSort1 == 2)
                    {
                        foreach (var num in descSort)
                        {
                            int dayLate = NmbDays(books[num.Key].loanDate, books[num.Key].returnDate);
                            double toPay = dayLate * 0.5;
                            if (dayLate == 0)
                                Console.WriteLine($"[{num}] - '{books[num.Key].title}' {books[num.Key].author}, datum posudbe {books[num.Key].loanDate}, " +
                                    $"datum vraćanja {books[num.Key].returnDate}. Ne kasni");
                            else
                                Console.WriteLine($"[{num}] - '{books[num.Key].title}' {books[num.Key].author}, datum posudbe {books[num.Key].loanDate}, " +
                                    $"datum vraćanja {books[num.Key].returnDate}. Zakasnina: {toPay}");
                        }
                    }
                    else
                        Console.WriteLine("Krivi unos");

                    break;

                case 4:
                    foreach(var book in books)
                    {
                        if(book.Value.returnDate.Year == 1)
                        {
                            int idMember = -1;
                            foreach (var member in members)
                            {
                                if(member.Value.nums.Contains(book.Key))
                                {
                                    idMember = member.Key;
                                    break;
                                }
                            }
                            if(idMember == -1)
                            {
                                Console.WriteLine("Greska");
                                return;
                            }
                                
                            Console.WriteLine($"Knjiga {book.Value.title}, autora {book.Value.author}, datum posudbe: {book.Value.loanDate} - " +
                                $"Član: {members[idMember].firstName} {members[idMember].lastName}");
                        }
                            
                    }


                    break;

                case 5:
                    Console.WriteLine("Unesite dio naslova ili autora: ");
                    string part = (Console.ReadLine() ?? "").Trim();

                    if (part == "")
                    {
                        Console.WriteLine("Krivi unos");
                        break;
                    }

                    bool found = false;
                    foreach (var book in books)
                    {
                        bool matches = book.Value.title.Contains(part, StringComparison.OrdinalIgnoreCase) ||
                                       book.Value.author.Contains(part, StringComparison.OrdinalIgnoreCase);

                        if (matches)
                        {
                            found = true;
                            int dayLate = NmbDays(book.Value.loanDate, book.Value.returnDate);
                            double toPay = dayLate * 0.5;
                            if (dayLate == 0)
                                Console.WriteLine($"[{book.Key}] - '{book.Value.title}' {book.Value.author}, datum posudbe {book.Value.loanDate}, " +
                                    $"datum vraćanja {book.Value.returnDate}. Ne kasni");
                            else
                                Console.WriteLine($"[{book.Key}] - '{book.Value.title}' {book.Value.author}, datum posudbe {book.Value.loanDate}, " +
                                    $"datum vraćanja {book.Value.returnDate}. Zakasnina: {toPay}");
                        }
                    }
                    if (!found)
                        Console.WriteLine("ne odgovara");

                    break;

                case 6:
                    List<string> monthName = new List<string>() { "SIJEČANJ", "VELJAČA", "OŽUJAK", "TRAVANJ", "SVIBANJ", "LIPANJ",
                    "SRPANJ", "KOLOVOZ", "RUJAN", "LISTOPAD", "STUDENI", "PROSINAC"};
                    List<int> months = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};
                    foreach(var month in months)
                    {
                        Console.WriteLine(monthName[month]);
                        foreach(var book in books)
                        {
                            if (book.Value.returnDate.Month == month)
                                Console.WriteLine($"[{book.Key}] - '{book.Value.title}' {book.Value.author}, datum posudbe {book.Value.loanDate}, " +
                                    $"datum vraćanja {book.Value.returnDate}");
                        }
                    }
                    break;

            }
        }

        static void Statistics(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members,
            Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)> books)
        {
            Console.WriteLine("Unesite id člana: ");
            int choice;
            bool h = int.TryParse(Console.ReadLine(), out choice);
            if (!h)
            {
                Console.WriteLine("Krivi unos");
                return;
            }
            int id = -1;
            foreach(var member in members)
            {
                if (choice == member.Key)
                {
                    id = choice;
                    break;
                }
            }

            if(id == -1)
            {
                Console.WriteLine("Greska");
                return;
            }

            var foundMem = members[id];

            Console.WriteLine("Ukupan broj posudbi: " + foundMem.nums.Count );

            int sumNotReturned = 0;
            foreach (var book in books)
            {
                if (foundMem.nums.Contains(book.Key))
                {
                    if (book.Value.returnDate.Year == 1)
                        sumNotReturned++;
                }
            }
            Console.WriteLine("Ukupan broj nevraćenih knjiga: " + sumNotReturned);

            double sum = 0.0;
            foreach (var book in books)
            {
                if(foundMem.nums.Contains(book.Key))
                {
                    int dayLate = NmbDays(book.Value.loanDate, book.Value.returnDate);
                    double toPay = dayLate * 0.5;
                    sum += toPay;
                }
            }
            Console.WriteLine("Ukupno za platiti: " + sum);
            int returnedCount = 0, sumDays = 0, maxLate = 0;
            foreach (var book in books)
            {
                if (!foundMem.nums.Contains(book.Key)) continue;
                if (book.Value.returnDate.Year != 1)
                {
                    returnedCount++;
                    sumDays += (book.Value.returnDate - book.Value.loanDate).Days;
                }
                int late = NmbDays(book.Value.loanDate, book.Value.returnDate);
                if (late > maxLate) maxLate = late;
            }

            double average = returnedCount > 0 ? (double)sumDays / returnedCount : 0;
            Console.WriteLine($"Prosječno trajanje posudbe: {average:0.00} dana");

            Console.WriteLine("Posudba/Posudbe s najvećim kašnjenjem: ");
            if (maxLate == 0)
                Console.WriteLine("Nijedna posudba ne kasni.");
            else
                foreach (var book in books)
                    if (foundMem.nums.Contains(book.Key) && NmbDays(book.Value.loanDate, book.Value.returnDate) == maxLate)
                        Console.WriteLine($"[{book.Key}] \"{book.Value.title}\" - {book.Value.author}, kašnjenje: {maxLate} dana");
            
            Console.WriteLine("Od kojeg datuma (DD/MM//YYYY): ");
            string dateFrom = Console.ReadLine();

            string[] datesFrom = dateFrom.Split('/');
            int dayFrom = int.Parse(datesFrom[0]);
            int monthFrom = int.Parse(datesFrom[1]);
            int yearFrom = int.Parse(datesFrom[2]);
            
            DateTime From = new DateTime(yearFrom, monthFrom, dayFrom);

            Console.WriteLine("Do kojeg datuma (DD/MM//YYYY): ");
            string dateTo = Console.ReadLine();

            string[] datesTo = dateTo.Split('/');
            int dayTo = int.Parse(datesTo[0]);
            int monthTo = int.Parse(datesTo[1]);
            int yearTo = int.Parse(datesTo[2]);

            DateTime To = new DateTime(yearTo, monthTo, dayTo);

            Console.WriteLine($"Posudba/Posudbe od datuma {From.ToString("yyyy-MM-dd")} do datuma {To.ToString("yyyy-MM-dd")}: ");
            foreach (var book in books)
            {
                if (foundMem.nums.Contains(book.Key) && (book.Value.loanDate > From && book.Value.loanDate < To))
                {
                    Console.WriteLine($"[{book.Key}] - '{book.Value.title}' {book.Value.author}, datum posudbe {book.Value.loanDate.ToString("yyyy-MM-dd")}, " +
                                $"datum vraćanja {book.Value.returnDate.ToString("yyyy-MM-dd")}.");
                }
            }




        }

    }

}


