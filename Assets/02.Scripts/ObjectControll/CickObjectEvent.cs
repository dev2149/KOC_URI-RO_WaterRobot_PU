using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CickObjectEvent : MonoBehaviour
{
    [SerializeField]
    enum Division
    {
        Part,
        Main
    }
    [SerializeField] Division _DivisionObject;
    GameObject _ObjectClickUI;
    GameObject[] _ExplantionPanel;
    ManualController _ManualController;
    private void OnEnable()
    {
        _ManualController = GameObject.Find("MaualARLogic").GetComponent<ManualController>();
        // 설명창
        _ExplantionPanel = new GameObject[GameObject.Find("Canvas").transform.Find("ExplationImage").childCount];
        for (int i = 0; i < _ExplantionPanel.Length; i++)
        {
            _ExplantionPanel[i] = GameObject.Find("Canvas").transform.Find("ExplationImage").GetChild(i).gameObject;
            _ExplantionPanel[i].SetActive(false);
        }
        _ObjectClickUI = GameObject.Find("Canvas").transform.Find("ActiveObject").transform.Find("ObjectClickText").gameObject;
        if (_DivisionObject == Division.Part)
        {
            _ObjectClickUI.SetActive(true);
        }
        else
        {
            _ObjectClickUI.SetActive(false);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < _ExplantionPanel.Length; i++)
        {
            _ExplantionPanel[i].SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.touchCount > 0 && _DivisionObject == Division.Part)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Physics.Raycast(ray, out hit);

            if (hit.collider != null)
            {
                _ObjectClickUI.SetActive(false);
                _ExplantionPanel[_ManualController.FlagNum].SetActive(true);
            }
        }
    }
}