using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Login : MonoBehaviour
{

    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button goToRegisterButton;
    public TMP_Text message;

    ArrayList credentials;

    void Start()
    {
        loginButton.onClick.AddListener(login);
        goToRegisterButton.onClick.AddListener(moveToRegister);

        if (File.Exists(Application.dataPath + "/credentials.txt"))
        {
            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        }
        else
        {
            Debug.Log("Credential file doesn't exist");
            message.text = "Credential file doesn't exist";
        }

    }



    void login()
    {
        bool isExists = false;

        credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        foreach (var i in credentials)
        {
            string line = i.ToString();
            //Debug.Log(line);
            //Debug.Log(line.Substring(11));
            //substring 0-indexof(:) - indexof(:)+1 - i.length-1
            if (i.ToString().Substring(0, i.ToString().IndexOf(":")).Equals(emailInput.text) &&
                i.ToString().Substring(i.ToString().IndexOf(":") + 1).Equals(passwordInput.text))
           {
                isExists = true;
                break;
           }
        }

        if (isExists)
        {
            Debug.Log($"Logging in '{emailInput.text}'");
            message.text = $"Logging in '{emailInput.text}'";
            loadWelcomeScreen();
        }
        else
        {
            Debug.Log("Incorrect credentials");
            message.text = "Incorrect credentials";
        }
    }

    void moveToRegister()
    {
        SceneManager.LoadScene("Register");
    }

    void loadWelcomeScreen()
    {
        SceneManager.LoadScene("Kill_Bill_Ninja");
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPagePlayfab : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TopText;
    [SerializeField] TextMeshProUGUI MessageText;

    [Header("Login")]
    [SerializeField] TMP_InputField EmailLoginInput;
    [SerializeField] TMP_InputField PasswordLoginInput;
    [SerializeField] GameObject LoginPage;

    [Header("Register")]
    [SerializeField] TMP_InputField UsernameRegisterInput;
    [SerializeField] TMP_InputField EmailRegisterInput;
    [SerializeField] TMP_InputField PasswordRegisterInput;
    [SerializeField] GameObject RegisterPage;


    [Header("Recovery")]
    [SerializeField] TMP_InputField EmailRecoveryInput;
    [SerializeField] GameObject RecoveryPage;

    [SerializeField] private GameObject WelcomeObject;
    [SerializeField] private Text WelcomeText;

    [SerializeField] private IndirectionLogin indirectionLogin;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        
    }
    #region Buttom Functions

    public void RegisterUser(){
        //controlli sulla password
        var request = new RegisterPlayFabUserRequest{
            DisplayName = UsernameRegisterInput.text,
            Email = EmailRegisterInput.text,
            Password = PasswordRegisterInput.text,

            RequireBothUsernameAndEmail = true //ricorda nel caso di errori di metter true
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnregisterSucces, OnError);

    }

    public void LoginUser(){
        var request = new LoginWithEmailAddressRequest{
            Email = EmailLoginInput.text,
            Password = PasswordLoginInput.text,

            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
               GetPlayerProfile = true
            }

        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSucces, OnError);
    }

    public void RecoverUser(){
        var request = new SendAccountRecoveryEmailRequest{
            Email = EmailLoginInput.text,
            TitleId = "41F35",

        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySucces, OnErrorRecovery);
    }

    private void OnErrorRecovery(PlayFabError result){
        MessageText.text = "No mail found";
        Debug.Log(result.GenerateErrorReport());
    }

    private void OnRecoverySucces(SendAccountRecoveryEmailResult result){
        MessageText.text = "Recovery mail sent";
    }

    private void OnLoginSucces(LoginResult result){

        string name = null;

        if(result.InfoResultPayload != null){
           name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        

        WelcomeObject.SetActive(true);
       
        WelcomeText.text = "Welcome " + name;

        if(indirectionLogin != null)  {
           indirectionLogin.playerName = name;
        }  

        StartCoroutine(LoadNextScene());
    }

    private void OnError(PlayFabError Error){
        MessageText.text = Error.ErrorMessage;
        Debug.Log(Error.GenerateErrorReport());
    }

    private void OnregisterSucces(RegisterPlayFabUserResult Result){
        MessageText.text = "New account is created";
    }

    public void OpenLoginPage(){
        TopText.text = "Login";
        MessageText.text = "";
        EmailLoginInput.text = "";
        PasswordLoginInput.text = "";
        LoginPage.SetActive(true);
        RegisterPage.SetActive(false);
        RecoveryPage.SetActive(false);

    }

    public void OpenRegisterPage(){
        TopText.text = "Register";
        MessageText.text = "";
        UsernameRegisterInput.text = "";
        EmailRegisterInput.text = "";
        PasswordRegisterInput.text = "";
        LoginPage.SetActive(false);
        RegisterPage.SetActive(true);
        RecoveryPage.SetActive(false);
    }

    public void OpenRecoveryPage(){
        TopText.text = "Recovery";
        MessageText.text = "";
        EmailRecoveryInput.text = "";
        LoginPage.SetActive(false);
        RegisterPage.SetActive(false);
        RecoveryPage.SetActive(true);
    }
    #endregion

    IEnumerator LoadNextScene(){
        yield return new WaitForSecondsRealtime(2f);
        MessageText.text = "Loggin in...";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}*/

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndirectionLogin : MonoBehaviour
{
    [SerializeField] public string playerName;
    
    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }
}
*/
