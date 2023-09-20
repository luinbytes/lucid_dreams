--[[
    @title
        Lucid Dreams

    @author
        lu.

    @description
        automatically downloads and launches Lucid Dreams upon universe4 launch.
        Thanks to typedef for the base.
--]]
local lucid_dreams = {

    -- url to download the .zip file
    url = "https://github.com/luinbytes/lucid_dreams/releases/download/stable/lucid_dreams.zip",

    -- file name
    file = "lucid_dreams.exe"

}

function lucid_dreams.on_loaded( script, sessions )

    -- debug
    fantasy.log( "Downloading lucid_dreams.zip..." )

    -- get required modules module
    local http = fantasy.http() -- http module
    local file = fantasy.file() -- file module

    -- download .zip to file
    http:to_file( lucid_dreams.url, lucid_dreams.file )

    -- check if our downloaded file exists
    if not file:exists( lucid_dreams.file ) then
        fantasy.log( "Download failed!" )
        return false
    end

    -- linux work
    if fantasy.os == "linux" then

        -- linux user detected
        fantasy.log("This is a windows only script!")

    else

        -- unzip the file in powershell
        fantasy.terminal( string.format( "powershell \"Expand-Archive %s -Force -D %s\"", lucid_dreams.file, sessions.directory ) )
        -- run the file
        fantasy.terminal( "powershell \"Start-Process -FilePath lucid_dreams.exe\"" )
        fantasy.log( "Lucid Dreams Launched!" )

    end

end

return lucid_dreams