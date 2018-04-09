# How to reference project

Because `solidproj` cannot be referenced directly, you have to follow these steps:

1. Right click on your executable project `MyCSharpProject`. Select `Build dependencies` -> `Project Dependencies`. Select checkbox with `$safeprojectname$`.
2. Add following snipped to your `MyCSharpProject.csproj` in order to make solidity binaries copied in your output directory
```
  <Target Name="CopySolidityFiles">
    <ItemGroup>
      <BinFiles Include="$(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)\*.bin" />
      <AbiFiles Include="$(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)\*.abi" />
    </ItemGroup>

    <Error Text="No bin files found" Condition="'@(BinFiles)' == ''"></Error>
    <Error Text="No abi files found" Condition="'@(AbiFiles)' == ''"></Error>

    <Copy SourceFiles="@(BinFiles)" DestinationFolder="$(TargetDir)" SkipUnchangedFiles="true" />
    <Copy SourceFiles="@(AbiFiles)" DestinationFolder="$(TargetDir)" SkipUnchangedFiles="true" />
  </Target>

  <Target Name="PostBuild" AfterTargets="PostBuildEvent" DependsOnTargets="CopySolidityFiles">
  </Target>

  <Target Name="AfterPublish" AfterTargets="Publish" DependsOnTargets="CopySolidityFiles">
  </Target>
```
3. If you are working on .Net Core solution and you run publish from CLI make sure you are massing `SolutionDir` parameter:
```ps
dotnet publish /p:SolutionDir=C:\Full\Path\To\The\Project\ -c Release
```
Rememer that path should end with backslash `\`

4. Delete this readme
