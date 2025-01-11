// Класс оружия
class Weapon
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public int Durability { get; set; }

    public Weapon(string name, int damage, int durability)
    {
        Name = name;
        Damage = damage;
        Durability = durability;
    }
}

// Класс аптечки
class Aid
{
    public string Name { get; set; }
    public int HealAmount { get; set; }

    public Aid(string name, int healAmount)
    {
        Name = name;
        HealAmount = healAmount;
    }
}

// Класс врага
class Enemy
{
    public string Name { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public Weapon Weapon { get; set; }

    public Enemy(string name, int maxHealth, Weapon weapon)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        Weapon = weapon;
    }

    public int Attack()
    {
        return Weapon.Damage;
    }
}

// Класс игрока
class Player
{
    public string Name { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public int Score { get; set; }
    public Weapon Weapon { get; set; }
    public Aid AidKit { get; set; }

    public Player(string name, int maxHealth, Weapon weapon)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        Weapon = weapon;
        Score = 0;
    }

    public void Heal()
    {
        if (AidKit != null)
        {
            CurrentHealth += AidKit.HealAmount;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            Console.WriteLine($"{Name} использует аптечку {AidKit.Name} и восстанавливает {AidKit.HealAmount} здоровья.");
            AidKit = null; // Используем аптечку
        }
        else
        {
            Console.WriteLine("У вас нет аптечек!");
        }
    }

    public int Attack()
    {
        return Weapon.Damage;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Добро пожаловать в игру 'Рогалик'!");

       
        var sword = new Weapon("Меч", 10, 100);




 
        var firstAidKit = new Aid("Первая помощь", 20);

  
        Console.Write("Введите имя вашего персонажа: ");
        string playerName = Console.ReadLine();
        Player player = new Player(playerName, 100, sword);
        player.AidKit = firstAidKit;

        
        List<Enemy> enemies = new List<Enemy>
            {
                new Enemy("Зомби", 30, new Weapon("Клыки", 5, 50)),
                new Enemy("Скелет", 25, new Weapon("Лук", 7, 40)),
                new Enemy("Гоблин", 20, new Weapon("Копье", 6, 30)),
                new Enemy("Дракон", 50, new Weapon("Огненное дыхание", 15, 20)),
                new Enemy("Вампир", 35, new Weapon("Клыки", 10, 25))
            };

        Random random = new Random();

     
        while (true)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Сразиться с врагом");
            Console.WriteLine("2. Использовать аптечку");
            Console.WriteLine("3. Выйти из игры");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                   
                    int enemyIndex = random.Next(enemies.Count);
                    Enemy enemy = enemies[enemyIndex];
                    Console.WriteLine($"Вы встретили {enemy.Name}!");

                  
                    while (player.CurrentHealth > 0 && enemy.CurrentHealth > 0)
                    {
                     
                        enemy.CurrentHealth -= player.Attack();
                        Console.WriteLine($"{player.Name} атакует {enemy.Name} и наносит {player.Attack()} урона!");

                        if (enemy.CurrentHealth > 0)
                        {
                      
                            player.CurrentHealth -= enemy.Attack();
                            Console.WriteLine($"{enemy.Name} атакует {player.Name} и наносит {enemy.Attack()} урона!");
                        }
                    }

                    if (player.CurrentHealth <= 0)
                    {
                        Console.WriteLine("Вы погибли! Игра окончена.");
                        return;
                    }
                    else
                    {
                        player.Score += 10;
                        Console.WriteLine($"Вы победили {enemy.Name}! Ваши очки: {player.Score}");
                    }
                    break;

                case "2":
                  
                    player.Heal();
                    break;

                case "3":
                    Console.WriteLine("Спасибо за игру! До свидания!");
                    return;

                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите снова.");
                    break;
            }
        }
    }
}
    
