Thank you for downloading Enhanced Editor++!

If you have any comments, questions, suggestions (including new features), or bug reports, please email me at walt@waltdestler.com. I appreciate your feedback!

Regardless of how you like Enhanced Editor++, please leave a review! Both positive and negative reviews are appreciated. (If you're having a particular problem, please email me and give me a chance to fix it or offer you a refund.)

The website for Enhanced Editor++ is at http://www.waltdestler.com/editorplusplus

------------
INTRODUCTION

Enhanced Editor++ is a powerful extension that fixes some of the most annoying issues and ommisions of the out-of-the-box Unity3D editor. As of the current version, there are seven main features:

1. Transform Copy & Paste - Allows copy & paste of a game object's position, rotation, and scale, using either local or global coordinates. Position, rotation, and scale can either be pasted all at once or individually.

2. Component Copy & Paste - Allows copy & paste of entire components at once, and works with both built-in components and custom scripts. An advanced "Clipboard" window allows you to select which individual variables will be pasted and even allows you to alter the values of those variables before they are pasted.

3. The Rememberer - Solves one of the most annoying aspects of the Unity3D editor, which is that any changes made to an object while the game is being played are forgotten as soon as the game stops. The Remember solves that by allowing you to "remember" the state of any object while playing and then "apply" the remembered state of those objects after the game is stopped.

4. Inspector Batching - Solves another big annoyance, which is that modifying an object using the Inspector only modifies one object at a time, even if multiple objects are selected. Inspector Batching solves this by (optionally) applying any modifications to all selected objects.

5. Import Settings Copier - After you apply your desired import settings to a texture, audio clip, or model, the Import Settings Copier allows you to copy those import settins to all other selected textures, audio clips, or models.

6. Usages Finder - Quickly find all usages of any Unity object, component, prefab, script, or asset by other objects in either the scene or the Assets folder.

7. Component Reorderer - This is an experimental feature currently in BETA that allows you to reorder the components on a game object.

Below you will find detailed instructions describing how to use each feature.

---------------
VERSION CHANGES

2.5
- Removed most source code files in favor of a single compiled .dll file. This should eliminate bugs with conflicting filenames. The old source code files will be automatically removed if detected.
- Added preliminary support for most new Unity 3.5 components. Not all variables of all components are supported yet. Full support will come shortly after Unity 3.5 is officially released.
2.4
- Fixed bug where batch modifications to prefabs sometimes were not saved.
2.3
- Added "Select All" and "Select None" buttons to the Clipboard window.
2.2
- Added feature to quickly find all usages of any Unity object, component, script, or asset by other objects in either the scene or the Assets folder.
2.1
- The Import Settings Copier for textures now properly copies platform-specific settings.
2.0
- Can right click on texture, model, and audio importers to copy import settings en masse to all selected textures, models, and audio clips.
- Added Component Reorderer.
- Added a "Paste New" button to each component in the Clipboard that adds a new component instead of replacing the values in an existing component.
- Added support for many previously-unsupported variables of built-in Unity components.
- Added batching support for most types of assets.
- Remembering of materials and meshes is now disabled by default. They can be re-enabled by selecting a check box in the Rememberer window.
- Miscellaneous bug fixes.
- The Batcher no longer has its own window that has to be open for batching to work. Simple use the new Batcher menu.
1.11
- Updated to support Unity 3.4.
1.10
- Fixed a bug when applying remembered variables.
1.9
- Fixed a bug where arrays weren't properly batched.
1.8
- Fixed an issue where switching scenes while playing caused remembered objects to be forgotten.
1.7
- Unity 3.2's Rigidbody.constraints property is now handled.
1.6
- Fixed a bug preventing build from working on mobile platforms.
1.5
- Fixed a bug where some custom components weren't being properly "remembered".
1.4
- Fixed an exception bug with the Batcher.
- The Batcher will now remember its enabled/disabled setting.
- User interface for the Batcher moved to its own window instead of a menu.
- Added a Remember Transform button to the Rememberer.
- Remembering, copying, and batching Animations now works properly.
- Extendable via static attributed methods.
1.3
- Fixed a few issues related to undo functionality.
1.2
- Added a Readme.txt file with detailed instructions.
1.1
- An object's "active" property is now remembered by the Remeberer and is now also batchable.
1.0
- Initial version.

------------
KNOWN ISSUES

- A few variables of a couple built-in components cannot be remembered, batched, or copy-and-pasted. This is because they are not exposed by Unity to scripting.
- Copying import settings of Materials, GuiSkins, and Cubemaps are not supported due to limitations and bugs in Unity.
- If you switch scenes while playing the game, and then "remember" an object in that new scene, it will be impossible to apply changes made to that object after the game has stopped.
- The Component Reorderer is currently in BETA has has some known deficiencies. Please see "COMPONENT REORDERER" below.

----------------------
TRANSFORM COPY & PASTE

Transform Copy & Paste allows you to copy & paste a game object's position, rotation, and/or scale, using either local or global coordinates. Position, rotation, and scale can either be pasted either all at once or individually.

How to use Transform Copy & Paste:
1. Select a single object whose global or local position, rotation, and/or scale you want to copy.
2. Click the Transform menu and then click Copy. The selected object's local position, global position, local rotation, global rotation, and local scale will each be copied into a special "transform clipboard".
3. Select one or more objects into which you want to paste any of those copied values.
4. Click the Transform menu and then click one of the Paste commands, as detailed here:
   - Paste Local: Pastes the copied local position, local rotation, and local scale into all of the selected objects.
   - Paste Global: Pastes the copied global position and global rotation into all of the selected objects.
   - Paste Local Position: Pastes the copied local position into all of the selected objects.
   - Paste Global Position: Pastes the copied global position into all of the selected objects.
   - Paste Local Rotation: Pastes the copied local rotation into all of the selected objects.
   - Paste Global Rotation: Pastes the copied global rotation into all of the selected objects.
   - Paste Local Scale: Pastes the copied local scale into all of the selected objects.

Additional Notes:
- It's okay to select multiple objects before clicking Copy, but only the transform of the active object (the one shown in the inspector) will be copied.
- Copying an object's transform will not erase anything in your operating system's clipboard, nor will it erase anything in the component clipboard described in the next section.
- Because of a limitation with Unity3D, you cannot copy & paste global scale.
- Under the Transform menu there is also a Create Empty Child command. Clicking this command creates an empty game object that is parented to the selected object.

----------------------
COMPONENT COPY & PASTE

Component Copy & Paste allows you to copy & paste entire components at once. It works with both built-in components and custom scripts. An advanced "Clipboard" window allows you to select which individual variables will be pasted and even alter the values of those variables before they are pasted.

How to use Component Copy & Paste (basic):
1. Select a single object whose component you want to copy.
2. Right-click on the bold name of the desired component in the Inspector and then click Copy. All of the variables for that component will be copied into a special clipboard specific to that type of component.
3. Select an object whose component into which you want to paste copied variables.
4. Right-click on the bold name of the desired component in the Inspector and then click Paste. All of the variables copied from the first component will be pasted into this second component.

Using the Clipboard window (advanced):
1. Click the Window menu and then click Clipboard. The Clipboard window will appear. If you have already copied any components via the above method, you will see those components listed in the window. Otherwise, you will see an empty window except for short instruction text at the top.
2. If you have not already done so, copy a component into the clipboard using steps 1 and 2 of the basic instructions.
3. Click the little + button to the right of the name of any component in the Clipboard window to expand that component. You will now see all of the variables and their values copied from that component.
4. On the right side of the window next to every variable is a small check box. Unchecking a box will prevent its variable from being pasted when you select Paste in step 4 of the basic instructions.
5. The values of most copied variables can be edited directly in the Clipboard window, just as if the variable was shown in the inspector. Of course, editing these values won't change any components until you paste those edited values into a component.

Additional Notes:
- It's okay to select multiple objects before clicking Copy, but only the component of the active object (the one shown in the inspector) will be copied.
- There is a separate clipboard for each type of component. This means that copying one type of component will never overwrite variables copied from another type of component, even if the variables have the same name.
- Copying an object's transform will not erase anything in your operating system's clipboard, nor will it erase anything in the transform clipboard described in the previous section.
- If you want to paste a copied component into the same component of multiple selected objects, simply turn on Inspector Batching as explained later.
- Clicking the "Paste New" button will create a new component on the active game object and paste the copied values into that new component.

--------------
THE REMEMBERER

The Rememberer solves one of the most annoying aspects of the Unity3D editor, which is that any changes made to an object while the game is being played are forgotten as soon as the game stops. The Remember solves that by allowing you to "remember" the state of any object while playing and then "applying" the remembered state of those objects after the game is stopped.

How to use The Rememberer (basic):
1. Click the Play button to start playing your game.
2. Click the Window menu and then click Rememberer. A window with some instructions and four buttons will appear.
3. Select any object in the scene and modify it, such as moving its position or changing a variable of one of its components.
4. In the Rememberer window, click the Remember button. The name of the selected object will now be listed in the Rememberer window.
5. Repeat steps 3 & 4 for any number of other objects. You will see each object listed in the Rememberer window.
6. Stop the game. All of your modified objects will now return to their original states... But the Rememberer window will still list any objects you told it to remember.
7. Click the Apply All button. The state of any objects you "remembered" will now be restored.
8. Click the Forget All button. All remembered objects will now be forgotten. You should usually click this after applying so that you don't accidentally re-apply some changes in the future when you didn't mean to.

Advanced Usage:
- Double-click an object's name in the Rememberer window to select that object.
- Each object has an Apply button and a Forget button, allowing you to apply or forget changes made to an individual object.
- Click on the little + button to the right of an object to expand your view of that object. Using this expanded view you can apply or forget changes made to individual components or variables, as well as edit the remembered values of any variables, similar to how variables can be edited using the Clipboard window. The components themselves also have little + buttons, allowing you to apply, forget, or edit the remembered variables in those components.

Additional Notes:
- If you want to remember only an object's transform, click the Remember Transform button instead. This is easier than remembering an object and then forgetting all of its other components and variables.
- Selecting multiple objects and clicking Remember will remember the state of all of those objects.
- The remembered state of an object is the object's state as of the time you clicked the Remember button. Any future changes made will be forgotten, unless you again click the Remember button after making those additional changes.
- Any children of the selected objects will also be automatically remembered. You can see these children by clicking the little + button of the parent. You can individually apply or forget these children as well.
- Adding components or children to an object is not currently supported by the Rememberer. Such changes will be forgotten.

------------------
INSPECTOR BATCHING

Inspector Batching solves another big annoyance of Unity3D, which is that modifying an object using the Inspector only modifies one object at a time, even if multiple objects are selected. Inspector Batching solves this by (optionally) applying any modifications to all selected objects.

How to use Inspector Batching:
1. By default, batching of modifications is disabled. To enable batching, click on the Batcher menu and then click Enable Batching.
2. Select more than one object.
3. Using the Inspector, make a modification, such as changing the object's name or a component variable common across the selected objects. (Exception: modifications to the transform won't get batched by default. See Additional Notes below.)
4. Observe that the change you made has been applied to all selected objects!

