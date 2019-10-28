# Four Scenes Template

A unity project template that can served every purpose you can think of.
This template was inspired by unity games projects that were published on PC, Android, iOS and Switch platforms.

## How to use it

### Installation
1. You MUST include FredericRP's Standard Asset git module to this project for it to compile. See "FredericRP Standard Assets" section below
2. Import the package
3. Include the 4 scenes included in the correct order (from 1. to 4..) in your build settings
4. Change the Scene Name parameter of the "PlayButton" GameObject on the scene "4.menu" to be the first scene of your game.

### Editor test

1. Load the scene "1.staticLoading" and unload every other scene.
2. Hit "Play"

You should see the 4 scenes be loaded one after another, with a progress bar on 2nd and 3rd scene, and access the menu scene.
Clicking on the Play button launch your game.

## FredericRP Standard Assets

This project uses 6 of the Standard Assets freely available on [FredericRP GitHub page](https://github.fredericrp.com), so you must include it for the package to compile.

You can choose to :
- import the package available in the release section of the project (requires 1.1.0 version at least)
- add the assets as a git sub module with the following command

> git submodule add git@github.com:FredericRP/Standard-Assets.git "Assets/Standard-Assets"
