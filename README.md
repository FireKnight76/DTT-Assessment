# Perfect Maze Generator (Unity)

## [Demo Video](https://youtu.be/Q7e8uEJlCSo)

## Description 
This project was my take on a _**perfect maze**_ generator using Unity and C#. This project utilizes a recursive backtracking method to check all available spots and goes back in the list if there are no empty spots left next to the current spot.

## Technical requirements
- C#
- Unity 6.3 LTS or newer

## Project requirements

### User story 1: As a developer, I want to generate a perfect maze, so that I can showcase my technical capabilities.
- I want to implement a perfect maze generation algorithm.✅
- I want the algorithm implementation to create a visual representation.✅

### User story 2: As a user, I want to be able to configure the maze, so that I can control its visual representation.
- I want to be able to set the width of the maze in the user interface.✅
- I want to be able to set the height of the maze in the user interface.✅
- I want to be able to generate an unevenly sized maze.✅
- I want to be able to start generation from within the user interface.✅
- I want to be able to (re)generate the maze at any time.✅
- I want to be able to see the maze in its entirety.✅
- 
### User story 3: As a user, I want to generate mazes of varying sizes, so I have a better view of
the algorithm's visual representation.
- I want to be able to configure the maze’s size to a maximum of 250x250 cells.✅
- I want to be able to configure the maze’s width and height to at least 10x10 in size.✅
- I want to be able to generate a maze without significant impact on performance.✅
- 
### User story 4: As a user, I want a responsive user interface, so that I can view the maze on multiple device resolutions.
- I want the user interface to look good on Desktop (1920x1080).✅
- I want the user interface to look good on iPad (2048x1536).✅
- I want the user interface to look good on iPhoneX (2436x1125).✅

## Launching the game
1. Add the Unity folder to your projects in Unity.
2. Launch the added folder in Unity.
3. Press the play button.
4. Put the values you want in the input fields.
5. Press _**(Re)generate maze**_ and watch the generation.

## Features
- Generate a maze at your desired size(each between 10 and 250).
- Supports uneven mazes.
- Responsive UI that fits in multiple resolutions.
- Camera that moves and zooms out to keep the entire maze visible.
- Regenerate the maze whenever you want.

## Extra features
- Take complete control over the camera(toggled by escape).
- Switch between 2- and 3D whenever you want.
- Pause the generation of the maze at any time.
- Save the generated maze by moving the _**Maze**_ GameObject somewhere else in the inspector when the generation is done.
- Get a visual signal that shows when the _**perfect maze**_ is done generating.

## Sources
- [How to Procedurally Generate a Perfect Maze (Unity Tutorial)](https://www.youtube.com/watch?v=_aeYq5BmDMg&t=22s)
- [How to Make a Maze Generation Algorithm in Unity](https://www.youtube.com/watch?v=OutlTTOm17M&t=1s)
  
