using System.Drawing;

namespace Domain
{
    public class Trolleybus : ModelElement
    {
        public bool IsBroken { get; private set; }
        public bool IsDisconnected { get; private set; }
        public bool EmergencyAppointed { get; set; }

        public int Number { get; }

        private readonly TrolleyTrace _trace;

        public Trolleybus(int number, Action<string> notification, Point point, TrolleyTrace trace) : base(notification)
        {
            Point = point;
            IsBroken = false;
            IsDisconnected = false;
            Number = number;
            _trace = trace;
        }

        public override void Start()
        {
            var random = new Random();
            while (!IsStopped)
            {
                Task.Delay(200).Wait();
                if (!IsLocked && !IsBroken && !IsDisconnected)
                    RandomBreak(random);
                if (!IsLocked && !IsBroken && !IsDisconnected)
                    RandomDisconnectFromElectricity(random);
                if (!IsLocked && !IsBroken && !IsDisconnected)
                    Move();
            }
        }

        public void Repair()
        {
            IsBroken = false;
            IsLocked = false;
            EmergencyAppointed = false;
            Notification($"Троллейбус номер {Number} отремонтирован");
        }

        public void ConnectToElectricity()
        {
            IsDisconnected = false;
            IsLocked = false;
            Notification($"Троллейбус номер {Number} подключен к напряжению");
        }

        private void Move()
        {
            this.Point = _trace.NextPoint(this.Point);
        }

        private void RandomBreak(Random random)
        {
            if (IsLocked || random.Next(0, 10) >= 1) return;
            IsBroken = true;
            IsLocked = true;
            Notification($"Троллейбус номер {Number} сломан");
        }

        private void RandomDisconnectFromElectricity(Random random)
        {
            if (IsLocked || random.Next(0, 10) >= 1) return;
            IsDisconnected = true;
            IsLocked = true;
            Notification($"Троллейбус номер {Number} отсоединен от напряжения");
        }
    }
}
