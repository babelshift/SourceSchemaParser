properties {
  $baseDir = resolve-path .
  $buildDir = "$baseDir\build"
  $sourceDir = "$baseDir\src"
  $artifactsDir = "$baseDir\artifacts"
  $global:config = "Debug"
}

task default -depends local
task local -depends init, compile
task ci -depends clean, release, local

task clean {
  Remove-Item "$artifactsDir" -recurse -force -ErrorAction SilentlyContinue | Out-Null
}

task init { 
  exec { .\build\dotnet\install.ps1 --Version "1.0.0-preview3-003171" | Out-Default }
  exec { dotnet --version | Out-Default }
}

task release {
  $global:config = "Release"
}

task compile -depends clean {
  $projectPath = "$sourceDir\SourceSchemaParser\project.json"

  $version = if ($env:APPVEYOR_BUILD_NUMBER -ne $NULL) { $env:APPVEYOR_BUILD_NUMBER } else { '0' }
  $version = "{0:D5}" -f [convert]::ToInt32($version, 10)

  exec { dotnet restore $projectPath }
  exec { dotnet build $projectPath -c $config }
  exec { dotnet pack $projectPath -c $config -o $artifactsDir --version-suffix $version }
}