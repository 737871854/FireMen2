/**
 * Copyright (广州纷享游艺设备有限公司-研发视频组) 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：FightTime.cs
 * 简    述：战斗中的逻辑帧时间
 * 创建标识：廖凯
 */

using UnityEngine;

class FightTime {
    public static float deltaTime {
        get { return 0.016666666666f; }
    }

    public static int frameCount {
        get { return Time.frameCount; }
    }
}
