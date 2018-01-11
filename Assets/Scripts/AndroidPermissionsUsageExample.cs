using System.Collections;
using UnityEngine;

public class AndroidPermissionsUsageExample : MonoBehaviour
{
    private const string STORAGE_PERMISSION = "android.permission.READ_EXTERNAL_STORAGE";

    // Function to be called first (by UI button)
    // For example, click on Avatar to change it from the device gallery
    public void OnBrowseGalleryButtonPress()
    {
        if (!CheckPermissions())
        {
            Debug.LogWarning("Missing permission to browse device gallery, please grant the permission first");

            // Your code to show in-game pop-up with the explanation why you need this permission (required for Google Featuring program)
            // This pop-up should include a button "Grant Access" linked to the function "OnGrantButtonPress" below
            return;
        }

        // Your code to browse Android Gallery
        Debug.Log("Browsing Gallery...");
    }

    private bool CheckPermissions()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            return true;
        }

        return AndroidPermissionsManager.IsPermissionGranted(STORAGE_PERMISSION);
    }

    public void OnGrantButtonPress()
    {
        AndroidPermissionsManager.RequestPermission(new []{STORAGE_PERMISSION}, new AndroidPermissionCallback(
            grantedPermission =>
            {
                // The permission was successfully granted, restart the change avatar routine
                OnBrowseGalleryButtonPress();
            },
            deniedPermission =>
            {
                // The permission was denied.
                // Show in-game pop-up message stating that the user can change permissions in Android Application Settings
                // if he changes his mind (also required by Google Featuring program)
            }));
    }
}
