using Microsoft.Data.SqlClient;

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
                    else if (dbHash != password)
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
    }
}
  