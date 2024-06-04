using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EffectManager : MonoBehaviour
{
    [SerializeField] VisualEffect effect;
    [SerializeField] float effectDuration;

    public void ActivateEffect()
    {
        StartCoroutine(EffectCoroutine());
    }

    private IEnumerator EffectCoroutine()
    {
        effect.gameObject.SetActive(true);
        yield return new WaitForSeconds(effectDuration);
        effect.gameObject.SetActive(false);
    }
}
