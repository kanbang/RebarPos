Rebar marking and scheduling entities and commands for AutoCAD 2010 and above.

Developer Setup
---------------
Download and install ObjectARX SDK files under "C:\Autodesk" so that you have:
* C:\Autodesk\ObjectARX 2010
* C:\Autodesk\ObjectARX 2013
* C:\Autodesk\ObjectARX 2015
* C:\Autodesk\ObjectARX 2017

Old SDK Downloads: [2010](http://download.autodesk.com/akdlm/esd/dlm/objectarx/ObjectARX_2010_Win_64_and_32Bit.exe) | [2011](http://download.autodesk.com/esd/objectarx/2011/ObjectARX_2011_Win_64_and_32Bit.exe) | [2012](http://download.autodesk.com/esd/objectarx/2012/ObjectARX_2012_Win_64_and_32Bit.exe) | [2013](http://download.autodesk.com/esd/objectarx/2013/ObjectARX_2013_Win_64_and_32Bit.exe)

Recent SDK Downloads: http://www.objectarx.com/

Install the corresponding build tools:
* Visual Studio 2015 (v140) for AutoCAD 2017 and 2018
* Visual Studio 2012 (v110) for AutoCAD 2015 and 2016
* Visual Studio 2010 (v100) for AutoCAD 2013 and 2014
* Visual Studio 2008 (v90) for AutoCAD 2010, 2011 and 2012

Install corresponding boost libraries: http://www.boost.org/

Boost is assumed to be located `C:\Boost`. So that the include path is `C:\Boost\boost` and library paths are:
* `C:\Boost\lib64-msvc-14.0` for Visual Studio 2015
* `C:\Boost\lib64-msvc-12.0` for Visual Studio 2012
* `C:\Boost\lib64-msvc-10.0` for Visual Studio 2010
* `C:\Boost\lib64-msvc-9.0` for Visual Studio 2008

Application Compatibility
-------------------------
Following list defines compatibility between different AutoCAD API versions. See also: [AutoCAD 2018 online help](https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2018/ENU/AutoCAD-Customization/files/GUID-A6C680F2-DE2E-418A-A182-E4884073338A-htm.html)

| AutoCAD Release | Release Number | Supported .NET SDK       | .NET Framework | Toolset        |
|-----------------|----------------|--------------------------|----------------|----------------|
| AutoCAD 2018    | 22.0           | AutoCAD 2018             | 4.6            | VS 2015 (v140) |
| AutoCAD 2017    | 21.0           | AutoCAD 2017             | 4.6            | VS 2015 (v140) |
| AutoCAD 2016    | 20.1           | AutoCAD 2015, 2016       | 4.5            | VS 2012 (v110) |
| AutoCAD 2015    | 20.0           | AutoCAD 2015             | 4.5            | VS 2012 (v110) |
| AutoCAD 2014    | 19.1           | AutoCAD 2013, 2014       | 4.0            | VS 2010 (v100) |
| AutoCAD 2013    | 19.0           | AutoCAD 2013             | 4.0            | VS 2010 (v100) |
| AutoCAD 2012    | 18.2           | AutoCAD 2010, 2011, 2012 | 3.51 SP1       | VS 2008 (v90)  |
| AutoCAD 2011    | 18.1           | AutoCAD 2010, 2011	      | 3.51 SP1       | VS 2008 (v90)  |
| AutoCAD 2010    | 18.0           | AutoCAD 2010             | 3.51 SP1       | VS 2008 (v90)  |
