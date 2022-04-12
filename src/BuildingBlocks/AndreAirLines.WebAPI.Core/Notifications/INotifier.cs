using System.Collections.Generic;

namespace AndreAirLines.WebAPI.Core.Notifications
{
    public interface INotifier
    {
        void Handle(string notification);
        IEnumerable<string> GetNotifications();
        bool IsNotified();
        void Clear();
    }
}
