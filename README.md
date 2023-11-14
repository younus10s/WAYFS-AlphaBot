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

## Start server with IP address
dotnet run --urls "http://192.168.187.236:5175"