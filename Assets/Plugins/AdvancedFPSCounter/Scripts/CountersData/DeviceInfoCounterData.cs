using System;
using System.Text;
using UnityEngine;
using CodeStage.AdvanecedFPSCounter.Label;

namespace CodeStage.AdvanecedFPSCounter.CountersData
{
	/// <summary>
	/// Shows additional device information.
	/// </summary>
	[Serializable]
	public class DeviceInfoCounterData
	{

		[SerializeField]
		private bool enabled = false;

		[SerializeField]
		private LabelAnchor anchor = LabelAnchor.LowerLeft;

		[SerializeField]
		private Color color = new Color32(172, 172, 172, 255);
		private string colorCached;

		[SerializeField]
		private bool cpuModel = true;

		[SerializeField]
		private bool gpuModel = true;

		[SerializeField]
		private bool ramSize = true;

		[SerializeField]
		private bool screenData = true;

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal StringBuilder text;

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal bool dirty = false;
		private bool inited = false;

		/// <summary>
		/// Enables or disables counter with immediate label refresh.
		/// </summary>
		public bool Enabled
		{
			get { return enabled; }
			set
			{
				if (enabled == value || !Application.isPlaying) return;

				enabled = value;

				if (value)
				{
					if (!inited && (HasData())) Init();
				}
				else
				{
					if (inited) Uninit();
				}

				AFPSCounter.Instance.UpdateTexts();
			}
		}

		/// <summary>
		/// Changes counter's label. Refreshes both previous and current label.
		/// </summary>
		public LabelAnchor Anchor
		{
			get
			{
				return anchor;
			}
			set
			{
				if (anchor == value || !Application.isPlaying) return;
				LabelAnchor prevAnchor = anchor;
				anchor = value;
				if (!enabled) return;

				dirty = true;
				AFPSCounter.Instance.MakeDrawableLabelDirty(prevAnchor);
				AFPSCounter.Instance.UpdateTexts();
			}
		}

		/// <summary>
		/// Color of the device information counter.
		/// </summary>
		public Color Color
		{
			get { return color; }
			set
			{
				if (color == value || !Application.isPlaying) return;
				color = value;
				if (!enabled) return;

				colorCached = "<color=#" + AFPSCounter.Color32ToHex(color) + ">";
				Refresh();
			}
		}

		/// <summary>
		/// Shows CPU model name and maximum supported threads count.
		/// </summary>
		public bool CpuModel
		{
			get { return cpuModel; }
			set
			{
				if (cpuModel == value || !Application.isPlaying) return;
				cpuModel = value;
				if (!enabled) return;

				Refresh();
			}
		}

		/// <summary>
		/// Shows GPU model name, supported shader model (if possible) and total Video RAM size (if possible).
		/// </summary>
		public bool GpuModel
		{
			get { return gpuModel; }
			set
			{
				if (gpuModel == value || !Application.isPlaying) return;
				gpuModel = value;
				if (!enabled) return;

				Refresh();
			}
		}

		/// <summary>
		/// Shows total RAM size.
		/// </summary>
		public bool RamSize
		{
			get { return ramSize; }
			set
			{
				if (ramSize == value || !Application.isPlaying) return;
				ramSize = value;
				if (!enabled) return;

				Refresh();
			}
		}

		/// <summary>
		/// Shows screen resolution, size and DPI (if possible).
		/// </summary>
		public bool ScreenData
		{
			get { return screenData; }
			set
			{
				if (screenData == value || !Application.isPlaying) return;
				screenData = value;
				if (!enabled) return;

				Refresh();
			}
		}

		/// <summary>
		/// Updates counter's value and forces label refresh.
		/// </summary>
		public void Refresh()
		{
			if (!enabled || !Application.isPlaying) return;
			UpdateValue();
			AFPSCounter.Instance.UpdateTexts();
		}

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal void Init()
		{
			if (!enabled) return;
			if (!HasData()) return;

			if (colorCached == null)
			{
				colorCached = "<color=#" + AFPSCounter.Color32ToHex(color) + ">";
			}

			inited = true;

			if (text == null)
			{
				text = new StringBuilder();
			}
			else
			{
				text.Length = 0;
			}

			UpdateValue();
		}

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal void Uninit()
		{
			if (text != null) text.Length = 0;
			inited = false;

			AFPSCounter.Instance.MakeDrawableLabelDirty(anchor);
		}

		private void UpdateValue()
		{
			if (!inited && (HasData()))
			{
				Init();
				return;
			}

			if (inited && (!HasData()))
			{
				Uninit();
				return;
			}

			if (!enabled) return;

			bool needNewLine = false;

			text.Length = 0;
			text.Append(colorCached);

			if (cpuModel)
			{
				text.Append("CPU: ").Append(SystemInfo.processorType).Append(" (").Append(SystemInfo.processorCount).Append(" threads)");
				needNewLine = true;
			}

			if (gpuModel)
			{
				if (needNewLine) text.Append(AFPSCounter.NEW_LINE);
				text.Append("GPU: ").Append(SystemInfo.graphicsDeviceName);

				bool showSM = false;
				int sm = SystemInfo.graphicsShaderLevel;
				if (sm == 20)
				{
					text.Append(" (SM: 2.0");
					showSM = true;
				}
				else if (sm == 30)
				{
					text.Append(" (SM: 3.0");
					showSM = true;
				}
				else if (sm == 40)
				{
					text.Append(" (SM: 4.0");
					showSM = true;
				}
				else if (sm == 41)
				{
					text.Append(" (SM: 4.1");
					showSM = true;
				}
				else if (sm == 50)
				{
					text.Append(" (SM: 5.0");
					showSM = true;
				}

				int vram = SystemInfo.graphicsMemorySize;
				if (vram > 0)
				{
					if (showSM)
					{
						text.Append(", VRAM: ").Append(vram).Append(" MB)");
					}
					else
					{
						text.Append("(VRAM: ").Append(vram).Append(" MB)");
					}
				}
				else if (showSM)
				{
					text.Append(")");
				}
				needNewLine = true;
			}

			if (ramSize)
			{
				if (needNewLine) text.Append(AFPSCounter.NEW_LINE);

				int ram = SystemInfo.systemMemorySize;

				if (ram > 0)
				{
					text.Append("RAM: ").Append(ram).Append(" MB");
					needNewLine = true;
				}
			}

			if (screenData)
			{
				if (needNewLine) text.Append(AFPSCounter.NEW_LINE);
				Resolution res = Screen.currentResolution;

				text.Append("Screen: ").Append(res.width).Append("x").Append(res.height).Append("@").Append(res.refreshRate).Append("Hz (window size: ").Append(Screen.width).Append("x").Append(Screen.height);
				float dpi = Screen.dpi;
				if (dpi <= 0)
				{
					text.Append(")");
				}
				else
				{
					text.Append(", DPI: ").Append(dpi).Append(")");
				}
			}

			text.Append("</color>");
			dirty = true;
		}

		private bool HasData()
		{
			return cpuModel || gpuModel || ramSize || screenData;
		}
	}
}
