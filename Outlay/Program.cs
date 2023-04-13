using System.Text.RegularExpressions;

var users = new List<User>();
var outlays = new List<Outlay>();

var corrntUser = string.Empty;

Console.WriteLine("ыыоаарроараш");
while (true)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(" Menu");
    Console.WriteLine($"{(int)MainMenu.SignUp}. Sign up");
    Console.WriteLine($"{(int)MainMenu.SignIn}. Sign in");

    var menu =(MainMenu)int.Parse( Console.ReadLine());

    switch (menu)
    {
        case MainMenu.SignUp: SignUp(); break;
        case MainMenu.SignIn: SignIn(); break;
    }
}
void SignUp()
{
    var signUp = false;

    while (!signUp)
    {


        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" Sign up");

        Console.Write("Login: ");
        var login = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Yellow;
            
        Console.WriteLine("\nPasswords must at least 6 characters [%, a - z , A - Z , 0 - 9 ]");
        Console.Write("Password: ");
        var password = Console.ReadLine();

        if (ChecPassword(password))
        {
            var user = new User()
            {
                Login = login,
                Password = password

            };

            users.Add(user);
            signUp= true;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Succes!\n");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("The password did not match the request!\n");
        }
    }
}

bool   ChecPassword(string password)
{
    int passwordCount = 6;
    var kichikRx = new Regex(@"[a-z]");
    var kattaRx = new Regex(@"[A-Z]");
    var belgiRx = new Regex(@"[!-/]");
    var raqamRx = new Regex(@"[0-9]");

    int checCount = 0;

    checCount += password.Length >= passwordCount ? 1 : 0;

    var kichikHarfCheck =kichikRx.Match(password);
    checCount += kichikHarfCheck.Success ? 1 : 0;

    var kattaHarfCheck = kattaRx.Match(password);
    checCount += kattaHarfCheck.Success ? 1 : 0;

    var belgiCheck = belgiRx.Match(password);
    checCount += belgiCheck.Success ? 1 : 0;

    var raqamCheck = raqamRx.Match(password);
    checCount += raqamCheck.Success ? 1 : 0;

    if (checCount == 5)
    {
        return true;
    }


    return false;
}

void SignIn()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(" Sign in");

    Console.Write("Login: ");
    var login = Console.ReadLine();

    Console.Write("Password: ");
    var password = Console.ReadLine();
    Console.WriteLine();

    if (users.Any(user => user.Login == login && user.Password == password))
    {
        //xarajat fuction
        corrntUser = login;
        ShowOutlayMenu();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid user!");
    }
}

void ShowOutlayMenu()
{
    var isExit = false;
    while (!isExit)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Outlay menu");
        Console.WriteLine($"{(int)OutlayMenu.Outlay}. Outlay ");
        Console.WriteLine($"{(int)OutlayMenu.ShowOutlay}. Show outlay ");
        Console.WriteLine($"{(int)OutlayMenu.Calculete}. Calculate ");
        Console.WriteLine($"{(int)OutlayMenu.Exit}. Exit ");
        Console.WriteLine();


        var outlayMenu = (OutlayMenu)int.Parse(Console.ReadLine());

        switch (outlayMenu)
        {
            case OutlayMenu.Outlay:Outlay(); break;
            case OutlayMenu.ShowOutlay:ShowOutlay(); break;
            case OutlayMenu.Calculete: Calculete(); break;
            case OutlayMenu.Exit:isExit = true;break; 
        }
    }

}
void Outlay()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(" Outlay ");

    Console.Write("Name: ");
    var name = Console.ReadLine();

    Console.Write("Price: ");
    var price = int.Parse(Console.ReadLine());
    Console.WriteLine();
    var outlay = new Outlay()
    {
        Name = name,
        Price = price,
        UserLogin = corrntUser
    };

    outlays.Add(outlay);
}

void ShowOutlay()
{
    foreach (var outlay in outlays)
    {
        Console.WriteLine($"{outlay.UserLogin} : {outlay.Name}, {outlay.Price}");
    }
}

void Calculete()
{
    var totalOutlayPriceAll = outlays.Sum(outlay => outlay.Price);
    var userCount = users.Count();

    var avarage = totalOutlayPriceAll / userCount;

    Console.WriteLine($"Users: {userCount}  ,\n Avarage {avarage}");

    foreach (var user in users)
    {
        var totalUserOutlay = outlays.Where(outlay => outlay.UserLogin == user.Login).Sum(o => o.Price);

        Console.WriteLine($"{user.Login} : {totalUserOutlay - avarage}");
    }
}

enum MainMenu
{
    SignUp=1,
    SignIn
}

enum OutlayMenu
{
    Outlay = 1,
    ShowOutlay,
    Calculete,
    Exit
}