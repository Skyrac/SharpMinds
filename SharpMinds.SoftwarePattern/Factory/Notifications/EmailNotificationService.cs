namespace SharpMinds.SoftwarePattern.Factory.Notifications;

public class EmailNotificationService : INotificationService
{
    public void Send()
    {
        Console.WriteLine("Send Email");
    }
}