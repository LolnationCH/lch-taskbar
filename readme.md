# LCH-Taskbar

This is a prototype to make myself a complete custom taskbar for windows.

The project `lch-taskbar-wpf` contains the taskbar components and other stuff. 
Taskbar is always at the top, but you can modify the starting value so it is where you want it.

The project `lch-windows` has some code to get the process with visible windows. The rest is currently not used, but I planned to make something like i3 with it.

Screenshot :
![image](https://user-images.githubusercontent.com/9981795/215206487-d398ae7b-9d00-40c0-b45e-a368a13f4aea.png)

Left side :
  process list
  weather for a hardcoded location
  spotify satus
  
Middle :
  Title of the windows focus (in this case, firefox watch a youtube video
  
Right side :
  Output device and volume
  Bluetooth (paired or not)
  Everything button
  Network status (wifi name in case of wifi)
  Time/Date


## How to use

Download and compile the code. Then run the .exe.

Using net7.0.

For the `Everything` and `Spotify` button, you need those program installed.
