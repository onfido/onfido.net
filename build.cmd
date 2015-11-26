rd .\bin /S /Q
md .\bin

msbuild src/Onfido.NET.sln /p:Configuration=Release
copy .\Onfido\bin\Release\Onfido.dll .\bin\