using System.Drawing;

namespace Domain
{
    public abstract class ElemTrace
    {
        protected Point fromPoint;
        protected Point toPoint;

        protected readonly int stepX;
        protected readonly int stepY;

        protected int stepCount;

        public ElemTrace(Point fromPoint, Point toPoint)
        {
            this.fromPoint = fromPoint;
            this.toPoint = toPoint;
            this.stepCount = 15;
            this.stepX = Math.Abs(fromPoint.X - toPoint.X) / stepCount;
            this.stepY = Math.Abs(fromPoint.Y - toPoint.Y) / stepCount;
        }

        public abstract Point NextPoint(Point point);

        protected static double Distance(Point p1, Point p2)
        {
            return Math.Round(Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2)), 1);
        }
    }
}
