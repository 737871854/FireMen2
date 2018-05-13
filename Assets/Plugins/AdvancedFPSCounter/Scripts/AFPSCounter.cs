using System;
using System.Collections;
using CodeStage.AdvanecedFPSCounter.CountersData;
using CodeStage.AdvanecedFPSCounter.Label;

namespace CodeStage.AdvanecedFPSCounter
{
	using UnityEngine;

	/// <summary>
	/// Allows to see frames per second counter, memory usage counter and some simple hardware information right in running app on any device.
	/// Just use GameObject->Create Other->Code Stage->Advanced FPS Counter (wooh, pretty long, yeah?) menu item and you're ready to go!
	/// </summary>
	[AddComponentMenu("")] // sorry, but you shouldn't add it via Component menu, read above comment please
	public class AFPSCounter: MonoBehaviour
	{
		private const string CONTAINER_NAME = "Advanced FPS Counter";

#if !UNITY_FLASH
		/// <summary>
		/// For internal usage!
		/// </summary>
		internal static string NEW_LINE = Environment.NewLine;
#else
		/// <summary>
		/// For internal usage!
		/// </summary>
		internal const string NEW_LINE = "\n";
#endif

		private static AFPSCounter instance = null;

		/// <summary>
		/// Frames Per Second counter.
		/// </summary>
		public FPSCounterData fpsCounter = new FPSCounterData();

		/// <summary>
		/// Mono or heap memory counter.
		/// </summary>
		public MemoryCounterData memoryCounter = new MemoryCounterData();

		/// <summary>
		/// Device hardware info.
		/// Shows CPU name, cores (threads) count, GPU name, total VRAM, total RAM, screen DPI and screen size.
		/// </summary>
		public DeviceInfoCounterData deviceInfoCounter = new DeviceInfoCounterData();

		[SerializeField]
		private KeyCode hotKey = KeyCode.BackQuote;

		/// <summary>
		/// Allows to keep Advanced FPS Counter game object on new level (scene) load.
		/// </summary>
		public bool keepAlive = true;

		[SerializeField]
		private bool forceFrameRate = false;

		[SerializeField]
		[Range(-1, 200)]
		private int forcedFrameRate = -1;

		[SerializeField]
		private Vector2 anchorsOffset = new Vector2(5,5);

		[SerializeField]
		private Font labelsFont;

		[SerializeField]
		[Range(0, 100)]
		private int fontSize = 0;

		[SerializeField]
		[Range(0f, 10f)]
		private float lineSpacing = 1;

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal DrawableLabel[] labels;

		private int anchorsCount;
		private int cachedVSync = -1;
		private int cachedFrameRate = -1;
		private Coroutine hotKeyCoroutine = null;

#if UNITY_EDITOR
		
		[HideInInspector]
		[SerializeField]
		private bool fpsGroupToggle;
		
		[HideInInspector]
		[SerializeField]
		private bool memoryGroupToggle;

		[HideInInspector]
		[SerializeField]
		private bool deviceGroupToggle;

		[HideInInspector]
		[SerializeField]
		private bool lookAndFeelToggle;

		private const string MENU_PATH = "GameObject/Create Other/Code Stage/Advanced FPS Counter %&#F";

		[UnityEditor.MenuItem(MENU_PATH, false)]
		private static void AddToScene()
		{
			AFPSCounter counter = (AFPSCounter)FindObjectOfType(typeof(AFPSCounter));
			if (counter != null)
			{
				if (counter.IsPlacedCorrectly())
				{
					if (UnityEditor.EditorUtility.DisplayDialog("Remove Advanced FPS Counter?", "Advanced FPS Counter already exists in scene and placed correctly. Dou you wish to remove it?", "Yes", "No"))
					{
						DestroyImmediate(counter.gameObject);
					}
				}
				else
				{
					if (counter.MayBePlacedHere())
					{
						int dialogResult = UnityEditor.EditorUtility.DisplayDialogComplex("Fix existing Game Object to work with Adavnced FPS Counter?", "Advanced FPS Counter already exists in scene and placed onto empty Game Object \"" + counter.name + "\".\nDo you wish to let plugin configure and use this Game Object further? Press Delete to remove plugin from scene at all.", "Fix", "Delete", "Cancel");

						switch (dialogResult)
						{
							case 0:
								counter.FixCurrentGameObject();
								break;
							case 1:
								DestroyImmediate(counter);
								break;
							default:
								break;
						}
					}
					else
					{
						int dialogResult = UnityEditor.EditorUtility.DisplayDialogComplex("Move existing Adavnced FPS Counter to own Game Object?", "Looks like Advanced FPS Counter plugin is already exists in scene and placed incorrectly on Game Object \"" + counter.name + "\".\nDo you wish to let plugin move itself onto separate configured Game Object \"" + CONTAINER_NAME + "\"? Press Delete to remove plugin from scene at all.", "Move", "Delete", "Cancel");
						switch (dialogResult)
						{
							case 0:
								GameObject go = new GameObject(CONTAINER_NAME);
								AFPSCounter newCounter = go.AddComponent<AFPSCounter>();

								UnityEditor.EditorUtility.CopySerialized(counter, newCounter);

								DestroyImmediate(counter);
								break;
							case 1:
								DestroyImmediate(counter);
								break;
							default:
								break;
						}
					}
				}
			}
			else
			{
				GameObject go = new GameObject(CONTAINER_NAME);
				go.AddComponent<AFPSCounter>();
			}
		}

