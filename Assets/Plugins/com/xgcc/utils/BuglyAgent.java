package com.xgcc.utils;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.util.Log;

import com.tencent.bugly.crashreport.CrashReport;
import com.tencent.bugly.crashreport.CrashReport.UserStrategy;

import com.unity3d.player.UnityPlayer;

import java.util.LinkedHashMap;
import java.util.Map;

public class BuglyAgent
{
    private static final String TAG = "BuglyAgent";
    @SuppressLint("StaticFieldLeak")
    public static Activity mainActivity = UnityPlayer.currentActivity;

    private static final UserStrategy s_userStrategy = new UserStrategy(getCurrentContext());

    public static Context getCurrentContext() {
        return mainActivity.getApplicationContext();
    }

    public static void setSdkPackageName(String packageName) {
        s_userStrategy.setAppPackageName(packageName);
    }

    public static void setUserId(String userId) {
        CrashReport.setUserId(userId);
    }

    public static void setUserSceneTag(int sceneId) {
        CrashReport.setUserSceneTag(getCurrentContext(), sceneId);
    }

    public static void setSdkConfig(String key, String value) {
        CrashReport.setSdkExtraData(getCurrentContext(), key, value);
    }

    public static void putUserData(String key, String value) {
        CrashReport.putUserData(getCurrentContext(), key, value);
    }

    public static void printLog(String message) {
        Log.d(TAG, message);
    }

    public static void postException(int crashType, String name, String reason,
                                     String stackTrace, boolean quitProgram) {
        CrashReport.postException(crashType, name, reason, stackTrace, null);
    }

    public static void initCrashReport(String appId, String channel, String version,
                                       long delayTime, boolean isDebug) {
        s_userStrategy.setAppChannel(channel);
        s_userStrategy.setAppVersion(version);
        s_userStrategy.setAppReportDelay(delayTime);
        /*
        s_userStrategy.setCrashHandleCallback(new CrashReport.CrashHandleCallback() {
            public Map<String, String> onCrashHandleStart(int crashType, String errorType,
                                                          String errorMessage, String errorStack) {
                LinkedHashMap<String, String> map = new LinkedHashMap<String, String>();
                map.put("Key", "Value");
                return map;
            }

            @Override
            public byte[] onCrashHandleStart2GetExtraDatas(int crashType, String errorType,
                                                           String errorMessage, String errorStack) {
                try {
                    return "Extra data.".getBytes("UTF-8");
                } catch (Exception e) {
                    return null;
                }
            }

        });
        */
        CrashReport.initCrashReport(getCurrentContext(), appId, isDebug, s_userStrategy);
    }
}
