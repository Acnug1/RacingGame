using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _nextLevelButton;

    private int _currentScene;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClick);
    }

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene().buildIndex; // находим номер текущей сцены
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(_currentScene); // загружаем текущую сцену
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void OnNextLevelButtonClick()
    {
        if (_currentScene + 1 < SceneManager.sceneCountInBuildSettings) // если номер следующей сцены не больше, чем общее количество сцен
        {
            _currentScene++; // проматываем уровень на следующий
            SceneManager.LoadScene(_currentScene);
        }
        else
            SceneManager.LoadScene(0); // иначе загружаем игру с первого уровня
    }
}
