using Microsoft.Data.SqlClient;
using Personal_Investment_App.DatabaseConnection;
using System.Security.Cryptography;
using System.Text;

namespace DatabaseConnection
{
    public class DatabaseManager
    {
        private SqlConnection connection = new SqlConnection(
            @"Data Source=(local)\SQLEXPRESS;
          Initial Catalog=""Personal Investments"";
          Integrated Security=True;
          Encrypt=False");

        private string? activeUser = null;

        public LoginResult Login(string username, string password)
        {
            var result = new LoginResult();

            try
            {
                connection.Open();
                using (var command = new SqlCommand(
                    "SELECT password_hash FROM dbo.users WHERE username = @u",
                    connection))
                {
                    command.Parameters.AddWithValue("@u", username);
                    var dbHash = command.ExecuteScalar() as string;

                    if (dbHash == null)
                    {
                        result.Success = false;
                        result.Code = ErrorCode.UserNotFound;
                        result.ErrorMessage = "Nie ma takiego użytkownika.";
                    }
                    else
                    {
                        string inputPasswordHash = HashPassword(password);

                        if (dbHash != inputPasswordHash)
                        {
                            result.Success = false;
                            result.Code = ErrorCode.InvalidPassword;
                            result.ErrorMessage = "Nieprawidłowe hasło.";
                        }
                        else
                        {
                            result.Success = true;
                            result.Code = ErrorCode.None;
                            activeUser = username;
                        }
                    }
                }
            }
            catch (SqlException)
            {
                result.Success = false;
                result.Code = ErrorCode.DbUnavailable;
                result.ErrorMessage = "Błąd połączenia z bazą danych.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Code = ErrorCode.UnknownError;
                result.ErrorMessage = "Nieoczekiwany błąd: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public RegisterResult Register(string username, string password, string email)
        {
            var result = new RegisterResult();

            try
            {
                connection.Open();

                using (var command = new SqlCommand(
                    "SELECT COUNT(*) FROM dbo.users WHERE username = @u OR email = @e",
                    connection))
                {
                    command.Parameters.AddWithValue("@u", username);
                    command.Parameters.AddWithValue("@e", email);
                    int userExists = (int)command.ExecuteScalar();

                    if (userExists > 0)
                    {
                        result.Success = false;
                        result.Code = ErrorCode.UserAlreadyExists;
                        result.ErrorMessage = "Użytkownik o tej nazwie lub e-mailu już istnieje.";
                        return result;
                    }
                }

                string hashedPassword = HashPassword(password);

                using (var command = new SqlCommand(
                    "INSERT INTO dbo.users (username, password_hash, email) VALUES (@u, @p, @e)",
                    connection))
                {
                    command.Parameters.AddWithValue("@u", username);
                    command.Parameters.AddWithValue("@p", hashedPassword);
                    command.Parameters.AddWithValue("@e", email);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Success = true;
                        result.Code = ErrorCode.None;
                    }
                    else
                    {
                        result.Success = false;
                        result.Code = ErrorCode.UnknownError;
                        result.ErrorMessage = "Wystąpił problem podczas rejestracji użytkownika.";
                    }
                }
            }
            catch (SqlException)
            {
                result.Success = false;
                result.Code = ErrorCode.DbUnavailable;
                result.ErrorMessage = "Błąd połączenia z bazą danych.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Code = ErrorCode.UnknownError;
                result.ErrorMessage = "Nieoczekiwany błąd: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
  