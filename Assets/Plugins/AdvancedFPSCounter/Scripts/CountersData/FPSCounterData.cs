using System;
using System.Text;
using UnityEngine;
using CodeStage.AdvanecedFPSCounter.Label;

namespace CodeStage.AdvanecedFPSCounter.CountersData
{
	/// <summary>
	/// Shows frames per second counter.
	/// </summary>
	[Serializable]
	public class FPSCounterData
	{
		private const string COROUTINE_NAME = "UpdateFPSCounter";
		private const string TEXT_START = "<color=#{0}><b>";
		private const string TEXT_END = "</b></color>";

		[SerializeField]
		private bool enabled = true;

		[SerializeField]
		[Range(0.1f, 10f)]
		private float updateInterval = 0.5f;

		[SerializeField]
		private LabelAnchor anchor = LabelAnchor.UpperLeft;

		[SerializeField]
		private bool showAverage = true;

		/// <summary>
		/// Average FPS counter accumulative data will be reset on new scene load if enabled.
		/// </summary>
		public bool resetAverageOnNewScene = false;

		/// <summary>
		/// If FPS will drop below this value, colorWarning will be used for counter text.
		/// </summary>
		public int warningLevelValue = 30;

		/// <summary>
		/// If FPS will be equal or less this value, colorCritical will be used for counter text.
		/// </summary>
		public int criticalLevelValue = 10;

		[SerializeField]
		private Color colorNormal = new Color32(85, 218, 102, 255);
		private string colorNormalCached;
		private string colorNormalCachedAvg;

		[SerializeField]
		private Color colorWarning = new Color32(236, 224, 88, 255);
		private string colorWarningCached;
		private string colorWarningCachedAvg;

		[SerializeField]
		private Color colorCritical = new Color32(249, 91, 91, 255);
		private string colorCriticalCached;
		private string colorCriticalCachedAvg;

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal StringBuilder text;

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal bool dirty = false;

		private int previousValue = 0;
		private int averageSamples = 0;
		private float average = 0;
		private int averageRounded = 0;
		private int previousAverageValue = 0;

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

				if (enabled)
				{
					Init();
				}
				else
				{
					Uninit();
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
		/// Shows average FPS since game or scene start, depending on resetAverageOnNewScene field value.
		/// </summary>
		public bool ShowAverage
		{
			get { return showAverage; }
			set
			{
				if (showAverage == value || !Application.isPlaying) return;
				showAverage = value;
				if (!enabled) return;

				if (!showAverage) ResetAverage();

				Refresh();
			}
		}

		/// <summary>
		/// Color of the FPS counter while FPS is greater or equal warningLevelValue.
		/// </summary>
		public Color ColorNormal
		{
			get { return colorNormal; }
			set
			{
				if (colorNormal == value || !Application.isPlaying) return;
				colorNormal = value;
				if (!enabled) return;

				string color = String.Format(TEXT_START, AFPSCounter.Color32ToHex(colorNormal));
				colorNormalCached = color + "FPS: ";
				colorNormalCachedAvg = color + " AVG: ";

				Refresh();
			}
		}

		/// <summary>
		/// Color of the FPS counter while FPS between criticalLevelValue and warningLevelValue.
		/// </summary>
		public Color ColorWarning
		{
			get { return colorWarning; }
			set
			{
				if (colorWarning == value || !Application.isPlaying) return;
				colorWarning = value;
				if (!enabled) return;

				string color = String.Format(TEXT_START, AFPSCounter.Color32ToHex(colorWarning));
				colorWarningCached = color + "FPS: ";
				colorWarningCachedAvg = color + " AVG: ";

				Refresh();
			}
		}

		/// <summary>
		/// Color of the FPS counter while FPS is lower criticalLevelValue.
		/// </summary>
		public Color ColorCritical
		{
			get { return colorCritical; }
			set
			{
				if (colorCritical == value || !Application.isPlaying) return;
				colorCritical = value;
				if (!enabled) return;

				string color = String.Format(TEXT_START, AFPSCounter.Color32ToHex(colorCritical));
				colorCriticalCached = color + "FPS: ";
				colorCriticalCachedAvg = color + " AVG: ";

				Refresh();
			}
		}

		
		/// <summary>
		/// Updates counter's value and forces label refresh.
		/// </summary>
		public void Refresh()
		{
			if (!enabled || !Application.isPlaying) return;
			UpdateValue(previousValue, true);
			AFPSCounter.Instance.UpdateTexts();
		}

		/// <summary>
		/// Resets Average FPS counter accumulative data.
		/// </summary>
		public void ResetAverage()
		{
			averageSamples = 0;
			average = 0;
			averageRounded = 0;
		}

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal void Init()
		{
			if (!enabled) return;

			string color;

			if (colorNormalCached == null)
			{
				color = String.Format(TEXT_START, AFPSCounter.Color32ToHex(colorNormal));
				colorNormalCached = color + "FPS: ";
				colorNormalCachedAvg = color + " AVG: ";
			}

			if (colorWarningCached == null)
			{
				color = String.Format(TEXT_START, AFPSCounter.Color32ToHex(colorWarning));
				colorWarningCached = color + "FPS: ";
				colorWarningCachedAvg = color + " AVG: ";
			}

			if (colorCriticalCached == null)
			{
				color = String.Format(TEXT_START, AFPSCounter.Color32ToHex(colorCritical));
				colorCriticalCached = color + "FPS: ";
				colorCriticalCachedAvg = color + " AVG: ";
			}

			previousValue = 0;

			if (text == null)
			{
				text = new StringBuilder(100);
			}
			else
			{
				text.Length = 0;
			}

			text.Append(colorCriticalCached).Append("0").Append(TEXT_END);
			dirty = true;

			AFPSCounter.Instance.StartCoroutine(COROUTINE_NAME);
		}

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal void Uninit()
		{
			if (text != null) text.Length = 0;
			AFPSCounter.Instance.StopCoroutine(COROUTINE_NAME);
			AFPSCounter.Instance.MakeDrawableLabelDirty(anchor);
			ResetAverage();
		}

		private void RestartCoroutine()
		{
			AFPSCounter.Instance.StopCoroutine(COROUTINE_NAME);
			AFPSCounter.Instance.StartCoroutine(COROUTINE_NAME);
		}

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal void UpdateValue(int value, bool force = false)
		{
			if (!enabled) return;

			if (previousValue != value || force)
			{
				previousValue = value;
				dirty = true;
			}

			if (showAverage)
			{
				averageSamples++;
				average += (value - average) / averageSamples;

				averageRounded = Mathf.RoundToInt(average);

				if (previousAverageValue != averageRounded || force)
				{
					previousAverageValue = averageRounded;
					dirty = true;
				}
			}

			if (dirty)
			{
				string color;

				if (value >= warningLevelValue)
					color = colorNormalCached;
				else if (value <= criticalLevelValue)
					color = colorCriticalCached;
				else
					color = colorWarningCached;

				text.Length = 0;
				text.Append(color).Append(value).Append(TEXT_END);

				if (showAverage)
				{
					if (averageRounded >= warningLevelValue)
						color = colorNormalCachedAvg;
					else if (averageRounded <= criticalLevelValue)
						color = colorCriticalCachedAvg;
					else
						color = colorWarningCachedAvg;


					text.Append(color).Append(averageRounded).Append(TEXT_END);
				}
			}
		}
	}
}