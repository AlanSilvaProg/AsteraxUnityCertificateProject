using UnityEngine;
using System;
#if UNITY_IPHONE
using System.Runtime.InteropServices;
#endif

public class NativeShare : MonoBehaviour
{
	private Action callback;

	public void Share(string textToShare, Action appRetakeCallback = null)
	{
		callback = appRetakeCallback;
		if (textToShare.Length == 0)
		{
			Debug.LogWarning("Share Error: attempting to share nothing!");
			return;
		}

#if UNITY_ANDROID
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		intentObject.Call<AndroidJavaObject>("setType", "text/plain");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), textToShare);

		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("startActivity", intentObject);
#elif UNITY_IOS
		GeneralSharingiOSBridge.ShareSimpleText(text);
#else
				Debug.LogWarning( "NativeShare is not supported on this platform!" );
#endif
	}

	private void OnApplicationFocus(bool focus)
    {
		if (focus)
        {
            callback?.Invoke();
			callback = null;

		}
    }

}

#if UNITY_IPHONE
public class GeneralSharingiOSBridge
{		
	[DllImport("__Internal")]
	private static extern void _TAG_ShareSimpleText (string message);
			
	public static void ShareSimpleText (string message)
	{
		_TAG_ShareSimpleText (message);
	}
}
#endif