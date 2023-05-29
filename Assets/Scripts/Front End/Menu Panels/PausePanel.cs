namespace FrontEnd
{
    public class PausePanel : MenuPanel
    {
        #region BUTTONS
        public void BackMenuButton()
        {
            LevelManager.setupGame?.Invoke();
            LevelManager.pauseGame?.Invoke();
            UIManager.onOpenFrontEnd?.Invoke();
        }
        public void RestartGameButton()
        {
            LevelManager.pauseGame?.Invoke();
            LevelManager.setupGame?.Invoke();
            LevelManager.startGame?.Invoke();
        }
        public void BackToGameButton()
        {
            UIManager.onCloseFrontEnd?.Invoke();
            LevelManager.pauseGame?.Invoke();
        }
        #endregion
    }
}