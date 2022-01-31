namespace RatKing.SSM {

	public class StateFunctions<TState> {
		public TState name;
		public System.Action<TState> onStart; // parameter is the previous state (prevState)
		public System.Action<float> onUpdate; // parameter is delta time
		public System.Action<TState> onStop; // parameter is the next state (nextState)
		public object tempData;
	}

	// with target
	public class StateFunctions<TTarget, TState> {
		public TState name;
		public System.Action<TTarget, TState> onStart; // 2nd parameter is the previous state (prevState)
		public System.Action<TTarget, float> onUpdate; // parameter is delta time
		public System.Action<TTarget, TState> onStop; // 2nd parameter is the next state (nextState)
		public object tempData;
	}

}
