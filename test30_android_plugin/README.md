test30_android_plugin
====
UnityでAndroid向けプラグインを作ってみるサンプル。

Android側のServiceからUnityのメソッドを呼び出す際は、UnitySendMessageを使用している。


Android Studio
====
手元では、Android StudioでインストールするAndroid SDKは以下のディレクトリに配置するようにした。
    
    C:\local\android-sdk以下にインストールしておく。
    
    ※Windows環境では、デフォルトでC:\Users\ユーザ名\AppData\local\Android以下にインストールされる
    

作成したプロジェクトのgradle.propertiesで↓を指定しておく

    org.gradle.jvmargs=-Xmx512m -XX:MaxPermSize=512m

jarファイルの作り方
----
普通にアプリを作ったのち、File→New Moduleを選び、"Android Library"を作成

モジュール側のbuild.gradleは次のように記述

    apply plugin: 'com.android.library'
    
    android {
        compileSdkVersion 24
        buildToolsVersion "25.0.0"
    
        defaultConfig {
            minSdkVersion 23
            targetSdkVersion 24
            versionCode 1
            versionName "1.0"
    
            testInstrumentationRunner "android.support.test.runner.AndroidJUnitRunner"
    
        }
        buildTypes {
            release {
                minifyEnabled false
                proguardFiles getDefaultProguardFile('proguard-android.txt'), 'proguard-rules.pro'
            }
        }
    }
    
    dependencies {
        compile fileTree(dir: 'libs', include: ['*.jar'])
        androidTestCompile('com.android.support.test.espresso:espresso-core:2.2.2', {
            exclude group: 'com.android.support', module: 'support-annotations'
        })
        compile 'com.android.support:appcompat-v7:24.2.1'
        testCompile 'junit:junit:4.12'
    
        // ======== Unityのクラスをビルドするために、ここから追加 ========
        compile files('C:/Program Files/Unity/Editor/Data/PlaybackEngines/AndroidPlayer/Variations/il2cpp/Release/Classes/classes.jar')
        // ======== Unityのクラスをビルドするために、ここまで追加 ========
    }
    
    // ======== jarファイル出力のため、ここから追加 ========
    // タスクmakeJar-(release|debug)が追加される
    // タスクを実行するとlibtestservice\build\libs\以下にjarファイルが作成される
    // タスクの実行は、メニューのView→TaskWindows→Gradleを選択して開くGradle Projectsウインドウから実行すること
    // 参考  : http://stackoverflow.com/questions/16763090/how-to-export-library-to-jar-in-android-studio
    android.libraryVariants.all { variant ->
        task("makeJar-${variant.name}", type: Jar) {
            description "Bundles compiled .class files into a JAR file for $variant.name."
            dependsOn variant.javaCompile
            from variant.javaCompile.destinationDir
            exclude '**/R.class', '**/R$*.class', '**/R.html', '**/R.*.html'
        }
    }
    // ======== jarファイル出力のため、ここまで追加 ========

アプリ側のgradle.buildを次のように記述

    apply plugin: 'com.android.application'
    
    android {
        compileSdkVersion 24
        buildToolsVersion '25.0.0'
        defaultConfig {
            applicationId "net.sabamiso.android.androidplugintest"
            minSdkVersion 23
            targetSdkVersion 24
            versionCode 1
            versionName "1.0"
            testInstrumentationRunner "android.support.test.runner.AndroidJUnitRunner"
        }
        buildTypes {
            release {
                minifyEnabled false
                proguardFiles getDefaultProguardFile('proguard-android.txt'), 'proguard-rules.pro'
            }
        }
    }
    
    dependencies {
        compile fileTree(dir: 'libs', include: ['*.jar'])
        androidTestCompile('com.android.support.test.espresso:espresso-core:2.2.2', {
            exclude group: 'com.android.support', module: 'support-annotations'
        })
        compile 'com.android.support:appcompat-v7:24.0.0'
        testCompile 'junit:junit:4.12'
    
        // ======== testserviceモジュールを参照する設定をここに追加 ========
        compile project(':libtestservice')
        // ======== testserviceモジュールを参照する設定をここに追加 ========
    }

Unity
====

