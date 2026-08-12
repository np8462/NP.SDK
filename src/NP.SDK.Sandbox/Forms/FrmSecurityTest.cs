using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using NP.SDK.Contracts.Identity;
using NP.SDK.Contracts.Identity.Enums;
using NP.SDK.Contracts.Security;

using NP.SDK.Core.Authorization;
using NP.SDK.Core.Identity;

using NP.SDK.Security.Adapter;
using NP.SDK.Security.Provider;

namespace NP.SDK.Sandbox.Forms
{
    public partial class FrmSecurityTest : Form
    {
        // ---------------------------------------------------------
        // Security
        // ---------------------------------------------------------

        private readonly ISecurityProvider _securityProvider;
        private readonly ISecurityAdapter _securityAdapter;
        private readonly AuthorizationService _authorizationService;

        // ---------------------------------------------------------
        // Test users
        // ---------------------------------------------------------

        private readonly List<IUser> _users;

        // ---------------------------------------------------------
        // Constructor
        // ---------------------------------------------------------

        public FrmSecurityTest()
        {
            InitializeComponent();

            // -----------------------------------------------------
            // Create security chain
            // -----------------------------------------------------

            _securityProvider =
                new DefaultSecurityProvider();

            _securityAdapter =
                new DefaultSecurityAdapter(
                    _securityProvider);

            _authorizationService =
                new AuthorizationService(
                    _securityAdapter);

            // -----------------------------------------------------
            // Create test identity data
            // -----------------------------------------------------

            _users =
                new List<IUser>();

            CreateTestIdentity();

            // -----------------------------------------------------
            // Initialize UI
            // -----------------------------------------------------

            InitializeSecurityTestUI();

            // -----------------------------------------------------
            // Events
            // -----------------------------------------------------

            cmbUser.SelectedIndexChanged +=
                cmbUser_SelectedIndexChanged;

            btnCheckAuthorization.Click +=
                btnCheckAuthorization_Click;

            btnTestIdentity.Click +=
                btnTestIdentity_Click;

            btnTestAuthorization.Click +=
                btnTestAuthorization_Click;

            btnTestTransport.Click +=
                btnTestTransport_Click;

            btnRunFullTest.Click +=
                btnRunFullTest_Click;
        }

        // =========================================================
        // TEST IDENTITY
        // =========================================================

        private void CreateTestIdentity()
        {
            // -----------------------------------------------------
            // Permissions
            // -----------------------------------------------------

            Permission serverCreate =
                new Permission(
                    "PERM_SERVER_CREATE",
                    "Server.Create",
                    "Allows creation of a server.");
            //Permission serverCreate =
            //    new Permission(
            //        "Server.Create",
            //        "Server.Create",
            //        "Allows creation of a server.");

            Permission serverStart =
                new Permission(
                    "PERM_SERVER_START",
                    "Server.Start",
                    "Allows starting a server.");

            Permission serverStop =
                new Permission(
                    "PERM_SERVER_STOP",
                    "Server.Stop",
                    "Allows stopping a server.");

            Permission clientConnect =
                new Permission(
                    "PERM_CLIENT_CONNECT",
                    "Client.Connect",
                    "Allows connecting a client to a server.");

            Permission clientSend =
                new Permission(
                    "PERM_CLIENT_SEND",
                    "Client.Send",
                    "Allows sending data through a client connection.");

            // -----------------------------------------------------
            // Administrator Role
            // -----------------------------------------------------

            Role administratorRole =
                new Role(
                    "ROLE_ADMIN",
                    "Administrator",
                    "Full system administration role.");

            administratorRole.AddPermission(
                serverCreate);

            administratorRole.AddPermission(
                serverStart);

            administratorRole.AddPermission(
                serverStop);

            administratorRole.AddPermission(
                clientConnect);

            administratorRole.AddPermission(
                clientSend);

            // -----------------------------------------------------
            // Developer Role
            // -----------------------------------------------------

            Role developerRole =
                new Role(
                    "ROLE_DEVELOPER",
                    "Developer",
                    "Software development role.");

            developerRole.AddPermission(
                clientConnect);

            developerRole.AddPermission(
                clientSend);

            // -----------------------------------------------------
            // Administrator User
            // -----------------------------------------------------

            User administrator =
                new User(
                    "USER_ADMIN",
                    "admin",
                    "System Administrator");

            administrator.Status =
                UserStatus.Active;

            administrator.AddRole(
                administratorRole);

            // -----------------------------------------------------
            // Developer User
            // -----------------------------------------------------

            User developer =
                new User(
                    "USER_DEVELOPER",
                    "developer",
                    "Software Developer");

            developer.Status =
                UserStatus.Active;

            developer.AddRole(
                developerRole);

            // -----------------------------------------------------
            // Disabled User
            // -----------------------------------------------------

            User disabled =
                new User(
                    "USER_DISABLED",
                    "disabled",
                    "Disabled User");

            disabled.Status =
                UserStatus.Disabled;

            disabled.AddRole(
                developerRole);

            // -----------------------------------------------------
            // Add users
            // -----------------------------------------------------

            _users.Add(administrator);
            _users.Add(developer);
            _users.Add(disabled);
        }

