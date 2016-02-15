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
Root: "HKCU"; Subkey: "Software\OZOZ\RebarPos";

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

// Add registry entries of COM components after installation
procedure CurStepChanged(CurStep: TSetupStep);
var
  I: Integer;
  RegPath: String;
begin
  if CurStep = ssPostInstall then begin
    // Set registry keys
    (*
    // Type library
    RegPath := 'TypeLib\{26E9A3B0-6567-4857-AABB-E09AC4A7A8AE}';
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath, '', '');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\1.0', '', 'RebarPos 1.0 Type Library');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\1.0\0', '', '');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\1.0\0\win64', '', ExpandConstant('{app}\Bin\COMRebarPos.dbx'));
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\1.0\FLAGS', '', '0');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\1.0\HELPDIR', '', ExpandConstant('{app}\Bin'));
    
    // COM classes
    RegPath := 'CLSID\{97CAC17D-B1C7-49CA-8D57-D3FF491860FF}';
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath, '', 'ComRebarPos Class');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\Programmable', '', '');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\TypeLib', '', '{26E9A3B0-6567-4857-AABB-E09AC4A7A8AE}');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\InprocServer32', '', ExpandConstant('{app}\Bin\COMRebarPos.dbx'));
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\InprocServer32', 'ThreadingModel', 'Apartment');
    
    RegPath := 'CLSID\{BA77CFFF-0274-4D4C-BFE2-64A5731BAD37}';
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath, '', 'ComBOQTable Class');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\Programmable', '', '');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\TypeLib', '', '{26E9A3B0-6567-4857-AABB-E09AC4A7A8AE}');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\InprocServer32', '', ExpandConstant('{app}\Bin\COMRebarPos.dbx'));
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\InprocServer32', 'ThreadingModel', 'Apartment');  
    *);
  end;
end;

// Remove registry keys of COM classes when uninstalled
procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usPostUninstall then begin
    RegDeleteKeyIncludingSubkeys(HKEY_CLASSES_ROOT, 'TypeLib\{26E9A3B0-6567-4857-AABB-E09AC4A7A8AE}');
    RegDeleteKeyIncludingSubkeys(HKEY_CLASSES_ROOT, 'CLSID\{97CAC17D-B1C7-49CA-8D57-D3FF491860FF}');
    RegDeleteKeyIncludingSubkeys(HKEY_CLASSES_ROOT, 'CLSID\{BA77CFFF-0274-4D4C-BFE2-64A5731BAD37}');    
  end;
end;