Android SDK向けセットアップ
----

あらかじめAndroid StudioとJREをインストールしておく。

2017/3/19現在、Android SDKでインストールされるNDKは新しすぎてUnity 5.5.2f1では対応していないので、android-ndk-r10eをC:\local\android-ndk-r10eにインストールする。

  * https://developer.android.com/ndk/downloads/older_releases.html

UnityのメニューからEdit→Preference→External Toolsを開き、Android SDK, JRE, Android NDKのパスを設定する。

参考
  * unity3d - not finding android sdk (Unity) - Stack Overflow
    * http://stackoverflow.com/questions/42538433/not-finding-android-sdk-unity

apkをビルドしようとしたときにエラーが出たときは…
----

参考
  * unity3d - not finding android sdk (Unity) - Stack Overflow
    * http://stackoverflow.com/questions/42538433/not-finding-android-sdk-unity

以下のようなエラーが表示されたときは…

    Error:Invalid command android
    UnityEditor.HostView:OnGUI()
    
    CommandInvokationFailure: Unable to list target platforms. Please make sure the android sdk path is correct. See the Console for more details. 
    C:/Program Files/Java/jdk1.8.0_102\bin\java.exe -Xmx2048M -Dcom.android.sdkmanager.toolsdir="C:/local/android-sdk\tools" -Dfile.encoding=UTF8 -jar "C:\Program Files\Unity\Editor\Data\PlaybackEngines\AndroidPlayer/Tools\sdktools.jar" -
    

Android SDKに同梱されているSDK toolsをダウングレードすると解決できるみたい。

C:\local\android-sdkディレクトリにあるtoolsディレクトリを↓と入れ替える

 http://dl-ssl.google.com/android/repository/tools_r25.2.5-windows.zip


Unity(C#)からAndroid側のJavaのコードを呼び出す
----

初めにAndroidJNI.AttachCurrentThread()を実行して、Java VMにスレッドをAttachすること。
これを実行しておかないと、Android上でUnity側からJava側の関数を呼び出した際に落ちる

参考

  * https://docs.unity3d.com/jp/540/Manual/PluginsForAndroid.html
  * http://westhillapps.blog.jp/tag/%E3%83%8D%E3%82%A4%E3%83%86%E3%82%A3%E3%83%96%E9%80%A3%E6%90%BA


uGUIのサイズが妙に小さくなる
----
参考

  * http://monaski.hatenablog.com/entry/2015/05/11/015200

CanvasにattachされているCanvas Scalerの設定を"Scale With Screen Size"に変更しておく。

=== スマホを一定時間さわっていなくてもSleepしないようにする
参考

  * https://docs.unity3d.com/ScriptReference/Screen-sleepTimeout.html

    using UnityEngine;
    
    public class PreventSleep : MonoBehaviour {
      void Start () {
        // see also... http://docs.unity3d.com/Documentation/ScriptReference/Screen-sleepTimeout.html
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
      }    
    }

独自にAndroidManifest.xmlを配置したい場合
----

参考
  * http://blog.wizaman.net/archives/1026

serviceやpermissionの追加を行いたい場合は、Plugins/Android以下にAndroidManifest.xmlを配置する。

Androidアプリをビルドする際に、デフォルトで使用されるAndroidManifest.xmlとマージされる。
マージの際は、Plugins/Android/AndroidManfest.xmlの内容が優先される。

どのようなAndroidManifest.xmlの内容を記述すればいいか？については、ビルドの際に使用されるAndroidManifest.xmlを参照して確認することができる。
ビルドの際に、出力タイプをGradleに設定して"Export Project"を選択しておくと、ビルド時に使用されるファイル一式が出力されるので、この中に含まれるAndroidManifest.xmlをチェックする。

ただし、Unityが自動生成するAndroidManifest.xmlには罠がある。

この内容をまるごとコピーしてAndroidManifest.xmlを独自に作ると、アプリ起動時にActivityが見つからないというエラーが出力され、アプリが落ちるようになる。

これを回避するためには、<activity>タグのandroid:nameプロパティの内容を

    android:name="com.unity3d.player.UnityPlayerActivity">

と指定しておく。（なぜ初めからそう設定されていないのか…）

