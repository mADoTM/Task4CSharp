namespace Domain
{
    public abstract class ModelActingElement : ModelElement
    {
        protected Action? ElemAction;
        
        protected abstract void CheckEvents();
        
        protected ModelActingElement(Action<string> notification) : base(notification)
        {
            ElemAction = null;
        }
        public override void Start()
        {
            while (!IsStopped)
            {
                CheckEvents();
                ElemAction?.Invoke();
                Task.Delay(30).Wait();
            }
        }
    }
}
