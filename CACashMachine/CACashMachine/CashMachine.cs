using System;
using System.Linq;

namespace CACashMachine
{
    public class CashMachine
    {
        public static void SignIn()
        {
            Console.Write($"Введите логин: ");
            var login = Console.ReadLine();

            var user = UserStore.GetUserByLogin(login);

            if (user == null)
            {
                Console.WriteLine("Аккаунт не найден или он быд удалён. Нажмите любую клавишу чтобы повторить.");
                Console.ReadKey();
                return;
            }

            for (int attempt = 3; attempt > 0; attempt--)
            {
                Console.Write($"Введите пароль: ");
                var password = Console.ReadLine();

                if (user.Password == password)
                {
                    ShowUserMenu(user);
                    return;
                }

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Неверный пароль");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"У Вас осталось {attempt - 1} попытки для входа.");
                Console.ResetColor();
            }

            user.IsDeleted = true;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Пользователь удалён в целях безопасности.");
            Console.ResetColor();
            Console.ReadKey();
        }

        private static void ShowUserMenu(IUser user)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Меню пользователя:");
            Console.ResetColor();

            BankAction[] allowdActions;

            if (IsAdmin(user))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Вы залогинены под Админом.");
                Console.ResetColor();

                allowdActions = new BankAction[]
                {
                    BankAction.GetBalance,
                    BankAction.WithdrawMoney,
                    BankAction.DeleteUser,
                    BankAction.CheckUsers,
                    BankAction.ChangeMoney,
                    BankAction.EndWork
                };
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Вы залогинены под Пользователем.");
                Console.ResetColor();

                allowdActions = new BankAction[]
                {
                    BankAction.GetBalance,
                    BankAction.WithdrawMoney,
                    BankAction.EndWork
                };
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Выберите действие цифрой:");
            Console.ResetColor();

            while (true)
            {
                foreach (var item in allowdActions)
                {
                    Console.WriteLine($"[{(int)item}] {item}");
                }

                BankAction action = (BankAction)int.Parse(Console.ReadLine());

                if (!allowdActions.Contains(action))
                {
                    continue;
                }

                switch (action)
                {
                    case BankAction.GetBalance:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"[Вы выбрали действие просмотра баланса]");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Сумма вашего баланса: {user.Balance.ToString()}$");
                        Console.ResetColor();
                        break;

                    case BankAction.WithdrawMoney:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"[Вы выбрали действие снятия денег с баланса]\nСколько денег вы хотите снять?");
                        Console.ResetColor();

                        int.TryParse(Console.ReadLine(), out int widthrawMoney);

                        if (user.Balance <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"У Вас больше не осталось денег.");
                            Console.ResetColor();
                            break;
                        }

                        if (widthrawMoney > user.Balance)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Нельзя снять колличество большее вашего баланса");
                            Console.ResetColor();
                            break;
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        user.Balance = user.Balance - widthrawMoney;
                        Console.WriteLine($"Ваш остаток: {user.Balance}$");
                        Console.ResetColor();
                        break;

                    case BankAction.DeleteUser:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"[Вы выбрали действие удаление пользователя]\nНапишите логин удаляемого пользователя");
                        Console.ResetColor();

                        if (UserStore.Users.Count() == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Список логинов пуст!");
                            Console.ResetColor();
                            break;
                        }
                        else
                        {
                            for (int i = 0; i < UserStore.Users.Count(); i++)
                            {
                                if (UserStore.Users[i].IsDeleted == false)
                                    Console.WriteLine($"{i}) Логин: {UserStore.Users[i].Login}");
                            }

                            (user as AdminUser).DeleteUser();
                        }
                        break;

                    case BankAction.CheckUsers:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"[Вы выбрали действие проверки пользователей]");
                        Console.ResetColor();

                        if (UserStore.Users.Count() == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Список логинов пуст!");
                            Console.ResetColor();
                            break;
                        }
                        else
                        {
                            for (int i = 0; i < UserStore.Users.Count(); i++)
                            {
                                if (UserStore.Users[i].IsDeleted == false)
                                    Console.WriteLine($"{i}) Логин: {UserStore.Users[i].Login} | Баланс: {UserStore.Users[i].Balance}");
                            }
                        }
                        break;

                    case BankAction.ChangeMoney:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"[Вы выбрали действие изменения баланса пользователю]");
                        Console.ResetColor();

                        if (UserStore.Users.Count() == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Список логинов пуст!");
                            Console.ResetColor();
                            break;
                        }
                        else
                        {
                            for (int i = 0; i < UserStore.Users.Count(); i++)
                            {
                                if (UserStore.Users[i].IsDeleted == false)
                                    Console.WriteLine($"{i}) Логин: {UserStore.Users[i].Login} | Баланс {UserStore.Users[i].Balance}");
                            }

                            (user as AdminUser).ChangeMoney();
                        }
                        break;

                    case BankAction.EndWork:
                        return;
                }
            }
        }

        static bool IsAdmin(IUser user)
        {
            if (user is AdminUser)
                return true;

            return false;
        }
    }
}
