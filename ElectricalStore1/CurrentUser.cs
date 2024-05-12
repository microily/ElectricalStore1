using System;

namespace ElectricalStore1.Model
{
    public static class CurrentUser
    {
        private static Employee _loggedInUser;

        public static event EventHandler<Employee> UserLoggedIn;

        public static Employee LoggedInUser
        {
            get => _loggedInUser;
            private set
            {
                _loggedInUser = value;
                UserLoggedIn?.Invoke(null, value); // Генерируем событие о входе пользователя
            }
        }

        public static void Login(Employee user)
        {
            LoggedInUser = user;
        }

        public static void Logout()
        {
            LoggedInUser = null;
        }

        public static bool IsLoggedIn => LoggedInUser != null;
    }
}
