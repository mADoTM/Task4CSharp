using System.Drawing;

namespace Domain
{
    public abstract class ModelElement
    {
        public Point Point { get; set; }

        protected bool IsLocked { get; set; }

        protected bool IsStopped { get; }

        protected Action<string> Notification { get; }

        protected ModelElement(Action<string> notification)
        {
            this.IsLocked = false;
            this.IsStopped = false;
            this.Notification = notification;
        }

        public abstract void Start();
    }
}