Additional Notes:
- By default, batching of modifications to an object's transform is disabled. This is because you typically make transform modifications to groups of objects using the Scene window (such as moving all the selected objects to the left). Transform batching will break this nice feature of the Unity3D editor, so you usually want it disabled. But you can still enable it if you choose by clicking Enable Transform Batching under the Batcher menu. In this way you can, for example, type 5 into the X position field and all selected objects will move to align with the new X position.
- When a component's variable is modified, the batcher will modify the same variable on the same type of component in all other selected objects. In the case where an object has multiple components of the same type, the order of those same-typed components is used to determine which component's variable gets modified. For example, if you change the audio clip of an object's 2nd Audio Source component, then the 2nd Audio Source component of all other objects will be modified. Any objects with only one (or zero) Audio Source components will be unmodified.
- With batching enabled, you can batch add and remove components as well. It just works.
- In the current version, you cannot "revert to prefab" as a batch operation. The active object's variable will be reverted like usual, but then the other selected object's variables will be set to match the new value of the active object's variable and their variables will *not* be reverted.

----------------------
IMPORT SETTINGS COPIER

The Import Settings Copier allows you to copy the import settings of any single texture, audio clip, or model to all other selected textures, audio clips, and models.

How to use adjust the import settings of many textures, audio clips, or models at once:
1. Select all desired textures, audio clips, or models. (Only select one type.)
2. Adjust the import settings of the texture, audio clip, or model currently shown in the inspector.
3. Click the Apply button.
4. Right-click in the inspector on "Texture Importer", "Audio Importer", or "FBX Importer" and click on "Copy import settings to all other selected..."
5. The import settings of the texture, audio clip, or model shown in the inspector will be copied to all other textures, audio clips, or models. This process may take a while. Please be patient.

