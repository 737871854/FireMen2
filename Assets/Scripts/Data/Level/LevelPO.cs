/**
*    Copyright (c) 2015 Need co.,Ltd
*    All rights reserved

*    文件名称:    LevelPO.cs
*    创建标识:    
*    简    介:    完成场景难度ID（100*SceneID + Level）
*/
using System;
using System.Collections.Generic; 
using System.Text;
using LitJson; 
    public partial class LevelPO 
    {
        protected int m_Id;
        protected int m_SceneID;
        protected int m_Level;
        protected string m_SceneName;
        protected string[] m_MustCreateActor;
        protected string[] m_MustRescue;
        protected string[] m_MustKill;
        protected int[] m_CheckPointScores;

        public LevelPO(JsonData jsonNode)
        {
            m_Id = (int)jsonNode["Id"];
            m_SceneID = (int)jsonNode["SceneID"];
            m_Level = (int)jsonNode["Level"];
            m_SceneName = jsonNode["SceneName"].ToString() == "NULL" ? "" : jsonNode["SceneName"].ToString();
            {
                JsonData array = jsonNode["MustCreateActor"];
                m_MustCreateActor = new string[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_MustCreateActor[index] = array[index].ToString();
                }
            }
            {
                JsonData array = jsonNode["MustRescue"];
                m_MustRescue = new string[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_MustRescue[index] = array[index].ToString();
                }
            }
            {
                JsonData array = jsonNode["MustKill"];
                m_MustKill = new string[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_MustKill[index] = array[index].ToString();
                }
            }
            {
                JsonData array = jsonNode["CheckPointScores"];
                m_CheckPointScores = new int[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_CheckPointScores[index] = (int)array[index];
                }
            }
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

        public int Level
        {
            get
            {
                return m_Level;
            }
        }

        public string SceneName
        {
            get
            {
                return m_SceneName;
            }
        }

        public string[] MustCreateActor
        {
            get
            {
                return m_MustCreateActor;
            }
        }

        public string[] MustRescue
        {
            get
            {
                return m_MustRescue;
            }
        }

        public string[] MustKill
        {
            get
            {
                return m_MustKill;
            }
        }

        public int[] CheckPointScores
        {
            get
            {
                return m_CheckPointScores;
            }
        }

    }

