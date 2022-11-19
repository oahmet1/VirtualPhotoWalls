# RealityMixers
Photo-wall Generation on HoloLens.


## Installation 
1. Clone the repo to a local folder.
2. Open unity hub and click open -> add project from disk. Then select the clonned folder.
3. Set the project unity version to unity 2021.3.10f1 with WebGl UWP and Windows support modules.
4. Open micrososft mixed reality feature tool for unity and select the cloned project path.
5. Select the features shown in the image. Click next next.
    ![image](https://user-images.githubusercontent.com/88540292/202858272-23c05946-cf7b-4259-a1d4-1cca784f70dc.png)
6. Open the project from unity. Don't worry it will take very long.
7. Open Unity and Complete Mixed Reality Toolkit setup.
    - Select OpenXR PLugin
    - Select Platform as UWP with ARM64 processor from Build Settings.
    - Set OpenXR and then hololens feature group from Project Settings → XR-Plugin Management→OpenXR
    - Click apply settings from the MRTK Plugin Menu. Click fix all.
    - Click next and dowload text mesh pro(TMP) as suggested.
    - Edit → Project Settings → Player → Other Settings → Api compatibility level and change it to .NET FrameWork
8.  Create Visual Studio Files for Development. Skip this if you are not a developer.
    - Click Edit → Preferences → External Tools  Select Visual Studio
    - Click regenerate project files
    - Then Click Assets → Open C# project to open the visual studio solution.
    - Your Scripts are under Assembly-CSharp → Assets → Scripts
    - You can use your newly generated solution in your cloning directory to start development. 
9. From Scenes select SampleScene. Click Run or Deploy it directly to hololens using the build instructions.
