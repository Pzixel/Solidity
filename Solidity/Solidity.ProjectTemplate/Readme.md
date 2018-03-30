# How to reference project

Because `solidproj` cannot be referenced directly, you have to follow these steps:

1. Right click on your executable project `MyCSharpProject`. Select `Build dependencies` -> `Project Dependencies`. Select checkbox with `$safeprojectname$`.
2. Add following snipped to post build events of `MyCSharpProject` in order to make solidity binaries copied in your output directory
```
xcopy "$(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)\*.abi" "$(TargetDir)" /Y /I
xcopy "$(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)\*.bin" "$(TargetDir)" /Y /I
```
3. If you are working on .Net Core solution then edit your `csproj` file and add following code snippet to make it work with `dotnet publish`
```xml
<Target Name="CopySolidityFiles" AfterTargets="Publish">
   <Exec Command="xcopy &quot;$(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)\*.abi&quot; &quot;$(PublishDir)&quot; /Y /I&#xA;&#xA;&#xD;&#xA;xcopy &quot;$(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)\*.bin&quot; &quot;$(PublishDir)&quot; /Y /I" />
</Target>
```
Note that `SolutionDir` parameter is required to make postbuild scripts work. If you build it outside Visual Studio, don't forget to specify it manually, e.g.
```ps
dotnet publish /p:SolutionDir=C:\Full\Path\To\The\Project\ -c Release
```
Rememer that path should end with backslash `\`
4. Delete this readme