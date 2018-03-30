using System;

namespace CACashMachine
{
    class AdminUser : IUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Balance { get; set; }
        public bool IsDeleted { get; set; }

        public void DeleteUser()
        {
            Console.WriteLine("Введите логин для удаления пользователя");
            string login = Console.ReadLine();

            var user = UserStore.GetUserByLogin(login);

            if (user == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Ошибка! Данного логина нет или он был удалён!");
                Console.ResetColor();
                return;
            }

            if (user.Login == Login)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Вы не можете удалить сами себя!");
                Console.ResetColor();
                return;
            }

            user.IsDeleted = true;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Удалено!");
            Console.ResetColor();
        }

        public void ChangeMoney()
        {
            Console.WriteLine("Введите логин пользователя для изменения ему баланса");
            string login = Console.ReadLine();

            var user = UserStore.GetUserByLogin(login);

            if (user == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Ошибка! Данного логина нет или он был удалён!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Введите сумму, которая у пользователя будет на балансе");
            int.TryParse(Console.ReadLine(), out int changeMoney);

            if (changeMoney < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Сумма баланса не может быть меньше 0");
                Console.ResetColor();
                return;
            }

            user.Balance = changeMoney;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Успешно изменена сумма баланса пользователя: {login} на {user.Balance}$");
            Console.ResetColor();
        }
    }
}
