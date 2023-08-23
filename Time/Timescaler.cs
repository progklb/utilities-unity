using UnityEngine;

public class Timescaler : MonoBehaviour
{
	void Update()
	{
		// TODO Make this an editor util with a slider?


		if (Input.GetKey(KeyCode.Backspace))
		{
			Time.timeScale = 0.1f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}
}
