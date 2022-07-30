using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeInOutController : MonoBehaviour
{
    [SerializeField] Image _ImageFadeInOut;
    [SerializeField] Text _TxTFadeInOut;
    float create_timer = 3.0f;
    private void Start()
    {
        ChildLaod();
    }
    void ChildLaod()
    {
        _ImageFadeInOut = this.gameObject.transform.Find("Image").GetComponent<Image>();
        _TxTFadeInOut = this.gameObject.transform.Find("Image").transform.Find("Text").GetComponent<Text>();
        _ImageFadeInOut.color = new Color(_ImageFadeInOut.color.r, _ImageFadeInOut.color.g, _ImageFadeInOut.color.b, 1.0f);
        _TxTFadeInOut.color = new Color(_TxTFadeInOut.color.r, _TxTFadeInOut.color.g, _TxTFadeInOut.color.b, 1.0f);
        StartCoroutine(FIOCoroutine());
    }
    private void OnEnable()
    {
        _ImageFadeInOut = this.gameObject.transform.Find("Image").GetComponent<Image>();
        _TxTFadeInOut = this.gameObject.transform.Find("Image").transform.Find("Text").GetComponent<Text>();
        _ImageFadeInOut.color = new Color(_ImageFadeInOut.color.r, _ImageFadeInOut.color.g, _ImageFadeInOut.color.b, 1.0f);
        _TxTFadeInOut.color = new Color(_TxTFadeInOut.color.r, _TxTFadeInOut.color.g, _TxTFadeInOut.color.b, 1.0f);
        StartCoroutine(FIOCoroutine());
    }
    IEnumerator FIOCoroutine()
    {
        while (_ImageFadeInOut.color.a > 0.0f)
        {
            _ImageFadeInOut.color = new Color(_ImageFadeInOut.color.r, _ImageFadeInOut.color.g, _ImageFadeInOut.color.b, _ImageFadeInOut.color.a - (Time.deltaTime / create_timer));
            _TxTFadeInOut.color = new Color(_TxTFadeInOut.color.r, _TxTFadeInOut.color.g, _TxTFadeInOut.color.b, _TxTFadeInOut.color.a - (Time.deltaTime / create_timer));
            yield return null;
        }
        this.gameObject.SetActive(false);
    }
}