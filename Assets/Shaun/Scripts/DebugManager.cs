using UnityEngine;


public static class DebugManager {

	private static		bool		__showClassDebugs		=	true;
	private static		bool		__showBroadcastDebugs	=	true;
	private static		bool		__showFunctionDebugs	=	true;
	private static		bool		__showTraceDebugs		=	true;
	

	// PUBLIC
	// ---------------------------------------------------------------------------------------------------------------------------------------------------

	public static void ShowDebugLog(string type, string copy, object parameter1 = null, object parameter2 = null) {
		if (__showClassDebugs && type == "class") Debug.Log("++++	"	+	copy	+	"\n");
		
		if (__showBroadcastDebugs && type == "broadcast") {
			if (parameter1 == null) {
				Debug.Log("/////		"	+	copy	+	"\n");
			} else if (parameter2 == null) {
				Debug.Log("/////		"	+	copy	+	":	"	+	parameter1	+	"\n");
			} else {
				Debug.Log("/////		"	+	copy	+	":	"	+	parameter1	+	"	:	"	+	parameter2	+	"\n");
			}
		}

		if (__showFunctionDebugs && type == "function") {
			if (parameter1 == null) {
				Debug.Log("-----		"	+	copy	+	"\n");
			} else if (parameter2 == null) {
				Debug.Log("-----		"	+	copy	+	":	"	+	parameter1	+	"\n");
			} else {
				Debug.Log("-----		"	+	copy	+	":	"	+	parameter1	+	"	:	"	+	parameter2	+	"\n");
			}
		}

		if (__showTraceDebugs && type == "trace") {
			if (parameter1 == null) {
				Debug.Log(": : : : :		"	+	copy	+	"\n");
			} else if (parameter2 == null) {
				Debug.Log(": : : : :		"	+	copy	+	":	"	+	parameter1	+	"\n");
			} else {
				Debug.Log(": : : : :		"	+	copy	+	":	"	+	parameter1	+	"	:	"	+	parameter2	+	"\n");
			}
		}
	}

	public static void ShowDebugError(string copy) {
		Debug.LogError("ERROR ERROR ERROR!	"	+	copy	+	"\n");
	}
	
	public static void ShowDebugWarning(string copy) {
		Debug.LogWarning("WARNING WARNING WARNING!	"	+	copy	+	"\n");
	}
	
	public static void Pause() {
		Debug.Break();
	}
	
	
	
	// PRIVATE
	// ---------------------------------------------------------------------------------------------------------------------------------------------------
}
