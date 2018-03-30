# How to reference project

Because `solidproj` cannot be referenced directly, you have to follow these steps:

1. Right click on your executable project `MyCSharpProject`. Select `Build dependencies` -> `Project Dependencies`. Select checkbox with `$safeprojectname$`.
2. Add following snipped to post build events of `MyCSharpProject` in order to make solidity binaries copied in your output directory
```
xcopy "$(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)\*.abi" "$(TargetDir)" /Y /I
xcopy "$(SolutionDir)$safeprojectname$\bin\$(ConfigurationName)\*.bin" "$(TargetDir)" /Y /I
```
