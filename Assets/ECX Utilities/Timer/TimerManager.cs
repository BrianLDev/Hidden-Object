/*
ECX UTILITY SCRIPTS
Timer Manager (Singleton)
Handles management of all timers including update ticks, AddTimer, RemoveTimer, PauseAllTimers, etc.
Will automatically instantiate itself whenever a timer is created (via Singleton class), no need to manually instantiate.
Last updated: Mar 16, 2024
*/

using System.Collections.Generic;
using UnityEngine;

namespace EcxUtilities {
	public class TimerManager : Singleton<TimerManager> {
		public List<Timer> timerList { get; private set; } = new List<Timer>();
		public int timerCount => timerList.Count;
		private Queue<Timer> toAdd = new Queue<Timer>();
		private Queue<Timer> toDestroy = new Queue<Timer>();


		private void Update() {
			// Handle adding all timers before ticks so it doesn't modify List while iterating.
			while (toAdd.Count > 0) {
				timerList.AddRange(toAdd);
				toAdd.Clear();
		}

			// Tick all timers
			foreach (Timer timer in timerList)
				timer?.Tick(Time.deltaTime, Time.unscaledDeltaTime);
			
			// Handle removing timers after Ticks are complete so it doesn't modify List while iterating.
			while (toDestroy.Count > 0)
				timerList.Remove(toDestroy.Dequeue());
		}

		public void AddTimer(Timer timer) {
			toAdd.Enqueue(timer);
		}

		public void RemoveTimer(Timer timer) =>
			toDestroy.Enqueue(timer);

		public void PauseAllTimers() {
			foreach (Timer timer in timerList)
				timer.Pause();
		}

		public void UnPauseAllTimers() {
			foreach (Timer timer in timerList)
				timer.UnPause();
		}

		public void CancelAllTimers() {
			foreach (Timer timer in timerList)
				timer.Cancel();
		}

		public void DisposeAll() {
			foreach (Timer timer in timerList)
				timer.Dispose();
		}

		public void DisposeAllRepeating() {
			foreach (Timer timer in timerList) {
				if (timer.isRepeating)
					timer.Dispose();
			}
		}
	}
}