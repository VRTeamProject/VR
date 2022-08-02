using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//AuthManager ½Ì±ÛÅæÀ¸·Î ¹Ù²Ü°Í
public class AuthManager : MonoBehaviour
{
    public bool IsFirebaseReady { get; private set; }
    public bool IsSignInOnProgress { get; private set; }
    
    public TMP_InputField emailInput;
    public TMP_InputField passwordField;
    public Button signInButton;

    public static FirebaseApp firebaseApp;
    public static FirebaseAuth firebaseAuth;

    public static FirebaseUser User;

    public string userID;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        signInButton.interactable = false;

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var result = task.Result;
            if(result != DependencyStatus.Available)
            {
                Debug.LogError(result.ToString());
                IsFirebaseReady = false;
            }
            else
            {
                IsFirebaseReady = true;
                firebaseApp = FirebaseApp.DefaultInstance;
                firebaseAuth = FirebaseAuth.DefaultInstance;
            }

            signInButton.interactable = IsFirebaseReady;
        });
    }

    public void LogIn()
    {
        if(!IsFirebaseReady || IsSignInOnProgress || User != null)
        {
            return;
        }

        IsSignInOnProgress = true;
        signInButton.interactable = false;

        firebaseAuth.SignInWithEmailAndPasswordAsync(emailInput.text, passwordField.text).ContinueWithOnMainThread(
            task =>
            {
                //Debug.Log($"Log in Status: {task.Status}");
                IsSignInOnProgress = false;
                signInButton.interactable = true;

                if(task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                }
                else if(task.IsCanceled)
                {
                    Debug.LogError("Sign-in canceled");
                }
                else
                {
                    User = task.Result;
                    userID = emailInput.text.Split('@')[0];
                    SceneManager.LoadScene("Lobby");
                }
            });
    }
}
