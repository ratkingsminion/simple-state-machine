namespace RatKing.SSM {

	public class StateFunctions<TState> {
		public TState name;
		public System.Action<TState> onStart; // parameter is the previous state (prevState)
		public System.Action<float> onUpdate; // parameter is delta time
		public System.Action<TState> onStop; // parameter is the next state (nextState)
		public object userData;
		
		public StateFunctions<TState> OnStart(System.Action<TState> onStart) { this.onStart = onStart; return this; }
		public StateFunctions<TState> OnUpdate(System.Action<float> onUpdate) { this.onUpdate = onUpdate; return this; }
		public StateFunctions<TState> OnStop(System.Action<TState> onStop) { this.onStop = onStop; return this; }
		public StateFunctions<TState> UserData(object userData) { this.userData = userData; return this; }
	}

	// with target
	public class StateFunctions<TTarget, TState> {
		public TState name;
		public System.Action<TTarget, TState> onStart; // 2nd parameter is the previous state (prevState)
		public System.Action<TTarget, float> onUpdate; // parameter is delta time
		public System.Action<TTarget, TState> onStop; // 2nd parameter is the next state (nextState)
		public object userData;
		
		public StateFunctions<TTarget, TState> OnStart(System.Action<TTarget, TState> onStart) { this.onStart = onStart; return this; }
		public StateFunctions<TTarget, TState> OnUpdate(System.Action<TTarget, float> onUpdate) { this.onUpdate = onUpdate; return this; }
		public StateFunctions<TTarget, TState> OnStop(System.Action<TTarget, TState> onStop) { this.onStop = onStop; return this; }
		public StateFunctions<TTarget, TState> UserData(object userData) { this.userData = userData; return this; }
	}

}
