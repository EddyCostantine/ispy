using UnityEngine;

public class UIController : MonoBehaviourSingleton<UIController>
{
    [SerializeField] private GameObject splashScreen;
    [SerializeField] private GameObject loginScreen;
    [SerializeField] private GameObject selectLevelScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject leaderboardScreen;
    [SerializeField] private GameObject settingsScreen;

    private void Awake()
    {
        NavigationPhaseController.SetCurrentNavigationPhase(NavigationPhaseModel.NavigationPhase.SplashScreen);
        NavigationPhaseController.OnNavigationPhaseUpdate.AddListener(UpdateUI);
    }
    public void UpdateUI()
    {
        ResetAllScreens();

        switch (NavigationPhaseController.GetCurrentNavigationPhase())
        {
            case NavigationPhaseModel.NavigationPhase.SplashScreen: DisplaySplashScreen(); break;
            case NavigationPhaseModel.NavigationPhase.Login: DisplayLoginScreen(); break;
            case NavigationPhaseModel.NavigationPhase.SelectLevel: DisplaySelectLevelScreen(); break;
            case NavigationPhaseModel.NavigationPhase.Game: DisplayGameScreen(); break;
            case NavigationPhaseModel.NavigationPhase.Leaderboard: DisplayLeaderboardScreen(); break;
            case NavigationPhaseModel.NavigationPhase.Settings: DisplaySettingsScreen(); break;
        }
    }
    private void DisplaySettingsScreen() => settingsScreen.SetActive(true);
    private void DisplayLeaderboardScreen() => leaderboardScreen.SetActive(true);
    private void DisplayGameScreen() => gameScreen.SetActive(true);
    private void DisplaySelectLevelScreen() => selectLevelScreen.SetActive(true);
    private void DisplayLoginScreen() => loginScreen.SetActive(true);
    private void DisplaySplashScreen() => splashScreen.SetActive(true);
    private void ResetAllScreens()
    {
        splashScreen.SetActive(false);
        loginScreen.SetActive(false);
        selectLevelScreen.SetActive(false);
        gameScreen.SetActive(false);
        leaderboardScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }
}
