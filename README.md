Rebar marking and scheduling entities and commands for AutoCAD 2010 and above.

Developer Setup
---------------
Download and install ObjectARX SDK files under "C:\Autodesk" so that you have:
* C:\Autodesk\ObjectARX 2010
* C:\Autodesk\ObjectARX 2013
* C:\Autodesk\ObjectARX 2015

Old SDK Downloads: [2010](http://download.autodesk.com/akdlm/esd/dlm/objectarx/ObjectARX_2010_Win_64_and_32Bit.exe) | [2011](http://download.autodesk.com/esd/objectarx/2011/ObjectARX_2011_Win_64_and_32Bit.exe) | [2012](http://download.autodesk.com/esd/objectarx/2012/ObjectARX_2012_Win_64_and_32Bit.exe) | [2013](http://download.autodesk.com/esd/objectarx/2013/ObjectARX_2013_Win_64_and_32Bit.exe)

Recent SDK Downloads: http://www.objectarx.com/

Install the corresponding build tools:
* Visual Studio 2012 (v110) for AutoCAD 2015 and 2016
* Visual Studio 2010 (v100) for AutoCAD 2013 and 2014
* Visual Studio 2008 (v90) for AutoCAD 2010, 2011 and 2012

Install corresponding boost libraries: http://www.boost.org/

Boost is assumed to be located `C:\Boost`. So that the include path is `C:\Boost\boost` and library paths are: `C:\Boost\lib\vc90` for Visual Studio 2008, `C:\Boost\lib\vc100` for Visual Studio 2010 and `C:\Boost\lib\vc110` for Visual Studio 2012.

Application Compatibility
-------------------------
Following list copied from [AutoCAD 2016 online help](http://help.autodesk.com/view/ACD/2016/ENU/?guid=GUID-D54B0935-1638-4F97-8B37-1EC3635A1E71) defines compatibility between different AutoCAD API versions.

| AutoCAD Release | Supported .NET and ObjectARX SDK         | .NET Framework |
|-----------------|------------------------------------------|----------------|
| AutoCAD 2016    | AutoCAD 2015, AutoCAD 2016               | 4.5            |
| AutoCAD 2015    | AutoCAD 2015                             | 4.5            |
| AutoCAD 2014    | AutoCAD 2013, AutoCAD 2014               | 4.0            |
| AutoCAD 2013    | AutoCAD 2013                             | 4.0            |
| AutoCAD 2012    | AutoCAD 2010, AutoCAD 2011, AutoCAD 2012 | 3.51 SP1       |
| AutoCAD 2011    | AutoCAD 2010, 2011                       | 3.51 SP1       |
| AutoCAD 2010    | AutoCAD 2010                             | 3.51 SP1       |

As a result, the application is compiled for the following configurations:

| App. Release | ObjectARX SDK | .NET Framework | Toolset        | Supported AutoCAD Versions |
|--------------|---------------|----------------|----------------|--------------------------|
| Release-2015 | AutoCAD 2015  | 4.5            | VS 2012 (v110) | AutoCAD 2015, 2016       |
| Release-2013 | AutoCAD 2013  | 4.0            | VS 2010 (v100) | AutoCAD 2013, 2014       |
| Release-2010 | AutoCAD 2010  | 3.51 SP1       | VS 2008 (v90)  | AutoCAD 2010, 2011, 2012 |
