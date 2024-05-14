using System.Drawing;

namespace Domain
{
    public class OneWayPath : ElemTrace
    {
        public bool IsFinished { get; private set; }

        public OneWayPath(Point fromPoint, Point toPoint) : base(fromPoint, toPoint)
        {
            IsFinished = false;
        }

        public override Point NextPoint(Point point)
        {
            var signX = Math.Sign(toPoint.X - fromPoint.X);
            var signY = Math.Sign(toPoint.Y - fromPoint.Y);

            var newX = point.X + signX * stepX;
            var newY = point.Y + signY * stepY;

            var newPoint = new Point(newX, newY);
            
            IsFinished = (Distance(newPoint, toPoint) < (stepX + stepY));
            return newPoint;
        }
    }
}
