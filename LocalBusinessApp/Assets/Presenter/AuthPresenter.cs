using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthPresenter
{
    private readonly IAuthView _view;
    private readonly AuthService _service;

    public AuthPresenter(IAuthView view)
    {
        _view = view;
        _service = new AuthService();
    }

    public void OnRegister(AuthRequest request)
    {
        var result = _service.Register(request);
        _view.ShowResult(result);
    }

    public void OnLogin(AuthRequest request)
    {
        var result = _service.Login(request);
        _view.ShowResult(result);

        if (!result.IsSuccess)
            return;

        var role = result.UserData.Role;
        SessionManager.LoggedInUser = result.UserData;
        Debug.Log($"[Login] Logged in as: {role}");

        if (role == UserRole.User)
            SceneManager.LoadScene("BusinessesScene");
        else if (role == UserRole.Business)
            SceneManager.LoadScene("BusinessAdminScene");
    }

}
