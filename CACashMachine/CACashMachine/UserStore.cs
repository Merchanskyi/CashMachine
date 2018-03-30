using System.Linq;

namespace CACashMachine
{
    static class UserStore
    {
        public static IUser[] Users = new IUser[]
        {
            new SimpleUser() { Login = "Pasha", Password = "123", Balance = 123123123 },
            new AdminUser() { Login = "Tim", Password = "228", Balance = 1337 },
            new AdminUser() { Login = "Yarik", Password = "228", Balance = 1337 },
            new SimpleUser() { Login = "Neket", Password = "123", Balance = 123123123 },
            new SimpleUser() { Login = "Bobik", Password = "123", Balance = 123123123 },
            new AdminUser() { Login = "Ruslan", Password = "228", Balance = 1337 },
        };

        public static IUser GetUserByLogin(string login)
        {
            for (var i = 0; i < Users.Count(); i++)
            {
                var user = Users[i];

                if (user.Login.ToLower() == login.ToLower() && user.IsDeleted == false)
                {
                    return user;
                }
            }

            return null;
        }

        public static IUser GetUserByLoginAndPassword(string login, string password)
        {
            var user = GetUserByLogin(login);

            if (user == null)
            {
                return null;
            }

            if (user.Password == password)
            {
                return user;
            }

            return null;
        }
    }
}
