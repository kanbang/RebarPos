#dim Version[4]
#expr ParseVersion("..\RebarPosCommands\Release-2018\x64\RebarPosCommands.dll", Version[0], Version[1], Version[2], Version[3])
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
UninstallDisplayIcon=Setup.ico

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"
Name: "tr"; MessagesFile: "Turkish.isl"

[InstallDelete]
Type: filesandordirs; Name: "{app}\Bin"
Type: filesandordirs; Name: "{app}\Contents"

[Files]
; Application manifest
Source: ".\PackageContents.xml"; DestDir: "{app}"; Flags: ignoreversion; AfterInstall: PreparePackageXML('{#ShortAppVersion}')
; Package icon
Source: ".\icon.bmp"; DestDir: "{app}\Resources"
; AutoCAD R22 (2018) Libraries
Source: "..\NativeRebarPos\Release-2018\x64\NativeRebarPos.dbx"; DestDir: "{app}\Bin\2018\x64"; Flags: ignoreversion
Source: "..\COMRebarPos\Release-2018\x64\COMRebarPos.dbx"; DestDir: "{app}\Bin\2018\x64"; Flags: ignoreversion
Source: "..\ManagedRebarPos\Release-2018\x64\ManagedRebarPos.dll"; DestDir: "{app}\Bin\2018\x64"; Flags: ignoreversion
Source: "..\RebarPosCommands\Release-2018\x64\RebarPosCommands.dll"; DestDir: "{app}\Bin\2018\x64"; Flags: ignoreversion
Source: "..\RebarPosCommands\Libraries\LicenseCheck.dll"; DestDir: "{app}\Bin\2018\x64"; Flags: ignoreversion
; AutoCAD R21 (2017) Libraries
Source: "..\NativeRebarPos\Release-2017\x64\NativeRebarPos.dbx"; DestDir: "{app}\Bin\2017\x64"; Flags: ignoreversion
Source: "..\COMRebarPos\Release-2017\x64\COMRebarPos.dbx"; DestDir: "{app}\Bin\2017\x64"; Flags: ignoreversion
Source: "..\ManagedRebarPos\Release-2017\x64\ManagedRebarPos.dll"; DestDir: "{app}\Bin\2017\x64"; Flags: ignoreversion
Source: "..\RebarPosCommands\Release-2017\x64\RebarPosCommands.dll"; DestDir: "{app}\Bin\2017\x64"; Flags: ignoreversion
Source: "..\RebarPosCommands\Libraries\LicenseCheck.dll"; DestDir: "{app}\Bin\2017\x64"; Flags: ignoreversion
; Cuix file
Source: "..\Menu\RebarPos.cuix"; DestDir: "{app}\Resources\2017"
Source: "..\Menu\RebarPos.cuix"; DestDir: "{app}\Resources\2018"
; Menu resources
Source: "..\MenuIcons\Release\MenuIcons.dll"; DestDir: "{app}\Resources\2017"; DestName: "RebarPos.dll"
Source: "..\MenuIcons_Light\Release\MenuIcons_Light.dll"; DestDir: "{app}\Resources\2017"; DestName: "RebarPos_light.dll"
Source: "..\MenuIcons\Release\MenuIcons.dll"; DestDir: "{app}\Resources\2018"; DestName: "RebarPos.dll"
Source: "..\MenuIcons_Light\Release\MenuIcons_Light.dll"; DestDir: "{app}\Resources\2018"; DestName: "RebarPos_light.dll"

[Registry]
Root: HKCU; Subkey: "Software\OZOZ\RebarPos";
Root: HKCU; Subkey: "Software\OZOZ\RebarPos"; ValueType: string; ValueName: InstallPath; ValueData: {app};

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

// Registers DBX entitites
procedure AddDBXRegistry(AcVersion: String; ComRebarPosGUID: String; ComBOQTableGUID: String);
var
  RegPath: String;
begin
  // e.g. HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\ObjectDBX\R20.1\ActiveXCLSID
  RegPath := 'SOFTWARE\Autodesk\ObjectDBX\' + AcVersion + '\ActiveXCLSID';
  RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'RebarPos', ComRebarPosGUID);
  RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'BOQTable', ComBOQTableGUID);
end;

// Unregisters DBX entitites
procedure RemoveDBXRegistry(AcVersion: String);
var
  RegPath: String;
