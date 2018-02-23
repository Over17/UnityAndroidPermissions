package com.unity3d.plugin;

import android.app.Fragment;
import android.app.FragmentTransaction;
import android.content.pm.PackageManager;
import android.os.Bundle;

public class PermissionFragment extends Fragment
{
    public static final String PERMISSION_NAMES = "PermissionNames";

    private static final int PERMISSIONS_REQUEST_CODE = 15887;

    private final UnityAndroidPermissions.IPermissionRequestResult m_ResultCallbacks;

    public PermissionFragment()
    {
        m_ResultCallbacks = null;
    }

    public PermissionFragment(final UnityAndroidPermissions.IPermissionRequestResult resultCallbacks)
    {
        m_ResultCallbacks = resultCallbacks;
    }

    @Override public void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        if (m_ResultCallbacks == null)
        {
            getFragmentManager().beginTransaction().remove(this).commit();
        }
        else
        {
            String[] permissionNames = getArguments().getStringArray(PERMISSION_NAMES);
            requestPermissions(permissionNames, PERMISSIONS_REQUEST_CODE);
        }
    }

    @Override public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] grantResults)
    {
        if (requestCode != PERMISSIONS_REQUEST_CODE)
            return;

        for (int i = 0; i < permissions.length && i < grantResults.length; ++i)
        {
            if (grantResults[i] == PackageManager.PERMISSION_GRANTED)
                m_ResultCallbacks.OnPermissionGranted(permissions[i]);
            else
                m_ResultCallbacks.OnPermissionDenied(permissions[i]);
        }

        FragmentTransaction fragmentTransaction = getFragmentManager().beginTransaction();
        fragmentTransaction.remove(this);
        fragmentTransaction.commit();
    }
}
