using System.Windows.Controls;
using Domain;
using Image = System.Drawing.Image;

namespace WpfApp4
{
    public class VisualElement
    {
        public Image Image { get; } 
        public ModelElement ModelElement { get; }

        public VisualElement(ModelElement modelElem, Image image)
        {
            this.Image = image;
            this.ModelElement = modelElem;
        }
    }
}
