using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsCanvas : MonoBehaviour
{
    
    [SerializeField] private Button _back;

    private void Start()
    {
        _back.onClick.AddListener(()=> { SceneManager.LoadScene(0); });

    }

 
}
