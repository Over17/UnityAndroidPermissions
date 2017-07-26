using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsageExample : MonoBehaviour {

	private const string STORAGE_PERMISSION = "android.permission.READ_EXTERNAL_STORAGE";


	//Function which callled first, by UI button, for example click on Avatar to change it from device gallery
	public void OnBrowsGalleryButtonPress()
	{
		if (!CheckPermissions())
		{
			Debug.Log("Missing permission to brows device gallery, please grant permission first");

			//Your code to show in-game pop-up with explanation why do you need this permission (required for Google Featuring programm)
			//this pop-up should include button "Grant Access" which will be linked to the function "OnGrantButtonPress" bellow
			return;
		}

		//Your code to brows Android Gallery
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
				OnBrowsGalleryButtonPress();
			},
			deniedPermission =>
			{
				//Permission was not granted
				//Show in-game pop-up message stating that user can change permissions in Android Application Settings if he would change his mind (Also required by Google Featuring programm)
			}));
	}
}