begin
  // e.g. HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\ObjectDBX\R20.1\ActiveXCLSID
  RegPath := 'SOFTWARE\Autodesk\ObjectDBX\' + AcVersion + '\ActiveXCLSID';
  RegDeleteValue(HKEY_LOCAL_MACHINE, RegPath, 'RebarPos');
  RegDeleteValue(HKEY_LOCAL_MACHINE, RegPath, 'BOQTable');
end;

// Registers COM components
procedure AddCOMRegistry(AcVersion: String; TypeLibGUID: String; ComRebarPosGUID: String; ComBOQTableGUID: String);
var
  RegPath: String;
begin
  // Type library
  RegPath := 'TypeLib\' + TypeLibGUID;
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath, '', '');
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\1.0', '', 'RebarPos ' + AcVersion + ' 1.0 Type Library');
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\1.0\0', '', '');
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\1.0\0\win64', '', ExpandConstant('{app}\Bin\' + AcVersion + '\x64\COMRebarPos.dbx'));
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\1.0\0\win32', '', ExpandConstant('{app}\Bin\' + AcVersion + '\win32\COMRebarPos.dbx'));
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\1.0\FLAGS', '', '0');
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\1.0\HELPDIR', '', ExpandConstant('{app}\Bin' + AcVersion));
    
  // COM classes
  RegPath := 'CLSID\' + ComRebarPosGUID;
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath, '', 'ComRebarPos Class ' + AcVersion);
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\Programmable', '', '');
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\TypeLib', '', TypeLibGUID);
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\InprocServer32', '', ExpandConstant('{app}\Bin\' + AcVersion + '\x64\COMRebarPos.dbx'));
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\InprocServer32', 'ThreadingModel', 'Apartment');
    
  RegPath := 'CLSID\' + ComBOQTableGUID;
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath, '', 'ComBOQTable Class ' + AcVersion);
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\Programmable', '', '');
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\TypeLib', '', TypeLibGUID);
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\InprocServer32', '', ExpandConstant('{app}\Bin\' + AcVersion + '\x64\COMRebarPos.dbx'));
  RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\InprocServer32', 'ThreadingModel', 'Apartment');  
end;

// Unregisters COM components
procedure RemoveCOMRegistry(TypeLibGUID: String; ComRebarPosGUID: String; ComBOQTableGUID: String);
begin
  // Type library
  RegDeleteKeyIncludingSubkeys(HKEY_CLASSES_ROOT, 'TypeLib\' + TypeLibGUID);    
  // COM classes
  RegDeleteKeyIncludingSubkeys(HKEY_CLASSES_ROOT, 'CLSID\' + ComRebarPosGUID);
  RegDeleteKeyIncludingSubkeys(HKEY_CLASSES_ROOT, 'CLSID\' + ComBOQTableGUID);    
end;
  
// Add registry entries of COM components after installation
procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then begin
    // Set registry keys for COM classes
    // 2018
    AddCOMRegistry('2018', '{26E9A3B0-6567-4857-AABB-E09AC4A7A8A8}', '{97CAC17D-B1C7-49CA-8D57-D3FF491860F8}', '{BA77CFFF-0274-4D4C-BFE2-64A5731BAD38}');
    AddDBXRegistry('R22.0', '{97CAC17D-B1C7-49CA-8D57-D3FF491860F8}', '{BA77CFFF-0274-4D4C-BFE2-64A5731BAD38}');
    // 2017
    AddCOMRegistry('2017', '{26E9A3B0-6567-4857-AABB-E09AC4A7A8A7}', '{97CAC17D-B1C7-49CA-8D57-D3FF491860F7}', '{BA77CFFF-0274-4D4C-BFE2-64A5731BAD37}');
    AddDBXRegistry('R21.0', '{97CAC17D-B1C7-49CA-8D57-D3FF491860F7}', '{BA77CFFF-0274-4D4C-BFE2-64A5731BAD37}');
  end;
end;

// Remove registry keys of COM classes when uninstalled
procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usPostUninstall then begin
    // 2018
    RemoveCOMRegistry('{26E9A3B0-6567-4857-AABB-E09AC4A7A8A8}', '{97CAC17D-B1C7-49CA-8D57-D3FF491860F8}', '{BA77CFFF-0274-4D4C-BFE2-64A5731BAD38}');
    RemoveDBXRegistry('R22.0');
    // 2017
    RemoveCOMRegistry('{26E9A3B0-6567-4857-AABB-E09AC4A7A8A7}', '{97CAC17D-B1C7-49CA-8D57-D3FF491860F7}', '{BA77CFFF-0274-4D4C-BFE2-64A5731BAD37}');
    RemoveDBXRegistry('R21.0');
  end;
end;