		private bool MayBePlacedHere()
		{
			return (gameObject.GetComponentsInChildren<Component>().Length == 2 &&
					transform.childCount == 0 &&
					transform.parent == null);
		}

		private void FixCurrentGameObject()
		{
			gameObject.name = CONTAINER_NAME;
			transform.position = Vector3.zero;
			transform.rotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			tag = "Untagged";
            gameObject.layer = 5;//这里要修改显示UI层为5 //0;
			gameObject.isStatic = false;
		}
#endif

		/// <summary>
		/// Allows to control AFPSCounter from code. AFPSCounter instance will be spawned if not exists.
		/// </summary>
		public static AFPSCounter Instance
		{
			get
			{
				if (instance == null)
				{
					AFPSCounter counter = (AFPSCounter)FindObjectOfType(typeof(AFPSCounter));
					if (counter != null && counter.IsPlacedCorrectly())
					{
						//Debug.Log("AFPSCounter instance was found in scene!");
						instance = counter;
					}
					else
					{
						//Debug.Log("AFPSCounter instance will be created right now!");
						GameObject go = new GameObject(CONTAINER_NAME);
                        go.layer = 5;
						go.AddComponent<AFPSCounter>();
					}
				}
				return instance;
			}
		}

		/// <summary>
		/// Allows to see how your game performs on specified frame rate.
		/// <para>IMPORTANT: this option disables VSync while enabled!</para>
		/// </summary>
		/// Useful to check how you game's physics performs on slow devices for example.
		public bool ForceFrameRate
		{
			get { return forceFrameRate; }
			set
			{
				if (forceFrameRate == value || !Application.isPlaying) return;
				forceFrameRate = value;
				if (!enabled) return;

				RefreshForcedFrameRate();
			}
		}

		/// <summary>
		/// Desired frame rate for ForceFrameRate option, does not guarantee selected frame rate.
		/// Set to -1 to render as fast as possible in current conditions.
		/// </summary>
		public int ForcedFrameRate
		{
			get { return forcedFrameRate; }
			set
			{
				if (forcedFrameRate == value || !Application.isPlaying) return;
				forcedFrameRate = value;
				if (!enabled) return;

				RefreshForcedFrameRate();
			}
		}

		/// <summary>
		/// Used to enable / disable plugin at runtime. Set to KeyCode.None to disable.
		/// </summary>
		public KeyCode HotKey
		{
			get { return hotKey; }
			set
			{
				if (hotKey == value || !Application.isPlaying) return;
				hotKey = value;
				if (!enabled) return;

				RefreshHotKey();
			}
		}

		/// <summary>
		/// Pixel offset for anchored labels. Automatically applied to all 4 corners.
		/// </summary>
		public Vector2 AnchorsOffset
		{
			get { return anchorsOffset; }
			set
			{
				if (anchorsOffset == value || !Application.isPlaying) return;
				anchorsOffset = value;
				if (!enabled || labels == null) return;

				for (int i = 0; i < anchorsCount; i++)
				{
					labels[i].ChangeOffset(anchorsOffset);
				}
			}
		}

		/// <summary>
		/// Font to render labels with.
		/// </summary>
		public Font LabelsFont
		{
			get { return labelsFont; }
			set
			{
				if (labelsFont == value || !Application.isPlaying) return;
				labelsFont = value;
				if (!enabled || labels == null) return;

				for (int i = 0; i < anchorsCount; i++)
				{
					labels[i].ChangeFont(labelsFont);
				}
			}
		}

		/// <summary>
		/// The font size to use (for dynamic fonts).
		/// </summary>
		/// If this is set to a non-zero value, the font size specified in the font importer is overriden with a custom size. This is only supported for fonts set to use dynamic font rendering. Other fonts will always use the default font size.
		public int FontSize
		{
			get { return fontSize; }
			set
			{
				if (fontSize == value || !Application.isPlaying) return;
				fontSize = value;
				if (!enabled || labels == null) return;

				for (int i = 0; i < anchorsCount; i++)
				{
					labels[i].ChangeFontSize(fontSize);
				}
			}
		}

