using System;
using Aladdin.HASP;
using UnityEngine;
public class SafeNet : MonoBehaviour
{
    private Hasp hasp;
    private HaspDemo haspDemo=new HaspDemo();
    private string checkIndex ="";
    private string device = "";
    private string writeStrs="";
    private string readStrs = "";
    private int FeatureID = 9090;

    //加密狗初始化//
    public void  InitDog()
    {
        if (ioo.ioManager.inputType == E_InputType.Builtin)
        {
            return;
        }

        haspDemo = new HaspDemo();
        hasp = haspDemo.LoginDemo(HaspFeature.FromFeature(FeatureID));
        
        if (hasp == null||!hasp.IsLoggedIn())
        {
            Application.Quit();
            Debug.Log("登录加密狗失败！！！");
            return;
        }
        //haspDemo.WriteMessageFirst(hasp, HaspFileId.ReadWrite, "");
        device = SystemInfo.deviceUniqueIdentifier;
        //每次开启游戏时随机向加密狗中写入一个数字,防止监听及共享打印机破解加密狗\\
        long index =(long) UnityEngine.Random.Range(1000000000, 99999999999999);
        checkIndex =index.ToString();
        
        readStrs = haspDemo.ReadToStr(hasp,HaspFileId.ReadWrite);
        if (GetDeviceStr(readStrs)== "")
        {
            writeStrs += device;
        }
        else
        {
            writeStrs += GetDeviceStr(readStrs);
        }
        writeStrs += ",";
        writeStrs += checkIndex;
        
        haspDemo.WriteMessageFirst(hasp,HaspFileId.ReadWrite,writeStrs);
    }

    public void  CheckOutDog()
    {
        if (ioo.ioManager.inputType == E_InputType.Builtin)
        {
            return;
        }

        hasp = haspDemo.LoginDemo(HaspFeature.FromFeature(FeatureID));
        if (hasp == null || !hasp.IsLoggedIn())
        {
            Application.Quit();
            Debug.Log("登录加密狗失败！！！");
            return;
        }
        readStrs = haspDemo.ReadToStr(hasp, HaspFileId.ReadWrite);
        string devicestr =GetDeviceStr(readStrs);
        string checkindexStr =GetCheckIndexStr(readStrs);
        //每隔一段时间检验写入的数据是否是游戏一开始的数据//
       // Debug.Log(checkIndex + "  :  " + checkindexStr);
        //Debug.Log(device + "  :  " + devicestr);
        if (checkIndex != checkindexStr)
        {
            Application.Quit();
            Debug.Log("检验失败！！！");
            return;
        }
        if (device != devicestr)
        {
            Application.Quit();
            Debug.Log("检验失败！！！");
            return;
        }
        //DateTime time = DateTime.Now;
        //HaspStatus status = hasp.GetRtc(ref time);
        ////检验加密数据，总共120组，每个月随机其中的40组，3个月一个循环//
        //try
        //{
        //    int index = (UnityEngine.Random.Range(time.Month % 3 * 40, time.Month % 3 * 40 + 40));
        //    //Debug.Log(index+" : "+time.Month);
        //    string checkCode =VendorCode.DeCodeStrs[index];
        //    hasp.Decrypt(ref checkCode);
        //   // Debug.Log(checkCode + "  :  " + VendorCode.EncryptStrs[index]);
        //    if (checkCode != VendorCode.EncryptStrs[index])
        //    {
        //        Debug.Log("检验失败！！！");
        //        Application.Quit();
        //    }
        //}
        //catch
        //{
        //    int index = UnityEngine.Random.Range(0, VendorCode.DeCodeStrs.Length);
        //    string checkCode = VendorCode.DeCodeStrs[index];
        //    hasp.Decrypt(ref checkCode);
        //    Debug.Log(checkCode + "  :  " + VendorCode.EncryptStrs[index]);
        //    if (checkCode != VendorCode.EncryptStrs[index])
        //    {
        //        Debug.Log("检验失败！！！");
        //        Application.Quit();
        //    }
        //}
    }

    void CloseDog()
    {
        if (hasp == null)
            return;
        hasp.Logout();
        hasp.Dispose();
    }

    void OnDestroy()
    {
        CloseDog();
    }

    string GetDeviceStr(string value)
    {
        try
        {
            return value.Substring(0, value.IndexOf(","));
        }
        catch
        {
            return "";
        }
    }

    string GetCheckIndexStr(string value)
    {
        try
        {
            return value.Substring(value.IndexOf(",") + 1, value.Length - value.IndexOf(",") - 1);
        }
        catch
        {
            return "";
        }
       
    }

}
