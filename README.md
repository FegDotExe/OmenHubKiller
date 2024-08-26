# What is this?
This is a super sketchy utility app I wrote for windows out of pure frustration for the amount of services opened by single use apps, which brought much pain every time I wanted to terminate them.

The basic idea is that this software automates the process of killing many applications from the task manager, saving you much time for all your other precious activities.

# How does it work?
Once the `.exe` file is run, it will ask for an input file (which can be any utf-8 file; extension MUST be specified): that file should have the following format:
- If a line contains normal text, that will be interpreted as the name of a process which should be killed; you can view the process names by using the task manager app. The program will look for process ids associated with the given names and will attempt to kill them.
- If a line starts with `+`, that line will be interpreted as another file which should be considered. For example, let's say that you specified `1.txt` as the *killer* file and that `1.txt` contains a line which specifies `+2.txt`: `2.txt` will be considered as a *killer* file as well! The app currently has NO loopback protection, so if `2.txt` were to contain a line with `+1.txt`, the app would loop indefinitely until a stack overflow is reached.
- If a line contains a `#` character, all the text after that character will be considered as a comment. Even if you backslash it.

# Example!
I'll provide an example using killer files which I actually use with my pc:

The file `performance.txt` contains the following:
```
+omen.txt # Kills omen hub related tasks
OfficeClickToRun # Kills stupid office 365 stuff
mDNSResponder
+razer.txt # Kills razer related tasks
msedge
```

As you can see, the file uses `omen.txt` and `razer.txt` as references. `omen.txt` contains the following:
```
SystemOptimizer
OmenCommandCenterBackground
```
and `razer.txt` contains the following:
```
RzUpdateEngineService
CortexLauncherService
GameManagerService
GameManagerService3
RazerCortex
Razer Synapse Service
Razer Synapse Service Process
RazerCentralService
Razer Central
```