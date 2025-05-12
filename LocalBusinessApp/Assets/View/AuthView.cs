using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AuthView : MonoBehaviour, IAuthView
{
    public TMP_InputField emailInput, passwordInput, confirmInput, fullNameInput;
    public TMP_Dropdown roleDropdown;
    public TMP_Text feedbackText;

    private AuthPresenter presenter;

    void Start()
    {
        SQLiteService.Init();
        presenter = new AuthPresenter(this);
    }

    public void OnLoginClick()
    {
        presenter.OnLogin(new AuthRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        });
    }

    public void OnRegisterClick()
    {
        presenter.OnRegister(new AuthRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            ConfirmPassword = confirmInput.text,
            FullName = fullNameInput.text,
            Role = (UserRole)roleDropdown.value
        });
    }

    public void ShowResult(AuthResponse response)
    {
        if (response.IsSuccess)
            feedbackText.text = $"Welcome, {response.UserData.FullName}";
        else
            feedbackText.text = $"Error: {response.ErrorMessage}";
    }
}
