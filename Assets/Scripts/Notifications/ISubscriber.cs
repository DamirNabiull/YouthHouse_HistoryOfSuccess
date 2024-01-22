using UnityEngine;

namespace Notifications
{
    public interface ISubscriber
    {
        public void Notify()
        {
            MakeNotificationAction();
        }

        void MakeNotificationAction();
    }
}