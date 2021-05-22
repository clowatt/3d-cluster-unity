# 3d-cluster-unity
Create a 3D visualization of a star cluster with Unity

![image](https://user-images.githubusercontent.com/19656975/117451671-2ed9c900-af19-11eb-8ba9-0139679f49d6.png)

## Usage
1. Clone the code to your computer
2. Generate a new unity project with the contents of the 3d-unity-cluster folder
3. Place your text file as an asset in Unity
4. Select your text file and make it an Addressable Asset
5. In the InitializeStars set the location path of your Addressable Asset in the Asset File Location
6. Press Play

## Features
- [x] Generate stars from 6-field (position - velocity) comma deliniated file
- [x] Make the camera move in play mode with keyboard operations
- [x] Automated Camera scene to zoom in and out of the star cluster when 'play' is pressed
- [ ] Adjustable size, colour, and luminosity for star initialization
- [ ] Create a 3D Milky Way prefab 
- [ ] Create different scenes based on the outcome of the visualiation
- [ ] Add velocity vectors to the stars to be visible


## Requirements
- Unity 2020

## License
[MIT](https://choosealicense.com/licenses/mit/)
