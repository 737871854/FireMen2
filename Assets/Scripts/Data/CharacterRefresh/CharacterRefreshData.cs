/**
* 	Copyright (c) 2015 Need co.,Ltd
*	All rights reserved

*    文件名称:    CharacterRefreshData.cs
*    创建标志:    
*    简    介:    刷新ID（100000 + SceneId * 10000 + 个数）
*/
using System;
using System.Collections.Generic; 
using LitJson; 
    public partial class CharacterRefreshData 
    {
        protected static CharacterRefreshData instance;
        protected Dictionary<int,CharacterRefreshPO> m_dictionary;

        public static CharacterRefreshData Instance
        {
            get{
                if(instance == null)
                {
                    instance = new CharacterRefreshData();
                }
                return instance;
            }
        }

        protected CharacterRefreshData()
        {
            m_dictionary = new Dictionary<int,CharacterRefreshPO>();
        }

        public CharacterRefreshPO GetCharacterRefreshPO(int key)
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
                CharacterRefreshPO po = new CharacterRefreshPO(element);
                CharacterRefreshData.Instance.m_dictionary.Add(po.Id, po);
            }
        }
    }