        // =========================================================
        // UI INITIALIZATION
        // =========================================================

        private void InitializeSecurityTestUI()
        {
            // -----------------------------------------------------
            // Users
            // -----------------------------------------------------

            cmbUser.Items.Clear();

            foreach (IUser user in _users)
            {
                cmbUser.Items.Add(user);
            }

            if (cmbUser.Items.Count > 0)
                cmbUser.SelectedIndex = 0;

            // -----------------------------------------------------
            // Operations
            // -----------------------------------------------------

            cmbOperation.Items.Clear();

            cmbOperation.Items.Add("Create");
            cmbOperation.Items.Add("Start");
            cmbOperation.Items.Add("Stop");
            cmbOperation.Items.Add("Connect");
            cmbOperation.Items.Add("Send");
            cmbOperation.Items.Add("Receive");

            if (cmbOperation.Items.Count > 0)
                cmbOperation.SelectedIndex = 0;

            // -----------------------------------------------------
            // Resources
            // -----------------------------------------------------

            cmbResource.Items.Clear();

            cmbResource.Items.Add("Server");
            cmbResource.Items.Add("Client");

            if (cmbResource.Items.Count > 0)
                cmbResource.SelectedIndex = 0;

            // -----------------------------------------------------
            // Initial result state
            // -----------------------------------------------------

            ClearResults();
        }

        // =========================================================
        // USER SELECTION
        // =========================================================

        private void cmbUser_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            IUser user =
                cmbUser.SelectedItem as IUser;

            if (user == null)
                return;

            // -----------------------------------------------------
            // Status
            // -----------------------------------------------------

            lblStatus.Text =
                user.Status.ToString();

            // -----------------------------------------------------
            // Roles
            // -----------------------------------------------------

            lstRoles.Items.Clear();

            foreach (IRole role in user.Roles)
            {
                if (role != null)
                    lstRoles.Items.Add(role);
            }

            // -----------------------------------------------------
            // Permissions
            // -----------------------------------------------------

            RefreshPermissions(user);

            // -----------------------------------------------------
            // Log
            // -----------------------------------------------------

            Log(
                "User selected: " +
                user.UserName);

            Log(
                "Status: " +
                user.Status);

            Log(
                "Roles: " +
                user.Roles.Count);

            Log(
                "--------------------------------");
        }

        private void cmbResource_SelectedIndexChanged(
    object sender,
    EventArgs e)
        {
            cmbOperation.Items.Clear();

            string resource =
                cmbResource.SelectedItem as string;

            if (resource == "Server")
            {
                cmbOperation.Items.Add("Create");
                cmbOperation.Items.Add("Start");
                cmbOperation.Items.Add("Stop");
            }
            else if (resource == "Client")
            {
                cmbOperation.Items.Add("Connect");
                cmbOperation.Items.Add("Send");
                cmbOperation.Items.Add("Receive");
            }

            if (cmbOperation.Items.Count > 0)
                cmbOperation.SelectedIndex = 0;
        }

        // =========================================================
        // REFRESH PERMISSIONS
        // =========================================================

        private void RefreshPermissions(
            IUser user)
        {
            lstPermissions.Items.Clear();

            if (user == null)
                return;

            foreach (IRole role in user.Roles)
            {
                if (role == null)
                    continue;

                foreach (IPermission permission
                    in role.Permissions)
                {
                    if (permission == null)
                        continue;

                    string text =
                        role.Name +
                        "  →  " +
                        permission.Name;

                    if (!lstPermissions.Items.Contains(text))
                        lstPermissions.Items.Add(text);
                }
            }
        }

