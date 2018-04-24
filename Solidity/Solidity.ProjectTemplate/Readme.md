# How to reference project

Because `solidproj` cannot be referenced directly, you have to follow these steps:

1. Right click on your executable project `MyCSharpProject`. Select `Build dependencies` -> `Project Dependencies`. Select checkbox with `$safeprojectname$`.
2. Add following snipped to your `MyCSharpProject.csproj` in order to make solidity binaries copied in your output directory
```
  <Target Name="CopySolidityFilesAfterBuild" AfterTargets="PostBuildEvent">
    <ItemGroup>
      <BinFiles Include="$(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)\*.bin" />
      <AbiFiles Include="$(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)\*.abi" />
    </ItemGroup>

    <Error Text="No bin files found in path $(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)" Condition="'@(BinFiles)' == ''"></Error>
    <Error Text="No abi files found in path $(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)" Condition="'@(AbiFiles)' == ''"></Error>

    <Copy SourceFiles="@(BinFiles)" DestinationFolder="$(TargetDir)" SkipUnchangedFiles="true" />
    <Copy SourceFiles="@(AbiFiles)" DestinationFolder="$(TargetDir)" SkipUnchangedFiles="true" />
  </Target>

  <Target Name="CopySolidityFilesAfterPublish" AfterTargets="Publish">
    <ItemGroup>
      <BinFiles Include="$(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)\*.bin" />
      <AbiFiles Include="$(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)\*.abi" />
    </ItemGroup>

    <Error Text="No bin files found in path $(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)" Condition="'@(BinFiles)' == ''"></Error>
    <Error Text="No abi files found in path $(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)" Condition="'@(AbiFiles)' == ''"></Error>

    <Copy SourceFiles="@(BinFiles)" DestinationFolder="$(PublishDir)" SkipUnchangedFiles="true" />
    <Copy SourceFiles="@(AbiFiles)" DestinationFolder="$(PublishDir)" SkipUnchangedFiles="true" />
  </Target>
```
3. If you are working on .Net Core solution and you run publish from CLI make sure you are massing `SolutionDir` parameter:
```ps
dotnet publish /p:SolutionDir=C:\Full\Path\To\The\Project\ -c Release
```
Note that path ends with backslash `\`
4. Delete this readme