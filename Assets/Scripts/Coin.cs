using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Coin : MonoBehaviour
{
    private void OnEnable()
    {
        UpDown();
    }
    private void UpDown()
    {
        var sequance = DOTween.Sequence();
        sequance.Append(gameObject.transform.DOMoveY(1, .5f));
        sequance.Append(gameObject.transform.DOMoveY(0, .5f)).OnComplete(() => UpDown());
    }
}