		/// <summary>
		/// The font size to use (for dynamic fonts).
		/// </summary>
		/// If this is set to a non-zero value, the font size specified in the font importer is overriden with a custom size. This is only supported for fonts set to use dynamic font rendering. Other fonts will always use the default font size.
		public float LineSpacing
		{
			get { return lineSpacing; }
			set
			{
				if (lineSpacing == value || !Application.isPlaying) return;
				lineSpacing = value;
				if (!enabled || labels == null) return;

				for (int i = 0; i < anchorsCount; i++)
				{
					labels[i].ChangeLineSpacing(lineSpacing);
				}
			}
		}

		// preventing direct instantiation =P
		private AFPSCounter() { }

		/// <summary>
		/// Use it to completely dispose AFPSCounter.
		/// </summary>
		public void Dispose()
		{
			instance = null;
			Destroy(gameObject);
		}

		private void Awake()
		{
			//Debug.Log("AFPSCounter Awake, instance = " + instance);

			if (instance != null)
			{
				Debug.LogWarning("Only one Advanced FPS Counter instance allowed!");
				Destroy(this);
				return;
			}

			if (!IsPlacedCorrectly())
			{
				Debug.LogWarning("Advanced FPS Counter is placed in scene incorrectly and will be auto-destroyed! Please, use \"GameObject->Create Other->Code Stage->Advanced FPS Counter\" menu to correct this!");
				Destroy(this);
				return;
			}

#if UNITY_EDITOR

			Camera[] cameras = Camera.allCameras;
			int len = cameras.Length;

			float highestCameraDeph = float.MinValue;
			float highestSuitableCameraDeph = float.MinValue;

			for (int i = 0; i < len; i++)
			{
				Camera cam = cameras[i];
				if (cam.depth > highestCameraDeph)
				{
					highestCameraDeph = cam.depth;
				}
				
				GUILayer guiLayer = cameras[i].GetComponent<GUILayer>();
				if (guiLayer != null && guiLayer.enabled)
				{
					if (cam.depth > highestSuitableCameraDeph)
					{
						highestSuitableCameraDeph = cam.depth;
					}
				}
			}

			if (highestCameraDeph != highestSuitableCameraDeph)
			{
				Debug.LogWarning("Advanced FPS Counter needs top-most (highest depth) camera with enabled GUILayer to be visible!");
			}
#endif

			instance = this;
			DontDestroyOnLoad(gameObject);

			anchorsCount = Enum.GetNames(typeof(LabelAnchor)).Length;
			labels = new DrawableLabel[anchorsCount];

			for (int i = 0; i < anchorsCount; i++)
			{
				labels[i] = new DrawableLabel((LabelAnchor)i, anchorsOffset, labelsFont, fontSize, lineSpacing);
			}

			RefreshHotKey();
		}

		private bool IsPlacedCorrectly()
		{
			return (gameObject.name == CONTAINER_NAME &&
					gameObject.GetComponentsInChildren<Component>().Length == 2 &&
					transform.childCount == 0 &&
					transform.parent == null);
		}

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal void MakeDrawableLabelDirty(LabelAnchor anchor)
		{
			labels[(int)anchor].dirty = true;
		}

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal void UpdateTexts()
		{
			//Debug.Log("UpdateTexts " + Time.realtimeSinceStartup);
			if (!enabled) return;

			bool anyContentPresent = false;

			//Debug.Log("UpdateTexts1 " + FPSCounter.Enabled);
			//Debug.Log("UpdateTexts2 " + FPSCounter.text);

			if (fpsCounter.Enabled)
			{
				DrawableLabel label = labels[(int)fpsCounter.Anchor];
				if (label.newText.Length > 0) label.newText.Append(NEW_LINE);
				label.newText.Append(fpsCounter.text);
				label.dirty |= fpsCounter.dirty;
				fpsCounter.dirty = false;

				anyContentPresent = true;
			}

			if (memoryCounter.Enabled)
			{
				DrawableLabel label = labels[(int)memoryCounter.Anchor];
				if (label.newText.Length > 0) label.newText.Append(NEW_LINE);
				label.newText.Append(memoryCounter.text);
				label.dirty |= memoryCounter.dirty;
				memoryCounter.dirty = false;

				anyContentPresent = true;
			}

			if (deviceInfoCounter.Enabled)
			{
				DrawableLabel label = labels[(int)deviceInfoCounter.Anchor];
				if (label.newText.Length > 0) label.newText.Append(NEW_LINE);
				label.newText.Append(deviceInfoCounter.text);
				label.dirty |= deviceInfoCounter.dirty;
				deviceInfoCounter.dirty = false;

				anyContentPresent = true;
			}

			if (anyContentPresent)
			{
				for (int i = 0; i < anchorsCount; i++)
				{
					labels[i].CheckAndUpdate();
				}
			}
			else
			{
				for (int i = 0; i < anchorsCount; i++)
				{
					labels[i].Clear();
				}
			}
		}

