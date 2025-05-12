using UnityEngine;

public class AuthResponse
{
    public bool IsSuccess;
    public string ErrorMessage;
    public User UserData;
    public string Token; // You can use GUID or some session string
}

