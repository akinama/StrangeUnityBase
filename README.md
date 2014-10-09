# StrangeUnityBase

Unity3d base project based on StrangeIoC.

## Structure

### Top Level Folders
* Assets/Core: All first-party project files go here.
* Assets/Plugins: Native plugins.
* Assets/Sample: A sample project mirroring the structure of Core. You can delete this safely.
* Assets/ThirdParty: Third-party assets and scripts go here.
 
### Core Folders

You should keep these in Core, but reorganize locally as needed.

* Audio
* Effects
* GUI
* Materials
* Meshes
* Prefabs
* Resources
* Scenes
* Scripts
* Textures

### Script Folders

These are divided into MVCS layout following StrangeIoC's structure. For what goes where, see the sample project and [StrangeIoC's documentation](http://strangeioc.github.io/strangeioc/TheBigStrangeHowTo.html).

* controller
* model
* service
* view

## Sample Project

Start by opening and running the sample scene at Assets/Sample/Scenes/Sample.unity. Use WASD or arrow keys to move. This scene lets you move the player (BLUE sphere) and saves your grid position (RED sphere) whenever you cross into a new square. Restarting the scene will place you back in the square you left off at.

Files to read first:
* Assets/Sample/Scripts/SampleContext.cs
* Assets/Sample/Scripts/SampleRoot.cs
* Assets/Sample/Scripts/view/SampleView.cs
* Assets/Sample/Scripts/view/SampleMediator.cs
* Assets/Sample/Scripts/controller/SampleMoveCommand.cs
* Assets/Sample/Scripts/model/SampleModel.cs
* Assets/Sample/Scripts/model/SampleServiceStub.cs
