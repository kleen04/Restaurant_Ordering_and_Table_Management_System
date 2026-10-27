using System;
using System.Data;
using System.Net.Mail;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using Restaurant_Ordering_and_Management_System.DBContext;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Service
{
    /// <summary>
    /// The one place where concrete classes are created and wired together
    /// (composition root). Forms receive the INTERFACES they need through their
    /// constructors and never create a DatabaseConnection, DbHelper or service
    /// themselves, so each form depends on abstractions only.
    /// </summary>
    public class AppServices
    {
        public ITableService Tables { get; }
        public IOrderService Orders { get; }
        public IMenuService Menu { get; }
        public IStaffService Staff { get; }
        public IInventoryService Inventory { get; }
        public IReportService Reports { get; }
        public IAccountService Accounts { get; }

        public AppServices(ITableService tables, IOrderService orders, IMenuService menu,
                           IStaffService staff, IInventoryService inventory, IReportService reports,
                           IAccountService accounts)
        {
            Tables = tables;
            Orders = orders;
            Menu = menu;
            Staff = staff;
            Inventory = inventory;
            Reports = reports;
            Accounts = accounts;
        }

        /// <summary>Builds the real, database-backed services once at start-up.</summary>
        public static AppServices CreateDefault()
        {
            DbHelper dbHelper = new DbHelper(new DatabaseConnection());
            TableService tables = new TableService(dbHelper);

            return new AppServices(
                tables,
                new OrderService(dbHelper, tables),
                new MenuService(dbHelper),
                new StaffService(dbHelper),
                new InventoryService(dbHelper),
                new ReportService(dbHelper),
                new AccountService(dbHelper));
        }
    }

    public class AccountService : IAccountService
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 150000;
        private readonly DbHelper _dbHelper;

        public AccountService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        public bool HasAccounts()
        {
            DataTable result = _dbHelper.ExecuteQuery("sp_Account_HasAny");
            if (result.Rows.Count == 0)
            {
                throw new InvalidOperationException("The account availability query returned no result.");
            }
            return Convert.ToInt32(result.Rows[0]["AccountCount"]) > 0;
        }

        public AuthenticatedUser Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            DataTable result = _dbHelper.ExecuteQuery("sp_Account_GetByUsername",
                new MySqlParameter("@p_Username", username.Trim()));
            if (result.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = result.Rows[0];
            byte[] salt = (byte[])row["PasswordSalt"];
            byte[] expectedHash = (byte[])row["PasswordHash"];
            byte[] actualHash = HashPassword(password, salt);
            if (!FixedTimeEquals(actualHash, expectedHash))
            {
                return null;
            }

            return new AuthenticatedUser
            {
                UserId = Convert.ToInt32(row["UserID"]),
                Username = row["Username"].ToString(),
                Email = row["Email"].ToString(),
                Role = row["Role"].ToString()
            };
        }

        public void RegisterFirstAdmin(string username, string email, string password)
        {
            username = (username ?? string.Empty).Trim();
            email = (email ?? string.Empty).Trim();
            ValidateCredentials(username, email, password);

            byte[] salt = new byte[SaltSize];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            byte[] passwordHash = HashPassword(password, salt);
            try
            {
                _dbHelper.ExecuteNonQuery("sp_Account_CreateFirstAdmin",
                    new MySqlParameter("@p_Username", username),
                    new MySqlParameter("@p_Email", email),
                    new MySqlParameter("@p_PasswordSalt", salt),
                    new MySqlParameter("@p_PasswordHash", passwordHash));
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    throw new InvalidOperationException("That username or email is already in use.", ex);
                }
                throw;
            }
        }

        private static void ValidateCredentials(string username, string email, string password)
        {
            if (username.Length < 3 || username.Length > 32)
            {
                throw new ArgumentException("Username must be between 3 and 32 characters.", nameof(username));
            }
            foreach (char character in username)
            {
                if (!char.IsLetterOrDigit(character) && character != '.' && character != '_' && character != '-')
                {
                    throw new ArgumentException("Username may contain letters, numbers, periods, underscores, and hyphens only.", nameof(username));
                }
            }
            if (string.IsNullOrWhiteSpace(email) || email.Length > 254)
            {
                throw new ArgumentException("Enter a valid email address.", nameof(email));
            }
            try
            {
                MailAddress address = new MailAddress(email);
                if (!string.Equals(address.Address, email, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException("Enter a valid email address.", nameof(email));
                }
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Enter a valid email address.", nameof(email), ex);
            }
            if (string.IsNullOrEmpty(password) || password.Length < 12 || password.Length > 128)
            {
                throw new ArgumentException("Password must be between 12 and 128 characters long.", nameof(password));
            }
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(
                password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                return deriveBytes.GetBytes(HashSize);
            }
        }

        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            if (left == null || right == null || left.Length != right.Length)
            {
                return false;
            }

            int difference = 0;
            for (int i = 0; i < left.Length; i++)
            {
                difference |= left[i] ^ right[i];
            }
            return difference == 0;
        }
    }
}
