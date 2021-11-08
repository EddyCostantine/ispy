using System;
using UnityEngine;

public class LoginUIPresenter
{
    private LoginUIView view;

    public LoginUIPresenter(LoginUIView view)
    {
        this.view = view;
    }
    public void ClientSideUsernameTextValidation(string text)
    {
        if (!string.IsNullOrEmpty(text))//Perform required validation here
        {
            view.EnableSigninButton();
        }
        else
            view.DisableSignInButton();
    }
    public void ClientSidePasswordTextValidation(string text)
    {
        if (!string.IsNullOrEmpty(text))//Perform required validation here
        {
            view.EnableSigninButton();
        }
        else
            view.DisableSignInButton();
    }
    public void ReceiveCredentials(string username, string password)
    {
        ValidateCredentials(username, password);
    }
    private void ValidateCredentials(string username, string password)
    {
        //TODO Change this to use API
        bool isValidCredentials = false;
        if (username == "user" && password == "user")
            isValidCredentials = true;
        //--------------------------//

        if (isValidCredentials)
            HandleCorrectCredentialsReceived();
        else
            HandleWrongCredentialsReceived();
    }
    private void HandleCorrectCredentialsReceived()
    {
        Debug.Log("Correct Credentials.");
        NavigationPhaseController.NextNavigationPhase();
    }
    private void HandleWrongCredentialsReceived()
    {
        Debug.Log("Wrong Credentials.");
        view.ShowErrorMessage();
        view.ShowErrorBorders();
        view.GreyOutLogin();
    }

    internal void HandleSettingsButtonClicked()
    {
        NavigationPhaseController.SetCurrentNavigationPhase(NavigationPhaseModel.NavigationPhase.Settings);
    }
}
