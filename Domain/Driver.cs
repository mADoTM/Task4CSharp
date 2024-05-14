using System.Drawing;

namespace Domain
{
    public class Driver : ModelActingElement
    {
        private string Name { get; }
        
        private readonly Trolleybus _trolleybus;

        public Driver(string name, Trolleybus trolleybus, Action<string> notification) : base(notification)
        {
            this.Name = name;
            this._trolleybus = trolleybus;
            this.Point = GetTrolleyPoint();
        }

        protected override void CheckEvents()
        {
            if (!IsLocked && _trolleybus.IsDisconnected)
            {
                IsLocked = true;
                Notification($"Водитель {Name} пошёл присоединять троллейбус номер {_trolleybus.Number} к напряжению");
                ElemAction = ConnectTrolleyOfBusToElectricity;
            }
            else
            {
                this.Point = GetTrolleyPoint();
            }
        }

        private void ConnectTrolleyOfBusToElectricity()
        {
            lock (_trolleybus)
            {
                var trace = new OneWayPath(this.Point, new Point(this.Point.X + 150, this.Point.Y + 50));
                while (!trace.IsFinished)
                {
                    this.Point = trace.NextPoint(this.Point);
                    Task.Delay(200).Wait();
                }
                
                trace = new OneWayPath(this.Point, GetTrolleyPoint());
                while (!trace.IsFinished)
                {
                    this.Point = trace.NextPoint(this.Point);
                    Task.Delay(200).Wait();
                }
                _trolleybus.ConnectToElectricity();
            }
            
            IsLocked = false;
            ElemAction = null;
        }

        private Point GetTrolleyPoint()
        {
            return new Point(_trolleybus.Point.X, _trolleybus.Point.Y + 70);
        }
    }
}
