namespace SharpMinds.SoftwarePattern.Factory.Notifications;

public class PushNotificationService: INotificationService
{
    public void Send()
    {
        Console.WriteLine("Send Push Notification");
    }
}