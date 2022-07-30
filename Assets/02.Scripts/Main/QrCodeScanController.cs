using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QrCodeScanController : MonoBehaviour
{
    ManualController _MainObejctInfo;
    public QRFoundation.QRCodeTracker qrcode;
    // Start is called before the first frame update
    void Start()
    {
        ChildLaod();
    }
    void ChildLaod()
    {
        _MainObejctInfo = GameObject.Find("MaualARLogic").GetComponent<ManualController>();
        _MainObejctInfo._ObjectGroup = new GameObject[GameObject.Find("AR_ObejctGroup").gameObject.transform.Find("UriRow_PartList").childCount];
        for (int i = 0; i < _MainObejctInfo._ObjectGroup.Length; i++)
        {
            _MainObejctInfo._ObjectGroup[i] = GameObject.Find("AR_ObejctGroup").transform.Find("UriRow_PartList").GetChild(i).gameObject;
            _MainObejctInfo._ObjectGroup[i].SetActive(false);
        }
        qrcode = GameObject.Find("AR Camera").GetComponent<QRFoundation.QRCodeTracker>();

        if (qrcode.registeredString == "http://m.site.naver.com/" + "0XkvY" + "QRCodeImg")
        {
            _MainObejctInfo.QRCodeAR(0);
        }
        else if (qrcode.registeredString == "http://m.site.naver.com/" + "0Xkw5" + "QRCodeImg")
        {
            _MainObejctInfo.QRCodeAR(1);
        }
        else if (qrcode.registeredString == "http://m.site.naver.com/" + "0XkwM" + "QRCodeImg")
        {
            _MainObejctInfo.QRCodeAR(2);
        }
        else if (qrcode.registeredString == "http://m.site.naver.com/" + "0XkwT" + "QRCodeImg")
        {
            _MainObejctInfo.QRCodeAR(3);
        }
        else if (qrcode.registeredString == "http://m.site.naver.com/" + "0Xkwg" + "QRCodeImg")
        {
            _MainObejctInfo.QRCodeAR(4);
        }
        else if (qrcode.registeredString == "http://m.site.naver.com/" + "0Xkwz" + "QRCodeImg")
        {
            _MainObejctInfo.QRCodeAR(5);
        }
        else if (qrcode.registeredString == "http://m.site.naver.com/" + "0XkwF" + "QRCodeImg")
        {
            _MainObejctInfo.QRCodeAR(6);
        }
        else if (qrcode.registeredString == "http://m.site.naver.com/" + "0Xkws" + "QRCodeImg")
        {
            _MainObejctInfo.QRCodeAR(7);
        }
        else if (qrcode.registeredString == "http://m.site.naver.com/" + "0XkvL" + "QRCodeImg")
        {
            _MainObejctInfo.QRCodeAR(8);
        }
        else if (qrcode.registeredString == "http://m.site.naver.com/" + "0XkuO" + "QRCodeImg")
        {
            _MainObejctInfo.QRCodeAR(9);
        }
    }


}
