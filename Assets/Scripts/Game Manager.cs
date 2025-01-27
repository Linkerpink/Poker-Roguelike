using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isDragging = false;

    private static GameManager instance;

    public int round = -1;
    public float roundScoreQuota = 10;
    public float roundScore = -1;

    private TextMeshProUGUI m_roundScoreText;
    private TextMeshProUGUI m_handTypeText;

    private Deck m_deck;

    private GameObject m_shop;

    public enum GameStates
    {
        Game,
        Shop,
        Paused,
        MainMenu,
    }

    public GameStates gameState = GameStates.MainMenu;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject gameManagerObj = new GameObject("GameManager");
                    instance = gameManagerObj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            gameState = GameStates.Game;
            m_shop = GameObject.Find("Shop");
            m_deck = FindObjectOfType<Deck>();
            m_roundScoreText = GameObject.Find("Round Score Text")?.GetComponent<TextMeshProUGUI>();

            if (m_shop == null || m_roundScoreText == null)
            {
                Debug.LogWarning("Shop or Round Score Text not found in the Main scene.");
            }
        }
    }

    private void Update()
    {
        if (m_roundScoreText != null)
        {
            m_roundScoreText.SetText(roundScore.ToString("F2"));
        }

        switch (gameState)
        {
            case GameStates.MainMenu:
                break;

            case GameStates.Game:
                if (m_shop != null)
                {
                    m_shop.SetActive(false);
                }
                break;

            case GameStates.Shop:
                if (m_shop != null)
                {
                    m_shop.SetActive(true);
                }
                break;

            case GameStates.Paused:
                break;

            default:
                break;
        }
    }

    public void ChangeDragging(bool _dragging)
    {
        isDragging = _dragging;
    }

    public void ChangeScene(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }

    public void ChangeGameState(string _state)
    {
        //used chatgpt for the type switching
        if (Enum.TryParse(_state, true, out GameStates newState))
        {
            gameState = newState;
            if (newState == GameStates.Game)
            {
                //Reset cards and stuff
                roundScore = 0;
                roundScoreQuota += 10;
                m_deck.ResetDeck();
            }

            Debug.Log("Game state changed to: " + gameState);
        }
        else
        {
            Debug.LogError("Invalid game state: " + _state);
        }
    }
}