How to copy import settings from a texture, audio clip, or models to other textures, audio clips, or models:
1. Select the texture, audio clip, or model from which you want to copy import settings.
2. Holding the Ctrl key, select all the other textures, audio clips, or models to which you want to copy the import settings.
3. Right-click in the inspector on "Texture Importer", "Audio Importer", or "FBX Importer" and click on "Copy import settings to all other selected..."
4. The import settings of the texture, audio clip, or model shown in the inspector will be copied to all other textures, audio clips, or models. This process may take a while. Please be patient.

Additional Notes:
- Copying import settings of Materials, GuiSkins, and Cubemaps are not supported due to limitations and bugs in Unity.

-------------
USAGES FINDER

The Usages Finder quickly finds all usages of any Unity object, component, prefab, script, or asset by other objects in either the scene or the Assets folder.

How to find the objects that "use" a particular object:
1. Select the object (in either the scene or the project assets list) whose usages you want to find.
2. Click on the "Find Usages" menu and then click either "In Scene" or "In Assets":
   - In Scene: The Usages Finder will search for all objects and components in the currently-open scene that have a variable that refers to the selected object.
   - In Assets: The Usages Finder will search for all objects, components, and assets in the Assets folder that have a variable that refers to the selected object.
3. The Usages Finder window will open and it will start searching for usages of the selected object. This process may take a while, especially when searching "In Assets".

