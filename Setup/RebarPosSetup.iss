#dim Version[4]
#expr ParseVersion("..\RebarPosCommands\Release-2015\RebarPosCommands.dll", Version[0], Version[1], Version[2], Version[3])
#define AppVersion Str(Version[0]) + "." + Str(Version[1]) + "." + Str(Version[2]) + "." + Str(Version[3])
#define ShortAppVersion Str(Version[0]) + "." + Str(Version[1])

[Setup]
PrivilegesRequired=admin
AppName="{cm:AppName}"
AppVersion={#ShortAppVersion}
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
DisableProgramGroupPage=yes
OutputBaseFilename=RebarPosPluginSetup v{#ShortAppVersion}
OutputDir=Bin
DisableDirPage=yes
DefaultDirName={userappdata}\Autodesk\ApplicationPlugins\RebarPos.bundle
UsePreviousAppDir=no
DisableReadyPage=yes
ShowLanguageDialog=no
WizardImageFile=LargelImage.bmp
WizardSmallImageFile=SmallImage.bmp
UsePreviousLanguage=no
SetupIconFile=Setup.ico

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"
Name: "tr"; MessagesFile: "Turkish.isl"

[InstallDelete]
Type: filesandordirs; Name: "{app}\Contents\2010"
Type: filesandordirs; Name: "{app}\Contents\2013"
Type: filesandordirs; Name: "{app}\Contents\2015"

[Files]
; Application manifest
Source: "..\Package\PackageContents.xml"; DestDir: "{app}"; Flags: ignoreversion; AfterInstall: PreparePackageXML('{#ShortAppVersion}')
; AutoCAD R18 (2010, 2011, 2012) Libraries
Source: "..\NativeRebarPos\Release-2010\NativeRebarPos.dbx"; DestDir: "{app}\Bin\2010"; Flags: ignoreversion
Source: "..\COMRebarPos\Release-2010\COMRebarPos.dbx"; DestDir: "{app}\Bin\2010"; Flags: ignoreversion
Source: "..\ManagedRebarPos\Release-2010\ManagedRebarPos.dll"; DestDir: "{app}\Bin\2010"; Flags: ignoreversion
Source: "..\RebarPosCommands\Release-2010\RebarPosCommands.dll"; DestDir: "{app}\Bin\2010"; Flags: ignoreversion
; AutoCAD R19 (2013, 2014) Libraries
Source: "..\NativeRebarPos\Release-2013\NativeRebarPos.dbx"; DestDir: "{app}\Bin\2013"; Flags: ignoreversion
Source: "..\COMRebarPos\Release-2013\COMRebarPos.dbx"; DestDir: "{app}\Bin\2013"; Flags: ignoreversion
Source: "..\ManagedRebarPos\Release-2013\ManagedRebarPos.dll"; DestDir: "{app}\Bin\2013"; Flags: ignoreversion
Source: "..\RebarPosCommands\Release-2013\RebarPosCommands.dll"; DestDir: "{app}\Bin\2013"; Flags: ignoreversion
; AutoCAD R20 (2015, 2016) Libraries
Source: "..\NativeRebarPos\Release-2015\NativeRebarPos.dbx"; DestDir: "{app}\Bin\2015"; Flags: ignoreversion
Source: "..\COMRebarPos\Release-2015\COMRebarPos.dbx"; DestDir: "{app}\Bin\2015"; Flags: ignoreversion
Source: "..\ManagedRebarPos\Release-2015\ManagedRebarPos.dll"; DestDir: "{app}\Bin\2015"; Flags: ignoreversion
Source: "..\RebarPosCommands\Release-2015\RebarPosCommands.dll"; DestDir: "{app}\Bin\2015"; Flags: ignoreversion
; Menu resources
Source: "..\Menu\*.cuix"; DestDir: "{app}\Resources"
Source: "..\MenuIcons\Release\MenuIcons.dll"; DestDir: "{app}\Resources"; DestName: "RebarPos.dll"
Source: "..\MenuIcons_Light\Release\MenuIcons_Light.dll"; DestDir: "{app}\Resources"; DestName: "RebarPos_light.dll"
Source: "..\Package\icon.bmp"; DestDir: "{app}\Resources"

[Registry]
Root: HKCU; Subkey: "Software\OZOZ\RebarPos";
Root: HKCU; Subkey: "Software\OZOZ\RebarPos"; ValueType: string; ValueName: InstallPath; ValueData: {app};
; AutoCAD R20 (2015, 2016) ObjectDBX entries
Root: HKLM; Subkey: "SOFTWARE\Autodesk\ObjectDBX\R20.0\ActiveXCLSID"; ValueType: string; ValueName: RebarPos; ValueData: "{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}";
Root: HKLM; Subkey: "SOFTWARE\Autodesk\ObjectDBX\R20.1\ActiveXCLSID"; ValueType: string; ValueName: RebarPos; ValueData: "{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}";
; AutoCAD R20 (2015, 2016) Type Library
Root: HKCR; Subkey: "TypeLib\{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}";
Root: HKCR; Subkey: "TypeLib\{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}\1.0"; ValueType: string; ValueData: "ComRebarPos Type Library for AutoCAD R20";
Root: HKCR; Subkey: "TypeLib\{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}\1.0\0";
Root: HKCR; Subkey: "TypeLib\{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}\1.0\0\win64"; ValueType: string; ValueData: "{app}\Bin\2015\COMRebarPos.dbx";
Root: HKCR; Subkey: "TypeLib\{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}\1.0\FLAGS"; ValueType: string; ValueData: "0";
Root: HKCR; Subkey: "TypeLib\{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}\1.0\HELPDIR"; ValueType: string; ValueData: "{app}\Bin\2015";
; AutoCAD R20 (2015, 2016) COM Objects
Root: HKCR; Subkey: "COMREBARPOSLib.ComRebarPos2015.1"; ValueType: string; ValueData: "ComRebarPos Class for AutoCAD R20";
Root: HKCR; Subkey: "COMREBARPOSLib.ComRebarPos2015.1\CLSID"; ValueType: string; ValueData: "{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}";
Root: HKCR; Subkey: "COMREBARPOSLib.ComRebarPos2015"; ValueType: string; ValueData: "ComRebarPos Class for AutoCAD R20";
Root: HKCR; Subkey: "COMREBARPOSLib.ComRebarPos2015\CLSID"; ValueType: string; ValueData: "{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}";
Root: HKCR; Subkey: "COMREBARPOSLib.ComRebarPos2015\CurVer"; ValueType: string; ValueData: "COMREBARPOSLib.ComRebarPos2015.1";
Root: HKCR; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}"; ValueType: string; ValueData: "ComRebarPos Class for AutoCAD R20";
Root: HKCR; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}\ProgID"; ValueType: string; ValueData: "COMREBARPOSLib.ComRebarPos2015.1";
Root: HKCR; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}\VersionIndependentProgID"; ValueType: string; ValueData: "COMREBARPOSLib.ComRebarPos2015";
Root: HKCR; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}\Programmable";
Root: HKCR; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}\InprocServer32"; ValueType: string; ValueData: "{app}\Bin\2015\COMRebarPos.dbx";
Root: HKCR; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}\InprocServer32"; ValueType: string; ValueName: ThreadingModel; ValueData: "Apartment";
Root: HKCR; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}\TypeLib"; ValueType: string; ValueData: "{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}";

[Dirs]
Name: "{userdocs}\RebarPos"

[CustomMessages]
tr.AppName=Donatý Pozlandýrma ve Metraj Komutlarý AutoCAD Eklentisi (64 bit)
AppName=Rebar Marking and Scheduling AutoCAD Plugin (64 bit)

[Code]
// Applies setup modifications to the package manifest file
procedure PreparePackageXML(Version: String);
var
  XMLDoc, RootNode: Variant;
  FileName: String;
begin
  FileName := ExpandConstant(CurrentFileName);
  XMLDoc := CreateOleObject('MSXML2.DOMDocument');
  XMLDoc.async := False;
  XMLDoc.resolveExternals := False;
  XMLDoc.load(FileName);
  
  RootNode := XMLDoc.documentElement;
  RootNode.setAttribute('AppVersion', Version);

  XMLDoc.Save(FileName);
end;
