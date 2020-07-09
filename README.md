
[![Generic badge](https://img.shields.io/badge/Build%20status-passing-sucess.svg)](https://shields.io/)
[![GitHub stars](https://img.shields.io/github/stars/nma76/jBot)](https://github.com/nma76/jBot/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/nma76/jBot)](https://github.com/nma76/jBot/network)
[![Twitter](https://img.shields.io/twitter/url?style=social&url=https%3A%2F%2Fgithub.com%2Fnma76%2FjBot%2F)](https://twitter.com/intent/tweet?text=jBot:&url=https%3A%2F%2Fgithub.com%2Fnma76%2FjBot%2F)
 
 # jBOT, TwitterBot
This is my implementation of a Twitter-bot, written in c# (NET Core 3), that looks for hashtags and response to them with defined actions.
  
Make sure you have a Twitter account with APi communication enabled. You will also need your secrets and tokens. Read more at https://developer.twitter.com. 
  
The code is written on MacOS using visual Studio Mac and Visual Studio Code and tested on MacOS and Rasbian (Raspberry Pi 3). The daemon should work on any \*nix that supports Net Core and uses Systemd. The console-app should run on any plattform that supports Net Core.
  
## How to build

### Using make
Build the apps using make if you're on a system with make installed. There's a few make targets:

Cleans the solution, build and publish everything  
<code>make</code> or <code>make all</code>    

Cleans the solution   
<code>make clean</code>  
  
Restore nuget packages  
<code>make restore</code>  
  
Build everything  
<code>make build</code>  
  
Publish both TweetConsole and Daemon  
<code>make publish</code>  
  
Publish TweetConsole  
<code>make tweetconsole</code>  
  
Publish daemon  
<code>make daemon</code>  
  
### Without make
Use standard commands to clean and build:  
  
Cleans solution  
<code>dotnet clean</code>  
  
Restore nuget packages  
<code>dotnet restore</code>
  
Build everything  
<code>dotnet build</code>  
  
Publish TweetConsole for Linux ARM  
<code>dotnet publish jBot.TweetConsole/jBot.TweetConsole.csproj -r linux-arm -c Release /p:PublishSingleFile=true</code>  

Publish TweetConsole for Windows 64  
<code>dotnet publish jBot.TweetConsole/jBot.TweetConsole.csproj -r win-x64 -c Release /p:PublishSingleFile=true</code>  

Publish TweetConsole for MacOS (Catalina)  
<code>dotnet publish jBot.TweetConsole/jBot.TweetConsole.csproj -r osx.10.15-x64 -c Release /p:PublishSingleFile=true</code>  

Publish Daemon for Linux ARM  
<code>dotnet publish jBot.Daemon/jBot.Daemon.csproj -r linux-arm -c Release /p:PublishSingleFile=true</code>  

Publish Daemon for Windows 64  
<code>dotnet publish jBot.Daemon/jBot.Daemon.csproj -r win-x64 -c Release /p:PublishSingleFile=true</code>  

Publish Daemon for MacOS (Catalina)  
<code>dotnet publish jBot.Daemon/jBot.Daemon.csproj -r osx.10.15-x64 -c Release /p:PublishSingleFile=true</code>  

## Projects overview  
### jBot.Lib  
Contains all logic for the actual twitter communictaion. Implement your own actions by modifying Capabilities.cs and ActionHandler.cs.

### jBot.TweetConsole
Is a console app that runs once and quits. I use this mostly for development and debuging.

**Installation**  
The only installation required is adding correct values to the appsettings.json (there is an example file in the repo). If you want to use the included action to add overlay images you'll need to copy the image to the folder configured.

### jBot.Daemon
Is i Linux daemon that runs continously in systemd.
  
**Installation**    
You should probably add a user to your system that runs the daemon. In this example i use user pi (since i run this app in a raspberryPi)  
  
Create a directory for the daemon:  
<code>mkdir /opt/jBot.Daemon</code>
  
Set permissions on the directory, if using a separate account:  
<code>chown pi:pi jBot.Daemon/</code>
  
Copy the application to the newly created directory. Locally this can be done with something like this:  
<code>cp -r * /opt/jBot.Daemon</code>
  
or to a remote system, something like this:  
<code>
scp -r * pi@192.168.1.101:/opt/jBot.Daemon  
</code>
  
Create configuration file for systemd:  
<code>nano /etc/systemd/system/jBot.daemon.service</code>

Content of file, something like this:  
  
[Unit]  
Description=jBot daemon  
DefaultDependencies=no  
   
[Service]  
Type=notify  
ExecStart=/opt/jBot.Daemon/jBot.Daemon.dll  
WorkingDirectory=/opt/jBot.Daemon
User=pi  
Group=pi    
   
[Install]  
WantedBy=multi-user.target  
    
Reload daemons:  
<code>sudo systemctl daemon-reload</code>

Check status of daemon:  
<code>sudo systemctl status jBot.daemon</code>

Start daemon:  
<code>sudo systemctl start jBot.daemon.service</code>

If you want the daemon to start when system boots, run:  
<code>sudo systemctl enable jBot.daemon.service</code>
  
To read the logs:
<code>sudo journalctl -u jBot.daemon</code>
