using UnityEngine;

public class GameUIPresenter
{
    private GameUIView view;

    public GameUIPresenter(GameUIView view)
    {
        this.view = view;

        GameController.Instance.StartLevel(PlayerPrefs.GetInt("desiredLevel"), 120.0f);//TODO Get duration from level json
        GameController.Instance.OnHealthChanged.AddListener(UpdateHealth);
        GameController.Instance.OnPlayerScoreChanged.AddListener(UpdateScore);
        GameController.Instance.OnTimerChanged.AddListener(UpdateTimer);
        GameController.Instance.OnGuessMessageChanged += UpdateISpyMessage;
    }

    private void UpdateISpyMessage(string arg0)
    {
        view.SetISpyLabel($"{Localisation.GetLocalisedValue("key_ISpyLabel")}\n{arg0}");
    }

    private void UpdateTimer(float arg0)
    {
        view.SetTimerLabel($"{Localisation.GetLocalisedValue("key_TimerLabel")} - {Helpers.FormatDisplayTime(arg0)}");
    }

    private void UpdateScore(float arg0)
    {
        view.SetScoreLabel($"{Localisation.GetLocalisedValue("key_ScoreLabel")} - {arg0}");
    }

    private void UpdateHealth(int arg0)
    {
        view.UpdateHearts(arg0);
    }

    internal void ValidateCurrentSelection()
    {
        GameController.Instance.ValidateSelection();
    }

    ~GameUIPresenter()
    {
        GameController.Instance.OnHealthChanged.RemoveListener(UpdateHealth);
        GameController.Instance.OnPlayerScoreChanged.RemoveListener(UpdateScore);
        GameController.Instance.OnTimerChanged.RemoveListener(UpdateTimer);
        GameController.Instance.OnGuessMessageChanged -= UpdateISpyMessage;
    }
}
