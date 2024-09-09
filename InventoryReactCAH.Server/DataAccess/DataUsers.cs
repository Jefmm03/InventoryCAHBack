

using InventoryReactCAH.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.DirectoryServices.AccountManagement;

namespace InventoryReactCAH.Server.DataAccess
{
    public class DataUsers
    {
        private readonly ItinventoryModelContext dbContext;

        public DataUsers(ItinventoryModelContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public bool ValidateCredentials(string account, string password, string domain, string user, string pass)
        {
            try
            {
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domain, user, pass))
                {
                    bool result = ctx.ValidateCredentials(account, password, ContextOptions.Negotiate);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // existe en la base de datos
        public bool UserExistsInDatabase(string username)
        {
            try
            {
                
                return dbContext.Users.Any(u => u.Username == username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

