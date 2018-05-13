/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   CustomFontCreater.cs
 * 
 * 简    介:    
 * 
 * 创建标识：   Pancake 2017/4/8 15:10:07
 * 
 * 修改描述：
 * 
 */

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using LitJson;
using System.Text;
using System.Text.RegularExpressions; 

public class CustomFontCreater
{
    /// <summary>
    /// 修正Unity对TexturePacker打包图集分割不准
    /// </summary>
    [MenuItem("MyTools/TexturePacker/矫正精灵坐标位置")]
    static public void MetaCorrect()
    {      
         foreach (Object o in Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets))
         {
             string AssetFile = AssetDatabase.GetAssetPath(o);
             string PathNameNotExt = AssetFile.Remove(AssetFile.LastIndexOf('.'));
             string TextPathName = PathNameNotExt + ".txt";
             string MatPathName = PathNameNotExt + ".mat";
             string metaPathName = PathNameNotExt + ".png.meta";

             string tempPathName = PathNameNotExt + ".png.txt";

             string FileNameNotExt = PathNameNotExt.Remove(0, PathNameNotExt.LastIndexOf('/') + 1);

             string result = string.Empty;
             using (StreamReader sr = new StreamReader(TextPathName, Encoding.UTF8))
             {
                 result = sr.ReadToEnd();
             }
             JsonData jsonData = JsonMapper.ToObject(result);


             int index = 0;
             string[] lines = System.IO.File.ReadAllLines(metaPathName);
             for (int i = 0; i < lines.Length; ++i)
             {
                 if (!lines[i].Contains("rect:"))
                     continue;
                 ++i;
                 string key = FileNameNotExt + "_" + index++ + ".png";
                 string[] kv = lines[++i].Split(':');
                 kv[1] = jsonData["frames"][key]["frame"]["x"].ToString();
                 lines[i] = kv[0] + ": " + kv[1];

                 kv = lines[++i].Split(':');
                 kv[1] = jsonData["frames"][key]["frame"]["y"].ToString();
                 lines[i] = kv[0] + ": " + kv[1];

                 kv = lines[++i].Split(':');
                 kv[1] = jsonData["frames"][key]["frame"]["w"].ToString();
                 lines[i] = kv[0] + ": " + kv[1];

                 kv = lines[++i].Split(':');
                 kv[1] = jsonData["frames"][key]["frame"]["h"].ToString();
                 lines[i] = kv[0] + ": " + kv[1];
             }

             File.Delete(metaPathName);

             using (StreamWriter sw = new StreamWriter(tempPathName, false))
             {
                 for (int i = 0; i < lines.Length; ++i)
                     sw.WriteLine(lines[i]);
             }

             File.Move(@tempPathName, @metaPathName);
         }

         AssetDatabase.SaveAssets();
         AssetDatabase.Refresh();
    }

    /// <summary>
    /// 创建数字字体,此方法可拓展
    /// </summary>
    [MenuItem("MyTools/Font/TexturePacker 创建数字字体")]
    static public void CreateCustomFont()
    {
        foreach (Object o in Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets))
        {
            Texture2D tex = o as Texture2D;

            if (!tex)
            {
                continue;
            }
            string AssetFile = AssetDatabase.GetAssetPath(o);

            //修改贴图导入方式
            TextureImporter texImporter = (TextureImporter)AssetImporter.GetAtPath(AssetFile);
            if (texImporter == null)
            {
                continue;
            }

            texImporter.textureType = TextureImporterType.Sprite;
            texImporter.textureFormat = TextureImporterFormat.AutomaticTruecolor;

            texImporter.SaveAndReimport();

            tex = (Texture2D)AssetDatabase.LoadAssetAtPath(AssetFile, typeof(Texture2D));

            string PathNameNotExt = AssetFile.Remove(AssetFile.LastIndexOf('.'));

            string FileNameNotExt = PathNameNotExt.Remove(0, PathNameNotExt.LastIndexOf('/') + 1);

            string TextPathName = PathNameNotExt + ".txt";
            string MatPathName  = PathNameNotExt + ".mat";
            string FontPathName = PathNameNotExt + ".fontsettings";


            string result = string.Empty;
            using (StreamReader sr = new StreamReader(TextPathName, Encoding.UTF8))
            {
                result = sr.ReadToEnd();
            }
            JsonData jsonData = JsonMapper.ToObject(result);

            Material mat = (Material)AssetDatabase.LoadAssetAtPath(MatPathName, typeof(Material));
            if (mat == null)
            {
                //创建材质球
                mat = new Material(Shader.Find("GUI/Text Shader"));
                mat.SetTexture("_MainTex", tex);
                AssetDatabase.CreateAsset(mat, MatPathName);
            }
            else
            {
                mat.shader = Shader.Find("GUI/Text Shader");
                mat.SetTexture("_MainTex", tex);
            }

            Font font = (Font)AssetDatabase.LoadAssetAtPath(FontPathName, typeof(Font));

            if (font == null)
            {
                font = new Font(FileNameNotExt);

                AssetDatabase.CreateAsset(font, FontPathName);
            }

            font.material = mat;

            float texWidth = tex.width;
            float texHeight = tex.height;

            List<CharacterInfo> _list = new List<CharacterInfo>();
            
            for (int i = 0; i < jsonData["frames"].Count; ++i)
            {
                CharacterInfo info = new CharacterInfo();
                info.index      = i + 48;
                info.uv.x       = 0.1f * i;
                info.uv.y       = 0;
                info.uv.width   = 0.1f;
                info.uv.height  = 1;
                info.vert.x     = 0;
                info.vert.y     = (int)jsonData["frames"][FileNameNotExt + "_" + i + ".png"]["frame"]["h"] * 0.5f;
                info.vert.width = (int)jsonData["frames"][FileNameNotExt + "_" + i + ".png"]["frame"]["w"];
                info.vert.height= -(int)jsonData["frames"][FileNameNotExt + "_" + i + ".png"]["frame"]["h"];
                info.advance    = (int)jsonData["frames"][FileNameNotExt + "_" + i + ".png"]["frame"]["w"] - 20;
                _list.Add(info);
            }

            font.characterInfo = _list.ToArray();

            AssetImporter importer = AssetImporter.GetAtPath(AssetFile);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}