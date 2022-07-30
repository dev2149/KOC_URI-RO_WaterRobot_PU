using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Net;
using System;
using TMPro;
using Lean.Touch;
public class ManualController : MonoBehaviour
{
    [HideInInspector] public GameObject[] _ObjectGroup;
    GameObject _CanvasSelectImage;
    bool _FlagSelectImage;
    public GameObject[] _ActiveObjectUI;
    TMP_InputField _WritePassWord;
    public string PassWord { get { return _PassWord; } }
    string _PassWord;
    int _flagNum;
    public int FlagNum { get { return _flagNum; } }
    bool _PassWordActivity;
    // PasswordFaild
    GameObject _FaildPassword;
    // RoTate , Fixed , Init Button
    Button[] _ObjectControllButtonGroup;
    [HideInInspector] public Sprite[] _RockSprite;
    LeanPinchScale _ObejctScale;
    public bool _Lock;
    // ButtonActive
    Button[] _ManualButtonGroup;
    // ObjectRotateInit
    GameObject _TargetObject;
    // BackGround
    bool _BGFlag;
    GameObject _BackGround;
    Button _EnvironmentBtn;
    // Brochure
    public GameObject _Brochure;
    public bool _FalgBrochuer;
    // CheckList
    CheckListController _CheckList;
    // Audio
    public AudioSource music_play;
    public AudioClip clip_item;
    void Start()
    {
        ChildLaod();
    }
    void ChildLaod()
    {
        // ObectGroup
        _flagNum = 0;
        _TargetObject = null;
        _CanvasSelectImage = GameObject.Find("Canvas").transform.Find("ItemSelect").gameObject;
        _ObjectGroup = new GameObject[GameObject.Find("AR_ObejctGroup").gameObject.transform.Find("UriRow_PartList").childCount];
        for (int i = 0; i < _ObjectGroup.Length; i++)
        {
            _ObjectGroup[i] = GameObject.Find("AR_ObejctGroup").transform.Find("UriRow_PartList").GetChild(i).gameObject;
            _ObjectGroup[i].SetActive(false);
        }
        // ItemPanel
        _FlagSelectImage = false;
        _CanvasSelectImage.SetActive(_FlagSelectImage);
        // Password
        _WritePassWord = GameObject.Find("Canvas").transform.Find("PasswordPanel").GetComponent<TMP_InputField>();
        _WritePassWord.gameObject.SetActive(false);
        _PassWordActivity = false;
        _FaildPassword = GameObject.Find("Canvas").transform.Find("PasswordFaild").gameObject;
        _FaildPassword.SetActive(false);
        // ObjectControll
        _ObjectControllButtonGroup = new Button[GameObject.Find("Canvas").transform.Find("UIButton").transform.Find("ObjectController").childCount];
        for (int i = 0; i < _ObjectControllButtonGroup.Length; i++)
        {
            _ObjectControllButtonGroup[i] = GameObject.Find("Canvas").transform.Find("UIButton").transform.Find("ObjectController").GetChild(i).GetComponent<Button>();
            _ObjectControllButtonGroup[i].interactable = false;
        }
        _Lock = false;
        _ObejctScale = GameObject.Find("AR_ObejctGroup").transform.Find("UriRow_PartList").GetComponent<LeanPinchScale>();
        _ObejctScale.ScaleClamp = true;
        // Button Group
        _ManualButtonGroup = new Button[GameObject.Find("Canvas").transform.Find("ItemSelect").
            transform.Find("ManualAR").transform.Find("Contents").childCount];
        for (int i = 0; i < _ManualButtonGroup.Length; i++)
        {
            _ManualButtonGroup[i] = GameObject.Find("Canvas").transform.Find("ItemSelect").
            transform.Find("ManualAR").transform.Find("Contents").GetChild(i).GetComponent<Button>();
        }
        // ActiveObjectUi
        _ActiveObjectUI = new GameObject[GameObject.Find("Canvas").transform.Find("ActiveObject").childCount];
        for (int i = 0; i < _ActiveObjectUI.Length; i++)
        {
            _ActiveObjectUI[i] = GameObject.Find("Canvas").transform.Find("ActiveObject").GetChild(i).gameObject;
        }
        _ActiveObjectUI[1].SetActive(false);
        // BackGournd
        _EnvironmentBtn = GameObject.Find("Canvas").transform.Find("UIButton").transform.Find("Environment").GetComponent<Button>();
        _BGFlag = false;
        _BackGround = GameObject.Find("BackGround").transform.Find("BG").gameObject;
        _EnvironmentBtn.interactable = false;
        _BackGround.SetActive(false);
        //Brochure
        _Brochure = GameObject.Find("Canvas").transform.Find("Brochure").gameObject;
        _Brochure.SetActive(false);
        _FalgBrochuer = false;
        //CheckList
        _CheckList = GameObject.Find("Canvas").transform.Find("CheckList").GetComponent<CheckListController>();
        // Audio
        music_play = GetComponent<AudioSource>();
    }
    public void ARPassWord()
    {
        ReadPassword();
        if (_WritePassWord.text == _PassWord)
        {
            _PassWordActivity = true; // 패스 워드 일치시
            ClickManualObject();
        }
        else
        {
            SelectItemPanel();
            _FaildPassword.GetComponent<Text>().text = "비밀번호가 틀렸습니다.";
            _FaildPassword.SetActive(true);
        }
    }
    public void NumIdxChange(int _idx) // 원하는 오브젝트 클릭 했을때 받는 번호
    {
        _flagNum = _idx;
        if (_PassWordActivity)
        {
            // 버튼 활성화
            for (int i = 0; i < _ManualButtonGroup.Length; i++)
            {
                _ManualButtonGroup[i].interactable = true;
            }
            ClickManualObject();
        }
    }
    public void QRCodeAR(int _num)
    {
        if (!_ObjectGroup[_flagNum].activeSelf)
        {
            _EnvironmentBtn.interactable = true; // 배경 활성화
            _ManualButtonGroup[_num].interactable = false;
            _ObjectGroup[_num].SetActive(true);
            _TargetObject = _ObjectGroup[_num].gameObject; // 오브젝트 게임 컴포넌트 저장
            _flagNum = _num;
            ObjectActiveCommand();
        }
    }
    public void ClickManualObject() // 원하는 오브젝트 활성화
    {
        if (!_ObjectGroup[_flagNum].activeSelf)
        {
            Play_Sound(clip_item , music_play);
            SelectItemPanel();
            SetActiveFalseObject();
            _EnvironmentBtn.interactable = true; // 배경 활성화
            _ManualButtonGroup[_flagNum].interactable = false;
            _ObjectGroup[_flagNum].SetActive(true);
            _TargetObject = _ObjectGroup[_flagNum].gameObject; // 오브젝트 게임 컴포넌트 저장
            ObjectActiveCommand();
        }
    }
    public void ObjectActiveCommand()
    {
        for (int i = 0; i < _ObjectControllButtonGroup.Length; i++)
        {
            _ActiveObjectUI[0].SetActive(false); // FitToScanOverRay
            _ObjectControllButtonGroup[i].interactable = true;
        }
    }
    public void SelectItemPanel() // 오브젝트 선택창 및 비밀번호 창 비활성화
    {
        _FlagSelectImage = !_FlagSelectImage;
        _CanvasSelectImage.SetActive(_FlagSelectImage);
        _WritePassWord.gameObject.SetActive(false);
        _WritePassWord.text = "";
        _FalgBrochuer = false;
        _Brochure.SetActive(_FalgBrochuer);
        _CheckList._ThisObjectFlag = false;
        _CheckList.gameObject.transform.Find("Panel").gameObject.SetActive(false);
        _CheckList._WritePassWord = GameObject.Find("Canvas").transform.Find("DataPasswordPanel").GetComponent<TMP_InputField>();
        _CheckList._WritePassWord.gameObject.SetActive(false);
    }
    public void ItemPanelBack()
    {
        _FlagSelectImage = false;
        _CanvasSelectImage.SetActive(_FlagSelectImage);
        _WritePassWord.gameObject.SetActive(false);
        _WritePassWord.text = "";
    }
    #region ButtonControll
    // PositionRock
    public void LockObject()
    {
        _Lock = !_Lock;
        if (_Lock)
        {
            _ObjectControllButtonGroup[1].interactable = false;
            _ObejctScale.ScaleClamp = false;
            SetImage(_RockSprite[0]);
        }
        else
        {
            _ObjectControllButtonGroup[1].interactable = true;
            _ObejctScale.ScaleClamp = true;
            SetImage(_RockSprite[1]);
        }
    }
    // Position Init
    public void ObjectPositionInit()
    {
        if (_TargetObject != null)
        {
            _TargetObject.gameObject.transform.localRotation = Quaternion.identity;
            _TargetObject.GetComponent< ObjectRotateController>().xAngle = 0;
            _TargetObject.GetComponent< ObjectRotateController>().yAngle = 0;
        }
            _ObejctScale.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); // 사이즈 초기화
    }
    // Object Active False
    public void SetActiveFalseObject()
    {
        for (int i = 0; i < _ObjectGroup.Length; i++)
        {
            _ObjectGroup[i].SetActive(false);
            _ManualButtonGroup[i].interactable = true;
        }
        var QRSet = _ObjectGroup[0].GetComponentInParent<QrCodeScanController>();
        ObjectPositionInit();
        _EnvironmentBtn.interactable = false;
        _BackGround.SetActive(false);
        _ActiveObjectUI[0].SetActive(true);
        _ActiveObjectUI[1].SetActive(false);
        for (int i = 0; i < _ObjectControllButtonGroup.Length; i++)
        {
            _ObjectControllButtonGroup[i].interactable = false;
        }
        QRSet.qrcode.registeredString = "a"; // qr 코드 초기화 해주는 부분
    }
    public void ActiveFlagBG()
    {
        _BGFlag = !_BGFlag;
        if (_BGFlag)
        {
            _EnvironmentBtn.interactable = true;
            _BackGround.SetActive(true);
        }
        else
        {
            _BackGround.SetActive(false);
        }
    }
    public void OpenPDF()
    {
        _FalgBrochuer = !_FalgBrochuer;
        if (_FalgBrochuer)
        {
            ItemPanelBack();
            SetActiveFalseObject();
            ObjectPositionInit();
            _CheckList.AllInit();
            var _val = _Brochure.transform.Find("ViewPort").transform.Find("Content").gameObject;
            _val.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            _Brochure.SetActive(true);
        }
        else
        {
            _Brochure.SetActive(false);
        }
    }
    #endregion
    void SetImage(Sprite _Img)
    {
        _ObjectControllButtonGroup[2].GetComponent<Image>().sprite = _Img;
    }
    public void Play_Sound(AudioClip _clip , AudioSource _source)
    {
        _source.Stop();
        _source.clip = _clip;
        _source.time = 0.0f;
        _source.Play();
    }
    public void ReadPassword() // 서버 에서 비밀번호 정보 읽어 드리기
    {
        // KOC Password server 회사 내부 정보
        string ftpPath = "";
        string user = "";
        string pass = "";
        //string ftpPath = "ftp://simglab.synology.me/Server/Password.txt";
        //string user = "simgserver";
        //string pass = "Simg2732!";
        FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpPath);
        req.Method = WebRequestMethods.Ftp.DownloadFile;
        req.Credentials = new NetworkCredential(user, pass);

        using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
        {
            Stream stream = resp.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream))
            {
                _PassWord = reader.ReadToEnd();
                Debug.Log(_PassWord);
            }
        }
    }
    public void app_quit()
    {
        Debug.Log("종료!");
        Application.Quit();
    }
}
