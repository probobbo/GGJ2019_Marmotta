using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using Managers;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

public class Music : MonoBehaviour
{

    [SerializeField] [EventRef] private string _musicRef;
    private EventInstance _musicInstance;

    [SerializeField] private Slider _barra;
    
    private void Start()
    {
        _musicInstance = AudioManager.PlayAudio(_musicRef);
        
        EventManager.Instance.OnPlayingStateChanged.AddListener((value) =>
        {
            if (value == GameManager.PlayingState.Lost)
            {
                AudioManager.StopAudio(_musicInstance);
            }
            
        });
    }

    private void Update()
    {
        AudioManager.SetParameterToInstance(_musicInstance, "Value",_barra.value);
    }

    private void OnDestroy()
    {
        AudioManager.StopAudio(_musicInstance);
    }
}
