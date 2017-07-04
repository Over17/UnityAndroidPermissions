using UnityEngine;

public class AndroidPermissionCallback : AndroidJavaProxy
{
    public AndroidPermissionCallback() : base("com.unity3d.player.UnityAndroidPermissions$IPermissionRequestResult") { }

    // Handle permission granted
    public virtual void OnPermissionGranted(string permissionName)
    {
        //Debug.Log("Permission " + permissionName + " GRANTED");
    }

    // Handle permission denied
    public virtual void OnPermissionDenied(string permissionName)
    {
        //Debug.Log("Permission " + permissionName + " DENIED!");
    }
}

public class AndroidPermissionsManager
{
    private static AndroidJavaObject m_Activity;
    private static AndroidJavaObject m_PermissionService;

    private static AndroidJavaObject GetActivity()
    {
        if (m_Activity == null)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            m_Activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
        return m_Activity;
    }

    private static AndroidJavaObject GetPermissionsService()
    {
        if (m_PermissionService == null)
        {
            m_PermissionService = new AndroidJavaObject("com.unity3d.player.UnityAndroidPermissions");
        }
        return m_PermissionService;
    }


    public static bool IsPermissionGranted(string permissionName)
    {
        return GetPermissionsService().Call<bool>("IsPermissionGranted", GetActivity(), permissionName);
    }

    public static void RequestPermission(string[] permissionNames, AndroidPermissionCallback callback)
    {
        GetPermissionsService().Call("RequestPermissionAsync", GetActivity(), permissionNames, callback);
    }
}
