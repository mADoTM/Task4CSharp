using System.Drawing;

namespace Domain
{
    public class EmergencyService : ModelActingElement
    {
        public List<Trolleybus> Trolleys { get; }

        private Trolleybus? _brokenTrolley;

        public EmergencyService(Action<string> notification, List<Trolleybus> trolleys, Point point) : base(notification)
        {
            this.Point = point;
            this.Trolleys = trolleys;
        }

        private void RepairTrolley()
        {
            var defaultPoint = this.Point;
            OneWayPath path;
            
            if (_brokenTrolley != null)
                lock (Trolleys)
                {
                    path = new OneWayPath(this.Point, new Point(_brokenTrolley.Point.X + 100, _brokenTrolley.Point.Y + 50));
                    while (!path.IsFinished)
                    {
                        this.Point = path.NextPoint(this.Point);
                        Task.Delay(200).Wait();
                    }
                    _brokenTrolley.Repair();
                }
            path = new OneWayPath(this.Point, defaultPoint);
            while (!path.IsFinished)
            {
                this.Point = path.NextPoint(this.Point);
                Task.Delay(200).Wait();
            }
            IsLocked = false;
            ElemAction = null;
        }

        protected override void CheckEvents()
        {
            if (IsLocked) return;
            lock (Trolleys)
            {
                _brokenTrolley = Trolleys.FirstOrDefault
                    (trolley => trolley is { IsBroken: true, EmergencyAppointed: false });
            }

            if (_brokenTrolley == null) return;
            _brokenTrolley.EmergencyAppointed = true;
            IsLocked = true;
            Notification($"Аварийная служба отправлена на ремонт троллейбуса номер {_brokenTrolley.Number}");
            ElemAction = RepairTrolley;
        }
    }
}
