using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Local_GameManager : MonoBehaviour
{

    public static Local_GameManager Instance;

    public Local_Player player1, player2;

    [SerializeField] GameObject winnerPanel;
    [SerializeField] TMP_Text winnerTextUI;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WhoIsPunching(Local_Player player, int damage = 1)
    {
        if (player == player1)
        {
            if (!player2.isBlocking && !player2.isDead)
            {
                player2.Hit(damage);
            }
        }
        else
        {
            if (!player1.isBlocking && !player1.isDead)
            {
                player1.Hit(damage);
            }
        }
    }

    public void WhoDie(Local_Player player)
    {
        winnerPanel.SetActive(true);
        if (player == player1)
        {
            winnerTextUI.text = "PLAYER 2 WINS!";
        }
        else
        {
            winnerTextUI.text = "PLAYER 1 WINS!";
        }
    }

    public void Fight()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    
}
