 public class NavigationPhaseModel
{
    public enum NavigationPhase
    {
        Settings = -1,
        SplashScreen = 0,
        Login = 1,
        SelectLevel = 2,
        Game = 3,
        Leaderboard = 4,
    }
    public static NavigationPhase currentNavigationPhase = NavigationPhase.SplashScreen;
}
