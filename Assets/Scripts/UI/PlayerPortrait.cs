using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerPortrait : MonoBehaviour
{
    [Header("References")]
    public Image portraitImage;
    public Health playerHealth;

    [Header("Sprites by Health %")]
    public Sprite hp80_100;
    public Sprite hp50_79;
    public Sprite hp15_49;
    public Sprite hp1_14;
    public Sprite damagedSprite;
    public Sprite deadSprite;

    [Header("Shake Settings")]
    public float shakeDuration = 0.15f;
    public float shakeStrength = 6f;

    Vector3 originalPosition;
    bool isDead;
    bool isTakingDamage;

    void Start()
    {
        originalPosition = transform.localPosition;

        if (playerHealth != null)
        {
            playerHealth.OnDamaged += OnDamageTaken;
            playerHealth.OnDied += OnPlayerDied;
        }

        UpdatePortrait();
    }

    void Update()
    {
        if (!isDead && !isTakingDamage)
            UpdatePortrait();
    }

    void UpdatePortrait()
    {
        float percent = playerHealth.CurrentHealthPercent;

        if (percent >= 0.8f)
            portraitImage.sprite = hp80_100;
        else if (percent >= 0.5f)
            portraitImage.sprite = hp50_79;
        else if (percent >= 0.15f)
            portraitImage.sprite = hp15_49;
        else if (percent > 0)
            portraitImage.sprite = hp1_14;
    }

    void OnDamageTaken()
    {
        if (isDead) return;

        StopAllCoroutines();
        StartCoroutine(DamageReaction());
    }

    IEnumerator DamageReaction()
    {
        isTakingDamage = true;

        if (damagedSprite != null)
            portraitImage.sprite = damagedSprite;

        yield return StartCoroutine(Shake());

        isTakingDamage = false;
    }

    void OnPlayerDied()
    {
        isDead = true;

        if (deadSprite != null)
            portraitImage.sprite = deadSprite;
    }

    IEnumerator Shake()
    {
        float time = 0f;

        while (time < shakeDuration)
        {
            Vector3 offset = Random.insideUnitCircle * shakeStrength;
            transform.localPosition = originalPosition + offset;
            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnDamaged -= OnDamageTaken;
            playerHealth.OnDied -= OnPlayerDied;
        }
    }
}
