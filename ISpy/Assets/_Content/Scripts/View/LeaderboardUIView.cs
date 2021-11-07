using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LeaderboardUIView : MonoBehaviour
{
    private LeaderboardUIPresenter presenter;

    private Label highScoreLabel;
    private Label highScore;
    private Label scoreLabel;
    private Label score;

    private Button levelSelectionButton;
    private Button playAgainButton;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        highScoreLabel = root.Q<Label>("HighScoreLabel");
        highScore = root.Q<Label>("HighScore");
        scoreLabel = root.Q<Label>("ScoreLabel");
        score = root.Q<Label>("Score");
        levelSelectionButton = root.Q<Button>("LevelSelectionButton");
        playAgainButton = root.Q<Button>("PlayAgainButton");

        levelSelectionButton.clicked += OnLevelSelectionButtonClicked;
        playAgainButton.clicked += OnPlayAgainButtonClicked;

        levelSelectionButton.text = Localisation.GetLocalisedValue("key_LevelSelectionButton");
        playAgainButton.text = Localisation.GetLocalisedValue("key_PlayAgainButton");

        highScoreLabel.text = Localisation.GetLocalisedValue("key_HighScoreLabel");
        scoreLabel.text = Localisation.GetLocalisedValue("key_ScoreLabel");

        presenter = new LeaderboardUIPresenter(this);
    }

    private void OnPlayAgainButtonClicked()
    {
        presenter.HandlePlayAgainButtonClicked();
    }

    private void OnLevelSelectionButtonClicked()
    {
        presenter.HandleLevelSelectionButtonClicked();
    }
    public void SetHighScoreLabel(string text)
    {
        highScore.text = text;
    }
    public void SetScoreLabel(string text)
    {
        score.text = text;
    }
}
