using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // TMPro
    [Header("The Score")]
    public TextMeshProUGUI HeightUI;
    // scripts
    private PlayerController pc;

    // Gameobjects
    [Header("GameObjects To Drag In")]
    public GameObject GameOverScreen;
    public GameObject goodPlatform;
    public GameObject oneJumpPlatform;
    public GameObject badPlatform;
    public GameObject springPlatform;
    private GameObject platformTypeToSpawn;
    private GameObject lastPlatformTypeSpawned;

    // floats
    private float prevouisHighestYPlatform;
    private float minRandomSpawnPos;

    [Header("Platform Move Stats")]
    public static float platformMoveSpeed;
    // ints
    public static int chanceToBeat;
    
    [Header("How long you want each 'level' to be")]
    public int PlatformsToSpawn;


    private int chanceForGoodPlatform;
    [Header("Good Platform stats")]
    public int GoodPlatformMinChance;
    public int GoodPlatformMaxChance;
 
    private int chanceForOneJumpPlatform;
    [Header("One Jump Platform stats")]
    public int OneJumpPlatFormMinChance;
    public int OneJumpPlatformMaxChance;
    
    private int chanceForBadPlatform;
    [Header("Bad Platform stats")]
    public int BadPlatformMinChance;
    public int BadPlatformMaxChance;
    // Start is called before the first frame update
    void Start()
    {
        platformMoveSpeed = 0.5f;
        chanceToBeat = 95;
        minRandomSpawnPos = 0.5f;
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnNextPlatForms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HeightUI.text = $"Height:{Mathf.Round(pc.playerHighestYPos)}";
    }

    public void spawnNextPlatForms()
    {
        Vector2 spawnPosition = new Vector2();

        for (int platformsSpawned = 0; platformsSpawned < PlatformsToSpawn; platformsSpawned++)
        {
            spawnPosition.y = Random.Range(minRandomSpawnPos, 2.5f) + prevouisHighestYPlatform;
            spawnPosition.x = Random.Range(-7.5f, 7.5f);
            minRandomSpawnPos += 0.1f;
            if(minRandomSpawnPos > 2)
            {
                minRandomSpawnPos = 2;
            }
            chanceForGoodPlatform = Random.Range(GoodPlatformMinChance, GoodPlatformMaxChance);
            chanceForOneJumpPlatform = Random.Range(OneJumpPlatFormMinChance, OneJumpPlatformMaxChance);
            chanceForBadPlatform = Random.Range(BadPlatformMinChance, BadPlatformMaxChance);
            if (chanceForGoodPlatform > chanceForOneJumpPlatform)
            {
                platformTypeToSpawn = goodPlatform;
            }
            else if (chanceForOneJumpPlatform > chanceForBadPlatform)
            {
                platformTypeToSpawn = oneJumpPlatform;
            }
            else
            {
                platformTypeToSpawn = badPlatform;
            }
            if (platformTypeToSpawn == goodPlatform || platformTypeToSpawn == oneJumpPlatform)
            {
                int springPlatformChance = Random.Range(1, 101);
                if(springPlatformChance > 98)
                {
                    platformTypeToSpawn = springPlatform;
                }
            }
            if (platformTypeToSpawn == badPlatform && lastPlatformTypeSpawned == badPlatform)
            {
                platformTypeToSpawn = oneJumpPlatform;
                Debug.Log("Double Death platform spawn Prevented");
            }
            Instantiate(platformTypeToSpawn, spawnPosition, transform.rotation);
            if (platformsSpawned >= 10)
            {
                platformTypeToSpawn.tag = "LastPlatforms";
            }
            prevouisHighestYPlatform = spawnPosition.y;
            lastPlatformTypeSpawned = platformTypeToSpawn;
            chanceToBeat -= 1;
            platformMoveSpeed += 0.005f;
            if(chanceToBeat < 25)
            {
                chanceToBeat = 25;
            }
            if (platformMoveSpeed > 3)
            {
                platformMoveSpeed = 3;
            }
        }
        // Game will get harder over time
        GoodPlatformMinChance -= 10;
        OneJumpPlatFormMinChance += 10;
        BadPlatformMinChance += 5;
        BadPlatformMaxChance += 15;
        if (BadPlatformMinChance > 75)
        {
            BadPlatformMinChance = 75;
        }

        if (BadPlatformMaxChance > 100)
        {
            BadPlatformMaxChance = 100;
        }

        if (OneJumpPlatFormMinChance > 65)
        {
            OneJumpPlatFormMinChance = 65;
        }

        if (GoodPlatformMinChance < 1)
        {
            GoodPlatformMinChance = 1;
        }
    }

    public void GameOver()
    {
        if (pc.playerHighestYPos > PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", Mathf.Round(pc.playerHighestYPos));
        }
        
        StartCoroutine(timeBeforeGoingToTitleScreen());
        GameOverScreen.SetActive(true);
        pc.enabled = false;
        pc.rb.gravityScale = 99;
        pc.rb.velocity = Vector2.zero;       
    }

    IEnumerator timeBeforeGoingToTitleScreen()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    
}
