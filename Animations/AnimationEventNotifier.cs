using System;

using UnityEngine;

namespace Utilities.Animations
{
	/// <summary>
	/// A generic way of receiving animation events from <see cref="Animation"/> clips.
	/// Listeners can then subscribe to this component's event and respond as needed.
	/// </summary>
	public class AnimationEventNotifier : MonoBehaviour
	{
		#region EVENTS
		public event Action onEventReceived = delegate { };
		public event Action<int> onIntEventReceived = delegate { };
		public event Action<float> onFloatEventReceived = delegate { };
		public event Action<string> onStringEventReceived = delegate { };
		#endregion


		#region PUBLIC API
		public void ReceiveEvent()
		{
			onEventReceived();
		}

		public void ReceiveEventWithInt(int i)
		{
			onIntEventReceived(i);
		}

		public void ReceiveEventWithFloat(float f)
		{
			onFloatEventReceived(f);
		}

		public void ReceiveEventWithString(string s)
		{
			onStringEventReceived(s);
		}
		#endregion
	}
}
