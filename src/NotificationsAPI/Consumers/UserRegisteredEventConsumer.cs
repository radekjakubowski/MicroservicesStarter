using Common;
using MassTransit;

namespace NotificationsAPI;

public class UserRegisteredEventConsumer : IConsumer<UserRegisteredEvent>
{
  public UserRegisteredEventConsumer()
  {
  }

  public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
  {
    Console.WriteLine(context.Message.Email);
    // could do anything here - send email or sms or whatever with confirmation link
    await context.ConsumeCompleted;
  }
}
