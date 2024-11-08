using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEditor.Build.Content;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int maxAmmo = 15;
    public static int ammoLeft;
    public static int pegsLeft;
    public static int ballsInScene;
    public static bool gameFinished;
    private static int points;
    [SerializeField]
    private TextMeshProUGUI ammoLeftText;
    [SerializeField]
    private TextMeshProUGUI pegsLeftText;
    [SerializeField]
    private TextMeshProUGUI pointsTotalText;

    //variables for displaying points gained
    [SerializeField]
    private TextMeshProUGUI pointsGainedText;
    [SerializeField]
    private Canvas mainCanvas;
    private Queue<TextMeshProUGUI> textQueue;
    private static Vector3 textPosition;
    private static int pointIncrement;
    private static bool hitPeg;
    private float hitTimer = 1;
    private float hitCountdown;

    [SerializeField]
    private GameObject winScreen, loseScreen;
    [SerializeField]
    private TextMeshProUGUI winScreenText;

    private void Start()
    {
        ammoLeft = maxAmmo;
        points = 0;
        pointsTotalText.text = "0";
        textQueue = new Queue<TextMeshProUGUI>();
        hitCountdown = hitTimer;
        gameFinished = false;
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    private void Update()
    {
        ammoLeftText.text = ammoLeft.ToString();
        pegsLeftText.text = pegsLeft.ToString();
        pointsTotalText.text = points.ToString();
        hitCountdown -= Time.deltaTime;
        if (hitPeg)
        {
            DisplayPointsGained();
            hitPeg = false;
        }
        if (textQueue.Count != 0 && (hitCountdown <= 0 || gameFinished))
        {
            HidePointsGained();
            if (!gameFinished) hitCountdown = hitTimer;
        }
    }

    public static void UpdateGameStatus()
    {
        if (gameFinished)
            return;
        if (pegsLeft <= 0)
        {
            gameFinished = true;
            FindObjectOfType<GameController>().winScreen.SetActive(true);
            FindObjectOfType<GameController>().winScreenText.text += "\nScore : " + points;
        }
        else if (ammoLeft <= 0 && ballsInScene <= 0)
        {
            gameFinished = true;
            FindObjectOfType<GameController>().loseScreen.SetActive(true);
        }
    }

    public static void UpdatePoints(int multiplier, Vector3 pegPosition)
    {
        pointIncrement = 10 * multiplier;
        points += pointIncrement;
        textPosition = pegPosition;
        hitPeg = true;
    }

    private void DisplayPointsGained()
    {
        if (hitCountdown <= 0) hitCountdown = hitTimer;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(textPosition);
        float pointIncrementf = (float)pointIncrement;
        float colorIncrement = Mathf.Max(1 - ((pointIncrementf / 10) - 1) / 10, 0);
        pointsGainedText.color = new Color(pointsGainedText.color.r, colorIncrement, colorIncrement);
        pointsGainedText.text = "+" + pointIncrement.ToString();
        textQueue.Enqueue(Instantiate(pointsGainedText, screenPosition, new Quaternion(), mainCanvas.transform));
    }

    private void HidePointsGained()
    {
        TextMeshProUGUI textToDelete = textQueue.Dequeue();
        Destroy(textToDelete);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}