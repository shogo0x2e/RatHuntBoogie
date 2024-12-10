using System;
using System.Text;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class WatchButton : MonoBehaviour {
    [SerializeField] private TextMeshPro boogieText;
    [SerializeField] private float boogieShowDuration = 6F;
    [SerializeField] public float updateTime = 0.066F;
    [SerializeField] private GameObject WatchScreen;
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

    private float halfWidth;
    private float halfHeight;

    private const float coolDownBeforePress = 0.2F;
    private float coolDownBeforePressTimeAcc = 0;

    public void Start() {
        boogieSong = GetComponent<AudioSource>();

        halfWidth = Mathf.Abs(watchLeft.transform.localPosition.x);
        halfHeight = Mathf.Abs(watchUp.transform.localPosition.y);

        boogieText.enabled = false;
    }

    public void Update() {
        coolDownBeforePressTimeAcc += Time.deltaTime;

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

    public void FixedUpdate() {
        if (!boogieText.enabled) {
            return;
        }

        if (Random.Range(0F, 1F) < 0.056F) {
            SpawnBoogieParticle();
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (!AllowTrigger(other)) {
            return;
        }

        if (coolDownBeforePressTimeAcc < coolDownBeforePress) {
            return;
        }

        coolDownBeforePressTimeAcc = 0;

        isPlaying = !isPlaying;
        if (isPlaying) {
            EnableBoogieText();
            boogieSong.Play();
        } else {
            HideBoogieText();
            boogieSong.Stop();
        }
    }

    private static bool AllowTrigger(Object other) {
        return other.name.Contains("Paw");
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
        const float mFactor = 1.36F;
        const float dFactor = 3.2F;

        float rdX = Random.Range(mFactor * halfWidth / dFactor, mFactor * halfWidth);
        if (Random.Range(0F, 1F) < 1F / 2F) {
            rdX = -rdX;
        }

        float rdY = Random.Range(-mFactor * halfHeight / dFactor, mFactor * halfHeight);
        if (Random.Range(0F, 1F) < 1F / 2F) {
            rdY = -rdY;
        }

        float rdZ = Random.Range(-0.08F, -0.02F);

        GameObject spawnedParticle = Instantiate(boogieParticle, WatchScreen.transform);
        spawnedParticle.transform.localPosition = new Vector3(rdX, rdY, rdZ);

        Destroy(spawnedParticle.gameObject, 2.46F);
    }
}
