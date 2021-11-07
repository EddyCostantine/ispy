using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUIView : MonoBehaviour
{
    private GameUIPresenter presenter;

    private Button hintButton;
    private Button validateButton;
    private Label messageLabel;
    private Label iSpyLabel;
    private Label timerLabel;
    private Label scoreLabel;
    private VisualElement heartOne;
    private VisualElement heartTwo;
    private VisualElement heartThree;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        hintButton = root.Q<Button>("HintButton");
        validateButton = root.Q<Button>("ValidateButton");
        messageLabel = root.Q<Label>("MessageLabel");
        iSpyLabel = root.Q<Label>("ISpyLabel");
        timerLabel = root.Q<Label>("TimerLabel");
        scoreLabel = root.Q<Label>("ScoreLabel");
        heartOne = root.Q<VisualElement>("HeartOne");
        heartTwo = root.Q<VisualElement>("HeartTwo");
        heartThree = root.Q<VisualElement>("HeartThree");

        hintButton.text = Localisation.GetLocalisedValue("key_HintButton");
        validateButton.text = Localisation.GetLocalisedValue("key_ValidateButton");
        messageLabel.text = Localisation.GetLocalisedValue("key_GameMessageLabel");
        iSpyLabel.text = "";
        timerLabel.text = "";
        scoreLabel.text = "";

        hintButton.clicked += OnHintButtonClicked;
        validateButton.clicked += OnValidateButtonClicked;

        presenter = new GameUIPresenter(this);
    }

    private void OnValidateButtonClicked()
    {
        presenter.ValidateCurrentSelection();
    }

    private void OnHintButtonClicked()
    {
        throw new NotImplementedException();
    }

    public void SetISpyLabel(string text)
    {
        iSpyLabel.text = text;
    }
    public void SetMessageLabel(string text)
    {
        messageLabel.text = text;
    }
    public void SetTimerLabel(string text)
    {
        timerLabel.text = text;
    }
    public void SetScoreLabel(string text)
    {
        scoreLabel.text = text;
    }
    public void UpdateHearts(int health)
    {
        switch (health)
        {
            case 3:
                break;
            case 2:
                heartOne.style.display = DisplayStyle.None;
                break;
            case 1:
                heartOne.style.display = DisplayStyle.None;
                heartTwo.style.display = DisplayStyle.None;
                break;
            case 0:
                heartOne.style.display = DisplayStyle.None;
                heartTwo.style.display = DisplayStyle.None;
                heartThree.style.display = DisplayStyle.None;
                break;
            default:
                break;
        }
    }
}
