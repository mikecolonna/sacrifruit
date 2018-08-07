using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionObject : Reversible {

	public abstract void ResetState ();

	public abstract bool isActivated ();
		
}
