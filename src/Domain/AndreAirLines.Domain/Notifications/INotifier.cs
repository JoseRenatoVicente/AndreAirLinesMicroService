using System.Collections.Generic;

namespace AndreAirLines.Domain.Notifications
{
    public interface INotifier
    {
        void Handle(string notification);
        IEnumerable<string> GetNotifications();
        bool IsNotified();
        void Clear();
    }
}
