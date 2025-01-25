namespace SharpMinds.SoftwarePattern.Factory.Notifications;

public class SmsNotificationService: INotificationService
{
    public void Send()
    {
        Console.WriteLine("Send SMS");
    }
}