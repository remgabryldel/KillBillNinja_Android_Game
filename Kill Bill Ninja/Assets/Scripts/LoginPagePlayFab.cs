using System.Collections;
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
}
