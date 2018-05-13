/*
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 
 * 
 * 文件名称：   GameModePlayer.cs
 * 
 * 简    介:    负责执行玩家输入逻辑操作
 * 
 * 创建标识：   Pancake 2017/4/26 9:45:23
 * 
 * 修改描述：
 * 
 */


using UnityEngine;
using System.Collections.Generic;

public partial class GameMode : MonoBehaviour
{
    // TODO: 将继承FSMBase的类整理统一处理
    private void OnPlayerInput()
    {
        for (int i = 0; i < Define.MAX_PLAYER_NUMBER; ++i)
        {
            Player player = ioo.playerManager.GetPlayer(i);
            // 玩家进入游戏
            if (ioo.playerManager.IsPlaying(i))
            {

                // 水标显示
                Vector3 pos = Vector3.zero;
                Camera camera = ioo.gameMode.UICamera;
                Canvas canvas = ioo.gameMode.UICanvas;
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, screenPos[i], camera, out pos))
                {
                    ioo.playerManager.SetWaterPosition(i, pos);
                }

                ICharacter character = null;
                IHitPoint hitPoint = null;
                GameObject goBind = null;

                // 选角色图像
                if (!ioo.playerManager.HasHead(i) && ioo.gameMode.State == E_GameState.SelectCharacter)
                {
                    if (ioo.characterSystem.PickSelectCharacter(screenPos[i], out character, out goBind))
                    {
                        character.UnderAttack(player);
                    }
                }

                // 选地图
                if (ioo.gameMode.State == E_GameState.SelectMap)
                {
                    if (ioo.characterSystem.PickSelectMap(screenPos[i], out character, out goBind))
                    {
                        character.UnderAttack(player);
                    }
                }

                if (player.FireTime >= Define.GAME_CONFIG_WATER_DAMAGE_INTERVAL)
                {
                    player.FireTime = 0;

                    // 游戏中
                    if (ioo.gameMode.State == E_GameState.Play)
                    {
                        if(ioo.characterSystem.PickCharacter(screenPos[i], out character, out goBind))
                        {
                            character.UnderAttack(player);
                        }

                        if(ioo.characterSystem.PickHitPoint(screenPos[i], out hitPoint, out goBind))
                        {
                            hitPoint.UnderAttack(player);
                        }
                    }
                }
                else
                {
                    player.FireTime += Time.fixedDeltaTime;
                }
            }
        }
    }
}
