using Notif_BLL;
using Notif_DLL;
using Notif_Models;
using System;
using System.Collections.Generic;

namespace NotificationInterface
{
    internal class Program
    {
        public static void ViewNotifications(List<Notification> notifications)
        {
            if (notifications.Count == 0)
            {
                Console.WriteLine("No notifications found.");
            }
            else
            {
                Console.WriteLine("List of Notifications:");
                foreach (var notif in notifications)
                    
                {
                    Console.WriteLine($"Student ID: {notif.StudentID}");
                    Console.WriteLine($"Sender: {notif.StudentS}");
                    Console.WriteLine($"Reciever: {notif.StudentR}");

                    Console.WriteLine($"Content: {notif.Content}");
                    Console.WriteLine($"Date Modified: {notif.DateModified}");
                    Console.WriteLine($"Is Read: {notif.IsRead}");
                    Console.WriteLine("-------------------------");
                }
            }
        }
        static void Main(string[] args)
        {
            SqlNotif sqlNotif = new SqlNotif();
            NotificationManagement notificationManagement = new NotificationManagement(); // Instantiate NotificationManagement

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Send Notification");
                Console.WriteLine("2. View List of Notifications");
                Console.WriteLine("3. Delete Notification");
                Console.WriteLine("4. Exit");

                Console.Write("Enter the number of your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter student ID: ");
                        string studentId = Console.ReadLine();

                        Console.Write("Enter sender name: ");
                        string senderName = Console.ReadLine();

                        Console.Write("Enter receiver name: ");
                        string receiverName = Console.ReadLine();

                        Console.Write("Enter notification content: ");
                        string content = Console.ReadLine();

                        if (!string.IsNullOrEmpty(senderName))
                        {
                            notificationManagement.SendNotification(senderName, receiverName, content, studentId);
                        }
                        else
                        {
                            // Handle the case when senderName is empty or whitespace.
                            Console.WriteLine("Invalid sender name. Please try again.");
                            continue; // Go back to the beginning of the loop to try again.
                        }

                        var notification = new Notification
                        {
                            StudentID = studentId,
                            senderName = new User { Name = senderName },
                            receiverName = new User { Name = receiverName },
                            Content = content,
                            DateModified = DateTime.Now,
                            IsRead = false
                        };
                        sqlNotif.StoreNotifications(notification);

                        Console.WriteLine("\nNotification sent and saved successfully!\n");
                        break;

                    case "2":
                        List<Notification> storedNotifications = sqlNotif.GetSaveNotifications();
                        ViewNotifications(storedNotifications);
                        break;

                    case "3":
                        Console.Write("Enter student ID to delete: ");
                        string studentIdToDelete = Console.ReadLine();

                        sqlNotif.DeleteNotification(studentIdToDelete);
                        Console.WriteLine("Notification deleted successfully!\n");
                        break;

                    case "4":
                        Console.WriteLine("Exiting the program...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