Additional Notes:
- When you have selected an object in the scene, you cannot search for assets that use the selected object since it is impossible for an asset to refer to an object in a scene.
- When searching for usages of a prefab in the scene, any prefab instances will be displayed in the Usages Finder window.
- When searching for usages of a GameObject, the Usages Finder will also automatically search for usages of any of its components, children, and children's components.
- You can right-click on a component's bold name in the Inspector and select "Find Usages in Scene" or "Find Usages in Assets" to find usages of that particular component.
- Due to restrictions in Unity, the Usages Finder cannot search inactive game objects.

-------------------
COMPONENT REORDERER

The Component Reorderer is an experimental feature that allows you to change the order in which components are defined on a game object. You may want to do this just for organizational purposes, but there are also some cases where the order of components matters, such as with image effects.

WARNING: Use with extreme caution! Reording components is currently in BETA and may not work perfectly or have unexpected side effects. Please make sure to back up the object or prefab you are modifying in case Unity's "undo" function doesn't work. See the Known Deficiencies section below.

How to reorder components:
1. Click the Window menu and then click Component Reorderer. A window with some text and buttons will appear.
2. Select the game object (or prefab) whose components you want to reorder.
3. Click the Up and Down buttons next to component names to move those components up and down in the order.
4. When satisfied with the new order, click the Apply button to make the changes.
5. If everything works, the components should now be in the new order. If there were errors, the Component Reorderer will *attempt* to undo the changes it made.

Additional Notes:
- Click the Revert button to change the order of components back to its original order. Note that once you hit Apply, the order cannot be reverted!
- Some components require other components to be ordered before. In this case, you will get a little error message underneath a component if it requires another component to be ordered before.

Known Deficiencies:
- Batching does not work when reordering components.
- Some components, such as the Flare Layer, cannot be reordered and may cause errors.
- Any variable references that point *to* reordered components will be lost.
- Some custom components may lose data when reordering.
- Changes to prefab assets cannot be undone!

------------------
EXTENDING EDITOR++

Though Editor++ works with all built-in components and any custom components using public fields, some Editor++ functionality will not work with some advanced custom components because it doesn't know how to copy the data in those components.

Luckily, it's possible to extend Editor++ to work with these advanced custom components. Take a look at the Editor++/DefaultObjectVariables.cs code file. It contains a static method for each type that Editor++ knows how to handle. Each of these methods returns an IEnumerable<ObjectVariableBase> and has an [ObjectVariables(typeof(SomeType))] attribute. You can define your own such methods anywhere in the project to handle custom types.

Each of the objects enumerated by these methods must extend from the ObjectVariableBase class, which is an abstract layer through which a particular "variable" of an object is accessed. If you simply need to handle a component's properties, you can return one ObjectPropertyVariable object for each such property. Or if you need extra-special behavior, you can extend the ObjectVariableBase class yourself.

-----------------------
GETTING THE SOURCE CODE

As of version 2.5, Enhanced Editor++ no longer comes with its source code included in the downloadable package, except for the DefaultObjectVariables.cs file. This change is simply to solve a bug that some users were having with conflicting filenames. However, you bought Enhanced Editor++, and I believe that you should have access to the source code. If you want the source code, please simply email me at walt@waltdestler.com and include your invoice number so that I can verify your purchase.