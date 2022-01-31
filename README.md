# ssm
Simple state-machine for C#, usable with Unity

Usage (Unity):

```C#
  [SerializeField] Transform someObject;
  RatKing.SSM.StateMachine<string> ssmTest;

  void Start() {
    ssmTest = new RatKing.SSM.StateMachine<string>(Debug.LogError);

    var timer = 0f;
    
    ssmTest.AddState("A", new RatKing.SSM.StateFunctions<string>() {
      onStart = prevState => {
        if (prevState == default) { Debug.Log("begin"); }
        else { Debug.Log("switched state to A"); }
      },
      onUpdate = dt => {
        timer -= dt * 2f;
        someObject.position = new Vector3(0f, Mathf.Sin(timer), 0f);
      }
    });
    
    ssmTest.AddState("B", 
      oldState => { // onStart
        Debug.Log("switched state to B");
      },
      dt => { // onUpdate
        timer += dt * 2f;
        someObject.position = new Vector3(0f, 2f * Mathf.Sin(timer), 0f);
      }
    );
    
    ssmTest.SetState("A");
  }

  void Update() {
    ssmTest.Update(Time.smoothDeltaTime);
    if (Input.GetKeyDown(KeyCode.T)) {
      ssmTest.SetState(ssmTest.CurState.name == "A" ? "B" : "A");
    }
  }
```
