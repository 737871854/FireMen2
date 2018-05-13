/**
* 	Copyright (c) 2015 Need co.,Ltd
*	All rights reserved

*    文件名称:    CharacterData.cs
*    创建标志:    
*    简    介:    怪物ID
*/
using System;
using System.Collections.Generic; 
using LitJson; 
    public partial class CharacterData 
    {
        protected static CharacterData instance;
        protected Dictionary<int,CharacterPO> m_dictionary;

        public static CharacterData Instance
        {
            get{
                if(instance == null)
                {
                    instance = new CharacterData();
                }
                return instance;
            }
        }

        protected CharacterData()
        {
            m_dictionary = new Dictionary<int,CharacterPO>();
        }

        public CharacterPO GetCharacterPO(int key)
        {
            if(m_dictionary.ContainsKey(key) == false)
            {
                return null;
            }
            return m_dictionary[key];
        }

        static public void LoadHandler(LoadedData data)
        {
            JsonData jsonData = JsonMapper.ToObject(data.Value.ToString());
            if (!jsonData.IsArray)
            {
                return;
            }
            for (int index = 0; index < jsonData.Count; index++)
            {
                JsonData element = jsonData[index];
                CharacterPO po = new CharacterPO(element);
                CharacterData.Instance.m_dictionary.Add(po.Id, po);
            }
        }
    }
