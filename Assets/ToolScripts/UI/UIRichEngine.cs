/**
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 2015,广州擎天柱网络科技有限公司
 
 * All rights reserved.
 *
 * 文件名称： RichEngine.cs
 * 简   述： 处理富文排版的功能
 * 创建标识： uscq 2015/12/10
 * 
 */

using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class UIRichEngine : MonoBehaviour
{
    public RichLines.RichAnalyzer richAnalyzer
    {
        get;
        private set;
    }

#if UNITY_EDITOR
    public bool _isRequestReload = true;
    private string GetFormatString()
    {
        string format = "";
        UnityEngine.UI.Text textParent = this.GetComponent<UnityEngine.UI.Text>();
        if (null != textParent)
        {
            format = textParent.text;
        }

        if (string.IsNullOrEmpty(format))
        {
            format = "[divsize='300,150']"
                + "[height=125][image='Assets/UI/Atlas/common/1_solider.png']"
                + "[divalign=rm][color=red][size=30]IF YOU SEE THIS, [color=green]IT WORK[!color]"
                + "-- BY [STYLE=ib]ANONYMOUS"
                + "";
        }
        return format;
    }
#endif

    void Start()
    {

    }
    void Update()
    {
#if UNITY_EDITOR
        if (!_isRequestReload)
            return;
        _isRequestReload = false;

        foreach (UnityEngine.UI.Text itr in GetComponentsInChildren<UnityEngine.UI.Text>())
            if (itr.gameObject != this.gameObject)
                Destroy(itr.gameObject);
        foreach (UnityEngine.UI.Image itr in GetComponentsInChildren<UnityEngine.UI.Image>())
            if (itr.gameObject != this.gameObject)
                Destroy(itr.gameObject);

        LoadFormat(GetFormatString());
#endif
    }

    void LoadFormat(string format)
    {
        if (null == richAnalyzer)
            richAnalyzer = new RichLines.RichAnalyzer();

        richAnalyzer.Attach(this.gameObject, format);

#if ! UNITY_EDITOR
        UnityEngine.UI.Text textParent = this.GetComponent<UnityEngine.UI.Text>();
        if (null != textParent)
            textParent.text = richAnalyzer.GetSurplus();
#endif
    }
}

namespace RichLines
{
    internal class Status
    {
        System.Collections.Generic.List<stash> stack = new System.Collections.Generic.List<stash>();

        internal class stash
        {
            public stash(string tag, object oldval)
            {
                _tag = tag;
                _oldval = oldval;
            }
            public string _tag;
            public object _oldval;
        };

        public void push(string tag, object oldval)
        {
            stack.Add(new stash(tag, oldval));
        }
        public object pop(string tag)
        {
            object ret = null;
            int offset = stack.FindLastIndex(s => s._tag == tag);
            if (offset >= 0)
            {
                ret = stack[offset]._oldval;
                stack.RemoveAt(offset);
            }
            return ret;
        }

    }

