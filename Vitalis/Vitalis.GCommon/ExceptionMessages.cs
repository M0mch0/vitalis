using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitalis.GCommon
{
    public static class ExceptionMessages
    {
        public const string RoleSeedingExceptionMessage = "There was an error while trying to seed the role {0}";

        public const string AdminUserSeedingEmailNotFound = "Admin email not found in configuration";

        public const string AdminUserSeedingPasswordNotFound = "Admin password not found in configuration";

        public const string AdminUserSeedingException = "There was an error while seeding the admin user";
    }
}
