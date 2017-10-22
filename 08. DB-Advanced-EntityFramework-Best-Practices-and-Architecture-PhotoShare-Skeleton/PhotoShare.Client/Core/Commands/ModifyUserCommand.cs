namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Service;
    using System;

    public class ModifyUserCommand
    {
        private readonly UserService userService;
        private readonly TownService townService;

        public ModifyUserCommand(UserService userService, TownService townService)
        {
            this.userService = userService;
            this.townService = townService;
        }
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            string username = data[0];
            string propType = data[1];
            string value = data[2];

            User u = this.userService.GetUserByUsername(username);

            if (u == null)
            {
                throw new ArgumentException();
            }

            if (propType == "Password")
            {
                u.Password = value;
            }
            else if (propType == "BornTown")
            {
                Town t = this.townService.GetByTownName(value);

                if (t == null)
                {
                    throw new ArgumentException();
                }

                u.BornTown = t;
            }
            else if (propType == "CurrentTown")
            {
                Town t = this.townService.GetByTownName(value);

                if (t == null)
                {
                    throw new ArgumentException();
                }

                u.CurrentTown = t;
            }
            else
            {
                throw new ArgumentException($"Property {propType} not supported!");
            }

            this.userService.UpdateUser(u);

            return $"User {username} {propType} is {value}.";
        }
    }
}
