using System;
using System.Collections.Generic;
using System.Linq;
using ServiceDesk.WebApp.Domain;

namespace ServiceDesk.WebApp.Services
{
    public interface IUserService
    {
        IList<User> GetAllUsers();
        string GetUserName(Guid id);
    }

    public class UserService : IUserService
    {
        // OptionSet values are hard coded for now. Later on an admin view will be developed to let admin 
        // create and edit optionsets in the app.

        private readonly List<User> _users = new List<User>() {
            new User() { Id = Guid.Parse("64dac6a0-dea6-49dc-9fc3-b7aad6b0a0dc"), Name = "Kalle Karlsson" },
            new User() { Id = Guid.Parse("83e82129-7b6b-41fa-8115-067b6d2901bd"), Name = "Benke Bus" },
            new User() { Id = Guid.Parse("5a64b022-b95f-43d8-b2b3-ce88fbe102cb"), Name = "Fia Fransson" },
            new User() { Id = Guid.Parse("eaf15c2f-f159-4167-a5b3-0740740c3041"), Name = "Per Persson" }
          };

        public IList<User> GetAllUsers()
        {
            return _users;
        }

        public string GetUserName(Guid id)
        {
            return _users.Where(u => u.Id.Equals(id)).Select(u => u.Name).SingleOrDefault();
        }
    }
}