# WAYFS AlphaBot

## Connecting to RPi
We followed the guide for the AlphaBot2 on the [Wiki page](https://hiq365.atlassian.net/wiki/spaces/Labbet/pages/18055489/Robot) to get the RPi connected to a mobile hotspot. With the pi on a network, it can be headlessly accessed via ssh-connection or VNC.

### SSH
If both the computer and RPi are on the same network,
```console
ssh pi@raspberrypi.local
```
can be used. No IP address is needed.

### VNC
Download VNC. Connect to the RPi using SSH to find the AlphaBots IP address (ifconfig). Open VNC and create a new connection (file->new connection...). Enter the IP address, and a name. Select the VNC server and enter username (pi) and password (HiQBot1!) if prompted.

## Installing .NET SDK on the RPi (raspbian)

### Install via curl
```console
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel Current
```

```console
echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
```

```console
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.bashrc
```

```console
source ~/.bashrc
```

## Add GPIO package for pin access

```console
dotnet add package System.Device.Gpio --version 2.2.0-*
```

## Usage

To run the Robot, modify the Program.cs main method as needed, then run it from a command-line.
The GridBot, and AlphaBot, classes contain functionality to control the Robot's behaviour.
The Alphabot is general purpose, and the GridBot abstracts its functionality and adds behaviour specific to traversing a grid.

### Run Backend & Frontend

To run the backend on your local machine:
* Go to './WAYFS-AlphaBot/Backend/AlphaBotApp'
* Type: dotnet run --urls "http://<rpi-ip-address>:<port>"

You can also use the `--project` flag:
* Go to './WAYFS-AlphaBot'
* Type: dotnet run --project Backend/AlphaBotApp/WAYFS-AlphaBot.csproj --urls "http://<rpi-ip-address>:<port>"

To run the backend on the Raspberry Pi:
* The LEDs require root permission to work so in order to run with the lights you need to build and run in two steps.
* Go to './WAYFS-AlphaBot'
* Build as usual with `dotnet build`. The executable gets built to './Backend/AlphaBotApp/bin/Debug/net7.0'
* Go to './Backend/AlphaBotApp/bin/Debug/net7.0'
* Type: `sudo -E ./WAYFS-AlphaBot -u "http://<rpi-ip-address>:<port>"`.

To run the frontend:
* Go to './WAYFS-AlphaBot/client'
* Type: npm run dev -- --host

To run the app:
* Go to './WAYFS-AlphaBot/controller-app/Controller'
* Type: expo start

### Testing

The backend is organized in three C# projects.
* AlphaBotApp.csproj - ASPNET.Core web app containing our main entry point.
* AlphaBotLib.csproj - .NET class library containing the robot specific code that we want to test.
* AlphaBotLib.Tests.csproj - xUnit test project. Contains example unit tests.

To run the backend tests:
* Go to './WAYFS-AlphaBot'
* Type: `dotnet test`

### Programmatic commands
The first option is to control the robot programmatically. Create a new Main method and populate it with initializations, and then commands as wanted. Both Alphabot, and GridBot instances can be controlled this way.
Find their documentation in their respective files.

### .txt file commands
The GridBot has the option of being controlled via a .txt file of commands.
To do so, create a GridBot and a TxtParser and attach the GridBot to the TxtParser.
Then use the TxtParser.RunFile(string FileName) function to run the commands in the file.

The commands have to be of the form found below, and separated by a line separator.

Commands:
* Place X,Y,Direction - Places the robot on a (x,y) virtual grid. The direction is ["NORTH", "WEST", "SOUTH", "EAST"] This command should be the first one to execute
* MOVE - Allows the robot to move relative to the direction it faces
* LEFT - Makes the robot turn anti-clock wise
* RIGHT - Makes the robot turn clock wise

Use the dotnet run command with the flag -t or --txt to run the program in TxtParser mode. The flag should be followed by a filename.

## Commands from a frontend site, or app
The Robot can also be run in tandem with a site. This way commands can be sent, and feedback recieved, in real time.

Use the dotnet run command with the flag -u or --urls to run the program in AppCmdParser mode. The flag should be followed by a valid URL and port (like <http://localhost:5175>).

Using the -u flag, another flag, -d or --dummy, Can be set to run the backend without initializing an AlphaBot. This is useful for working on the frontend locally without a working backend.

The frontend also needs to be launched. This is done npm run dev -- --host.