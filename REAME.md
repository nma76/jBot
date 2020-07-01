# jBOT, TwitterBot
This is my implementation of a Twitter-bot that looks for hashtags and response to them

# Projects   
**jBot.Lib**  
Contains all logic for the actual twitter communictaion

**jBot.TweetConsole**  
Is a console app that runs once and quits

**jBot.Daemon**  
Is i Linux daemon that runs continously

***Installation***    
mkdir /opt/jBot.Daemon  
cp -r * /opt/jBot.Daemon  
  
nano /etc/systemd/system/jBot.daemon.service  

[Unit]  
Description=jBot daemon  
DefaultDependencies=no  
   
[Service]  
Type=oneshot  
RemainAfterExit=no  
ExecStart=/usr/share/dotnet/dotnet jBot.daemon.dll  
WorkingDirectory=/opt/jBot.Daemon  
   
[install]





sudo dotnet publish /p:Configuration=Release -f netcoreapp3.1