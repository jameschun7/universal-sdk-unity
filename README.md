# Unity SDK

SDK is Login, InAppPurchase, Push, etc in Unity3d.

  * [Overview](#overview)
  * [Features](#features)
  * [Installation](#installation)
  * [Setup](#setup)
  * [Example](#example)
    + [SetupSDK](#setupsdk)
    + [Login](#login)
    + [Purchase](#purchase)
    + [Push](#push)
    + [ImageBanner](#imagebanner)
    + [Fast WebBrowser](#fast-webbrowser)


## Overview
- Login
- Purchase
- Push
- ImageBanner
- Fast WebBrowser

## Features
#### Android
- Google Login Version
- Facebook Version
- Apple Login Support
- Jcenter
- AndroidX
- Chrome Custom Tabs

#### iOS
- Google Login Version
- Facebook Version
- Cocoapods
- Capability Support
- Safari View Controller

## Installation

#### Download Page

To get the latest Universal SDK for Unity, download the `.unitypackage` file from our [Downloads](https://github.com/jameschun7/simple-firebase-sdk-unity).

#### Import into your project

With your Unity project open, double-click on the downloaded `.unitypackage` file. Import everything in the package, as seen here:

![import unity package]()

#### Add UniversalSDK prefab to your scene

After importing the package, in your **Project** panel, you'll find a **UniversalSDK** prefab under `Assets/UniversalSDK/`. Drag it to the **Hierarchy** panel of the scene to which you want to add Login:

![add prefab]()



## Setup
### Android
##### Build Settings > Player Settings > Publishing Settings

* ##### mainTemplate.gradle

```
dependencies {
    implementation fileTree(dir: 'libs', include: ['*.jar'])

    implementation 'com.universal.sdk:universalsdk:0.2.2'

    implementation 'com.google.code.gson:gson:2.8.5'
    implementation "org.jetbrains.kotlin:kotlin-stdlib-jdk7:1.3.11"

**DEPS**}
```

* ##### launcherTemplate.gradle

```
dependencies {
    implementation project(':unityLibrary')
    implementation 'androidx.multidex:multidex:2.0.1'//added
    }

android {
    compileSdkVersion **APIVERSION**
    buildToolsVersion '**BUILDTOOLS**'

    compileOptions {
        sourceCompatibility JavaVersion.VERSION_1_8
        targetCompatibility JavaVersion.VERSION_1_8
    }

    defaultConfig {
        minSdkVersion **MINSDKVERSION**
        targetSdkVersion **TARGETSDKVERSION**
        applicationId '**APPLICATIONID**'
        ndk {
            abiFilters **ABIFILTERS**
        }
        versionCode **VERSIONCODE**
        versionName '**VERSIONNAME**'

        multiDexEnabled true //added

        resValue("string", "facebook_app_id", "string") //added
        resValue("string", "google_web_client_id", "string") //added
    }
    ...
```

* ##### gradleTemplate.properties

```
android.useAndroidX=true
android.enableJetifier=true
```

* ##### baseProjectTemplate.gradle

```
allprojects {
    buildscript {
        repositories {**ARTIFACTORYREPOSITORY**
            google()
            jcenter()
            maven {
                url "https://dl.bintray.com/james-chun-dev/universal-sdk"
            }
        }

        dependencies {            
            classpath 'com.android.tools.build:gradle:3.4.0'
            classpath "org.jetbrains.kotlin:kotlin-gradle-plugin:1.3.11"
            classpath 'com.google.gms:google-services:4.3.0'
            **BUILD_SCRIPT_DEPS**
        }
    }

    repositories {**ARTIFACTORYREPOSITORY**
        google()
        jcenter()
        maven {
            url "https://dl.bintray.com/james-chun-dev/universal-sdk"
        }
        flatDir {
            dirs "${project(':unityLibrary').projectDir}/libs"
        }
    }
}

task clean(type: Delete) {
    delete rootProject.buildDir
}
```

#### Firebase 안드로이드 앱 설정

* 프로젝트 생성 및 Android 앱 등록
* SHA 인증서 지문 등록
* google-services.json 다운로드(Firebase 로그인 설정 후 다운로드 해주세요.)

##### google-services.json 파일 변환

1. google-services.json 파일을 xml 형태로 변환합니다. 파일 변환은 [Convert google-services.json to values XML](https://dandar3.github.io/android/google-services-json-to-xml.html) 에서 지원합니다.
2. 변환된 google-services.xml 파일은 `Assets/Plugins/Android/res/values`  에 복사합니다.

### iOS

Preferences > Universal SDK 탭을 통해서 설정.
- Facebook App ID 에 값을 입력합니다.
- 애플로그인을 활성화시키면 유니티 빌드시 Capability 설정까지 자동으로 설정됩니다.

![sdk settings]()

## Example

#### SetupSDK

위 설정이 완료되면 실행시 자동으로 SDK 초기화가 진행됩니다.

```
public class UniversalSDK : MonoBehaviour
{
    static UniversalSDK instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SetupSDK();
    }

    public static UniversalSDK Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("UniversalSDK");
                instance = go.AddComponent<UniversalSDK>();
            }
            return instance;
        }
    }

    void SetupSDK()
    {
        NativeInterface.SetupSDK(Guid.NewGuid().ToString());
    }
    ...
```

#### Login

Now, you can implement Login in the scene where the UniversalSDK (GameObject) exists. For example:

```c#
public class MainController : MonoBehaviour
{    
    public void OnClickExampleLogin()
    {        
        UniversalSDK.Instance.Login(LoginType, result =>
        {
            result.Match(
                value => {
                    //Debug.Log("Login OK. User display name : " + value.UserProfile.DisplayName);
                },
                error => {
                    //Debug.Log("Login failed : " + error.Message);
                });
        });
    }
    ...
```

#### Purchase

#### Push

#### ImageBanner

#### Fast WebBrowser