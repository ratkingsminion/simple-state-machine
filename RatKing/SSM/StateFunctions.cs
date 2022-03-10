namespace RatKing.SSM {

	public class StateFunctions<TState> {
		public TState name;
		public System.Action<TState> start; // parameter is the previous state (prevState)
		public System.Action<float> update; // parameter is delta time
		public System.Action<TState> stop; // parameter is the next state (nextState)
		public object userData;
		
		public StateFunctions<TState> OnStart(System.Action<TState> onStart) { this.start = onStart; return this; }
		public StateFunctions<TState> OnUpdate(System.Action<float> onUpdate) { this.update = onUpdate; return this; }
		public StateFunctions<TState> OnStop(System.Action<TState> onStop) { this.stop = onStop; return this; }
		public StateFunctions<TState> UserData(object userData) { this.userData = userData; return this; }
	}

	// with target
	public class StateFunctions<TTarget, TState> {
		public TState name;
		public System.Action<TTarget, TState> start; // 2nd parameter is the previous state (prevState)
		public System.Action<TTarget, float> update; // parameter is delta time
		public System.Action<TTarget, TState> stop; // 2nd parameter is the next state (nextState)
		public object userData;
		
		public StateFunctions<TTarget, TState> OnStart(System.Action<TTarget, TState> start) { this.start = start; return this; }
		public StateFunctions<TTarget, TState> OnUpdate(System.Action<TTarget, float> update) { this.update = update; return this; }
		public StateFunctions<TTarget, TState> OnStop(System.Action<TTarget, TState> stop) { this.stop = stop; return this; }
		public StateFunctions<TTarget, TState> UserData(object userData) { this.userData = userData; return this; }
	}

}
