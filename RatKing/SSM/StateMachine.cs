using System.Collections.Generic;

namespace RatKing.SSM {

	public class StateMachine<TState> {
		protected Dictionary<TState, StateFunctions<TState>> states = new Dictionary<TState, StateFunctions<TState>>();
		static readonly StateFunctions<TState> stateNone = new StateFunctions<TState>() { name = default };
		public StateFunctions<TState> CurState { get; private set; } = stateNone;
		public event System.Action<TState, TState> OnStateChange = null; // prevState, nextState
		event System.Action<string> LogError;

		//

#if UNITY_5_3_OR_NEWER
		public StateMachine() {
			LogError = UnityEngine.Debug.LogError;
		}
#endif

		public StateMachine(System.Action<string> logError) {
			LogError = logError;
		}

		public StateFunctions<TState> AddState(TState name, System.Action<TState> onStart = null, System.Action<float> onUpdate = null, System.Action<TState> onStop = null) {
			return AddState(name, new StateFunctions<TState>() { onStart = onStart, onUpdate = onUpdate, onStop = onStop });
		}

		public StateFunctions<TState> AddState(TState name, StateFunctions<TState> stateFunctions) {
			if (name.Equals(default(TState))) { LogError("The default state must be empty!"); return null; }
			stateFunctions.name = name;
			states.Add(name, stateFunctions);
			return stateFunctions;
		}

		public void Update(float dt) {
			CurState.onUpdate?.Invoke(dt);
		}

		public bool ClearState() {
			return SetState(default(TState));
		}

		public bool SetState(TState name) {
			if (Equals(name, CurState.name)) { return false; }
			StateFunctions<TState> nextState;
			if (Equals(name, default(TState))) { nextState = stateNone; }
			else if (!states.TryGetValue(name, out nextState)) { LogError("State " + name + " is not defined!"); return false; }
			CurState.onStop?.Invoke(name);
			var prevStateName = CurState.name;
			CurState = nextState;
			OnStateChange?.Invoke(prevStateName, name);
			nextState.onStart?.Invoke(prevStateName);
			return true;
		}

		public bool SetState(TState name, out StateFunctions<TState> nextState) {
			if (Equals(name, CurState.name)) { nextState = null; return false; }
			if (Equals(name, default(TState))) { nextState = stateNone; }
			else if (!states.TryGetValue(name, out nextState)) { LogError("State " + name + " is not defined!"); return false; }
			CurState.onStop?.Invoke(name);
			var prevStateName = CurState.name;
			CurState = nextState;
			OnStateChange?.Invoke(prevStateName, name);
			nextState.onStart?.Invoke(prevStateName);
			return true;
		}

		public bool TryGetState(TState name, out StateFunctions<TState> state) {
			return states.TryGetValue(name, out state);
		}

		public StateFunctions<TState> GetState(TState name) {
			return states[name];
		}
	}

	public class StateMachine<TTarget, TState> {
		readonly TTarget target;
		protected readonly Dictionary<TState, StateFunctions<TTarget, TState>> states = new Dictionary<TState, StateFunctions<TTarget, TState>>();
		static readonly StateFunctions<TTarget, TState> stateNone = new StateFunctions<TTarget, TState>() { name = default };
		public StateFunctions<TTarget, TState> CurState { get; private set; } = stateNone;
		public event System.Action<TTarget, TState, TState> OnStateChange = null; // prevState, nextState
		event System.Action<string> LogError;

		//

#if UNITY_5_3_OR_NEWER
		public StateMachine(TTarget target) {
			this.target = target;
			LogError = UnityEngine.Debug.LogError;
		}
#endif

		public StateMachine(TTarget target, System.Action<string> logError) {
			this.target = target;
			LogError = logError;
		}

		public StateFunctions<TTarget, TState> AddState(TState name, System.Action<TTarget, TState> onStart = null, System.Action<TTarget, float> onUpdate = null, System.Action<TTarget, TState> onStop = null) {
			return AddState(name, new StateFunctions<TTarget, TState>() { onStart = onStart, onUpdate = onUpdate, onStop = onStop });
		}

		public StateFunctions<TTarget, TState> AddState(TState name, StateFunctions<TTarget, TState> stateFunctions) {
			if (name.Equals(default(TState))) { LogError("The default state must be empty!"); return null; }
			stateFunctions.name = name;
			states.Add(name, stateFunctions);
			return stateFunctions;
		}

		public void Update(float dt) {
			CurState.onUpdate?.Invoke(target, dt);
		}

		public bool ClearState() {
			return SetState(default(TState));
		}

		public bool SetState(TState name) {
			if (Equals(name, CurState.name)) { return false; }
			StateFunctions<TTarget, TState> nextState;
			if (Equals(name, default(TState))) { nextState = stateNone; }
			else if (!states.TryGetValue(name, out nextState)) { LogError("State " + name + " is not defined!"); return false; }
			CurState.onStop?.Invoke(target, name);
			var prevStateName = CurState.name;
			CurState = nextState;
			OnStateChange?.Invoke(target, prevStateName, name);
			nextState.onStart?.Invoke(target, prevStateName);
			return true;
		}

		public bool SetState(TState name, out StateFunctions<TTarget, TState> nextState) {
			if (Equals(name, CurState.name)) { nextState = null; return false; }
			if (Equals(name, default(TState))) { nextState = stateNone; }
			else if (!states.TryGetValue(name, out nextState)) { LogError("State " + name + " is not defined!"); return false; }
			CurState.onStop?.Invoke(target, name);
			var prevStateName = CurState.name;
			CurState = nextState;
			OnStateChange?.Invoke(target, prevStateName, name);
			nextState.onStart?.Invoke(target, prevStateName);
			return true;
		}

		public bool TryGetState(TState name, out StateFunctions<TTarget, TState> state) {
			return states.TryGetValue(name, out state);
		}

		public StateFunctions<TTarget, TState> GetState(TState name) {
			return states[name];
		}
	}
}
