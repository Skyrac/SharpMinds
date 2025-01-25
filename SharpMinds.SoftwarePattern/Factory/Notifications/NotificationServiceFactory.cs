namespace SharpMinds.SoftwarePattern.Factory.Notifications;

public static  class NotificationServiceFactory
{
    public static INotificationService CreateNotificationService(NotificationType notificationType)
    {
        return notificationType switch
        {
            NotificationType.Email => new EmailNotificationService(),
            NotificationType.SMS => new SmsNotificationService(),
            NotificationType.Push => new PushNotificationService(),
            _ => throw new ArgumentOutOfRangeException(nameof(notificationType), notificationType, null)
        };
    }
}

public enum NotificationType
{
    SMS,
    Email,
    Push
}