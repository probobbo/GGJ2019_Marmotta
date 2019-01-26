using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private Button _resumeBtn;
    [SerializeField] private Button _menuBtn;
    [SerializeField] private Button _quitBtn;

    private void Start()
    {
        _menuBtn.onClick.AddListener(() => SceneManager.LoadScene(0));
        _quitBtn.onClick.AddListener(Application.Quit);
        _resumeBtn.onClick.AddListener(() =>
        {
            EventSystem.current.SetSelectedGameObject(null);
            _panel.SetActive(false);
        });

    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            _panel.SetActive(!_panel.activeInHierarchy);
            EventSystem.current.SetSelectedGameObject(_resumeBtn.gameObject);

        }
    }
}
