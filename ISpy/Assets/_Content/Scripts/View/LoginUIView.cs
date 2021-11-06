using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoginUIView : MonoBehaviour
{
    private LoginUIPresenter presenter;

    private Label errorMessageLabel;
    private Label usernameLabel;
    private TextField usernameInputField;
    private Label passwordLabel;
    private TextField passwordInputField;
    private Label showPassword;
    private Button signUpButton;
    private Button signInButton;

    private void OnEnable()
    {
        presenter = new LoginUIPresenter(this);

        var root = GetComponent<UIDocument>().rootVisualElement;

        errorMessageLabel = root.Q<Label>("ErrorMessageLabel");
        usernameLabel = root.Q<Label>("UsernameLabel");
        usernameInputField = root.Q<TextField>("UsernameInputField");
        passwordLabel = root.Q<Label>("PasswordLabel");
        passwordInputField = root.Q<TextField>("PasswordInputField");
        showPassword = root.Q<Label>("ShowPasswordLabel");
        signUpButton = root.Q<Button>("SignUpButton");
        signInButton = root.Q<Button>("SignInButton");

        errorMessageLabel.text = Localisation.GetLocalisedValue("key_SignInErrorMessage");
        usernameLabel.text = Localisation.GetLocalisedValue("key_Username");
        passwordLabel.text = Localisation.GetLocalisedValue("key_Password");
        showPassword.text = Localisation.GetLocalisedValue("key_ShowPassword");
        signUpButton.Q<Label>("Label").text = Localisation.GetLocalisedValue("key_SignUp");
        signInButton.Q<Label>("Label").text = Localisation.GetLocalisedValue("key_SignIn");
        signInButton.clicked += OnSignInButtonClicked;
        signUpButton.clicked += OnSignUpButtonClicked;

        usernameInputField.RegisterCallback<ChangeEvent<string>>(OnUsernameTextChanged);
        passwordInputField.RegisterCallback<ChangeEvent<string>>(OnPasswordTextChanged);
        showPassword.RegisterCallback<ClickEvent>(OnTogglePasswordVisibility);

        DisableSignInButton();
    }
    private void OnTogglePasswordVisibility(ClickEvent evt)
    {
        passwordInputField.isPasswordField = !passwordInputField.isPasswordField;
    }
    private void OnUsernameTextChanged(ChangeEvent<string> changeEvent)
    {
        presenter.ClientSideUsernameTextValidation(changeEvent.newValue);
    }
    private void OnPasswordTextChanged(ChangeEvent<string> changeEvent)
    {
        presenter.ClientSidePasswordTextValidation(changeEvent.newValue);
    }
    private void OnSignUpButtonClicked()
    {
        throw new NotImplementedException();
    }
    private void OnSignInButtonClicked()
    {
        presenter.ReceiveCredentials(usernameInputField.text.ToString(), passwordInputField.text.ToString());
    }
    public void EnableSigninButton()
    {
        if (!string.IsNullOrEmpty(usernameInputField.text.ToString()) && !string.IsNullOrEmpty(passwordInputField.text.ToString()))
        {
            //TODO Move To Config
            Color loginEnabledColor = Helpers.ColorFromHex("00FF80");
            signInButton.style.backgroundColor = loginEnabledColor;

            signInButton.SetEnabled(true);
        }
        else
            signInButton.SetEnabled(false);
    }
    public void DisableSignInButton()
    {
        signInButton.SetEnabled(false);
        GreyOutLogin();
    }
    public virtual void ShowErrorMessage()
    {
        errorMessageLabel.style.display = DisplayStyle.Flex;
    }
    public virtual void ShowErrorBorders()
    {
        //TODO Move To Config
        Color borderColor = Helpers.ColorFromHex("D35858");

        var emailInputText = ((List<VisualElement>)usernameInputField.Children())[0];
        Helpers.SetAllBordersColor(emailInputText, borderColor);

        var passwordInputBackround = ((List<VisualElement>)passwordInputField.Children())[0];
        Helpers.SetAllBordersColor(passwordInputBackround, borderColor);
    }
    public void GreyOutLogin()
    {
        //TODO Move To Config
        Color loginGreyedColor = Helpers.ColorFromHex("D8D8D8");
        signInButton.style.backgroundColor = loginGreyedColor;
    }
}
