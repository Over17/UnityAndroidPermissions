using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsageExample : MonoBehaviour {

    private const string STORAGE_PERMISSION = "android.permission.READ_EXTERNAL_STORAGE";


    //Function which called first, by UI button, for example click on Avatar to change it from device gallery
    public void OnBrowseGalleryButtonPress()
    {
        if (!CheckPermissions())
        {
            Debug.Log("Missing permission to browse device gallery, please grant permission first");

            //Your code to show in-game pop-up with explanation why do you need this permission (required for Google Featuring program)
            //this pop-up should include a button "Grant Access" which will be linked to the function "OnGrantButtonPress" below
            return;
        }

        //Your code to browse Android Gallery
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
                //Permission was successfully granted, restart the change avatar routine
                OnBrowseGalleryButtonPress();
            },
            deniedPermission =>
            {
                //Permission was not granted
                //Show in-game pop-up message stating that user can change permissions in Android Application Settings if he would change his mind (Also required by Google Featuring programm)
            }));
    }
}
