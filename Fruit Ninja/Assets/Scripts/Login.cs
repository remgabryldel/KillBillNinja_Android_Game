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
        SceneManager.LoadScene("Ninja_Bill");
    }
}
