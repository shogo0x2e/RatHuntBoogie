using System.Text;
using TMPro;
using UnityEngine;

public class WatchButton : MonoBehaviour {
    [SerializeField] private TextMeshPro boogieText;
    [SerializeField] private float boogieShowDuration = 6F;
    [SerializeField] public float updateTime = 0.066F;

    private const string boogieString = "Boogie Time";

    public static readonly string[] RainbowColors = {
        "#F43545",
        "#FA8901",
        "#FAD717",
        "#00BA71",
        "#00C2DE",
        "#00418D",
        "#5F2879"
    };

    public bool isPlaying = false;

    private AudioSource boogieSong;
    private float showTimeAcc = 0;

    private int currStartIndex = 0;
    private float currTimeAcc = 0;

    public void Start() {
        boogieSong = GetComponent<AudioSource>();

        boogieText.enabled = false;
    }

    public void Update() {
        if (!boogieText.enabled) {
            return;
        }

        showTimeAcc += Time.deltaTime;
        if (showTimeAcc >= boogieShowDuration) {
            HideBoogieText();
            return;
        }

        currTimeAcc += Time.deltaTime;
        if (currTimeAcc < updateTime) {
            return;
        }

        StringBuilder sb = new StringBuilder();
        int i = currStartIndex;
        foreach (char c in boogieString) {
            sb.Append($"<color={RainbowColors[i]}>{c}</color>");
            i = (i + 1) % RainbowColors.Length;
        }

        boogieText.text = sb.ToString();
        currStartIndex--;
        if (currStartIndex < 0) {
            currStartIndex = RainbowColors.Length - 1;
        }

        currTimeAcc = 0;
    }

    public void OnTriggerEnter(Collider other) {
        isPlaying = !isPlaying;
        if (isPlaying) {
            EnableBoogieText();
            boogieSong.Play();
        } else {
            HideBoogieText();
            boogieSong.Stop();
        }
    }

    private void EnableBoogieText() {
        Watch.GetInstance().HideTexts();

        boogieText.enabled = true;
        showTimeAcc = 0;
        currTimeAcc = updateTime; // So that it updates directly
    }

    private void HideBoogieText() {
        Watch.GetInstance().ShowTexts();

        boogieText.enabled = false;
    }
}
