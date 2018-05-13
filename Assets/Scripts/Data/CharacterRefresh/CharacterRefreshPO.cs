/**
*    Copyright (c) 2015 Need co.,Ltd
*    All rights reserved

*    文件名称:    CharacterRefreshPO.cs
*    创建标识:    
*    简    介:    刷新ID（100000 + SceneId * 10000 + 个数）
*/
using System;
using System.Collections.Generic; 
using System.Text;
using LitJson; 
    public partial class CharacterRefreshPO 
    {
        protected int m_Id;
        protected int m_SceneID;
        protected int m_Stage;
        protected int m_IsKinematic;
        protected string m_Desc;
        protected string m_Tag;
        protected int m_CharacterID;
        protected int m_ActionType;
        protected int m_Loop;
        protected int[] m_RefreshRate;
        protected float m_AppeareTime;
        protected int m_Interval;
        protected float m_DisappearTime;
        protected int m_StepCount;
        protected float m_LevelRate;
        protected int m_TargetAgentID;
        protected string m_AppeareArea;
        protected string m_StayArea;
        protected float[] m_StayTime;
        protected string m_WindowName;
        protected string m_CricleName;
        protected float[] m_AppearePoint;
        protected float[] m_LocalEulerAngles;
        protected float m_BegineLocalScale;
        protected float m_TargetLocalScale;
        protected float m_LocalScaleTime;
        protected float m_FactorSpeed;

        public CharacterRefreshPO(JsonData jsonNode)
        {
            m_Id = (int)jsonNode["Id"];
            m_SceneID = (int)jsonNode["SceneID"];
            m_Stage = (int)jsonNode["Stage"];
            m_IsKinematic = (int)jsonNode["IsKinematic"];
            m_Desc = jsonNode["Desc"].ToString() == "NULL" ? "" : jsonNode["Desc"].ToString();
            m_Tag = jsonNode["Tag"].ToString() == "NULL" ? "" : jsonNode["Tag"].ToString();
            m_CharacterID = (int)jsonNode["CharacterID"];
            m_ActionType = (int)jsonNode["ActionType"];
            m_Loop = (int)jsonNode["Loop"];
            {
                JsonData array = jsonNode["RefreshRate"];
                m_RefreshRate = new int[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_RefreshRate[index] = (int)array[index];
                }
            }
            m_AppeareTime = (float)(double)jsonNode["AppeareTime"];
            m_Interval = (int)jsonNode["Interval"];
            m_DisappearTime = (float)(double)jsonNode["DisappearTime"];
            m_StepCount = (int)jsonNode["StepCount"];
            m_LevelRate = (float)(double)jsonNode["LevelRate"];
            m_TargetAgentID = (int)jsonNode["TargetAgentID"];
            m_AppeareArea = jsonNode["AppeareArea"].ToString() == "NULL" ? "" : jsonNode["AppeareArea"].ToString();
            m_StayArea = jsonNode["StayArea"].ToString() == "NULL" ? "" : jsonNode["StayArea"].ToString();
            {
                JsonData array = jsonNode["StayTime"];
                m_StayTime = new float[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_StayTime[index] = (float)(double)array[index];
                }
            }
            m_WindowName = jsonNode["WindowName"].ToString() == "NULL" ? "" : jsonNode["WindowName"].ToString();
            m_CricleName = jsonNode["CricleName"].ToString() == "NULL" ? "" : jsonNode["CricleName"].ToString();
            {
                JsonData array = jsonNode["AppearePoint"];
                m_AppearePoint = new float[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_AppearePoint[index] = (float)(double)array[index];
                }
            }
            {
                JsonData array = jsonNode["LocalEulerAngles"];
                m_LocalEulerAngles = new float[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_LocalEulerAngles[index] = (float)(double)array[index];
                }
            }
            m_BegineLocalScale = (float)(double)jsonNode["BegineLocalScale"];
            m_TargetLocalScale = (float)(double)jsonNode["TargetLocalScale"];
            m_LocalScaleTime = (float)(double)jsonNode["LocalScaleTime"];
            m_FactorSpeed = (float)(double)jsonNode["FactorSpeed"];
        }

        public int Id
        {
            get
            {
                return m_Id;
            }
        }

        public int SceneID
        {
            get
            {
                return m_SceneID;
            }
        }

        public int Stage
        {
            get
            {
                return m_Stage;
            }
        }

        public int IsKinematic
        {
            get
            {
                return m_IsKinematic;
            }
        }

        public string Desc
        {
            get
            {
                return m_Desc;
            }
        }

        public string Tag
        {
            get
            {
                return m_Tag;
            }
        }

        public int CharacterID
        {
            get
            {
                return m_CharacterID;
            }
        }

        public int ActionType
        {
            get
            {
                return m_ActionType;
            }
        }

        public int Loop
        {
            get
            {
                return m_Loop;
            }
        }

        public int[] RefreshRate
        {
            get
            {
                return m_RefreshRate;
            }
        }

        public float AppeareTime
        {
            get
            {
                return m_AppeareTime;
            }
        }

        public int Interval
        {
            get
            {
                return m_Interval;
            }
        }

        public float DisappearTime
        {
            get
            {
                return m_DisappearTime;
            }
        }

        public int StepCount
        {
            get
            {
                return m_StepCount;
            }
        }

        public float LevelRate
        {
            get
            {
                return m_LevelRate;
            }
        }

        public int TargetAgentID
        {
            get
            {
                return m_TargetAgentID;
            }
        }

        public string AppeareArea
        {
            get
            {
                return m_AppeareArea;
            }
        }

        public string StayArea
        {
            get
            {
                return m_StayArea;
            }
        }

        public float[] StayTime
        {
            get
            {
                return m_StayTime;
            }
        }

        public string WindowName
        {
            get
            {
                return m_WindowName;
            }
        }

        public string CricleName
        {
            get
            {
                return m_CricleName;
            }
        }

        public float[] AppearePoint
        {
            get
            {
                return m_AppearePoint;
            }
        }

        public float[] LocalEulerAngles
        {
            get
            {
                return m_LocalEulerAngles;
            }
        }

        public float BegineLocalScale
        {
            get
            {
                return m_BegineLocalScale;
            }
        }

        public float TargetLocalScale
        {
            get
            {
                return m_TargetLocalScale;
            }
        }

        public float LocalScaleTime
        {
            get
            {
                return m_LocalScaleTime;
            }
        }

        public float FactorSpeed
        {
            get
            {
                return m_FactorSpeed;
            }
        }

    }

