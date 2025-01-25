using SharpMinds.SoftwarePattern.Factory.Notifications;

namespace SharpMinds.Unit.Tests.SoftwarePattern.Factory;

public class NotificationServiceFactoryTests
{
    [Test]
    [Arguments(NotificationType.Email, typeof(EmailNotificationService))]
    [Arguments(NotificationType.SMS, typeof(SmsNotificationService))]
    [Arguments(NotificationType.Push, typeof(PushNotificationService))]
    public async Task
        CreateNotificationService_ShouldReturnRelevantNotificationService_WhenTypeRelatedNotificationType(
            NotificationType notificationType, 
            Type serviceType)
    {
        var service = NotificationServiceFactory.CreateNotificationService(notificationType);
        await Assert.That(service.GetType()).IsEqualTo(serviceType);
    }
}