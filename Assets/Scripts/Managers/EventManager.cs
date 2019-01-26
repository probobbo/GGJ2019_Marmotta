using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
	public class EventManager : MonoBehaviour
	{
		public class StateChangedEvent : UnityEvent<GameManager.PlayingState> { }
		public class ButtonPressed : UnityEvent<InputManager.ControllerButtons> { }
		public class QuickTimeSuccess : UnityEvent<bool> { }
		public class QuickTimeEventStart : UnityEvent<float> { }

		public static EventManager Instance;

		public UnityEvent OnGameStarted;
		public UnityEvent OnPause;
		public UnityEvent OnUnpause;
		public QuickTimeSuccess OnQuickTimeSuccess;
		public StateChangedEvent OnPlayingStateChanged;
		public ButtonPressed OnButtonPressed;
		public QuickTimeEventStart OnQuickTimeEventStart;

		private void Awake()
		{
			if (!Instance)
			{
				Instance = this;

				OnGameStarted = new UnityEvent();
				OnPause = new UnityEvent();
				OnUnpause = new UnityEvent();
				OnQuickTimeSuccess = new QuickTimeSuccess();
				OnPlayingStateChanged = new StateChangedEvent();
				OnButtonPressed = new ButtonPressed();
				OnQuickTimeEventStart = new QuickTimeEventStart();
			}
			else if (Instance != this)
				Destroy(gameObject);

			DontDestroyOnLoad(this);
		}

	}
}