        // =========================================================
        // CREATE SECURITY CONTEXT
        // =========================================================

        private SecurityContext CreateContext()
        {
            IUser user =
                cmbUser.SelectedItem as IUser;

            string operation =
                cmbOperation.SelectedItem as string;

            string resource =
                cmbResource.SelectedItem as string;

            SecurityContext context =
                new SecurityContext();

            context.User = user;
            context.Operation = operation;
            context.Resource = resource;

            // -----------------------------------------------------
            // Sandbox test mode
            // -----------------------------------------------------

            context.IsTestMode = true;

            return context;
        }

        // =========================================================
        // TEST IDENTITY
        // =========================================================

        private SecurityResult TestIdentity()
        {
            SecurityContext context =
                CreateContext();

            if (context == null)
            {
                return SecurityResult.Deny(
                    "TEST001",
                    "Security context could not be created.");
            }

            SecurityResult result =
                _securityAdapter.Authenticate(
                    context);

            if (result.Success)
            {
                lblIdentityResult.Text =
                    "Identity: PASS";

                Log(
                    "[IDENTITY] PASS");

                return result;
            }

            lblIdentityResult.Text =
                "Identity: DENIED";

            Log(
                "[IDENTITY] DENIED");

            Log(
                result.Code +
                " - " +
                result.Message);

            return result;
        }

        // =========================================================
        // TEST AUTHORIZATION
        // =========================================================

        private SecurityResult TestAuthorization()
        {
            SecurityContext context =
                CreateContext();

            if (context == null)
            {
                return SecurityResult.Deny(
                    "TEST002",
                    "Security context could not be created.");
            }

            SecurityResult identity =
                _securityAdapter.Authenticate(
                    context);

            if (!identity.Success)
            {
                lblAuthorizationResult.Text =
                    "Authorization: DENIED";

                Log(
                    "[AUTHORIZATION] DENIED - Identity failed");

                return identity;
            }

            SecurityResult authorization =
                _securityAdapter.Authorize(
                    context);

            if (authorization.Success)
            {
                lblAuthorizationResult.Text =
                    "Authorization: PASS";

                Log(
                    "[AUTHORIZATION] PASS");

                return authorization;
            }

            lblAuthorizationResult.Text =
                "Authorization: DENIED";

            Log(
                "[AUTHORIZATION] DENIED");

            Log(
                authorization.Code +
                " - " +
                authorization.Message);

            return authorization;
        }

        // =========================================================
        // TEST TRANSPORT
        // =========================================================

        private SecurityResult TestTransport()
        {
            SecurityContext context =
                CreateContext();

            if (context == null)
            {
                return SecurityResult.Deny(
                    "TEST003",
                    "Security context could not be created.");
            }

            SecurityResult result =
                _securityAdapter.ValidateTransport(
                    context);

            if (result.Success)
            {
                lblTransportResult.Text =
                    "Transport: PASS";

                Log(
                    "[TRANSPORT] PASS");

                return result;
            }

            lblTransportResult.Text =
                "Transport: DENIED";

            Log(
                "[TRANSPORT] DENIED");

            Log(
                result.Code +
                " - " +
                result.Message);

            return result;
        }

        // =========================================================
        // CHECK AUTHORIZATION BUTTON
        // =========================================================

        private void btnCheckAuthorization_Click(
            object sender,
            EventArgs e)
        {
            ClearResults();

            IUser user =
                cmbUser.SelectedItem as IUser;

            if (user == null)
            {
                Log(
                    "No user selected.");

                return;
            }

            string operation =
                cmbOperation.SelectedItem as string;

            string resource =
                cmbResource.SelectedItem as string;

            Log(
                "Authorization check");

            Log(
                "User: " +
                user.UserName);

            Log(
                "Operation: " +
                operation);

            Log(
                "Resource: " +
                resource);

            Log(
                "--------------------------------");

            SecurityResult identity =
                TestIdentity();

            if (!identity.Success)
            {
                lblFinalResult.Text =
                    "Final Result: DENIED";

                return;
            }

            SecurityResult authorization =
                _securityAdapter.Authorize(
                    CreateContext());

            if (authorization.Success)
            {
                lblAuthorizationResult.Text =
                    "Authorization: PASS";

                lblFinalResult.Text =
                    "Final Result: ALLOWED";

                Log(
                    "[FINAL] ALLOWED");
            }
            else
            {
                lblAuthorizationResult.Text =
                    "Authorization: DENIED";

                lblFinalResult.Text =
                    "Final Result: DENIED";

                Log(
                    "[FINAL] DENIED");

                Log(
                    authorization.Code +
                    " - " +
                    authorization.Message);
            }
        }

