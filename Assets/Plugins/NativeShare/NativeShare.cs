using UnityEngine;
#if UNITY_ANDROID || UNITY_IOS
using NativeShareNamespace;
#endif

public class NativeShare
{
	public enum ShareResult { Unknown = 0, Shared = 1, NotShared = 2 };

	public delegate void ShareResultCallback( ShareResult result, string shareTarget );

#if !UNITY_EDITOR && UNITY_IOS
	[System.Runtime.InteropServices.DllImport( "__Internal" )]
	private static extern void _NativeShare_Share( string[] files, int filesCount, string subject, string text, string link );
#endif

	private string text = string.Empty;
	private string title = string.Empty;

	private ShareResultCallback callback;

	public NativeShare SetText( string text )
	{
		this.text = text ?? string.Empty;
		return this;
	}

	public NativeShare SetTitle( string title )
	{
		this.title = title ?? string.Empty;
		return this;
	}

	public NativeShare SetCallback( ShareResultCallback callback )
	{
		this.callback = callback;
		return this;
	}

	public void Share()
	{
		if( text.Length == 0 )
		{
			Debug.LogWarning( "Share Error: attempting to share nothing!" );
			return;
		}
		
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
			"Can you beat my score?");
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
			intentObject, "Share your new score");
		currentActivity.Call("startActivity", chooser);

/*
#if UNITY_EDITOR
		Debug.Log( "Shared!" );

		if( callback != null )
			callback( ShareResult.Shared, null );
#elif UNITY_IOS
		NSShareResultCallbackiOS.Initialize( callback );

		_NativeShare_Share(new string[0], 0, title, text, "");
#else
		Debug.LogWarning( "NativeShare is not supported on this platform!" );
#endif*/
	}
}