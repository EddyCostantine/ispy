using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NavigationPhaseController
{
    public static Stack<NavigationPhaseModel.NavigationPhase> navigationPhaseStack = new Stack<NavigationPhaseModel.NavigationPhase>();
    public static UnityEvent OnNavigationPhaseUpdate = new UnityEvent();

    public static NavigationPhaseModel.NavigationPhase GetCurrentNavigationPhase()
    {
        return NavigationPhaseModel.currentNavigationPhase;
    }
    public static NavigationPhaseModel.NavigationPhase GetPreviousNavigationPhase()
    {
        if (navigationPhaseStack.Count > 1)
            return navigationPhaseStack.Peek();
        else
            return NavigationPhaseModel.NavigationPhase.SplashScreen;
    }
    public static void SetCurrentNavigationPhase(NavigationPhaseModel.NavigationPhase navigationPhase)
    {
        NavigationPhaseModel.currentNavigationPhase = navigationPhase;
        navigationPhaseStack.Push(NavigationPhaseModel.currentNavigationPhase);
        OnNavigationPhaseUpdate.Invoke();
    }
    public static void NextNavigationPhase()
    {
        switch(GetCurrentNavigationPhase())
        {
            case NavigationPhaseModel.NavigationPhase.SplashScreen : SetCurrentNavigationPhase(NavigationPhaseModel.NavigationPhase.Login);break;
            case NavigationPhaseModel.NavigationPhase.Login: SetCurrentNavigationPhase(NavigationPhaseModel.NavigationPhase.SelectLevel);break;
            case NavigationPhaseModel.NavigationPhase.SelectLevel: SetCurrentNavigationPhase(NavigationPhaseModel.NavigationPhase.Game);break;
            case NavigationPhaseModel.NavigationPhase.Game: SetCurrentNavigationPhase(NavigationPhaseModel.NavigationPhase.Leaderboard);break;
            case NavigationPhaseModel.NavigationPhase.Leaderboard: SetCurrentNavigationPhase(NavigationPhaseModel.NavigationPhase.SelectLevel);break;
        }
        Debug.Log("Changed phase to: " + GetCurrentNavigationPhase());
    }
    public static void PreviousNavigationPhase()
    {
        navigationPhaseStack.Pop();
        if (navigationPhaseStack.Count > 0)
            SetCurrentNavigationPhase(navigationPhaseStack.Pop());
        else
            SetCurrentNavigationPhase(NavigationPhaseModel.NavigationPhase.SplashScreen);

        OnNavigationPhaseUpdate.Invoke();
    }
}
