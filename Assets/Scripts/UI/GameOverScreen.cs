using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private Button _nextLevelButton;

    public void EnableGameOverScreen()
    {
        _gameOverText.gameObject.SetActive(true);
        _nextLevelButton.gameObject.SetActive(true);
    }
}
