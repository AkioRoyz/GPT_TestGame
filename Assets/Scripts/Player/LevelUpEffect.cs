using UnityEngine;

public class LevelUpEffect : MonoBehaviour
{
    [Header("References")]
    public PlayerExperience experience;

    [Header("Visual")]
    public GameObject levelUpEffectPrefab;
    public Vector3 spawnOffset = new Vector3(0, 1.5f, 0);

    void Start()
    {
        if (experience != null)
            experience.OnLevelUp += PlayLevelUpEffect;
    }

    private void OnDestroy()
    {
        if (experience != null)
            experience.OnLevelUp -= PlayLevelUpEffect;
    }

    void PlayLevelUpEffect()
    {
        if (levelUpEffectPrefab == null) return;

        Instantiate(
            levelUpEffectPrefab,
            transform.position + spawnOffset,
            Quaternion.identity
        );
    }
}
