# Virtual Spatial Design Unity Project

### Details
Student Name : Bashar Beshoti



### About The Project
Virtual Spatial Design, is a BSc final project for Computer Science at University Of Haifa supervised by **Prof Roi Poranne**. It leverages geometric comprehension and full algebraic functionality to render scenes in real-time. The primary aim of the project is to facilitate interior designing through various functionalities such as PushPull+, Mesh Model Transformation, Scalping Mesh Model, among others.


### Weekly Updates
#### Week 1 - Learn C# & Unity Editor
Mainly, Reading Books and Doucments, watching videos on Youtube. 

- Youtube Videos:
    1. Learn C# Beginner FREE Tutorial Course - CodeMonkey
    2. Learn Unity Beginner/Intermediate 2024 (FREE COMPLETE Course - Unity Tutorial) - CodeMonkey
    3. How to make a VR game - Unity XR Toolkit 2022 - Valem Tutorials

- Books List : 
    1. Thorn, A. 2015. Mastering Unity scripting : learn advanced C# tips and techniques to make professional-grade games with Unity. Packt Publishing, Birmingham, England.
    2. Hardman, C. 2020. Game Programming with Unity and C# : A Complete Beginner’s Guide. Apress : Imprint: Apress, Berkeley, CA.

- Doucmentation : 
    1. Unity Documentation



#### Week 2 - Setting OpenXR & MixedReality Package

- Via `MixedRealityFeatureTool`, I downloaded `MRTK3` package , then sets up the VR setup enabling player movement.
- Installation of `ProBuilder` plugin. 
- `XR Device Simulation` enabling player controls in action on PC.



#### Week 3 - Creating A Basic Enviroment & Character Movement & Setting L/R Controllers
Building an enviroment along with setting up L/R controllers as it should be. Afterwards, worked on Character ActionMap for Character Movement.


#### Week 4 - ProBuilder Script API 
Creating classes Polygon, Rectangle,Quad and Point as 2D Mesh Objects. For the 3D Mesh Objects, I've used ProBuilder `CreateShape` Supporting multiple different game objects useful for archicture in real-time.



#### Week 5 - Real-time Line Drawing + GUI Modification
Instead of blindly chosing vertices, i worked on drawing a line that visualize the edges of the polygon during drawing procedure. Furthermore, i added Canvas that hold ons the main GUI for user that accompany the user during runtime.




#### Week 6 - Real-time Selection & Hover Highlighting
Adding MeshCollider for each created Mesh during runtime and enabling both Hover-Highlight and Selection.



#### Week 7 - Basic Transformation & Singleton Class
Creating the basic Transformation for Mesh Objects (Scale,Rotate and Translate) where the user can modify objects `Transform` componenet. Moreover, establishing a singleton class that contain all the nessecary data to move data from scene to another and hold on the nessecary game objects.




#### Week 8 - Scenes Managment + Scene UI
Creating Multiple Scenes; Welcome, Settings, Main and Editor. where Welcome is an intro page for the project, the Settings responsible for Charachter & Game Settings, lastly Main and Editor are the project pillars that I am relying on. 



#### Week 9 - PushPull+ (Extrusion/Inclusion) + Measure Tool
Creating Extrusion and Inclusion in runtime over game object that has been sent to Editor scene where it provides mesh object shape manipulation + Optimization process where i deleted the non-nessecary elements (Vertices and Faces).



#### Week 10 - Options List + Game Message
Creating a Options list which contain :
1. Subdivision Game Object: 
2. Flipping Edges
3. Flipping Faces
4. Triangulate / To Quad
5. Undo
6. Redo
7. Visualize Skeleton/Grid
8. Viusualize Vertices


#### Week 11 - Face Subtraction & Hole Generation
Subtraction of a Sub-face from the main face where it divide the face into 9 different faces. Hole generation contain same principle except it deletes the subface as well generating a hole inside a face.


#### Week 12 - Fixing Buggs
I've modified the structure of the scenes and added few things and fixed few bugs along the execution.









