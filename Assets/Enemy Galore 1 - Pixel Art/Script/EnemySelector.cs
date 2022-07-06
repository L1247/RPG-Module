#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public class EnemySelector : MonoBehaviour
{
#region Private Variables

    [Header("Buttons")]
    [SerializeField]
    private Button[] allAnimationButtons;

    [SerializeField]
    private Button[] enableAnimationButtons;

    [Header("Game Objects")]
    [SerializeField]
    private GameObject[] allEnemyGOs;

    [SerializeField]
    private GameObject enableEnemy;

    [Header("Text")]
    [SerializeField]
    private Text enemyName;

#endregion

#region Unity events

    public void Start()
    {
        enemyName.text = "";
    }

#endregion

#region Public Methods

    public void _DisableAllButtons()
    {
        for (var i = 0 ; i < allAnimationButtons.Length ; i++) allAnimationButtons[i].interactable = false;
    }

    public void _DisableAllEnemies()
    {
        for (var i = 0 ; i < allEnemyGOs.Length ; i++) allEnemyGOs[i].gameObject.SetActive(false);
    }

    public void _EnableButton()
    {
        for (var i = 0 ; i < enableAnimationButtons.Length ; i++) enableAnimationButtons[i].interactable = true;
    }

    public void _EnableEnemy()
    {
        enableEnemy.SetActive(true);
    }

    public void _Rename()
    {
        enemyName.text = enableEnemy.gameObject.name;
    }

    public void ChangeEnemies()
    {
        _DisableAllEnemies();
        _DisableAllButtons();
        _EnableEnemy();
        _EnableButton();
        _Rename();
    }

#endregion
}