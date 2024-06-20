using AutoTestConsole;
using Newtonsoft.Json;


var tests = new List<Test>();
var results = new List<Result>();
var users = new List<User>();
int input;
string currentUsername;
ReadData();
MainMenu();


void MainMenu()
{
    Console.WriteLine("Dasturimizga xush kelibsiz :)");
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Register");
    Console.Write(">>>");
    input = int.Parse(Console.ReadLine()!);
    if (input == 1)
    {
        Login();
    }
    else if (input == 2)
    {
        Register();
    }
    else
    {
        MainMenu();
    }
}


void Login()
{
    currentUsername = String.Empty;
    Console.Write("Username kiriting >>>");
    var username = Console.ReadLine();
    var user = users.FirstOrDefault(u => u.Username == username);
    if (user == null)
    {
        Console.WriteLine("Bunday user topilmadi");
        MainMenu();
    }
        
        
    Console.Write("Password kiriting >>>");
    var password = Console.ReadLine();
    var check = user.Password == password;
    if (!check)
    {
        Console.WriteLine("Notogri password, qayta urinib koring :");
        Login();
    }
    
    Console.WriteLine($"{username} Xush kelibsiz :)");
    currentUsername = username;
    ShowMenu();
}

void Register()
{
    Console.Write("Username kiriting >>>");
    var username = Console.ReadLine();
    var check = users.Any(u => u.Username == username);
    if (check)
    {
        Console.WriteLine("Bunday user allaqachon mavjud \n Iltimos qayta urinib koring");
        Register();
    }
    Console.Write("Password kiriting >>>");
    var password = Console.ReadLine();

    var user = new User()
    {
        Username = username,
        Password = password
    };
    users.Add(user);
    MainMenu();
}


void ShowMenu()
{
    Console.WriteLine("1. Test ishlash");
    Console.WriteLine("2. Natijalarimni ko'rish");
    Console.WriteLine("3. Logout");

    Console.Write(">>>");
    input = int.Parse(Console.ReadLine());
    
    switch (input)
    {
        case 1 : TakeTest(); break;
        case 3: Logout(); break;
    }
}

void TakeTest()
{
    var result = new Result()
    {
        Username = currentUsername,
        CorrectAnswersCount = 0
    };
    foreach (var test in tests)
    {
        Console.WriteLine($"{test.Id} : {test.Question}");
        int i = 1;
        foreach (var choice in test.Choices)
        {
            Console.WriteLine($"{i}) {choice}");
            i++;
        }
        Console.Write(">>>");
        input = int.Parse(Console.ReadLine());
        if (test.Choices[input - 1].Answer)
        {
            result.CorrectAnswersCount += 1;
        }

        
        if (test.Id == 20)
        {
            Console.WriteLine($"Sizning natijangiz : \n To'gri javoblar : {result.CorrectAnswersCount} \n No'tog'ri javoblar : {result.InCorrectAnswersCount}");
            ShowMenu();
        }
    }
}

void Logout()
{
    currentUsername = string.Empty;
    MainMenu();
}


void ReadData()
{
    var jsonData = File.ReadAllText("uzlotin.json");

    tests = JsonConvert.DeserializeObject<List<Test>>(jsonData);
}


