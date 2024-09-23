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
    
    
    
    
    
    void Start()
    {

        Debug.Log("Before INIt, levelkey= " + PlayerPrefs.GetInt(LevelKey));
        if (!PlayerPrefs.HasKey(LevelKey))
        {
            PlayerPrefs.SetInt(LevelKey, 0);
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs init to 0");
        }
        currentLevel = PlayerPrefs.GetInt(LevelKey);
        Debug.Log("Starting Level at: " + currentLevel);
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemyAi = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();

        if (SceneManager.GetActiveScene().buildIndex != currentLevel)
        {
            SceneManager.LoadScene(currentLevel);
        }
        
    }
public void LoadLevel(int levelIndex)
    {
        Debug.Log("Setting level to: " + levelIndex);
        PlayerPrefs.SetInt(LevelKey, levelIndex);
        PlayerPrefs.Save();

        Debug.Log("Loading Level: " + levelIndex);
        SceneManager.LoadScene(levelIndex);
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGameToLevel0();
        }
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
        
        currentLevel = PlayerPrefs.GetInt(LevelKey);
        SceneManager.LoadScene(currentLevel);
    }

    public void ResetGameToLevel0()
    {
        PlayerPrefs.SetInt(LevelKey, 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
