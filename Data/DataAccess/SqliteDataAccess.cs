using BeyondDisabilityBlazorApp.Data.Models;
using Dapper;
using System;
using System.Data.SQLite;

namespace BeyondDisabilityBlazorApp.Data.DataAccess
{
    public class SqliteDataAccess
    {
        public static int CurrentUserId { get; set; } = -1;
        public static string CurrentUserName { get; set; }

        //Allows to connect C# code to SQLite database
        private static string _connectionString = @"Data Source=.\BeyondDisability.db;Version=3;";

        //UserTable operations
        public static List<UserModel> LoadUsers()
        {
            using (var connect = new SQLiteConnection(_connectionString))
            {
                var query = "SELECT * FROM User";
                var result = connect.Query<UserModel>(query, new DynamicParameters());

                return result.ToList();
            }

        }
        public static void SaveUser(UserModel userModel)
        {
            using (var connect = new SQLiteConnection(_connectionString))
            {
                var command = "INSERT INTO User (Email, Password, Role) VALUES (@Email, @Password, @Role);";
                connect.Execute(command, userModel);    
            }
        }

        //ReminderTable operations
        public static List<ReminderModel> LoadReminders()
        {
            using (var connect = new SQLiteConnection(_connectionString))
            {
                var query = $"SELECT * FROM Reminder WHERE UserId = {CurrentUserId}";
                var result = connect.Query<ReminderModel>(query, new DynamicParameters());

                return result.ToList();
            }

        }
        public static void SaveReminder(ReminderModel reminderModel)
        {
            reminderModel.UserId = CurrentUserId;

            using (var connect = new SQLiteConnection(_connectionString))
            {
                var command = "INSERT INTO Reminder (UserId, Title, Date) VALUES (@UserId, @Title, @Date);";
                connect.Execute(command, reminderModel);
            }

        }

        public static void DeleteReminder(int reminderId)
        {
            using (var connect = new SQLiteConnection(_connectionString))
            {
                var command = $"DELETE FROM Reminder WHERE Id = {reminderId};";
                connect.Execute(command);
            }
        }

        //GroceryPal Table operations
        public static List<GroceryPalModel> LoadGroceryPals()
        {
            using (var connect = new SQLiteConnection(_connectionString))
            {
                var query = "SELECT * FROM GroceryPal";
                var result = connect.Query<GroceryPalModel>(query, new DynamicParameters());

                return result.ToList();
            }
        }
       
        public static void SaveGroceryPal(GroceryPalModel groceryPalModel)
        {
            groceryPalModel.UserId = CurrentUserId;

            using (var connect = new SQLiteConnection(_connectionString))
            {
                var command = "INSERT INTO GroceryPal (UserId, Name, Contact, Pricing, Details) VALUES (@UserId, @Name, @Contact, @Pricing, @Details);";
                connect.Execute(command, groceryPalModel);
            }
        }

        public static void UpdateGroceryPal(GroceryPalModel groceryPalModel)
        {
            using (var connect = new SQLiteConnection(_connectionString))
            {
                var command = "UPDATE GroceryPal SET Name = @Name, Contact = @Contact, Pricing = @Pricing, Details = @Details WHERE UserId = @UserId AND Id = @Id;";
                connect.Execute(command, groceryPalModel);
            }
        }

        //EmergencyNoticeTable operations
        public static List<EmergencyNoticeModel> LoadEmergencyNotices()
        {
            DeleteOldEmergencyNotices();

            using (var connect = new SQLiteConnection(_connectionString))
            {
                var query = "SELECT * FROM EmergencyNotice";
                var result = connect.Query<EmergencyNoticeModel>(query, new DynamicParameters());

                return result.ToList();
            }

        }
        public static void SaveEmergencyNotice(EmergencyNoticeModel saveModel)
        {
            saveModel.UserName = CurrentUserName;

            using (var connect = new SQLiteConnection(_connectionString))
            {
                var command = "INSERT INTO EmergencyNotice (UserName, Lattitude, Longitude, Date) VALUES (@UserName, @Lattitude, @Longitude, @Date);";
                connect.Execute(command, saveModel);
            }
        }
        public static void DeleteEmergencyNotice(int noticeId)
        {
            using (var connect = new SQLiteConnection(_connectionString))
            {
                var command = $"DELETE FROM EmergencyNotice WHERE Id = {noticeId};";
                connect.Execute(command);
            }
        }

        public static void DeleteOldEmergencyNotices()
        {
            using (var connect = new SQLiteConnection(_connectionString))
            {
                var command = "DELETE FROM EmergencyNotice WHERE Date <= DATETIME('now', '-1 day')";
                connect.Execute(command);
            }
        }
    }
}
