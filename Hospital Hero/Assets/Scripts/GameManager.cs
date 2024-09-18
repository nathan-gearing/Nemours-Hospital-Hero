using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerController playerController;
    private EnemyAI enemyAi;
    public float deathAnimationDuration = 3.0f;
    private const string LevelKey = "CurrentLevel";
    public int currentLevel;
    
    public void LoadLevel(int levelIndex)
    {
        PlayerPrefs.SetInt(LevelKey, levelIndex);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene(levelIndex);
    }

    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt(LevelKey, currentLevel);
    }
    
    
    
    void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemyAi = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void RequestLevelTransition(int levelIndex, float loadTime)
    {
        StartCoroutine(HandleTransition(levelIndex, loadTime));
    }

    private IEnumerator HandleTransition(int levelIndex, float loadTime)
    {
        yield return new WaitForSeconds(loadTime);

        LoadLevel(levelIndex);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerPlayerDeath()
    {
        StartCoroutine(PlayerDeath());
    }

    private IEnumerator PlayerDeath()
    {
        playerController.enabled = false;
        enemyAi.enabled = false;

        playerAnimator.SetTrigger("dead");

        yield return new WaitForSeconds(deathAnimationDuration);
        
        Destroy(playerController.gameObject);
        
        
        SceneManager.LoadScene(GetCurrentLevel());
    }
}
