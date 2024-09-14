# Virtual Spatial Design Unity Project

### Details
Student Name : Bashar Beshoti

### About The Project
Virtual Spatial Design, is a BSc final project at University Of Haifa supervised by **Prof Roi Poranne**. It leverages geometric comprehension and full algebraic functionality to render scenes in real-time. The primary aim of the project is to facilitate interior designing through various functionalities such as PushPull+, Mesh Model Transformation, Scalping Mesh Model, among others.


### Full Review
[![Full Video Review](https://github.com/user-attachments/assets/80dfe6f5-24fe-478d-b8b2-e407f82690e3)](https://youtu.be/fqUmv61P8oQ)



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


<image src="https://github.com/user-attachments/assets/99fe096b-9248-445b-96f2-ee41481e9287" width="360" height="360"/>

<em> Figure 1 : `XR Device Simulation` - This utility lets you simulate user inputs from plain key presses (be it from a keyboard and mouse combo or a controller) to drive the XR headset and controller devices in the scene. </em>

#### Week 3 - Creating A Basic Enviroment & Character Movement & Setting L/R Controllers
Building an enviroment along with setting up L/R controllers as it should be. Afterwards, worked on Character ActionMap for Character Movement.


<image src="https://github.com/user-attachments/assets/2b55ca34-1868-4c88-853f-54c40f2887d0" width="360" height="360"/>

<em> Figure 2 : Character Movement in user interface provide varity of actions </em>


<image src="https://github.com/user-attachments/assets/ea0a8ee8-baf9-410e-92d6-536219eb7fcd" width="360" height="360"/>

<em> Figure 3 : First Planned UI </em>

#### Week 4 - ProBuilder Script API 
Creating classes Polygon, Rectangle,Quad and Point as 2D Mesh Objects. For the 3D Mesh Objects, I've used ProBuilder `CreateShape` Supporting multiple different game objects useful for archicture in real-time.

<image src="https://github.com/user-attachments/assets/f3cf9f7d-5b59-4bbd-b4d6-581242096721" width="720" height="405"/>

<em> Figure 4 : Drawing 2D Meshes; Point, Quad, Rectangle and Polygon </em>

#### Week 5 - Real-time Line Drawing + GUI Modification
Instead of blindly chosing vertices, i worked on drawing a line that visualize the edges of the polygon during drawing procedure. Furthermore, i added Canvas that hold ons the main GUI for user that accompany the user during runtime.


<image src="https://github.com/user-attachments/assets/ccaf7174-1b0b-4d4c-afc9-ccc094ca6d74" width="720" height="405"/>

<em> Figure 5 : Drawing 2D Meshes along with line drawing to preview polygon </em>


<image src="https://github.com/user-attachments/assets/f834859c-b352-4d5c-b87b-7c402d3c208f" width="720" height="405"/>

<em> Figure 6 : Inserting 3D Meshes </em>


#### Week 6 - Real-time Selection & Hover Highlighting
Adding MeshCollider for each created Mesh during runtime and enabling both Hover-Highlight and Selection.

<image src="https://github.com/user-attachments/assets/ddb2df02-cbf6-43e3-a62e-3557ae52f848" width="720" height="405"/>

<em> Figure 7 : Real-time Selection & Hover Highlighting over runtime created objects only </em>


#### Week 7 - Basic Transformation & Singleton Class
Creating the basic Transformation for Mesh Objects (Scale,Rotate and Translate) where the user can modify objects `Transform` componenet. Moreover, establishing a singleton class that contain all the nessecary data to move data from scene to another and hold on the nessecary game objects.

<image src="https://github.com/user-attachments/assets/e45225b0-56ae-45a5-b01a-c89df645d3b7" width="720" height="405"/>

<em> Figure 8 : Grasp </em>

<image src="https://github.com/user-attachments/assets/8c363d12-67d2-437b-a468-79c550771d1f" width="720" height="405"/>

<em> Figure 9 : Rotate </em>

<image src="https://github.com/user-attachments/assets/92f0027f-89cc-4e30-85da-42c090906434" width="720" height="405"/>

<em> Figure 10 : Scale </em>

#### Week 8 - Scenes Managment + Scene UI
Creating Multiple Scenes; Welcome, Settings, Main and Editor. where Welcome is an intro page for the project, the Settings responsible for Charachter & Game Settings, lastly Main and Editor are the project pillars that I am relying on. 

<image src="https://github.com/user-attachments/assets/fe2c19fd-4f4f-457e-85ed-799a6c9ace67" width="720" height="405"/>

<em> Figure 11 : Editor Scene </em>

<image src="https://github.com/user-attachments/assets/17d7f731-6d04-42f0-97cb-c87e0a2d615d" width="720" height="405"/>

<em> Figure 12 : Measure Tool  </em>


<image src="https://github.com/user-attachments/assets/740199b4-708a-46b7-980f-710d58fcbc3e" width="360" height="202"/>


<image src="https://github.com/user-attachments/assets/ba759078-8e51-4a90-8177-969fd0b14ae9" width="360" height="202"/>

<em> Figure 13 : Pocket Canva & Element Level selection window </em>

#### Week 9 - PushPull+ (Extrusion/Inclusion)
Creating Extrusion and Inclusion in runtime over game object that has been sent to Editor scene where it provides mesh object shape manipulation + Optimization process where i deleted the non-nessecary elements (Vertices and Faces).

<image src="https://github.com/user-attachments/assets/68cc1f89-1617-4bfc-9cc4-141135b33bb5" width="720" height="405"/>

<em> Figure 14 : Pull/Extrusion </em>

<image src="https://github.com/user-attachments/assets/0e300398-1e07-4c4e-8273-8919e81e3208" width="720" height="405"/>

<em> Figure 15 : Push/Inclusion </em>

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


<image src="https://github.com/user-attachments/assets/395ba7fa-0130-4737-8d80-43014ee78363" width="720" height="405"/>

<em> Figure 16 : Options List </em>

<image src="https://github.com/user-attachments/assets/48c11486-e0f9-47be-b2cb-3dfc77cdf3b7" width="720" height="405"/>

<em> Figure 17 : Game Message/Alert - part 1 </em>

<image src="https://github.com/user-attachments/assets/228fafb2-d51a-40e7-8580-cf5c15cda6f9" width="720" height="405"/>

<em> Figure 17 : Game Message/Alert - part 2 </em>

#### Week 11 - Face Subtraction & Hole Generation
Subtraction of a Sub-face from the main face where it divide the face into 9 different faces. Hole generation contain same principle except it deletes the subface as well generating a hole inside a face.


<image src="https://github.com/user-attachments/assets/6f7e8344-954c-42c9-b5f1-14aca126b595" width="360" height="202"/>


<image src="https://github.com/user-attachments/assets/c6a2d423-8420-41a6-a287-dbfbf47dc40d" width="360" height="202"/>

<em> Figure 13 : Face Subtraction in action </em>

#### Week 12 - Fixing Buggs
I've modified the structure of the scenes and added few things and fixed few bugs along the execution.









