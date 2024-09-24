using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerController playerController;
    private EnemyAI enemyAi;
    private Health playerHealth;
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
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        if (SceneManager.GetActiveScene().buildIndex != currentLevel)
        {
            SceneManager.LoadScene(currentLevel);
        }
        
    }
public void LoadLevel(int levelIndex)
    {

        playerHealth.SavePlayerHealth();
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
        if (Input.GetKeyDown(KeyCode.Alpha1)) LoadLevel(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) LoadLevel(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) LoadLevel(2);
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
        
        SceneManager.LoadScene(currentLevel);
    }

    public void ResetGameToLevel0()
    {
        PlayerPrefs.SetInt(LevelKey, 0);
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
