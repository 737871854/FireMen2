/**
*    Copyright (c) 2015 Need co.,Ltd
*    All rights reserved

*    文件名称:    CharacterPO.cs
*    创建标识:    
*    简    介:    怪物ID
*/
using System;
using System.Collections.Generic; 
using System.Text;
using LitJson; 
    public partial class CharacterPO 
    {
        protected int m_Id;
        protected string m_Name;
        protected string m_Desc;
        protected int m_Type;
        protected int m_HeadID;
        protected int m_MapID;
        protected int m_BaseSpeed;
        protected float m_RotationSpeed;
        protected float[] m_MoveOrProRate;
        protected string[] m_HitBone;
        protected float m_DisappearTime;
        protected float m_ValidTime;
        protected float m_AttackTime;
        protected float m_FogTime;
        protected float m_FreezeTime;
        protected int m_Score;
        protected int m_Health;
        protected int m_DamageValue;
        protected int[] m_Skills;
        protected int[] m_Drop0;
        protected int[] m_Drop1;
        protected int[] m_Drop2;
        protected string m_BornEffect;
        protected string m_HitEffect;
        protected string m_DieEffet;
        protected float m_Offset_Y;
        protected string[] m_Materials;
        protected string m_ExplodeEffectSound;
        protected string m_DestroyEffectSound;
        protected string[] m_DestroySound;

        public CharacterPO(JsonData jsonNode)
        {
            m_Id = (int)jsonNode["Id"];
            m_Name = jsonNode["Name"].ToString() == "NULL" ? "" : jsonNode["Name"].ToString();
            m_Desc = jsonNode["Desc"].ToString() == "NULL" ? "" : jsonNode["Desc"].ToString();
            m_Type = (int)jsonNode["Type"];
            m_HeadID = (int)jsonNode["HeadID"];
            m_MapID = (int)jsonNode["MapID"];
            m_BaseSpeed = (int)jsonNode["BaseSpeed"];
            m_RotationSpeed = (float)(double)jsonNode["RotationSpeed"];
            {
                JsonData array = jsonNode["MoveOrProRate"];
                m_MoveOrProRate = new float[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_MoveOrProRate[index] = (float)(double)array[index];
                }
            }
            {
                JsonData array = jsonNode["HitBone"];
                m_HitBone = new string[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_HitBone[index] = array[index].ToString();
                }
            }
            m_DisappearTime = (float)(double)jsonNode["DisappearTime"];
            m_ValidTime = (float)(double)jsonNode["ValidTime"];
            m_AttackTime = (float)(double)jsonNode["AttackTime"];
            m_FogTime = (float)(double)jsonNode["FogTime"];
            m_FreezeTime = (float)(double)jsonNode["FreezeTime"];
            m_Score = (int)jsonNode["Score"];
            m_Health = (int)jsonNode["Health"];
            m_DamageValue = (int)jsonNode["DamageValue"];
            {
                JsonData array = jsonNode["Skills"];
                m_Skills = new int[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_Skills[index] = (int)array[index];
                }
            }
            {
                JsonData array = jsonNode["Drop0"];
                m_Drop0 = new int[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_Drop0[index] = (int)array[index];
                }
            }
            {
                JsonData array = jsonNode["Drop1"];
                m_Drop1 = new int[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_Drop1[index] = (int)array[index];
                }
            }
            {
                JsonData array = jsonNode["Drop2"];
                m_Drop2 = new int[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_Drop2[index] = (int)array[index];
                }
            }
            m_BornEffect = jsonNode["BornEffect"].ToString() == "NULL" ? "" : jsonNode["BornEffect"].ToString();
            m_HitEffect = jsonNode["HitEffect"].ToString() == "NULL" ? "" : jsonNode["HitEffect"].ToString();
            m_DieEffet = jsonNode["DieEffet"].ToString() == "NULL" ? "" : jsonNode["DieEffet"].ToString();
            m_Offset_Y = (float)(double)jsonNode["Offset_Y"];
            {
                JsonData array = jsonNode["Materials"];
                m_Materials = new string[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_Materials[index] = array[index].ToString();
                }
            }
            m_ExplodeEffectSound = jsonNode["ExplodeEffectSound"].ToString() == "NULL" ? "" : jsonNode["ExplodeEffectSound"].ToString();
            m_DestroyEffectSound = jsonNode["DestroyEffectSound"].ToString() == "NULL" ? "" : jsonNode["DestroyEffectSound"].ToString();
            {
                JsonData array = jsonNode["DestroySound"];
                m_DestroySound = new string[array.Count];
                for (int index = 0; index < array.Count; index++)
                {
                    m_DestroySound[index] = array[index].ToString();
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

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public string Desc
        {
            get
            {
                return m_Desc;
            }
        }

        public int Type
        {
            get
            {
                return m_Type;
            }
        }

        public int HeadID
        {
            get
            {
                return m_HeadID;
            }
        }

        public int MapID
        {
            get
            {
                return m_MapID;
            }
        }

        public int BaseSpeed
        {
            get
            {
                return m_BaseSpeed;
            }
        }

        public float RotationSpeed
        {
            get
            {
                return m_RotationSpeed;
            }
        }

        public float[] MoveOrProRate
        {
            get
            {
                return m_MoveOrProRate;
            }
        }

        public string[] HitBone
        {
            get
            {
                return m_HitBone;
            }
        }

        public float DisappearTime
        {
            get
            {
                return m_DisappearTime;
            }
        }

        public float ValidTime
        {
            get
            {
                return m_ValidTime;
            }
        }

        public float AttackTime
        {
            get
            {
                return m_AttackTime;
            }
        }

        public float FogTime
        {
            get
            {
                return m_FogTime;
            }
        }

        public float FreezeTime
        {
            get
            {
                return m_FreezeTime;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }
        }

        public int Health
        {
            get
            {
                return m_Health;
            }
        }

        public int DamageValue
        {
            get
            {
                return m_DamageValue;
            }
        }

        public int[] Skills
        {
            get
            {
                return m_Skills;
            }
        }

        public int[] Drop0
        {
            get
            {
                return m_Drop0;
            }
        }

        public int[] Drop1
        {
            get
            {
                return m_Drop1;
            }
        }

        public int[] Drop2
        {
            get
            {
                return m_Drop2;
            }
        }

        public string BornEffect
        {
            get
            {
                return m_BornEffect;
            }
        }

        public string HitEffect
        {
            get
            {
                return m_HitEffect;
            }
        }

        public string DieEffet
        {
            get
            {
                return m_DieEffet;
            }
        }

        public float Offset_Y
        {
            get
            {
                return m_Offset_Y;
            }
        }

        public string[] Materials
        {
            get
            {
                return m_Materials;
            }
        }

        public string ExplodeEffectSound
        {
            get
            {
                return m_ExplodeEffectSound;
            }
        }

        public string DestroyEffectSound
        {
            get
            {
                return m_DestroyEffectSound;
            }
        }

        public string[] DestroySound
        {
            get
            {
                return m_DestroySound;
            }
        }

    }

