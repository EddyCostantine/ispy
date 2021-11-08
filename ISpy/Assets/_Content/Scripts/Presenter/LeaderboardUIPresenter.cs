using System;
using UnityEngine;

public class LeaderboardUIPresenter
{
    private LeaderboardUIView view;

    public LeaderboardUIPresenter(LeaderboardUIView view)
    {
        this.view = view;

        view.SetHighScoreLabel(PlayerPrefs.GetFloat("HighScore").ToString());
        view.SetScoreLabel(PlayerPrefs.GetFloat("CurrentScore").ToString());
    }

    internal void HandlePlayAgainButtonClicked()
    {
        Debug.Log("Play Again");
        GameController.Instance.StartLevel(PlayerPrefs.GetInt("DesiredLevel"),120.0f);
        NavigationPhaseController.SetCurrentNavigationPhase(NavigationPhaseModel.NavigationPhase.Game);
    }

    internal void HandleLevelSelectionButtonClicked()
    {
        Debug.Log("Level Selection");
        NavigationPhaseController.SetCurrentNavigationPhase(NavigationPhaseModel.NavigationPhase.SelectLevel);
    }
}
