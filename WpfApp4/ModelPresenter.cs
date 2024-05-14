using System.Windows.Media;

namespace WpfApp4
{
    public class ModelPresenter
    {
        private readonly Graphics _graphics;
        private readonly List<VisualElement> _elems;
        private readonly System.Windows.Forms.Timer _timer;

        // public ModelPresenter(PictureBox pictureBox, List<VisualElement> elems)
        public ModelPresenter(PictureBox pictureBox, List<VisualElement> elems)
        {
            Bitmap bitmap = new(950, 800);
            _graphics = Graphics.FromImage(bitmap);

            _timer = new System.Windows.Forms.Timer
            {
                Interval = 30
            };
            

            this._elems = elems;
            _timer.Tick += (_, _) =>
            {
                _graphics.Clear(pictureBox.BackColor);
                foreach (var elem in this._elems)
                {
                    Draw(elem);
                }

                pictureBox.Image = bitmap;
            };
        }

        public void AddVisualElem(VisualElement elem)
        {
            lock (_elems)
            {
                _elems.Add(elem);
            }
        }

        private void Draw(VisualElement elem)
        {
            _graphics.DrawImage(
                elem.Image,
                elem.ModelElement.Point.X - elem.Image.Width / 2,
                elem.ModelElement.Point.Y - elem.Image.Height / 2
            );
        }

        public void Start()
        {
            _timer.Start();
        }
    }
}