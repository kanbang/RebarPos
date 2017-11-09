Rebar marking and scheduling entities and commands for AutoCAD 2010 and above.

Developer Setup
---------------
Download and install ObjectARX SDK files under "C:\Autodesk" so that you have:
* C:\Autodesk\ObjectARX 2018
* C:\Autodesk\ObjectARX 2017

Recent SDK Downloads: http://www.objectarx.com/

Install the corresponding build tools:
* Visual Studio 2015 (v140) for AutoCAD 2017 and 2018

Install corresponding boost libraries: http://www.boost.org/

Boost is assumed to be located `C:\Boost`. So that the include path is `C:\Boost\boost` and library paths are:
* `C:\Boost\lib64-msvc-14.0` for Visual Studio 2015

Application Compatibility
-------------------------
Following list defines compatibility between different AutoCAD API versions. See also: [AutoCAD 2018 online help](https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2018/ENU/AutoCAD-Customization/files/GUID-A6C680F2-DE2E-418A-A182-E4884073338A-htm.html)

| AutoCAD Release | Release Number | Supported .NET SDK       | .NET Framework | Toolset        |
|-----------------|----------------|--------------------------|----------------|----------------|
| AutoCAD 2018    | 22.0           | AutoCAD 2018             | 4.6            | VS 2015 (v140) |
| AutoCAD 2017    | 21.0           | AutoCAD 2017             | 4.6            | VS 2015 (v140) |
