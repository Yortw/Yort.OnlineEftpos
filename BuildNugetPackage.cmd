del /F /Q /S *.CodeAnalysisLog.xml

".nuget\NuGet.exe" pack -sym Yort.OnlineEftpos.nuspec -BasePath .\
pause

copy *.nupkg C:\Nuget.LocalRepository\
pause
