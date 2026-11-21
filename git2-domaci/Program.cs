using System.Security.Cryptography;

namespace Drugi_zadatak
{
    internal class Program
    {
        static int Main(string[] args)
        {
            int choice;
            Console.WriteLine("Dobar dan!\n");

            ///PROMINI LIST U NOVI DICTIONARY

            var books = new Dictionary<int, (string title, string author, DateTime loanDate, DateTime returnDate)>
            {
                {0, ("Hamlet", "William Shakespeare", new DateTime(2026, 11, 1), new DateTime(2026, 11, 12)) },
                {1, ("Patnje mladog Werthera", "J. W. Goethe", new DateTime(2026, 11, 19), new DateTime()) },
                {2, ("Gavran", "Edgar Allan Poe", new DateTime(2026, 10, 30), new DateTime()) },
                {3, ("Ponos i predrasude", "Jane Austen", new DateTime(2026, 11, 3), new DateTime(2026, 11, 17)) },
                {4, ("Gavran", "Edgar Allan Poe", new DateTime(2026, 11, 16), new DateTime()) },
                {5, ("1984", "George Orwell", new DateTime(2026, 11, 12), new DateTime(2026, 11, 20)) }
            };

            var nums = books.Keys.ToList();

            var members = new Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)>
            {
                {0, ("Angelina", "Jolie", new DateTime(1975, 6, 4), new List<int>{ 1, 2, 4}) },
                {1, ("Brad", "Pitt", new DateTime(1963, 12, 18), new List<int>{ 0, 3}) },
                {2, ("Edward", "Norton", new DateTime(1969, 8, 18), new List<int>{ 5}) }
            };

            bool repeat = true;

            while (repeat)
            {
                var anyKey = "";
                Console.WriteLine("Molimo odaberite željeno:\n");
                Console.WriteLine("1 - Članovi\n2 - Posudbe\n0 - Izlaz iz aplikacije\n");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Zbogom!\n\n");
                        repeat = false;
                        anyKey = Console.ReadLine();
                        Console.Clear();

                        return 0;

                    case 1:
                        Console.Clear();
                        Console.WriteLine("ČLANOVI\n");
                        MenuMember(members);
                        anyKey = Console.ReadLine();
                        Console.Clear();

                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("POSUDBE\n");
                        anyKey = Console.ReadLine();
                        break;

                    default:
                        Console.WriteLine("Krivi unos");
                        anyKey = Console.ReadLine();
                        Console.Clear();
                        break;

                }
            }




            return 0;
        }

        static int nmbDays(DateTime loan, DateTime returnDate)
        {
            if (returnDate.Year == 1)
                return 0;

            TimeSpan diff = DateTime.Today - loan;
            int daysDiff = diff.Days;
            return daysDiff;
        }
        static void MenuMember(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members)
        {
            Console.WriteLine("Odaberite:\n");
            Console.WriteLine("1 - Unos novog člana\n2 - Brisanje člana\n3 - Uređivanje člana\n4 - Pregled svih članova\n0 - Povratak na glavni izbornik\n");

            var anyKey = "";
            int memChoice = int.Parse(Console.ReadLine());
            Console.Clear();

            switch (memChoice)
            {
                case 0:
                    Console.WriteLine("Vracanje...\n");
                    anyKey = Console.ReadLine();

                    break;

                case 1:
                    Console.WriteLine("UNOS NOVOG ČLANA\n");
                    InputNewMember(members);
                    anyKey = Console.ReadLine();

                    break;

                case 2:
                    Console.WriteLine("BRISANJE ČLANA\n");
                    DeleteMember(members);
                    anyKey = Console.ReadLine();

                    break;

                case 3:
                    Console.WriteLine("UREĐIVANJE ČLANA\n");
                    EditMember(members);
                    anyKey = Console.ReadLine();

                    break;

                case 4:
                    Console.WriteLine("PREGLED SVIH ČLANOVA\n\n");
                    foreach (var member in members)
                    {
                        Console.WriteLine($"Id: {member.Key} Ime: {member.Value.firstName} {member.Value.lastName} Datum rođenja: {member.Value.dateOfBirth} Popis: BUUUUU\n");
                    }
                    anyKey = Console.ReadLine();

                    break;

                default:
                    Console.WriteLine("Krivi unos\n");
                    anyKey = Console.ReadLine();

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
            members.Add(members.Count, (firstLast[0], firstLast[1], dateOfBirth, new List<int> { 1 }));
        }

        static void DeleteMember(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members)
        {
            Console.WriteLine("Unesite ime i prezime ili id člana kojeg želite obrisati: ");
            var inputDel = Console.ReadLine();
            bool check = false;
            foreach (var member in members)
            {
                if (member.Key == int.Parse(inputDel))
                {
                    check = AskDelete(members, member.Key);
                    if (check)
                        members.Remove(member.Key);
                    else
                        Console.WriteLine("Član nije nađen");

                    break;
                }

                string firstLastTog = inputDel;
                string[] firstLast = inputDel.Split(" ");
                if (firstLast.Length == 2)
                    if (member.Value.firstName == firstLast[0] && member.Value.lastName == firstLast[1])
                    {
                        check = AskDelete(members, member.Key);
                        if (check)
                            members.Remove(member.Key);
                        else
                            Console.WriteLine("Član nije nađen");

                        break;
                    }
            }
            if (!check)
                Console.WriteLine("Krivi upis.");
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
        static void EditMember(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members)
        {
            Console.WriteLine("Unesite id člana kojeg želite promijeniti: ");
            int idEdit = int.Parse(Console.ReadLine());

            foreach (var member in members)
            {
                if (member.Key == idEdit)
                {
                    ChooseEdit(members, idEdit);
                }
            }

        }

        static void ChooseEdit(Dictionary<int, (string firstName, string lastName, DateTime dateOfBirth, List<int> nums)> members, int id)
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
                    Console.WriteLine("Trenutni datum: " + members[id].dateOfBirth + "\nUnesite novi datum (DD/MM/YYYY): ");

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
                    Console.WriteLine("Under construction...\n");
                    break;

                default:
                    Console.WriteLine("Krivi unos.");
                    break;

            }
            Console.WriteLine($"Uređeni član: {members[id].firstName} {members[id].lastName}, datum rođenja {members[id].dateOfBirth}\n");
        }


        //static void MenuBook()

    }

}