    internal class Situation
    {
        public string format = "";
        public long offset = 0;
        public GameObject root = null, mount = null;
        public GameObject textGo = null, imageGo = null;
        public RectTransform lastLine = null;
        public Regex regPlain = new Regex(@"^(\\\[|[^\[])+");
        public Regex regEsc = new Regex(@"(\\\[)");
        public Regex regAttr = new Regex(@"^((val)?\s*=([a-zA-Z0-9_\.-]+|""(\\\""|[^""])*?""|'(\\\'|[^'])*?'))?\s*\]");
        public Regex regTagBegin = new Regex(@"^\[([a-zA-Z0-9_-]+)\s*");
        public Regex regTagEnd = new Regex(@"^\[\s*!\s*([a-zA-Z0-9_-]+)\s*\]");
        public Vector2 usedSize = Vector2.zero;
        public Vector2 areaSize = Vector2.zero;
        public Status status = new Status();

        public bool FetchOpening(out string text)
        {
            Match mat = regTagBegin.Match(format);
            if (!mat.Success)
            {
                text = "";
                return false;
            }
            text = mat.Groups[1].ToString();
            MoveToNext(mat.ToString().Length);
            return true;
        }

        public bool FetchEnding(out string text)
        {
            Match mat = regTagEnd.Match(format);
            if (!mat.Success)
            {
                text = "";
                return false;
            }
            text = mat.Groups[1].ToString();
            MoveToNext(mat.ToString().Length);
            return true;
        }

        public bool FetchAttribute(out string text)
        {
            Match mat = regAttr.Match(format);
            if (!mat.Success)
            {
                text = "";
                return false;
            }

            text = mat.Groups[3].ToString();
            if (text.StartsWith("'") || text.StartsWith("\""))
            {
                text = text.Substring(1, text.Length - 2);
            }
            MoveToNext(mat.ToString().Length);
            return true;
        }

        public bool FetchPlain(out string text)
        {
            Match mat = regPlain.Match(format);
            if (!mat.Success)
            {
                text = "";
                return false;
            }

            text = mat.ToString();
            MoveToNext(text.Length);
            text = regEsc.Replace(text, "[");
            return true;
        }

        private void MoveToNext(int skip)
        {
            offset += skip;
            format = format.Substring(skip);
        }
    };

    // So, why it is a CLASS but not a STRUCT ?
    internal class Attribute
    {
        public bool overflowh = true;
        public bool overflowv = true;
        public bool underline = false;
        public bool rich = true;
        public string font = "Arial";
        public string color = "white";
        public string name = "richdiv";
        public string text = "";
        public string image = "";
        public string emoji = "";
        public string style = "n";
        public string align = "cm";
        public string x = "";
        public string y = "";
        public int size = 30;
        public float width = 0;
        public float height = 0;
        public float spacing = 1;
        public float scalex = 1;
        public float scaley = 1;

        // public bool br = false;
        public bool sp = false;
        public float paddingx = 0.7f;
        public float paddingy = 1.1f;
        public string divalign = "cm";
        public string divsize = "";
    }

    internal class Layout
    {
        public Attribute attr = new Attribute();
        private Attribute attrDefault = new Attribute();

        public Attribute defalutAttribute
        {
            get
            {
                return attrDefault;
            }
        }

        public bool GetDivSize(out Vector2 size)
        {
            size = Vector2.zero;
            return attr.divsize != "" && StringToVector2(attr.divsize, out size);
        }

        public bool StringToVector2(string s, out Vector2 size)
        {
            bool ret = false;
            do
            {
                size = Vector2.zero;
                string[] ss = s.Split(',');
                if (ss.Length != 2)
                    break;
                try
                {
                    size.x = (float)System.Convert.ToDouble(ss[0]);
                    size.y = (float)System.Convert.ToDouble(ss[1]);
                }
                catch (System.Exception)
                {
                    break;
                }
                ret = true;
            } while (false);

            if (!ret)
                Warning("expect two float number split by ',', but NOT '" + s + "'.");

            return ret;
        }

        public Vector2 getPosition()
        {
            float x = String2Float(attr.x, attrDefault.x);
            float y = String2Float(attr.y, attrDefault.y);
            return new Vector2(x, y);
        }

        public float String2Float(string s, string def)
        {
            if (s == "")
                return String2Float(def, "0");

            float ret = 0;
            try
            {
                ret = (float)System.Convert.ToDouble(s);
            }
            catch (System.Exception)
            {
                ret = String2Float(def, "0");
                Warning("expect a float number, but NOT '" + s + "'.");
            }
            return ret;
        }

        public Color GetColor()
        {
            Color color;
            if (!GetColor(attr.color, out color))
                GetColor(attrDefault.color, out color);
            return color;
        }

        bool GetColor(string s, out Color color)
        {
            color = Color.clear;
            string colorStr = s.ToLower();
            switch (colorStr)
            {
                case "black":
                    color = Color.black;
                    break;
                case "blue":
                    color = Color.blue;
                    break;
                case "clear":
                    color = Color.clear;
                    break;
                case "cyan":
                    color = Color.cyan;
                    break;
                case "gray":
                    color = Color.gray;
                    break;
                case "green":
                    color = Color.green;
                    break;
                case "magenta":
                    color = Color.magenta;
                    break;
                case "red":
                    color = Color.red;
                    break;
                case "white":
                    color = Color.white;
                    break;
                case "yellow":
                    color = Color.yellow;
                    break;
                default:
                    if (colorStr.StartsWith("#"))
                        colorStr = colorStr.Substring(1);
                    try
                    {
                        int rsl = System.Convert.ToInt32(colorStr, 16);
                        color.r = rsl & 0xff000000 >> (3 * 8);
                        color.g = rsl & 0x00ff0000 >> (2 * 8);
                        color.b = rsl & 0x0000ff00 >> (1 * 8);
                        color.a = rsl & 0x000000ff >> (0 * 8);
                    }
                    catch (System.Exception)
                    {
                        Warning("what is the kind of color with '" + s + "' ? fail to parse.");
                        return false;
                    }
                    break;
            }
            return true;
        }

        public Font GetFont()
        {
            Font font = LoadFont(attr.font, attr.size);
            if (null == font)
                font = LoadFont(attrDefault.font, attr.size);
            if (null == font)
                font = LoadFont(attrDefault.font, attrDefault.size);
            return font;
        }

        Font LoadFont(string pathOrName, int size)
        {
            Font font = null;
            if (pathOrName.IndexOf('/') >= 0)
            {
                ResourceMisc.AssetWrapper assetWrapper = ioo.resourceManager.LoadAsset(pathOrName, typeof(Font));
                font = assetWrapper.GetAsset() as Font;
                if (null == font)
                    Warning("path unavailable, try from OS's font " + pathOrName);
            }

            if (null == font)
                font = Font.CreateDynamicFontFromOSFont(pathOrName, size);

            if (null == font)
                Warning("cant load the font with name '" + pathOrName + "' on size " + size);
            return font;
        }

        public FontStyle GetFontStyle()
        {
            FontStyle fontStyle;
            if (!GetFontStyle(attr.style, out fontStyle))
                GetFontStyle(attrDefault.style, out fontStyle);
            return fontStyle;
        }

        bool GetFontStyle(string s, out FontStyle style)
        {
            switch (s.ToLower())
            {
                case "n":
                    style = FontStyle.Normal;
                    break;
                case "b":
                    style = FontStyle.Bold;
                    break;
                case "i":
                    style = FontStyle.Italic;
                    break;
                case "bi":
                case "ib":
                    style = FontStyle.BoldAndItalic;
                    break;
                default:
                    Warning("the font style specified '" + s + "' is NOT one of n|b|i|bi|ib.");
                    style = FontStyle.Normal;
                    return false;
            }
            return true;
        }

        public HorizontalWrapMode GetWarpH()
        {
            return attr.overflowh ? HorizontalWrapMode.Overflow : HorizontalWrapMode.Wrap;
        }

        public VerticalWrapMode GetWarpV()
        {
            return attr.overflowh ? VerticalWrapMode.Overflow : VerticalWrapMode.Truncate;
        }

        public TextAnchor GetAlignment()
        {
            TextAnchor anchor;
            if (!GetAlignment(attr.align, out anchor))
                GetAlignment(attrDefault.align, out anchor);
            return anchor;
        }

        public TextAnchor GetDivAlignment()
        {
            TextAnchor anchor;
            if (!GetAlignment(attr.divalign, out anchor))
                GetAlignment(attrDefault.divalign, out anchor);
            return anchor;
        }

        bool GetAlignment(string s, out TextAnchor align)
        {
            switch (s.ToLower())
            {
                case "lu":
                    align = TextAnchor.UpperLeft;
                    break;
                case "cu":
                    align = TextAnchor.UpperCenter;
                    break;
                case "ru":
                    align = TextAnchor.UpperRight;
                    break;
                case "lm":
                    align = TextAnchor.MiddleLeft;
                    break;
                case "cm":
                    align = TextAnchor.MiddleCenter;
                    break;
                case "rm":
                    align = TextAnchor.MiddleRight;
                    break;
                case "ll":
                    align = TextAnchor.LowerLeft;
                    break;
                case "cl":
                    align = TextAnchor.LowerCenter;
                    break;
                case "rl":
                    align = TextAnchor.LowerRight;
                    break;
                default:
                    Warning("the text anchor '" + s + "' is incorrect.");
                    align = TextAnchor.MiddleLeft;
                    return false; //  break;
            }
            return true;
        }
        void Warning(string warn)
        {
            Debug.LogWarning("[Layout.Attribute]: " + warn);
        }
    }

    public class RichAnalyzer
    {

        Situation situated;
        Layout layout;
        void RouteTags(string tags)
        {
            switch (tags)
            {
                default:
                    break;
                // case "br": ProduceNewline(); break;
                case "text":
                    OnProduceText();
                    break;
                case "image":
                    OnProduceImage();
                    break;
                case "emoji":
                    OnProduceEmoji();
                    break;
                case "divalign":
                    OnSetDivisionAlignment();
                    break;
                case "divsize":
                    OnSetDivisionSize();
                    break;
            }
        }

        void OnSetDivisionAlignment()
        {
            UnityEngine.UI.HorizontalLayoutGroup layoutGroup = situated.root.GetComponent<UnityEngine.UI.HorizontalLayoutGroup>();
            layoutGroup.childForceExpandHeight = false;
            layoutGroup.childForceExpandWidth = false;
            layoutGroup.childAlignment = layout.GetDivAlignment();
        }

        void OnSetDivisionSize()
        {
            Vector2 size;
            if (layout.GetDivSize(out size))
            {
                UnityEngine.UI.Text textRoot = situated.root.GetComponent<UnityEngine.UI.Text>();
                textRoot.rectTransform.sizeDelta = size;
            }
        }

        void OnProduceText()
        {
            GameObject textGo = MyInstantiate(situated.textGo);
            if (null == textGo)
                return;

            UnityEngine.UI.Text textObj = textGo.GetComponent<UnityEngine.UI.Text>();
            textObj.horizontalOverflow = layout.GetWarpH();
            textObj.verticalOverflow = layout.GetWarpV();
            textObj.fontSize = layout.attr.size;
            textObj.font = layout.GetFont();
            textObj.color = layout.GetColor();
            textObj.fontStyle = layout.GetFontStyle();
            textObj.alignment = layout.GetAlignment();
            textObj.lineSpacing = layout.attr.spacing;
            textObj.name = layout.attr.name;
            textObj.text = layout.attr.text;
            textObj.supportRichText = layout.attr.rich;

            float width = layout.attr.width, height = layout.attr.height;
            if (width <= 0)
                width = textObj.preferredWidth;
            if (height <= 0)
                height = textObj.preferredHeight;
            MountTo(textGo.GetComponent<RectTransform>(), width, height);
        }

        void OnProduceImage()
        {
            GameObject imageGo = MyInstantiate(situated.imageGo);
            if (null == imageGo)
                return;

            Sprite sprite = LoadResource(layout.attr.image, typeof(UnityEngine.Sprite)) as Sprite;
            if (null == sprite)
                return;

            UnityEngine.UI.Image image = imageGo.GetComponent<UnityEngine.UI.Image>();
            image.SetNativeSize();
            image.sprite = sprite;
            image.name = layout.attr.name;

            float width = layout.attr.width, height = layout.attr.height;
            if (height <= 0)
            {
                UnityEngine.UI.Text textParent = situated.mount.GetComponent<UnityEngine.UI.Text>();
                if (null != textParent)
                {
                    height = textParent.preferredHeight;
                    if (height <= 0)
                        height = sprite.rect.height;
                }
                else
                {
                    UnityEngine.UI.Image imageParent = situated.mount.GetComponent<UnityEngine.UI.Image>();
                    if (null != imageParent)
                        height = imageParent.rectTransform.sizeDelta.y;
                    else
                        Warning("an unusual mount parent is found, zero size is being used");
                }
                if (height <= 0)
                    height = sprite.rect.height;
            }
            if (width <= 0)
                width = height / sprite.rect.height * sprite.rect.width;

            MountTo(image.rectTransform, width, height);
        }

        void OnProduceEmoji()
        {
            Warning("generating emoji '" + layout.attr.emoji + "', and, blue screen!");
        }

        Vector2 GetGameObjectSize(GameObject go)
        {
            UnityEngine.UI.Image imageParent = go.GetComponent<UnityEngine.UI.Image>();
            if (null != imageParent)
            {
                return imageParent.rectTransform.sizeDelta;
            }

            UnityEngine.UI.Text textParent = go.GetComponent<UnityEngine.UI.Text>();
            if (null != textParent)
            {
                return textParent.rectTransform.sizeDelta;
            }

            RectTransform rectParent = go.GetComponent<RectTransform>();
            if (null != rectParent)
            {
                return rectParent.sizeDelta;
            }

            Warning("So, What kind of girl you are ? I can NOT crack you on type RectTransform");

            return Vector2.zero;
        }

        void MountTo(RectTransform rectTransform, float w, float h)
        {
            Vector2 childSize = new Vector2(w, h);
            rectTransform.localScale = new Vector3(layout.attr.scalex, layout.attr.scaley, 1);
            AppendOverParent(rectTransform, childSize);

            if (layout.attr.underline)
                AppendUlOn(rectTransform.gameObject);

            GameObject lineMount = situated.lastLine.gameObject;
            UnityEngine.UI.LayoutElement layoutElement = lineMount.GetComponent<UnityEngine.UI.LayoutElement>();
            layoutElement.minWidth = layoutElement.minWidth + w;
            layoutElement.minHeight = layoutElement.minHeight + h;
        }

        void AppendOverParent(RectTransform rectTransform, Vector2 size)
        {
            rectTransform.anchoredPosition3D = Vector3.zero;
            Vector2 rightCenter = new Vector2(1, 0.5f);
            if (situated.mount == situated.lastLine.gameObject)
            {
                Vector2 leftCenter = new Vector2(0, 0.5f);
                rectTransform.anchorMax = leftCenter;
                rectTransform.anchorMin = leftCenter;
            }
            else
            {
                rectTransform.anchorMax = rightCenter;
                rectTransform.anchorMin = rightCenter;
            }
            rectTransform.offsetMax = rightCenter;
            rectTransform.offsetMin = rightCenter;
            rectTransform.pivot = new Vector2(0, 0.5f);
            rectTransform.sizeDelta = size;
            rectTransform.localPosition = Vector3.zero;

            rectTransform.transform.SetParent(situated.mount.transform, false);

            situated.mount = rectTransform.gameObject;
        }

        void AppendUlOn(GameObject go)
        {
            UnityEngine.UI.Text textParent = go.GetComponent<UnityEngine.UI.Text>();
            UnityEngine.UI.Text textUl;
            float width = 0;
            if (null != textParent)
            {
                GameObject ulObj = GameObject.Instantiate(go);
                ulObj.transform.SetParent(go.transform);
                textUl = ulObj.GetComponent<UnityEngine.UI.Text>();
                width = textParent.preferredWidth;
                textUl.supportRichText = false;
                textUl.name = textParent.name + "_underline";
            }
            else
            {
                Warning("it is NOT allow attach an underline format to an image at this version");
                return;
                /* the following code is same Lua code, read it if u can, even though useless.
                 * it is keep for future feature, a image can be formated by under-line for example.
                 * 
                            parText = parent:GetComponent("Image")
                            if parText then
                              width = parText.rectTransform.sizeDelta.x
                              if width <= 0 then
                                width = parText.preferredWidth
                              end
                              ulText = GenTextComponent(parent.transform)
                            else
                              self:Warning("unknown parent GameObject type to set `Underline`")
                              return
                            end
                */
            }
            RectTransform rt = textUl.rectTransform;
            rt.anchoredPosition3D = Vector3.zero;
            rt.offsetMax = new Vector2(1, 0);
            rt.offsetMin = Vector2.zero;
            rt.anchorMax = Vector2.zero;
            rt.anchorMin = Vector2.zero;

            textUl.text = "_";
            rt.sizeDelta = new Vector2(textUl.preferredWidth, textUl.preferredHeight);
            rt.localScale = new Vector3(width / textUl.preferredWidth, 1, 1);
        }

        bool HandleOpenTags(string tag, string val)
        {
            System.Reflection.FieldInfo fieldInfo = typeof(Attribute).GetField(tag);
            string fieldName = fieldInfo.FieldType.Name;
            object newValue;
            if (fieldName == typeof(bool).Name)
            {
                val = val.ToLower();
                newValue = val == "" | val == "true" || val != "0" || val == "yes" || val == "on";
            }
            else if (fieldName == typeof(string).Name)
            {
                newValue = val != "" ? val : fieldInfo.GetValue(layout.attr);
            }
            else if (fieldName == typeof(int).Name)
            {
                int def = (int)fieldInfo.GetValue(layout.defalutAttribute), resl;
                newValue = val != "" && int.TryParse(val, out resl) ? resl : def;
            }
            else if (fieldName == typeof(float).Name)
            {
                float def = (float)fieldInfo.GetValue(layout.defalutAttribute), resl;
                newValue = val != "" && float.TryParse(val, out resl) ? resl : def;
            }
            else
            {
                Warning("uncoded for type '" + fieldName + "', the field may be newly.");
                return false;
            }

            situated.status.push(tag, fieldInfo.GetValue(layout.attr));
            fieldInfo.SetValue(layout.attr, newValue);

            RouteTags(tag);
            return true;
        }

        // it expects to call Attach() first befor any members vaild.
        // and should be reset the env for each call.
        public bool Attach(GameObject parent, string format)
        {
            // test of antecedent conditions
            if (null == parent)
            {
                Warning("Hello Kitten, that is what I going to say 'cause parent go is nil");
                return false;
            }

            // load prefab resources
            const int resMax = 4;
            string[] RlResourcePaths =
            {
                "Assets/Prefabs/ui/common/RichLinesText.prefab",
                "Assets/Prefabs/ui/common/RichLinesImage.prefab",
                "Assets/Prefabs/ui/common/RichLinesRoot.prefab",
                "Assets/Prefabs/ui/common/RichLinesMount.prefab",
            };
            GameObject[] RlResourceGo = new GameObject[resMax];
            for (int i = 0; i < resMax; ++i)
            {
                GameObject go = LoadResource(RlResourcePaths[i], typeof(GameObject)) as GameObject;
                if (null == go)
                    return false;
                RlResourceGo[i] = go;
            }

            // initialize
            situated = new Situation();
            layout = new Layout();
            situated.textGo = RlResourceGo[0];
            situated.imageGo = RlResourceGo[1];
            situated.root = MyInstantiate(RlResourceGo[2]);
            situated.format = format;
            // Destroying assets is not permitted to avoid data loss.
            // GameObject.Destroy(RlResourceGo[2]);

            // set layout root
            UnityEngine.UI.Text textRoot = situated.root.GetComponent<UnityEngine.UI.Text>();
            textRoot.text = "";
            textRoot.name = "RichZone";
            RectTransform rectTransform = textRoot.rectTransform;
            situated.areaSize = GetGameObjectSize(parent);
            Vector2 middleCenter = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = middleCenter;
            rectTransform.anchorMin = middleCenter;
            rectTransform.offsetMax = middleCenter;
            rectTransform.offsetMin = middleCenter;
            rectTransform.pivot = middleCenter;
            rectTransform.sizeDelta = situated.areaSize;
            rectTransform.SetParent(parent.transform);
            rectTransform.localScale = Vector3.one;
            rectTransform.localPosition = Vector3.zero; /*new Vector3(-situated.areaSize.x/2, 0)*/;
            UnityEngine.UI.HorizontalLayoutGroup horizontalLayoutGroup = situated.root.GetComponent<UnityEngine.UI.HorizontalLayoutGroup>();
            horizontalLayoutGroup.childForceExpandWidth = false;
            horizontalLayoutGroup.childForceExpandHeight = false;
            OnSetDivisionAlignment();

            // set layout mount
            GameObject mountGo = MyInstantiate(/*situated.textGo*/RlResourceGo[3]);
            if (null == mountGo)
                return false;
            UnityEngine.UI.Text text = mountGo.GetComponent<UnityEngine.UI.Text>();
            text.text = "";
            text.name = "RichMount";
            rectTransform = situated.lastLine = text.rectTransform;
            rectTransform.anchorMax = new Vector2(0, 0.5f);
            rectTransform.anchorMin = new Vector2(0, 0.5f);
            rectTransform.offsetMax = new Vector2(0, 0.5f);
            rectTransform.offsetMin = new Vector2(0, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.SetParent(situated.root.transform, false);
            rectTransform.localScale = Vector3.one;
            rectTransform.localPosition = new Vector3(-situated.areaSize.x, 0, 0);
            situated.mount = mountGo;

            while (ParseOnce() && !IsEndOfInput()) ;
            
            // Debug.Log("[RichEngine]: RichLines.RichAnalyzer done with " + IsEndOfInput());
            return IsEndOfInput();
        }

        GameObject MyInstantiate(GameObject go)
        {
            GameObject copied = GameObject.Instantiate(go);
            if (null == copied)
                Warning("cant instantiate Game object " + (null == go ? "nullptr" : go.ToString()));
            return copied;
        }

        public string GetSurplus()
        {
            return situated.format;
        }

        bool IsEndOfInput()
        {
            return string.IsNullOrEmpty(situated.format);
        }

        bool ParseOnce()
        {
            if (IsEndOfInput()) return false;

            if (situated.FetchPlain(out layout.attr.text))
            {
                return HandleOpenTags("text", layout.attr.text);
            }

            string tagName;
            bool isOpen = situated.FetchOpening(out tagName);
            if (!isOpen && !situated.FetchEnding(out tagName))
            {
                Warning("unterminated tag '" + tagName + "', misspell ? at " + situated.offset);
                return false;
            }

            tagName = tagName.ToLower();

            System.Reflection.FieldInfo fieldInfo = typeof(Attribute).GetField(tagName);
            if (null == fieldInfo)
            {
                Warning("unsupported tag '" + tagName + "', misspell ? at " + situated.offset);
                return false;
            }

            if (!isOpen)
            {
                object oldval = situated.status.pop(tagName);
                if (null != oldval)
                    fieldInfo.SetValue(layout.attr, oldval);
                // else
                //   Warning("I am not going to say no to girls who pretty");
                return true;
            }

            string tagVal;
            if (!situated.FetchAttribute(out tagVal))
            {
                Warning("opening tag '" + tagName + "', at " + situated.offset);
                return false;
            }
            return HandleOpenTags(tagName, tagVal);
        }

        void Warning(string ss)
        {
            Debug.LogWarning("[RichEngine]: " + ss);
        }

        object LoadResource(string path, System.Type type)
        {
            UnityEngine.Object ret = ioo.resourceManager.LoadAsset(path, type).GetAsset();

            if (null == ret)
            {
                Warning("can NOT load resource as " + type.ToString() + ", named " + path);
            }
            return ret;
        }
    }
}
