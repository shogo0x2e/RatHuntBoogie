using System.Text;
using TMPro;
using UnityEngine;

public class Watch : MonoBehaviour {
    private static Watch instance;

    public static Watch GetInstance() {
        return instance;
    }

    [SerializeField] private WatchButton watchButton;
    [SerializeField] private TextMeshPro topText; // Timer
    [SerializeField] private TextMeshPro midText; // Score
    [SerializeField] private TextMeshPro botText; // Wathever

    private const float gameDuration = 120F;
    private float remTime = gameDuration;

    private int currScore = 0;

    private int currStartIndex = 0;
    private float currTimeAcc = 0;

    public void Start() {
        instance = this;

        SetMidText();
    }

    public void Update() {
        remTime -= Time.deltaTime;
        SetTopText();

        if (remTime < 0) {
            // TODO: Stop
        }

        if (watchButton.isPlaying) {
            currTimeAcc += Time.deltaTime;
            if (currTimeAcc < watchButton.updateTime) {
                return;
            }

            StringBuilder sb = new StringBuilder();
            int i = currStartIndex;
            foreach (char c in "Booging") {
                sb.Append($"<color={WatchButton.RainbowColors[i]}>{c}</color>");
                i = (i + 1) % WatchButton.RainbowColors.Length;
            }

            SetBotText(sb.ToString());
            currStartIndex--;
            if (currStartIndex < 0) {
                currStartIndex = WatchButton.RainbowColors.Length - 1;
            }

            currTimeAcc = 0;
        } else {
            currTimeAcc = watchButton.updateTime;

            SetBotText("Meow");
        }
    }

    public void AddScore(int value) {
        currScore += value;
        SetMidText();
    }

    private void SetTopText() {
        int remTimeInt = (int)remTime;
        int remTimeMinutes = remTimeInt / 60;
        int remTimeSeconds = remTimeInt % 60;
        topText.text = "Time " + remTimeMinutes + ":" + remTimeSeconds;
    }

    private void SetMidText() {
        midText.text = "Score " + currScore;
    }

    private void SetBotText(string value) {
        botText.text = value;
    }

    public void ShowTexts() {
        SetEnableTexts(true);
    }

    public void HideTexts() {
        SetEnableTexts(false);
    }

    private void SetEnableTexts(bool value) {
        topText.enabled = value;
        midText.enabled = value;
        botText.enabled = value;
    }
}
