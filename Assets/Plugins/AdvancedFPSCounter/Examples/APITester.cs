using System;
using CodeStage.AdvanecedFPSCounter.Label;
using UnityEngine;

namespace CodeStage.AdvanecedFPSCounter
{

	public class APITester : MonoBehaviour
	{
		private int selectedTab = 0;
		private string[] tabs = new[] {"Common", "FPSCounter", "Memory Counter", "Device info"};

		private void OnGUI()
		{
			GUILayout.BeginArea(new Rect(120,120,Screen.width-240,Screen.height - 140));

			GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
			centeredStyle.alignment = TextAnchor.UpperCenter;
			centeredStyle.richText = true;
			GUILayout.Label("<b>Public API usage examples</b>", centeredStyle);
			centeredStyle.alignment = TextAnchor.UpperLeft;
			centeredStyle.richText = false;

			selectedTab = GUILayout.Toolbar(selectedTab, tabs);

			if (selectedTab == 0)
			{
				GUILayout.Space(10);
				AFPSCounter.Instance.enabled = GUILayout.Toggle(AFPSCounter.Instance.enabled, "Enabled");

				GUILayout.Label("Hot Key: ");
				int hotKeyIndex;

				if (AFPSCounter.Instance.HotKey == KeyCode.BackQuote)
				{
					hotKeyIndex = 1;
				}
				else
				{
					hotKeyIndex = (int)AFPSCounter.Instance.HotKey;
				}

				hotKeyIndex = GUILayout.Toolbar(hotKeyIndex, new[] { "None (disabled)", "BackQoute (`)"});
				if (hotKeyIndex == 1)
				{
					AFPSCounter.Instance.HotKey = KeyCode.BackQuote;
				}
				else
				{
					AFPSCounter.Instance.HotKey = KeyCode.None;
				}

				AFPSCounter.Instance.keepAlive = GUILayout.Toggle(AFPSCounter.Instance.keepAlive, "Keep Alive");

				AFPSCounter.Instance.ForceFrameRate = GUILayout.Toggle(AFPSCounter.Instance.ForceFrameRate, "Force FPS");
				AFPSCounter.Instance.ForcedFrameRate = (int)SliderLabel(AFPSCounter.Instance.ForcedFrameRate, -1, 100);

				GUILayout.Label("Pixel offset: ");
				float offsetX = AFPSCounter.Instance.AnchorsOffset.x;
				float offsetY = AFPSCounter.Instance.AnchorsOffset.y;

				offsetX = (int)SliderLabel(offsetX, 0, 100);
				offsetY = (int)SliderLabel(offsetY, 0, 100);

				AFPSCounter.Instance.AnchorsOffset = new Vector2(offsetX, offsetY);
					
				GUILayout.Label("Font Size: ");
				AFPSCounter.Instance.FontSize = (int)SliderLabel(AFPSCounter.Instance.FontSize, 0, 100);

				GUILayout.Label("Line spacing: ");
				AFPSCounter.Instance.LineSpacing = SliderLabel(AFPSCounter.Instance.LineSpacing, 0f, 10f);
			}
			else if (selectedTab == 1)
			{
				GUILayout.Space(10);

				AFPSCounter.Instance.fpsCounter.Enabled = GUILayout.Toggle(AFPSCounter.Instance.fpsCounter.Enabled, "Enabled");
				GUILayout.Label("Update Interval: ");
				AFPSCounter.Instance.fpsCounter.UpdateInterval = SliderLabel(AFPSCounter.Instance.fpsCounter.UpdateInterval, 0.1f, 10f);
				GUILayout.Label("Layout");
				AFPSCounter.Instance.fpsCounter.Anchor = (LabelAnchor)GUILayout.Toolbar((int)AFPSCounter.Instance.fpsCounter.Anchor, new[] { "UpperLeft", "UpperRight", "LowerLeft", "LowerRight" });
			
				GUILayout.BeginHorizontal();
				AFPSCounter.Instance.fpsCounter.ShowAverage = GUILayout.Toggle(AFPSCounter.Instance.fpsCounter.ShowAverage, "Average FPS");
				AFPSCounter.Instance.fpsCounter.resetAverageOnNewScene = GUILayout.Toggle(AFPSCounter.Instance.fpsCounter.resetAverageOnNewScene, "Reset Average On New Scene Load");
				if (GUILayout.Button("Reset now!"))
				{
					AFPSCounter.Instance.fpsCounter.ResetAverage();
				}
				GUILayout.EndHorizontal();
			}
			else if (selectedTab == 2)
			{
				GUILayout.Space(10);

				AFPSCounter.Instance.memoryCounter.Enabled = GUILayout.Toggle(AFPSCounter.Instance.memoryCounter.Enabled, "Enabled");
				GUILayout.Label("Update Interval: ");
				AFPSCounter.Instance.memoryCounter.UpdateInterval = SliderLabel(AFPSCounter.Instance.memoryCounter.UpdateInterval, 0.1f, 10f);
				GUILayout.Label("Layout");
				AFPSCounter.Instance.memoryCounter.Anchor = (LabelAnchor)GUILayout.Toolbar((int)AFPSCounter.Instance.memoryCounter.Anchor, new[] { "UpperLeft", "UpperRight", "LowerLeft", "LowerRight" });
				AFPSCounter.Instance.memoryCounter.PreciseValues = GUILayout.Toggle(AFPSCounter.Instance.memoryCounter.PreciseValues, "Precise (uses more system resources)");
				AFPSCounter.Instance.memoryCounter.MonoUsage = GUILayout.Toggle(AFPSCounter.Instance.memoryCounter.MonoUsage, "Show mono memory usage");
				AFPSCounter.Instance.memoryCounter.HeapUsage = GUILayout.Toggle(AFPSCounter.Instance.memoryCounter.HeapUsage, "Show heap memory usage (requires enabled Profiler)");
			}
			else if (selectedTab == 3)
			{
				GUILayout.Space(10);

				AFPSCounter.Instance.deviceInfoCounter.Enabled = GUILayout.Toggle(AFPSCounter.Instance.deviceInfoCounter.Enabled, "Enabled");
				GUILayout.Label("Layout");
				AFPSCounter.Instance.deviceInfoCounter.Anchor = (LabelAnchor)GUILayout.Toolbar((int)AFPSCounter.Instance.deviceInfoCounter.Anchor, new[] { "UpperLeft", "UpperRight", "LowerLeft", "LowerRight" });
				AFPSCounter.Instance.deviceInfoCounter.CpuModel = GUILayout.Toggle(AFPSCounter.Instance.deviceInfoCounter.CpuModel, "Show CPU model and maximum threads count");
				AFPSCounter.Instance.deviceInfoCounter.GpuModel = GUILayout.Toggle(AFPSCounter.Instance.deviceInfoCounter.GpuModel, "Show GPU model and total VRAM count");
				AFPSCounter.Instance.deviceInfoCounter.RamSize = GUILayout.Toggle(AFPSCounter.Instance.deviceInfoCounter.RamSize, "Show total RAM size");
				AFPSCounter.Instance.deviceInfoCounter.ScreenData = GUILayout.Toggle(AFPSCounter.Instance.deviceInfoCounter.ScreenData, "Show resolution, window size and DPI (if possible)");
			}

			GUILayout.EndArea();
		}

		private float SliderLabel(float sliderValue, float sliderMinValue, float sliderMaxValue)
		{
			GUILayout.BeginHorizontal();
			sliderValue = GUILayout.HorizontalSlider(sliderValue, sliderMinValue, sliderMaxValue);
			GUILayout.Space(10);
			//GUILayout.Label(sliderValue.ToString("0.00"), GUILayout.MaxWidth(50));
			GUILayout.Label(String.Format("{0:F2}", sliderValue), GUILayout.MaxWidth(50));
			GUILayout.EndHorizontal();

			return sliderValue;
		}
	}
}
