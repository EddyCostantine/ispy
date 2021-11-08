using System;
using UnityEngine;
using System.Collections;

public class SettingsUIPresenter
{
    private SettingsUIView view;

    public SettingsUIPresenter(SettingsUIView view)
    {
        this.view = view;
    }

    internal void HandleFullScreenToggle(bool newValue)
    {
        Screen.fullScreen = newValue;
    }

    internal void HandleMuteToggle(bool newValue)
    {
        Camera.main.transform.GetComponent<AudioSource>().mute = newValue;
    }

    internal void HandleBackButtonClicked()
    {
        NavigationPhaseController.PreviousNavigationPhase();
    }
}
