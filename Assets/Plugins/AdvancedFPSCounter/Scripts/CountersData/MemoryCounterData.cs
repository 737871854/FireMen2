using System;
using System.Text;
using UnityEngine;
using CodeStage.AdvanecedFPSCounter.Label;

#if UNITY_FLASH
using UnityEngine.Flash;
#endif

namespace CodeStage.AdvanecedFPSCounter.CountersData
{
	/// <summary>
	/// Shows memory usage data.
	/// </summary>
	[Serializable]
	public class MemoryCounterData
	{
		private const string COROUTINE_NAME = "UpdateMemoryCounter";
		private const string TEXT_START = "<color=#{0}><b>";
		private const string LINE_START_MONO = "MEM (mono): ";
		private const string LINE_START_FLASH = "MEM (flash): ";
		private const string LINE_START_HEAP = "MEM (heap): ";
		private const string LINE_END = " MB";
		private const string TEXT_END = "</b></color>";
		private const int MEMORY_DIVIDER = 1048576; // 1024^2

		[SerializeField]
		private bool enabled = true;

		[SerializeField]
		[Range(0.1f, 10f)]
		private float updateInterval = 1f;

		[SerializeField]
		private LabelAnchor anchor = LabelAnchor.UpperLeft;

		[SerializeField]
		private bool preciseValues = false;

		[SerializeField]
		private Color color = new Color32(234, 238, 101, 255);
		private string colorCached;

		[SerializeField]
		private bool monoUsage = true;