		private IEnumerator UpdateFPSCounter()
		{
			while (true)
			{
				float previousUpdateTime = Time.time;
				int previousUpdateFrames = Time.frameCount;

				yield return new WaitForSeconds(fpsCounter.UpdateInterval);

				float timeElapsed = Time.time - previousUpdateTime;
				int framesChanged = Time.frameCount - previousUpdateFrames;

				// flooring FPS
				int fps = (int)(framesChanged / (timeElapsed / Time.timeScale));

				fpsCounter.UpdateValue(fps);
				UpdateTexts();
			}
		}
		
		private IEnumerator UpdateMemoryCounter()
		{
			while (true)
			{
				yield return new WaitForSeconds(memoryCounter.UpdateInterval);

				memoryCounter.UpdateValue();
				UpdateTexts();
			}
		}

		private IEnumerator WaitForKeyDown(KeyCode keyCode)
		{
			while (true)
			{
				if (Input.GetKeyDown(keyCode))
				{
					SwitchCounter();
				}
				yield return null;
			}
		}

		private void SwitchCounter()
		{
			enabled = !enabled;
		}

		private void InitCounters()
		{
			fpsCounter.Init();
			memoryCounter.Init();
			deviceInfoCounter.Init();

			if (fpsCounter.Enabled || memoryCounter.Enabled || deviceInfoCounter.Enabled)
			{
				UpdateTexts();
			}
		}

		private void UninitCounters()
		{
			if (instance == null) return;

			fpsCounter.Uninit();
			memoryCounter.Uninit();
			deviceInfoCounter.Uninit();
		}

		private void RefreshForcedFrameRate()
		{
			RefreshForcedFrameRate(false);
		}

		private void RefreshForcedFrameRate(bool disabling)
		{
			if (forceFrameRate && !disabling)
			{
				if (cachedVSync == -1)
				{
					cachedVSync = QualitySettings.vSyncCount;
					cachedFrameRate = Application.targetFrameRate;
					QualitySettings.vSyncCount = 0;
				}
				
				Application.targetFrameRate = forcedFrameRate;
			}
			else
			{
				if (cachedVSync != -1)
				{
					QualitySettings.vSyncCount = cachedVSync;
					Application.targetFrameRate = cachedFrameRate;
					cachedVSync = -1;
				}
				
			}
		}

		private void RefreshHotKey()
		{
			if (hotKey != KeyCode.None)
			{
				if (hotKeyCoroutine == null)
				{
					hotKeyCoroutine = StartCoroutine("WaitForKeyDown", hotKey);
				}
				else
				{
					StopCoroutine("WaitForKeyDown");
					hotKeyCoroutine = StartCoroutine("WaitForKeyDown", hotKey);
				}
			}
			else
			{
				StopCoroutine("WaitForKeyDown");
				hotKeyCoroutine = null;
			}
		}

		private void OnLevelWasLoaded(int index)
		{
			if (!keepAlive)
			{
				Dispose();
			}
			else
			{
				if (fpsCounter.Enabled && fpsCounter.ShowAverage && fpsCounter.resetAverageOnNewScene)
				{
					fpsCounter.ResetAverage();
				}
			}
		}

		private void OnEnable()
		{
			InitCounters();
			Invoke("RefreshForcedFrameRate", 0.5f);
		}

		private void OnDisable()
		{
			UninitCounters();
			if (IsInvoking("RefreshForcedFrameRate")) CancelInvoke("RefreshForcedFrameRate");
			RefreshForcedFrameRate(true);

			for (int i = 0; i < anchorsCount; i++)
			{
				labels[i].Clear();
			}
		}

		private void OnDestroy()
		{
			if (labels != null)
			{
				for (int i = 0; i < anchorsCount; i++)
				{
					labels[i].Dispose();
				}

				Array.Clear(labels, 0, anchorsCount);
				labels = null;
			}
		}

		/// <summary>
		/// For internal usage!
		/// </summary>
		internal static string Color32ToHex(Color32 color)
		{
			return color.r.ToString("x2") + color.g.ToString("x2") + color.b.ToString("x2") + color.a.ToString("x2");
		}
	}
}