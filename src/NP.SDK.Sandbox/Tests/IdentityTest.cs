using System;
using NP.SDK.Contracts.Identity.Enums;
using NP.SDK.Core.Identity;
using NP.SDK.Core.Logging;

namespace NP.SDK.Sandbox.Tests
{
    public static class IdentityTest
    {
        public static void Run()
        {
            Logger logger = new Logger();

            logger.Info("=== Identity Test Started ===");

            try
            {
                // -------------------------------------------------
                // 1. Create Permissions
                // -------------------------------------------------

                Permission userRead =
                    new Permission(
                        "user.read",
                        "Read Users",
                        "Allows reading user information.");

                Permission userWrite =
                    new Permission(
                        "user.write",
                        "Write Users",
                        "Allows creating and modifying users.");

                Permission dataRead =
                    new Permission(
                        "data.read",
                        "Read Data",
                        "Allows reading application data.");

                logger.Debug("Permissions created.");


                // -------------------------------------------------
                // 2. Create Role
                // -------------------------------------------------

                Role administrator =
                    new Role(
                        "administrator",
                        "Administrator",
                        "Full application administrator role.");

                administrator.AddPermission(userRead);
                administrator.AddPermission(userWrite);
                administrator.AddPermission(dataRead);

                logger.Debug(
                    "Role created: " +
                    administrator.Name);


                // -------------------------------------------------
                // 3. Test duplicate permission
                // -------------------------------------------------

                administrator.AddPermission(userRead);

                logger.Debug(
                    "Permission count after duplicate test: " +
                    administrator.Permissions.Count);


                // -------------------------------------------------
                // 4. Create User
                // -------------------------------------------------

                User user =
                    new User(
                        "user-001",
                        "navid",
                        "Navid Piri");

                user.Status = UserStatus.Active;

                logger.Debug(
                    "User created: " +
                    user.UserName);


                // -------------------------------------------------
                // 5. Assign Role to User
                // -------------------------------------------------

                user.AddRole(administrator);

                logger.Debug(
                    "Role assigned to user: " +
                    administrator.Name);


                // -------------------------------------------------
                // 6. Test duplicate role
                // -------------------------------------------------

                user.AddRole(administrator);

                logger.Debug(
                    "Role count after duplicate test: " +
                    user.Roles.Count);


                // -------------------------------------------------
                // 7. Display User information
                // -------------------------------------------------

                logger.Info(
                    "User ID: " +
                    user.Id);

                logger.Info(
                    "User Name: " +
                    user.UserName);

                logger.Info(
                    "Display Name: " +
                    user.DisplayName);

                logger.Info(
                    "User Status: " +
                    user.Status);


                // -------------------------------------------------
                // 8. Display Roles and Permissions
                // -------------------------------------------------

                foreach (var role in user.Roles)
                {
                    logger.Info(
                        "Role: " +
                        role.Name);

                    foreach (var permission in role.Permissions)
                    {
                        logger.Info(
                            "  Permission: " +
                            permission.Id);
                    }
                }


                // -------------------------------------------------
                // 9. Test RemovePermission
                // -------------------------------------------------

                bool permissionRemoved =
                    administrator.RemovePermission(dataRead);

                logger.Debug(
                    "Permission removed: " +
                    permissionRemoved);

                logger.Debug(
                    "Permission count after removal: " +
                    administrator.Permissions.Count);


                // -------------------------------------------------
                // 10. Test RemoveRole
                // -------------------------------------------------

                bool roleRemoved =
                    user.RemoveRole(administrator);

                logger.Debug(
                    "Role removed: " +
                    roleRemoved);

                logger.Debug(
                    "Role count after removal: " +
                    user.Roles.Count);


                // -------------------------------------------------
                // 11. Final result
                // -------------------------------------------------

                logger.Info("Identity Test completed successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(
                    "Identity Test failed.",
                    ex);

                throw;
            }

            logger.Info("=== Identity Test Finished ===");
        }
    }
}