# Bug - Test 32bit process fails in .NET6

The unit test `UnitTest.Test32bit.TheTest` fails when launched via `dotnet test` cli when using .NET6.

```
$ cd UnitTest\bin\Debug\net6.0
$ dotnet test .\UnitTest.dll

> Microsoft (R) Test Execution Command Line Tool Version 17.0.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
  Failed TheTest [277 ms]
  Error Message:
     Expected: True
  But was:  False

  Stack Trace:
     at UnitTest.Test32Bit.TheTest() in C:\tibco\Source\TestProcess32\UnitTest\Test32Bit.cs:line 27

  Standard Output Messages:
 32bit stdout:
 32bit err: Failed to load the dll from [C:\Program Files\dotnet\host\fxr\6.0.0\hostfxr.dll], HRESULT: 0x800700C1
 The library hostfxr.dll was found, but loading it from C:\Program Files\dotnet\host\fxr\6.0.0\hostfxr.dll failed
   - Installing .NET prerequisites might help resolve this problem.
      https://go.microsoft.com/fwlink/?linkid=798306

Failed!  - Failed:     1, Passed:     0, Skipped:     0, Total:     1, Duration: 277 ms - UnitTest.dll (net6.0)
```

Running the `TestProcess` program works as expected:

```
dotnet run --project .\TestProcess

32bit stdout: Am I 32bit?: True

32bit err:
```

Setting `COREHOST_TRACE` and `COREHOST_TRACEFILE` produces the following tace file: [trace.txt](trace.txt)
Looking at the tail of the file it looks like the dll is being loaded with win-x64 identifier:

```
Property NATIVE_DLL_SEARCH_DIRECTORIES = C:\Program Files\dotnet\shared\Microsoft.NETCore.App\6.0.0\;
Property PLATFORM_RESOURCE_ROOTS = C:\TestProcess32\UnitTest\bin\Debug\net6.0\;
Property APP_CONTEXT_BASE_DIRECTORY = C:\TestProcess32\UnitTest\bin\Debug\net6.0\
Property APP_CONTEXT_DEPS_FILES = C:\TestProcess32\UnitTest\bin\Debug\net6.0\UnitTest.deps.json;C:\Program Files\dotnet\shared\Microsoft.NETCore.App\6.0.0\Microsoft.NETCore.App.deps.json
Property PROBING_DIRECTORIES = 
Property RUNTIME_IDENTIFIER = win10-x64
Property System.Reflection.Metadata.MetadataUpdater.IsSupported = false
CoreCLR path = 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\6.0.0\coreclr.dll', CoreCLR dir = 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\6.0.0\'
Loaded library from C:\Program Files\dotnet\shared\Microsoft.NETCore.App\6.0.0\coreclr.dll
Launch host: C:\TestProcess32\UnitTest\bin\Debug\net6.0\testhost.exe, app: C:\TestProcess32\UnitTest\bin\Debug\net6.0\testhost.dll, argc: 10, args: --port,62640,--endpoint,127.0.0.1:062640,--role,client,--parentprocessid,142724,--telemetryoptedin,false,
--- Begin breadcrumb write
Breadcrumbs will be written using a background thread
Breadcrumb thread write callback...
--- End breadcrumb write 1
Tracing enabled @ Tue Nov 16 13:42:45 2021 GMT
--- Invoked apphost [version: 6.0.0, commit hash: 4822e3c3aa77eb82b2fb33c9321f923cf11ddde6] main = {
TestProcess32.exe
}
Redirecting errors to custom writer.
The managed DLL bound to this executable is: 'TestProcess32.dll'
Using environment variable DOTNET_ROOT=[C:\Program Files\dotnet] as runtime location.
Reading fx resolver directory=[C:\Program Files\dotnet\host\fxr]
Considering fxr version=[2.1.30]...
Considering fxr version=[5.0.12]...
Considering fxr version=[6.0.0]...
Detected latest fxr version=[C:\Program Files\dotnet\host\fxr\6.0.0]...
Resolved fxr [C:\Program Files\dotnet\host\fxr\6.0.0\hostfxr.dll]...
Failed to load the dll from [C:\Program Files\dotnet\host\fxr\6.0.0\hostfxr.dll], HRESULT: 0x800700C1
The library hostfxr.dll was found, but loading it from C:\Program Files\dotnet\host\fxr\6.0.0\hostfxr.dll failed
  - Installing .NET prerequisites might help resolve this problem.
     https://go.microsoft.com/fwlink/?linkid=798306
```

