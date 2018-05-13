using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class IOHelper
{
    /// <summary>
    /// 获取文件字节长度;
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static long GetFileByteLength(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        return fileInfo.Length;
    }

    /// <summary>
    /// 复制文件;
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="copyPath"></param>
    public static void CopyFiles(string filePath, string copyPath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string dir = copyPath.Substring(0, copyPath.LastIndexOf("\\"));
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.Copy(filePath, copyPath, true);
            }
            else
            {
                if (!Directory.Exists(copyPath))
                {
                    Directory.CreateDirectory(copyPath);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }

    }
    /// <summary>
    /// 本地 移动文件夹及其文件
    /// </summary>
    /// <param name="dirPath"></param>
    /// <param name="copyPath"></param>
    public static void MoveDir(string dirPath, string copyPath)
    {
        try
        {
            Directory.Move(dirPath, copyPath);
        }
        catch (Exception e)
        {
            throw new Exception(e.ToString());
        }
    }
    /// <summary>
    /// 拷贝文件夹;
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    public static void CopyDirectory(string from, string to)
    {
        if (!Directory.Exists(to))
            Directory.CreateDirectory(to);
        // 子文件夹;
        foreach (string sub in Directory.GetDirectories(from))
            CopyDirectory(sub + "/", to + Path.GetFileName(sub) + "/");
        // 文件;
        foreach (string file in Directory.GetFiles(from))
            File.Copy(file, to + Path.GetFileName(file), true);
    }
    /// <summary>
    /// 保存文本
    /// </summary>
    /// <param name="info"></param>
    /// <param name="savePath"></param>
    public static void CreateTextFile(string txt, string savePath)
    {
        using (FileStream fst = new FileStream(savePath, FileMode.Create))
        {
            //写数据到txt
            using (StreamWriter swt = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8")))
            {
                //写入
                swt.WriteLine(txt);
            }
        }
    }

    /// <summary>
    /// 删除文件夹
    /// </summary>
    /// <param name="path"></param>
    public static void DeleteFolder(string path)
    {
        string[] strTemp;

        //先删除该目录下的文件
        strTemp = System.IO.Directory.GetFiles(path);
        foreach (string str in strTemp)
        {
            System.IO.File.Delete(str);
        }
        //删除子目录，递归
        strTemp = System.IO.Directory.GetDirectories(path);
        foreach (string str in strTemp)
        {
            DeleteFolder(str);
        }
        //删除该目录
        System.IO.Directory.Delete(path);
    }
    /// <summary>
    /// 打开文本;
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string OpenText(string file)
    {
        string result = string.Empty;
        if(!File.Exists(file)){
            return string.Empty;
        }
        using (StreamReader sr = new StreamReader(file, Encoding.UTF8))
        {
            result = sr.ReadToEnd();
        }
        return result;
    }

    public static bool CheckBitInByte(byte b, int index)
    {
        bool isTrue = false;
        switch (index)
        {
            case 7:
                isTrue = (b & 128) == 128 ? true : false;
                break;
            case 6:
                isTrue = (b & 64) == 64 ? true : false;
                break;
            case 5:
                isTrue = (b & 32) == 32 ? true : false;
                break;
            case 4:
                isTrue = (b & 16) == 16 ? true : false;
                break;
            case 3:
                isTrue = (b & 8) == 8 ? true : false;
                break;
            case 2:
                isTrue = (b & 4) == 4 ? true : false;
                break;
            case 1:
                isTrue = (b & 2) == 2 ? true : false;
                break;
            case 0:
                isTrue = (b & 1) == 1 ? true : false;
                break;
        }
        return isTrue;
    }


}
