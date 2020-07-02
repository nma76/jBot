# jBOT, TwitterBot
This is my implementation of a Twitter-bot, written in c# (NET Core 3), that looks for hashtags and response to them with defined actions.

## Projects   
### jBot.Lib  
Contains all logic for the actual twitter communictaion. Implement your own actions by modifying Capabilities.cs and ActionHandler.cs.

### jBot.TweetConsole
Is a console app that runs once and quits. I use this mostly fpr development and debuging.

**Installation**  
The only installation required is adding correct values to the apsettings.json (there is an example file in the repo). If you want to use the included action to add overlay images you'll need to copy the image to the folder configured.

### jBot.Daemon
Is i Linux daemon that runs continously.

**Installation**    
You should probably add a user to your system that runs the daemon. In this example i use user pi (since i run this app i a raspberryPi)  
  
Create a directory for the daemon:  
<code>mkdir /opt/jBot.Daemon</code>
  
Set permissions on the directory, if using a separate account:  
<code>chown pi:pi jBot.Daemon/</code>
  
copy the application to the newly created directory. Locally this can be done with something like this:  
<code>cp -r * /opt/jBot.Daemon</code>
  
or to a remote system, something like this:  
<code>
scp -r * pi@192.168.1.101:/opt/jBot.Daemon  
</code>
  
Create configuration files for systemd:  
<code>nano /etc/systemd/system/jBot.daemon.service</code>

Content of file, something like this:
[Unit]  
Description=jBot daemon  
DefaultDependencies=no  
   
[Service]  
Type=notify  
ExecStart=/usr/share/dotnet/dotnet jBot.Daemon.dll  
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
