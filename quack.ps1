if((get-process "DuckGame" -ea SilentlyContinue) -eq $Null){ 
        echo "Duck Game is not running. Doing nothing."
}

else{ 
    echo "Stopping Duck Game."
    Stop-Process -Name "DuckGame"
 }