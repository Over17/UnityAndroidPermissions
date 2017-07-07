package com.unity3d.player;

import android.app.Activity;
import android.app.Fragment;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.content.pm.PackageManager;
import android.os.Build;
import android.os.Bundle;

public class UnityAndroidPermissions
{
    private static final int PERMISSIONS_REQUEST_CODE = 15887;

    interface IPermissionRequestResult
    {
        void OnPermissionGranted(String permissionName);
        void OnPermissionDenied(String permissionName);
    }

    public boolean IsPermissionGranted (Activity activity, String permissionName)
    {
        if (Build.VERSION.SDK_INT < Build.VERSION_CODES.M)
            return true;
        if (activity == null)
            return false;
        return activity.checkSelfPermission(permissionName) == PackageManager.PERMISSION_GRANTED;
    }

    public void RequestPermissionAsync(Activity activity, final String[] permissionNames, final IPermissionRequestResult resultCallbacks)
    {
        if (Build.VERSION.SDK_INT < Build.VERSION_CODES.M)
            return;
        if (activity == null || permissionNames == null || resultCallbacks == null)
            return;

        final FragmentManager fragmentManager = activity.getFragmentManager();
        final Fragment request = new Fragment()
        {
            @Override public void onCreate(Bundle savedInstanceState)
            {
                super.onCreate(savedInstanceState);
                requestPermissions(permissionNames, PERMISSIONS_REQUEST_CODE);
            }

            @Override public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] grantResults)
            {
                if (requestCode != PERMISSIONS_REQUEST_CODE)
                    return;

                for (int i = 0; i < permissions.length && i < grantResults.length; ++i)
                {
                    if (grantResults[i] == PackageManager.PERMISSION_GRANTED)
                        resultCallbacks.OnPermissionGranted(permissions[i]);
                    else
                        resultCallbacks.OnPermissionDenied(permissions[i]);
                }

                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
                fragmentTransaction.remove(this);
                fragmentTransaction.commit();
            }
        };

        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.add(0, request);
        fragmentTransaction.commit();
    }
}
