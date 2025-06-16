using UnityEngine;
using EcxUtilities;

// TODO: ADD MORE FUN STUFF TO TIMER DEMO INCLUDING BUTTONS, 3 SECOND COUNTDOWN TIMER, ETC
public class TimerDemo : MonoBehaviour {
	Timer repeatingTimerCount, repeatingTimer;
	private int completeCount;


	private void Start() {
		repeatingTimerCount = Timer.Create(5, PrintStats, null, true);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.O)) {
			Timer.Create(3, OneShotDone);
			Debug.Log("One shot timer created (3 sec)");
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			// Need to make sure repeating timer isn't already created or we will lose orig ref and it will repeat forever, creating memory leak.
			if (repeatingTimer == null)
				repeatingTimer = Timer.Create(2, RepeatingDone, null, true);
			else
				repeatingTimer.Restart();
			Debug.Log("Repeating timer created (2 sec)");
		}

		if (Input.GetKeyDown(KeyCode.D)) {
			repeatingTimer.Dispose();
			Debug.Log("Repeating timer was disposed.");
		}

		if (Input.GetKeyDown(KeyCode.L)) {
			Timer.Create(0.5f, () => completeCount++);
			Debug.Log("Timer with lambda increment function created (0.5 sec)");
		}

		if (Input.GetKeyDown(KeyCode.S))
			PrintStats();
	}

	private void OneShotDone() {
		completeCount += 1;
		Debug.Log($"OneShot Timer is done!  Total completed: {completeCount}");
	}

	private void RepeatingDone() {
		completeCount += 1;
		Debug.Log($"Repeating Timer is done!  Total completed: {completeCount}");
	}
	
	private void PrintStats() =>
		Debug.Log($"** Current timer count is: {TimerManager.Instance.timerCount}.  Completion count is: {completeCount}");
}