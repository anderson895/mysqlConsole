using Notif_Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Notif_DLL
{
    public class SqlNotif
    {
        static string connectionString = "Data Source=DESKTOP-5423UT7;Initial Catalog=notifdatabase;Integrated Security=True";




        // Add a constructor to initialize the UserRepository
        private readonly UserRepository userRepository;

        public SqlNotif()
        {
            userRepository = new UserRepository();
        }

        public List<Notification> GetSaveNotifications()
        {
            List<Notification> storedNotifications = new List<Notification>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    var selectStatement = "SELECT StudentID, senderName, receiverName, Content, DateTime, IsRead FROM tblNotification";
                    SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        var studentID = reader.GetString(0);
                        var studentS = reader.GetString(1);
                        var studentR = reader.GetString(2);


                        var senderName = reader.GetString(1);
                        var receiverName = reader.GetString(2);
                        var content = reader.GetString(3);
                        var isRead = reader.GetBoolean(5);

                        // Retrieve user information from the UserRepository
                        var sender = userRepository.GetUserByName(senderName);
                        var receiver = userRepository.GetUserByName(receiverName);

                        DateTime dateModified;
                        if (reader.IsDBNull(4))
                        {
                            dateModified = DateTime.MinValue;
                        }
                        else
                        {
                            dateModified = reader.GetDateTime(4);
                        }

                        storedNotifications.Add(new Notification
                        {
                            StudentID = studentID,
                            StudentS = studentS,
                            StudentR = studentR,
                            senderName = sender,
                            receiverName = receiver,
                            Content = content,
                            DateModified = dateModified,
                            IsRead = isRead
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to the database: {ex.Message}");
                }
            }

            return storedNotifications;
        }

        public void StoreNotifications(Notification storenotifications)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var insertStatement = "INSERT INTO tblNotification (StudentID, senderName, receiverName, Content, DateTime, IsRead) " +
                                      "VALUES (@StudentID, @senderName, @receiverName, @Content, @DateTime, @IsRead)";
                SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);
                insertCommand.Parameters.AddWithValue("@StudentID", storenotifications.StudentID);
                insertCommand.Parameters.AddWithValue("@senderName", storenotifications.senderName.Name);
                insertCommand.Parameters.AddWithValue("@receiverName", storenotifications.receiverName.Name);
                insertCommand.Parameters.AddWithValue("@Content", storenotifications.Content);
                insertCommand.Parameters.AddWithValue("@DateTime", storenotifications.DateModified);
                insertCommand.Parameters.AddWithValue("@IsRead", storenotifications.IsRead);

                sqlConnection.Open();

                insertCommand.ExecuteNonQuery();
            }
        }

        public void DeleteNotification(string studentId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var deleteStatement = "DELETE FROM tblNotification WHERE StudentID = @StudentID";
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
                deleteCommand.Parameters.AddWithValue("@StudentID", studentId);

                try
                {
                    sqlConnection.Open();
                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Notification deleted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Notification not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting notification: {ex.Message}");
                }
            }
        }

    }
}