        // =========================================================
        // IDENTITY BUTTON
        // =========================================================

        private void btnTestIdentity_Click(
            object sender,
            EventArgs e)
        {
            ClearResults();

            SecurityResult result =
                TestIdentity();

            lblFinalResult.Text =
                result.Success
                    ? "Final Result: ALLOWED"
                    : "Final Result: DENIED";
        }

        // =========================================================
        // AUTHORIZATION BUTTON
        // =========================================================

        private void btnTestAuthorization_Click(
            object sender,
            EventArgs e)
        {
            ClearResults();

            SecurityResult result =
                TestAuthorization();

            lblFinalResult.Text =
                result.Success
                    ? "Final Result: ALLOWED"
                    : "Final Result: DENIED";
        }

        // =========================================================
        // TRANSPORT BUTTON
        // =========================================================

        private void btnTestTransport_Click(
            object sender,
            EventArgs e)
        {
            ClearResults();

            SecurityResult result =
                TestTransport();

            lblFinalResult.Text =
                result.Success
                    ? "Final Result: ALLOWED"
                    : "Final Result: DENIED";
        }

        // =========================================================
        // FULL TEST
        // =========================================================

        private void btnRunFullTest_Click(
            object sender,
            EventArgs e)
        {
            ClearResults();

            Log(
                "================================");

            Log(
                "FULL SECURITY TEST");

            Log(
                "================================");

            IUser user =
                cmbUser.SelectedItem as IUser;

            if (user == null)
            {
                Log(
                    "No user selected.");

                return;
            }

            Log(
                "User: " +
                user.UserName);

            Log(
                "Operation: " +
                cmbOperation.SelectedItem);

            Log(
                "Resource: " +
                cmbResource.SelectedItem);

            Log(
                "--------------------------------");

            // -----------------------------------------------------
            // 1. Identity
            // -----------------------------------------------------

            SecurityResult identity =
                TestIdentity();

            if (!identity.Success)
            {
                lblFinalResult.Text =
                    "Final Result: DENIED";

                Log(
                    "Full test stopped at Identity.");

                return;
            }

            // -----------------------------------------------------
            // 2. Authorization
            // -----------------------------------------------------

            SecurityResult authorization =
                TestAuthorization();

            if (!authorization.Success)
            {
                lblFinalResult.Text =
                    "Final Result: DENIED";

                Log(
                    "Full test stopped at Authorization.");

                return;
            }

            // -----------------------------------------------------
            // 3. Transport
            // -----------------------------------------------------

            SecurityResult transport =
                TestTransport();

            if (!transport.Success)
            {
                lblFinalResult.Text =
                    "Final Result: DENIED";

                Log(
                    "Full test stopped at Transport.");

                return;
            }

            // -----------------------------------------------------
            // Final
            // -----------------------------------------------------

            lblFinalResult.Text =
                "Final Result: ALLOWED";

            Log(
                "================================");

            Log(
                "[FINAL] ALLOWED");

            Log(
                "================================");
        }

        // =========================================================
        // CLEAR RESULTS
        // =========================================================

        private void ClearResults()
        {
            lblIdentityResult.Text =
                "Identity: -";

            lblAuthorizationResult.Text =
                "Authorization: -";

            lblTransportResult.Text =
                "Transport: -";

            lblFinalResult.Text =
                "Final Result: -";

            if (txtSecurityLog != null)
                txtSecurityLog.Clear();
        }

        // =========================================================
        // LOG
        // =========================================================

        private void Log(
            string message)
        {
            if (txtSecurityLog == null)
                return;

            txtSecurityLog.AppendText(
                DateTime.Now.ToString(
                    "HH:mm:ss") +
                "  " +
                message +
                Environment.NewLine);

            txtSecurityLog.SelectionStart =
                txtSecurityLog.Text.Length;

            txtSecurityLog.ScrollToCaret();
        }
    }
}