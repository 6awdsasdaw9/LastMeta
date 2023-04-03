namespace Code.UI.Interfaces
{
    public interface IHUDWindow
    {
        public bool ActiveSelf { get; }

        public void ShowOrHide();
        public void Show();
        public void Hide();
    }
}