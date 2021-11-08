using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsUIView : MonoBehaviour
{
    private SettingsUIPresenter presenter;

    private Label settingsLabel; 
    private Label languageLabel;
    private Button englishButton;
    private Button germanButton;
    private Toggle fullScreenToggle;
    private Toggle muteToggle;
    private Button backButton;


    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        settingsLabel = root.Q<Label>("SettingsLabel");
        languageLabel = root.Q<Label>("LanguageLabel");
        englishButton = root.Q<Button>("EnglishButton");
        germanButton = root.Q<Button>("GermanButton");
        fullScreenToggle = root.Q<Toggle>("FullScreenToggle");
        muteToggle = root.Q<Toggle>("MuteToggle");
        backButton = root.Q<Button>("BackButton");

        Init();

        englishButton.clicked += () => { Localisation.SetCurrentLanguage(Localisation.Language.English); Init(); };
        germanButton.clicked += () => { Localisation.SetCurrentLanguage(Localisation.Language.German); Init(); };
        fullScreenToggle.RegisterCallback<ChangeEvent<bool>>((evt) => { OnFullScreenToggle(evt); });
        muteToggle.RegisterCallback<ChangeEvent<bool>>((evt) => { OnMuteToggle(evt); });
        backButton.clicked += OnBackButtonClicked;

        presenter = new SettingsUIPresenter(this);
    }

    private void OnMuteToggle(ChangeEvent<bool> evt)
    {
        presenter.HandleMuteToggle(evt.newValue);
    }

    private void OnFullScreenToggle(ChangeEvent<bool> evt)
    {
        presenter.HandleFullScreenToggle(evt.newValue);
    }

    private void Init()
    {
        settingsLabel.text = Localisation.GetLocalisedValue("key_SettingsLabel");
        languageLabel.text = Localisation.GetLocalisedValue("key_LanguageLabel");
        englishButton.text = Localisation.GetLocalisedValue("key_ButtonEnglish");
        germanButton.text = Localisation.GetLocalisedValue("key_ButtonGerman");
        fullScreenToggle.label = Localisation.GetLocalisedValue("key_FullScreenToggle");
        muteToggle.label = Localisation.GetLocalisedValue("key_MuteToggle");
        backButton.text = Localisation.GetLocalisedValue("key_ButtonBack");
    }
    private void OnBackButtonClicked()
    {
        presenter.HandleBackButtonClicked();
    }
}
