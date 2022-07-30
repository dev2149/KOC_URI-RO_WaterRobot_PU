using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Net;
using System;
using TMPro;
public class CheckListController : MonoBehaviour
{
    // 절대 귀찬아서 그냥 배열 개수 준거 아님(사실 귀찬;;)
    [SerializeField] bool[] _Check = new bool[10];
    [SerializeField] GameObject[] _CheckObject = new GameObject[10];
    public bool _ThisObjectFlag;
    ManualController _ManualController;
    public TMP_InputField _WritePassWord;
    // FaildPassWord
    [SerializeField] GameObject _FaildPassWord;
    private void Start()
    {
        ChildLaod();
    }
    void ChildLaod()
    {
        // Password
        _ManualController = GameObject.Find("MaualARLogic").GetComponent<ManualController>();
        _WritePassWord = GameObject.Find("Canvas").transform.Find("DataPasswordPanel").GetComponent<TMP_InputField>();
        _WritePassWord.gameObject.SetActive(false);
        _ThisObjectFlag = false;
        for (int i = 0; i < _CheckObject.Length; i++)
        {
            _CheckObject[i] = this.gameObject.transform.Find("Panel").transform.Find("ContentCheck").GetChild(i).gameObject;
        }
        this.gameObject.transform.Find("Panel").gameObject.SetActive(false);
        _FaildPassWord = GameObject.Find("Canvas").transform.Find("PasswordFaild").gameObject;
        CheckListInti();
    }
    public void OpenList()
    {
        _ThisObjectFlag = !_ThisObjectFlag;
        CheckListInti();
        if (_ThisObjectFlag)
        {
            this.gameObject.transform.Find("Panel").gameObject.SetActive(true);
            _ManualController.SetActiveFalseObject();
            _ManualController.ItemPanelBack();
            _ManualController._FalgBrochuer = false;
            _ManualController._Brochure.SetActive(false);
        }
        else
        {
            this.gameObject.transform.Find("Panel").gameObject.SetActive(false);
            _WritePassWord.gameObject.SetActive(false);
            _WritePassWord.text = "";
        }
    }
    public void CheckList(int _ObjNum)
    {
        _Check[_ObjNum] = !_Check[_ObjNum];
        if (_Check[_ObjNum])
        {
            _CheckObject[_ObjNum].GetComponent<Text>().text = "√";
        }
        else
        {
            _CheckObject[_ObjNum].GetComponent<Text>().text = "";
        }
    }
    public void AllInit()
    {
        for (int i = 0; i < _Check.Length; i++)
        {
            _Check[i] = false;
            _CheckObject[i].GetComponent<Text>().text = "";
        }
        this.gameObject.transform.Find("Panel").gameObject.SetActive(false);
        _WritePassWord.gameObject.SetActive(false);
        _WritePassWord.text = "";
        _ThisObjectFlag = false;
    }
    public void ARPassWord()
    {
        _ManualController.ReadPassword();
        if (_WritePassWord.text == _ManualController.PassWord)
        {
            _WritePassWord.gameObject.SetActive(false);
            _WritePassWord.text = "";
            CSVCreate();
        }
        else
        {
            _WritePassWord.gameObject.SetActive(false);
            _WritePassWord.text = "";
            Alarm("비밀번호가 틀렸습니다.");
        }
    }
    public void CSVCreate()
    {
        using (var writer = new CsvFileWriter(Application.persistentDataPath + "/URI-T_Check_List.csv"))
        {
            List<string> columns = new List<string>() { "No.", "Item", "CheckList" };
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add("01");
            columns.Add("HPU Motor");
            columns.Add(_Check[0] == true ? "O" : "X");
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add("02");
            columns.Add("HPU Pump");
            columns.Add(_Check[1] == true ? "O" : "X");
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add("03");
            columns.Add("Waterpump Motor");
            columns.Add(_Check[2] == true ? "O" : "X");
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add("04");
            columns.Add("Waterjet System");
            columns.Add(_Check[3] == true ? "O" : "X");
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add("05");
            columns.Add("Manipulator System");
            columns.Add(_Check[4] == true ? "O" : "X");
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add("06");
            columns.Add("TSS440");
            columns.Add(_Check[5] == true ? "O" : "X");
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add("07");
            columns.Add("TSS350");
            columns.Add(_Check[6] == true ? "O" : "X");
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add("08");
            columns.Add("Multibeam Sonar");
            columns.Add(_Check[7] == true ? "O" : "X");
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add("09");
            columns.Add("DVL");
            columns.Add(_Check[8] == true ? "O" : "X");
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add("10");
            columns.Add("Depth Sensor");
            columns.Add(_Check[9] == true ? "O" : "X");
            writer.WriteRow(columns);
            columns.Clear();
        }
        call_ftpfileupload("URI-T_Check_List", Application.persistentDataPath + "/URI-T_Check_List.csv");
        Alarm("데이터를 전송 했습니다.");
        CheckListInti();
    }
    public void call_ftpfileupload(string server, string filename)
    {
        string serverFulladd = "URL"; // 회사 서버 주소 임으로 공유 불가
        
        string onlyfilename = Path.GetFileName(filename);
        // ftp 접속 설정
        FtpWebRequest ftprequest = (FtpWebRequest)WebRequest.Create(serverFulladd + onlyfilename);
        ftprequest.Credentials = new NetworkCredential("", ""); // ID PA 임으로 공유 불가

        ftprequest.UsePassive = true;
        ftprequest.UseBinary = true;
        ftprequest.KeepAlive = false;
        ftprequest.Method = WebRequestMethods.Ftp.UploadFile;

        // 파일 읽어오기
        byte[] fileContents = File.ReadAllBytes(filename);
        Stream requestStream = ftprequest.GetRequestStream();
        ftprequest.ContentLength = fileContents.Length;
        requestStream.Write(fileContents, 0, fileContents.Length);
        requestStream.Close();

        // 전송 요청
        try
        {
            FtpWebResponse res = (FtpWebResponse)ftprequest.GetResponse();

        }
        catch (WebException e)
        {
            FtpWebResponse response = (FtpWebResponse)e.Response;

            switch (response.StatusCode)
            {
                case FtpStatusCode.ActionNotTakenFileUnavailable:
                    {
                        Console.WriteLine("CreateFolders ] Probably the folder already exist : " + serverFulladd + onlyfilename);
                    }
                    break;
            }
        }
    }
    private void Alarm(string _Exp)
    {
        _FaildPassWord = GameObject.Find("Canvas").transform.Find("PasswordFaild").gameObject;
        _FaildPassWord.transform.Find("Image").transform.Find("Text").GetComponent<Text>().text = _Exp;
        _FaildPassWord.SetActive(true);
    }
    void CheckListInti()
    {
        for (int i = 0; i < _Check.Length; i++)
        {
            _Check[i] = false;
            _CheckObject[i].GetComponent<Text>().text = "";
        }
    }
}
