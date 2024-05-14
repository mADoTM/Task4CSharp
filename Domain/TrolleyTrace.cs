using System.Drawing;

namespace Domain
{
    public class TrolleyTrace : ElemTrace
    {
        private bool _isMovingRight;

        public TrolleyTrace(Point fromPoint, Point toPoint) : base(fromPoint, toPoint)
        {
            _isMovingRight = true;
            stepCount = 500;
        }

        public override Point NextPoint(Point point)
        {
            var signX = Math.Sign(toPoint.X - fromPoint.X);
            var signY = Math.Sign(toPoint.Y - fromPoint.Y);
            if (!_isMovingRight)
            {
                signX = -signX;
                signY = -signY;
            }
            var newX = point.X + signX * stepX;
            var newY = point.Y + signY * stepY;
            var newPoint = new Point(newX, newY);

            _isMovingRight = _isMovingRight switch
            {
                true when Distance(newPoint, toPoint) < (stepX + stepY) => false,
                false when Distance(newPoint, fromPoint) < (stepX + stepY) => true,
                _ => _isMovingRight
            };

            return newPoint;
        }
    }
}
