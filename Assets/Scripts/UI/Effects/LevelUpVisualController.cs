using UnityEngine;
using TMPro;
using System.Collections;

public class LevelUpVisualController : MonoBehaviour
{
    [Header("Icon Settings")]
    public GameObject iconPrefab;
    public int iconCount = 6;

    public Vector2 spawnAreaSize = new Vector2(1f, 0.3f);
    public Vector2 iconScaleRange = new Vector2(0.6f, 1.2f);
    public Vector2 iconSpeedRange = new Vector2(1.5f, 2.5f);
    public Vector2 iconLifetimeRange = new Vector2(0.8f, 1.2f);

    [Header("Text Reference")]
    public Transform levelUpText;

    [Header("Text Movement")]
    [Tooltip("Скорость движения текста вверх")]
    public float textMoveSpeed = 1f;

    [Tooltip("Общее время жизни текста")]
    public float textLifetime = 1.5f;

    [Header("Text Fade")]
    [Tooltip("Когда начинается fade (0–1 от времени жизни)")]
    [Range(0f, 1f)]
    public float fadeStartNormalized = 0.6f;

    [Tooltip("Использовать плавный fade (SmoothStep)")]
    public bool smoothFade = true;

    [Header("Text Scale")]
    [Tooltip("Начальный масштаб текста")]
    public float startTextScale = 1f;

    [Tooltip("Конечный масштаб текста")]
    public float endTextScale = 1.2f;

    private TextMeshPro textMesh;

    void Start()
    {
        SpawnIcons();

        if (levelUpText != null)
        {
            textMesh = levelUpText.GetComponent<TextMeshPro>();
            StartCoroutine(MoveFadeAndDestroyText());
        }

        Destroy(gameObject, 2.5f); // страховка
    }

    private void SpawnIcons()
    {
        if (iconPrefab == null) return;

        for (int i = 0; i < iconCount; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f),
                0f
            );

            GameObject icon = Instantiate(
                iconPrefab,
                transform.position + randomOffset,
                Quaternion.identity,
                transform
            );

            float scale = Random.Range(iconScaleRange.x, iconScaleRange.y);
            icon.transform.localScale = Vector3.one * scale;

            float speed = Random.Range(iconSpeedRange.x, iconSpeedRange.y);
            float lifetime = Random.Range(iconLifetimeRange.x, iconLifetimeRange.y);

            icon.GetComponent<LevelUpIconFly>()
                .Init(Vector3.up, speed, lifetime);
        }
    }

    private IEnumerator MoveFadeAndDestroyText()
    {
        float timer = 0f;

        Vector3 startPos = levelUpText.position;
        levelUpText.localScale = Vector3.one * startTextScale;

        Color startColor = textMesh.color;

        while (timer < textLifetime)
        {
            float t = timer / textLifetime;

            // Движение вверх
            levelUpText.position += Vector3.up * textMoveSpeed * Time.deltaTime;

            // Масштаб
            float scale = Mathf.Lerp(startTextScale, endTextScale, t);
            levelUpText.localScale = Vector3.one * scale;

            // Fade
            float fadeT = Mathf.Clamp01(
                (t - fadeStartNormalized) / (1f - fadeStartNormalized)
            );

            float alpha = smoothFade
                ? Mathf.SmoothStep(1f, 0f, fadeT)
                : Mathf.Lerp(1f, 0f, fadeT);

            Color c = startColor;
            c.a = alpha;
            textMesh.color = c;

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(levelUpText.gameObject);
    }
}
