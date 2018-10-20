test53_EncodeToJPG
====

![img01.gif](img01.gif)


Encode

    // Texture2D -> JPEG(byte[])
    int quality = 75; // default
    byte [] jpeg_data = texture.EncodeToJPG(quality);

Decode

    // JPEG(byte[]) -> Texture2D
    texture.LoadImage(jpeg_data);
    texture.Apply();


see also,,,

  - [Unity - Scripting API: ImageConversion.EncodeToJPG](https://docs.unity3d.com/ScriptReference/ImageConversion.EncodeToJPG.html)
  - [Unity - Scripting API: ImageConversion.LoadImage](https://docs.unity3d.com/ScriptReference/ImageConversion.LoadImage.html)

