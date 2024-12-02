using System.Text;
using TMPro;
using UnityEngine;

public class WatchButton : MonoBehaviour {
    [SerializeField] private TextMeshPro boogieText;
    [SerializeField] private float boogieShowDuration = 6F;
    [SerializeField] public float updateTime = 0.066F;
    [SerializeField] private GameObject watchCenter;
    [SerializeField] private GameObject watchLeft;
    [SerializeField] private GameObject watchUp;
    [SerializeField] private GameObject boogieParticle;

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

    private Vector3 boogieParticleCenter;
    private float halfWidth;
    private float halfHeight;

    public void Start() {
        boogieSong = GetComponent<AudioSource>();

        boogieParticleCenter = watchCenter.transform.position;
        halfWidth = Mathf.Abs(watchLeft.transform.localPosition.x);
        halfHeight = Mathf.Abs(watchUp.transform.localPosition.y);

        boogieText.enabled = false;
    }

    public void Update() {
        if (!boogieText.enabled) {
            return;
        }

        if (Random.Range(0F, 1F) < 0.1F) {
            SpawnBoogieParticle();
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

    private void SpawnBoogieParticle() {
        float rdX = Random.Range(boogieParticleCenter.x - 2 * halfWidth, boogieParticleCenter.x + 2 * halfWidth);
        float rdY = Random.Range(boogieParticleCenter.y - 2 * halfHeight, boogieParticleCenter.y + 2 * halfHeight);
        float rdZ = Random.Range(-0.1F, 0.1F);

        float rdScale = Random.Range(0.6F, 1.2F);

        GameObject spawnedParticle = Instantiate(boogieParticle,
            new Vector3(rdX, rdY, rdZ),
            Random.rotation);
        spawnedParticle.transform.localScale = new Vector3(rdScale, rdScale, rdScale);
    }
}