		[SerializeField]
		private bool heapUsage = true;

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal StringBuilder text;

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal bool dirty = false;
		private bool inited = false;

#if !UNITY_FLASH
		private long previousMonoValue = 0;
#else
		private int previousMonoValue = 0;
#endif

#if ENABLE_PROFILER
		private int previousHeapValue = 0;
#endif

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
		/// Counter's value update interval.
		/// </summary>
		public float UpdateInterval
		{
			get { return updateInterval; }
			set
			{
				if (updateInterval == value || !Application.isPlaying) return;
				updateInterval = value;
				if (!enabled) return;

				RestartCoroutine();
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
		/// Allows to output memory usage more precisely thus using more system resources.
		/// </summary>
		public bool PreciseValues
		{
			get { return preciseValues; }
			set
			{
				if (preciseValues == value || !Application.isPlaying) return;
				preciseValues = value;
				if (!enabled) return;

				Refresh();
			}
		}

		/// <summary>
		/// Color of the memory counter.
		/// </summary>
		public Color Color
		{
			get { return color; }
			set
			{
				if (color == value || !Application.isPlaying) return;
				color = value;
				if (!enabled) return;

				colorCached = String.Format(TEXT_START, AFPSCounter.Color32ToHex(color));
				Refresh();
			}
		}

		/// <summary>
		/// Allows to see mono memory usage. Also used to see private memory usage in Flash Player.
		/// </summary>
		public bool MonoUsage
		{
			get { return monoUsage; }
			set
			{
				if (monoUsage == value || !Application.isPlaying) return;
				monoUsage = value;
				if (!enabled) return;

				Refresh();
			}
		}

		/// <summary>
		/// Allows to see heap memory usage.
		/// Works with profiler enabled only!
		/// </summary>
		public bool HeapUsage
		{
			get { return heapUsage; }
			set
			{
				if (heapUsage == value || !Application.isPlaying) return;
				heapUsage = value;
				if (!enabled) return;

#if ENABLE_PROFILER
				Refresh();
#else
				if (heapUsage) Debug.LogWarning("You have enabled Heap Usage in AFPSCounter, but it requires enabled Profiler (it's disabled currently)!");
#endif
			}
		}

		/// <summary>
		/// Updates counter's value and forces label refresh.
		/// </summary>
		public void Refresh()
		{
			if (!enabled || !Application.isPlaying) return;
			UpdateValue(true);
			AFPSCounter.Instance.UpdateTexts();
		}

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal void Init()
		{
			if (!enabled) return;
			if (!HasData()) return;

			inited = true;

			previousMonoValue = 0;

#if ENABLE_PROFILER
			previousHeapValue = 0;
#endif

			if (colorCached == null)
			{
				colorCached = String.Format(TEXT_START, AFPSCounter.Color32ToHex(color));
			}

			if (text == null)
			{
				text = new StringBuilder(200);
			}
			else
			{
				text.Length = 0;
			}

			text.Append(colorCached);

			if (monoUsage)
			{
				string lineStart;

#if !UNITY_FLASH
				lineStart = LINE_START_MONO;
#else
				lineStart = LINE_START_FLASH;
#endif
				if (preciseValues)
				{
					text.Append(lineStart).AppendFormat("{0:F}", 0).Append(LINE_END);
				}
				else
				{
					text.Append(lineStart).Append(0).Append(LINE_END);
				}
			}


			if (heapUsage)
			{
#if ENABLE_PROFILER
				if (text.Length > 0) text.Append(AFPSCounter.NEW_LINE);
				if (preciseValues)
				{
					text.Append(LINE_START_HEAP).AppendFormat("{0:F}", 0).Append(LINE_END);
				}
				else
				{
					text.Append(LINE_START_HEAP).Append(0).Append(LINE_END);
				}
#else
				Debug.LogWarning("You have enabled Heap Usage in AFPSCounter, but it requires enabled Profiler (it's disabled currently)!");
#endif
			}

			text.Append(TEXT_END);

			AFPSCounter.Instance.StartCoroutine(COROUTINE_NAME);
			AFPSCounter.Instance.MakeDrawableLabelDirty(anchor);
		}

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal void Uninit()
		{
			if (text != null)
			{
				text.Length = 0;
			}

			AFPSCounter.Instance.StopCoroutine(COROUTINE_NAME);
			AFPSCounter.Instance.MakeDrawableLabelDirty(anchor);

			inited = false;
		}

		private void RestartCoroutine()
		{
			AFPSCounter.Instance.StopCoroutine(COROUTINE_NAME);
			AFPSCounter.Instance.StartCoroutine(COROUTINE_NAME);
		}

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal void UpdateValue(bool force = false)
		{
			if (!enabled) return;

			if (force)
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
			}

			if (monoUsage)
			{

#if !UNITY_FLASH
				long monoMemory;
				long monoDivisionResult = 0;
#else
				int monoMemory = 0;
				int monoDivisionResult = 0;
#endif

#if !UNITY_FLASH
	#if ENABLE_PROFILER
				monoMemory = UnityEngine.Profiling.Profiler.GetMonoUsedSize();
	#else
				monoMemory = System.GC.GetTotalMemory(false);
	#endif
#else
	#if UNITY_EDITOR
				monoMemory = (int)System.GC.GetTotalMemory(false);
	#else
				ActionScript.Import("com.unity.UnityNative");
				ActionScript.Statement("$num = GateFromFlashWorld.GetPrivateMemory();");
#endif
#endif

				bool newValue;
				if (preciseValues)
				{
					newValue = (previousMonoValue != monoMemory);
				}
				else
				{
					monoDivisionResult = monoMemory / MEMORY_DIVIDER;
					newValue = (previousMonoValue != monoDivisionResult);
				}

				if (newValue || force)
				{
					if (preciseValues)
					{
						previousMonoValue = monoMemory;
					}
					else
					{
						previousMonoValue = monoDivisionResult;
					}
					
					dirty = true;
				}
			}

#if ENABLE_PROFILER
			if (heapUsage)
			{
				int heapMemory = (int)UnityEngine.Profiling.Profiler.usedHeapSize;

				int heapDivisionResult = 0;
				bool newValue;
				if (preciseValues)
				{
					newValue = (previousHeapValue != heapMemory);
				}
				else
				{
					heapDivisionResult = heapMemory / MEMORY_DIVIDER;
					newValue = (previousHeapValue != heapDivisionResult);
				}

				if (newValue || force)
				{
					if (preciseValues)
					{
						previousHeapValue = heapMemory;
					}
					else
					{
						previousHeapValue = heapDivisionResult;
					}
					
					dirty = true;
				}
			}
#endif

			if (dirty)
			{
				bool needNewLine = false;

				text.Length = 0;
				text.Append(colorCached);

				if (monoUsage)
				{
#if UNITY_FLASH && !UNITY_EDITOR
					text.Append(LINE_START_FLASH);		
#else
					text.Append(LINE_START_MONO);
#endif

					if (preciseValues)
					{
						text.AppendFormat("{0:F}", previousMonoValue / (float)MEMORY_DIVIDER);
					}
					else
					{
						text.Append(previousMonoValue);
					}
					
					text.Append(LINE_END);
					needNewLine = true;
				}
				
#if ENABLE_PROFILER
				if (heapUsage)
				{
					if (needNewLine) text.Append(AFPSCounter.NEW_LINE);
					text.Append(LINE_START_HEAP);
					if (preciseValues)
					{
						text.AppendFormat("{0:F}", previousHeapValue / (float)MEMORY_DIVIDER);
					}
					else
					{
						text.Append(previousHeapValue);
					}
					text.Append(LINE_END);
				}
#endif
				text.Append(TEXT_END);

				//Debug.Log("text " + text);

				/*text.Append(colorCached);
				text.Append(previousMonoText).Append(previousHeapText);
				text.Length--;
				text.Append("</b></color>\n");*/
				//text.a

				//text = previousMonoText + previousHeapText;
				//text = text.Substring(0, text.Length - 1);
				//text = colorCached + text + "</b></color>\n";
			}
		}

		private bool HasData()
		{
#if ENABLE_PROFILER
			return monoUsage || heapUsage;
#else
			return monoUsage;
#endif
		}
	}
}