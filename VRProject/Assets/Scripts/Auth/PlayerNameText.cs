using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameText : MonoBehaviour
{
    private TextMeshProUGUI nameText;

    private void Start()
    {
        if(AuthManager.User != null)
        {
            //nameText.text = $"{AuthManager.User.Email} logged in";
        }
        else
        {
            nameText.text = "ERROR : AUTHMANAGER USER IS NULL";
        }
    }
}
