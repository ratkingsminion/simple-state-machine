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
		// this works with older Unity versions too, but afaik they don't have such definitions
		public StateMachine() {
			LogError = UnityEngine.Debug.LogError;
		}
#endif

		public StateMachine(System.Action<string> logError) {
			LogError = logError;
		}

		public StateFunctions<TState> AddState(TState name) {
			return AddState(name, new StateFunctions<TState>());
		}

		public StateFunctions<TState> AddState(TState name, System.Action<TState> start = null, System.Action<float> update = null, System.Action<TState> stop = null) {
			return AddState(name, new StateFunctions<TState>() { start = start, update = update, stop = stop });
		}

		public StateFunctions<TState> AddState(TState name, StateFunctions<TState> stateFunctions) {
			if (name.Equals(default(TState))) { LogError("The default state must be empty!"); return null; }
			stateFunctions.name = name;
			states.Add(name, stateFunctions);
			return stateFunctions;
		}

		public void Update(float dt) {
			CurState.update?.Invoke(dt);
		}

		public bool ClearState() {
			return SetState(default(TState));
		}

		public bool SetState(TState name) {
			if (Equals(name, CurState.name)) { return false; }
			StateFunctions<TState> nextState;
			if (Equals(name, default(TState))) { nextState = stateNone; }
			else if (!states.TryGetValue(name, out nextState)) { LogError("State " + name + " is not defined!"); return false; }
			CurState.stop?.Invoke(name);
			var prevStateName = CurState.name;
			CurState = nextState;
			OnStateChange?.Invoke(prevStateName, name);
			nextState.start?.Invoke(prevStateName);
			return true;
		}

		public bool SetState(StateFunctions<TState> nextState) {
			if (nextState == null) { nextState = stateNone; }
			if (nextState == CurState) { return false; }
			CurState.stop?.Invoke(nextState.name);
			var prevStateName = CurState.name;
			CurState = nextState;
			OnStateChange?.Invoke(prevStateName, nextState.name);
			nextState.start?.Invoke(prevStateName);
			return true;
		}

		public bool SetState(TState name, out StateFunctions<TState> nextState) {
			if (Equals(name, CurState.name)) { nextState = null; return false; }
			if (Equals(name, default(TState))) { nextState = stateNone; }
			else if (!states.TryGetValue(name, out nextState)) { LogError("State " + name + " is not defined!"); return false; }
			CurState.stop?.Invoke(name);
			var prevStateName = CurState.name;
			CurState = nextState;
			OnStateChange?.Invoke(prevStateName, name);
			nextState.start?.Invoke(prevStateName);
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

		public StateFunctions<TTarget, TState> AddState(TState name) {
			return AddState(name, new StateFunctions<TTarget, TState>());
		}

		public StateFunctions<TTarget, TState> AddState(TState name, System.Action<TTarget, TState> start = null, System.Action<TTarget, float> update = null, System.Action<TTarget, TState> stop = null) {
			return AddState(name, new StateFunctions<TTarget, TState>() { start = start, update = update, stop = stop });
		}

		public StateFunctions<TTarget, TState> AddState(TState name, StateFunctions<TTarget, TState> stateFunctions) {
			if (name.Equals(default(TState))) { LogError("The default state must be empty!"); return null; }
			stateFunctions.name = name;
			states.Add(name, stateFunctions);
			return stateFunctions;
		}

		public void Update(float dt) {
			CurState.update?.Invoke(target, dt);
		}

		public bool ClearState() {
			return SetState(default(TState));
		}

		public bool SetState(TState name) {
			if (Equals(name, CurState.name)) { return false; }
			StateFunctions<TTarget, TState> nextState;
			if (Equals(name, default(TState))) { nextState = stateNone; }
			else if (!states.TryGetValue(name, out nextState)) { LogError("State " + name + " is not defined!"); return false; }
			CurState.stop?.Invoke(target, name);
			var prevStateName = CurState.name;
			CurState = nextState;
			OnStateChange?.Invoke(target, prevStateName, name);
			nextState.start?.Invoke(target, prevStateName);
			return true;
		}

		public bool SetState(StateFunctions<TTarget, TState> nextState) {
			if (nextState == null) { nextState = stateNone; }
			if (nextState == CurState) { return false; }
			CurState.stop?.Invoke(target, nextState.name);
			var prevStateName = CurState.name;
			CurState = nextState;
			OnStateChange?.Invoke(target, prevStateName, nextState.name);
			nextState.start?.Invoke(target, prevStateName);
			return true;
		}

		public bool SetState(TState name, out StateFunctions<TTarget, TState> nextState) {
			if (Equals(name, CurState.name)) { nextState = null; return false; }
			if (Equals(name, default(TState))) { nextState = stateNone; }
			else if (!states.TryGetValue(name, out nextState)) { LogError("State " + name + " is not defined!"); return false; }
			CurState.stop?.Invoke(target, name);
			var prevStateName = CurState.name;
			CurState = nextState;
			OnStateChange?.Invoke(target, prevStateName, name);
			nextState.start?.Invoke(target, prevStateName);
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
