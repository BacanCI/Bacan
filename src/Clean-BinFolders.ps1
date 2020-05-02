# Deletes all bin, obj and .orig files
Get-ChildItem -Filter bin -Recurse | ?{ $_.PSIsContainer } | Remove-Item -recurse
Get-ChildItem -Filter obj -Recurse | ?{ $_.PSIsContainer } | Remove-Item -recurse
Get-ChildItem -Filter *.orig -Recurse | Remove-Item -recurse