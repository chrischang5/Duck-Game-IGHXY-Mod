# If Duck Game is currently not running, start it up.

if((get-process "DuckGame" -ea SilentlyContinue) -eq $Null){  
    echo "Duck Game is not running. Starting it up!"
}
# If it is running, close it and reopen it
else{ 
    echo "Stopping Duck Game."
    Stop-Process -Name "DuckGame"
 }
 # Thank you to u/Hrambert for the help on this
 Start-Process -FilePath "C:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe" -ArgumentList "-nointro", "-startineditor", "nofullscreen"