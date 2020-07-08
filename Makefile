all: clean restore build publish

clean:
	dotnet clean

restore:
	dotnet restore

build:
	dotnet build

publish:
	dotnet publish jBot.Daemon/jBot.Daemon.csproj -r linux-arm -c Release /p:PublishSingleFile=true
	dotnet publish jBot.Daemon/jBot.Daemon.csproj -r win-x64 -c Release /p:PublishSingleFile=true
	dotnet publish jBot.Daemon/jBot.Daemon.csproj -r osx.10.15-x64 -c Release /p:PublishSingleFile=true
	dotnet publish jBot.TweetConsole/jBot.TweetConsole.csproj -r linux-arm -c Release /p:PublishSingleFile=true
	dotnet publish jBot.TweetConsole/jBot.TweetConsole.csproj -r win-x64 -c Release /p:PublishSingleFile=true
	dotnet publish jBot.TweetConsole/jBot.TweetConsole.csproj -r osx.10.15-x64 -c Release /p:PublishSingleFile=true

run:
	dotnet run
