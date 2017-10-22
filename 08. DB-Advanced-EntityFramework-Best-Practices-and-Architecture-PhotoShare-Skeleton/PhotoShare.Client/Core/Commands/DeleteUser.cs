namespace PhotoShare.Client.Core.Commands
{
    using Service;
    using System;
    using System.Linq;

    public class DeleteUser
    {
        private UserService userService;

        public DeleteUser(UserService userService)
        {
            this.userService = userService;
        }
        // DeleteUser <username>
        public string Execute(string[] data)
        {
            string username = data[0];

            if (!this.userService.IsTaken(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            this.userService.Remove(username);

            return $"User {username} was deleted successfully!";
        }
    }
}
