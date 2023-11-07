# WAYFS AlphaBot

## Connecting to RPi:
We followed the guide for the AlphaBot2 on the [Wiki page](https://hiq365.atlassian.net/wiki/spaces/Labbet/pages/18055489/Robot) to get the RPi connected to a mobile hotspot. With the pi on a network, it can be headlessly accessed via ssh-connection or VNC. 

### SSH
If both the computer and RPi are on the same network ssh pi@raspberrypi.local can be used. No IP address is needed.

### VNC
Download VNC. Connect to the RPi using SSH to find the AlphaBots IP address (ifconfig). Open VNC and create a new connection (file->new connection...). Enter the IP address, and a name. Select the VNC server and enter username (pi) and password (HiQBot1!) if prompted.

## Installing .NET SDK on the RPi (raspbian):

### Install via curl:
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel Current

echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.bashrc
source ~/.bashrc

## Add GPIO package for pin access:

dotnet add package System.Device.Gpio --version 2.2.0-*

## Usage

To run the Robot, modify the Program.cs main method as needed, then run it from a command line. 
The GridBot, and AlphaBot, classes contain functionality to control the Robot's behaviour.
The Alphabot is general purpose, and the GridBot abstracts its functionality and adds behaviour specific to traversing a grid.

### Programmatic commands
The first option is to control the robot programmatically. Both Alphabot, and GridBot instances can be controlled this way.
Find their documentation in their respective files.

### .txt file commands
The GridBot has the option of being controlled via a .txt file of commands. 
To do so, create a GridBot and a TxtParser and attach the GridBot to the TxtParser.
Then use the TxtParser.RunFile(string FileName) function to run the commands in the file.

The commands have to be of the form found below, and separated by a line separator.

Commands:
* Place X,Y,Direction 	Places the robot on a (x,y) virtual grid. The direction is ["NORTH", "WEST", "SOUTH", "EAST"] This command should be the first one to execute
* MOVE					Allows the robot to move relative to the direction it faces
* LEFT					Makes the robot turn anti-clock wise
* RIGHT					Makes the robot turn clock wise