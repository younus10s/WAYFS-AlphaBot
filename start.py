import argparse
import os
import platform
import subprocess

parser = argparse.ArgumentParser(description="Run AlphaBotApp with free mode")
parser.add_argument('--free', action='store_const', const='--free', default='', help='Include this flag to run in free mode')
args = parser.parse_args()

def get_ip_address():
    if platform.system() == "Windows":
        return subprocess.getoutput("ipconfig").split("IPv4 Address. . . . . . . . . . . : ")[-1].split("\n")[0]
    else: # Linux
        return subprocess.getoutput("hostname -I").split()[0]

ip_address = get_ip_address()
command = ''

if platform.system() == "Windows":
    command = f".\\Backend\\AlphaBotApp\\bin\\Debug\\net7.0\\WAYFS-AlphaBot -d -s {args.free} -u \"http://{ip_address}:5000\""
else: # Linux
    command = f"./Backend/AlphaBotApp/bin/Debug/net7.0/WAYFS-AlphaBot -s {args.free} -u \"http://{ip_address}:5000\""

if platform.system() == "Linux":
    os.system(f"sudo -E {command}")
else:
    os.system(command)
