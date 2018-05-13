/**
 * Copyright 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：VersionControlArt.cs
 * 简	 述：美术版本控制类。
 * 作	 者：Fanki
 * 创建标识：2015/09/24
 */

using UnityEngine;
using System.Collections;

namespace Qtz.Q5.Version
{
	public class VersionControlArt : VersionControlProcess
	{
		public override void OnConfigFinish(bool isSucess, string msg)
		{
			string localPath = Config.Instance.GetLocalBundlePath();
			_pkgBundle.SetSpecialPackageUrl("file:///" + localPath);

			base.OnConfigFinish(isSucess, msg);
		}
	}
}
