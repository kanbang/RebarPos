[Setup]
PrivilegesRequired=admin
AppName="RebarPosPluginSetup Debug"
AppVersion=1.0
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
DisableProgramGroupPage=yes
OutputBaseFilename=RebarPosPluginSetup Debug
OutputDir=Bin
DefaultDirName=GetCOMDirPath()
DisableDirPage=yes
UsePreviousAppDir=no
DisableReadyPage=yes
ShowLanguageDialog=no
WizardImageFile=LargelImage.bmp
WizardSmallImageFile=SmallImage.bmp
UsePreviousLanguage=no
SetupIconFile=Setup.ico
UninstallDisplayIcon=Setup.ico

[Registry]
; AutoCAD R20 (2015, 2016) ObjectDBX entries
Root: "HKLM"; Subkey: "SOFTWARE\Autodesk\ObjectDBX\R20.0\ActiveXCLSID"; ValueType: string; ValueName: "RebarPos"; ValueData: "{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}"; Flags: uninsdeletekey
Root: "HKLM"; Subkey: "SOFTWARE\Autodesk\ObjectDBX\R20.0\ActiveXCLSID"; ValueType: string; ValueName: "BOQTable"; ValueData: "{{BA77CFFF-0274-4d4c-BFE2-64A5731BAD35}"; Flags: uninsdeletekey
; AutoCAD R20 (2015, 2016) Type Library
Root: "HKCR"; Subkey: "TypeLib\{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}"; Flags: uninsdeletekey
Root: "HKCR"; Subkey: "TypeLib\{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}\1.0"; ValueType: string; ValueData: "ComRebarPos Type Library for AutoCAD R20"
Root: "HKCR"; Subkey: "TypeLib\{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}\1.0\0"
Root: "HKCR"; Subkey: "TypeLib\{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}\1.0\0\win64"; ValueType: string; ValueData: {code:GetCOMLibPath}
Root: "HKCR"; Subkey: "TypeLib\{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}\1.0\FLAGS"; ValueType: string; ValueData: "0"
Root: "HKCR"; Subkey: "TypeLib\{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}\1.0\HELPDIR"; ValueType: string; ValueData: {code:GetCOMDirPath}
; AutoCAD R20 (2015, 2016) COM Objects
Root: "HKCR"; Subkey: "COMREBARPOSLib.ComRebarPos2015.1"; ValueType: string; ValueData: "ComRebarPos Class for AutoCAD R20"; Flags: uninsdeletekey
Root: "HKCR"; Subkey: "COMREBARPOSLib.ComRebarPos2015.1\CLSID"; ValueType: string; ValueData: "{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}"
Root: "HKCR"; Subkey: "COMREBARPOSLib.ComRebarPos2015"; ValueType: string; ValueData: "ComRebarPos Class for AutoCAD R20"; Flags: uninsdeletekey
Root: "HKCR"; Subkey: "COMREBARPOSLib.ComRebarPos2015\CLSID"; ValueType: string; ValueData: "{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}"
Root: "HKCR"; Subkey: "COMREBARPOSLib.ComRebarPos2015\CurVer"; ValueType: string; ValueData: "COMREBARPOSLib.ComRebarPos2015.1"
Root: "HKCR"; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}"; ValueType: string; ValueData: "ComRebarPos Class for AutoCAD R20"; Flags: uninsdeletekey
Root: "HKCR"; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}\ProgID"; ValueType: string; ValueData: "COMREBARPOSLib.ComRebarPos2015.1"
Root: "HKCR"; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}\VersionIndependentProgID"; ValueType: string; ValueData: "COMREBARPOSLib.ComRebarPos2015"
Root: "HKCR"; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}\Programmable"
Root: "HKCR"; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}\InprocServer32"; ValueType: string; ValueData: {code:GetCOMLibPath}
Root: "HKCR"; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}\InprocServer32"; ValueType: string; ValueName: "ThreadingModel"; ValueData: "Apartment"
Root: "HKCR"; Subkey: "CLSID\{{97CAC17D-B1C7-49ca-8D57-D3FF491860F5}\TypeLib"; ValueType: string; ValueData: "{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}"
;
Root: "HKCR"; Subkey: "COMREBARPOSLib.ComBOQTable2015.1"; ValueType: string; ValueData: "ComBOQTable Class for AutoCAD R20"; Flags: uninsdeletekey
Root: "HKCR"; Subkey: "COMREBARPOSLib.ComBOQTable2015.1\CLSID"; ValueType: string; ValueData: "{{BA77CFFF-0274-4d4c-BFE2-64A5731BAD35}"
Root: "HKCR"; Subkey: "COMREBARPOSLib.ComBOQTable2015"; ValueType: string; ValueData: "ComBOQTable Class for AutoCAD R20"; Flags: uninsdeletekey
Root: "HKCR"; Subkey: "COMREBARPOSLib.ComBOQTable2015\CLSID"; ValueType: string; ValueData: "{{BA77CFFF-0274-4d4c-BFE2-64A5731BAD35}"
Root: "HKCR"; Subkey: "COMREBARPOSLib.ComBOQTable2015\CurVer"; ValueType: string; ValueData: "COMREBARPOSLib.ComBOQTable2015.1"
Root: "HKCR"; Subkey: "CLSID\{{BA77CFFF-0274-4d4c-BFE2-64A5731BAD35}"; ValueType: string; ValueData: "ComBOQTable Class for AutoCAD R20"; Flags: uninsdeletekey
Root: "HKCR"; Subkey: "CLSID\{{BA77CFFF-0274-4d4c-BFE2-64A5731BAD35}\ProgID"; ValueType: string; ValueData: "COMREBARPOSLib.ComBOQTable2015.1"
Root: "HKCR"; Subkey: "CLSID\{{BA77CFFF-0274-4d4c-BFE2-64A5731BAD35}\VersionIndependentProgID"; ValueType: string; ValueData: "COMREBARPOSLib.ComBOQTable2015"
Root: "HKCR"; Subkey: "CLSID\{{BA77CFFF-0274-4d4c-BFE2-64A5731BAD35}\Programmable"
Root: "HKCR"; Subkey: "CLSID\{{BA77CFFF-0274-4d4c-BFE2-64A5731BAD35}\InprocServer32"; ValueType: string; ValueData: {code:GetCOMLibPath}
Root: "HKCR"; Subkey: "CLSID\{{BA77CFFF-0274-4d4c-BFE2-64A5731BAD35}\InprocServer32"; ValueType: string; ValueName: "ThreadingModel"; ValueData: "Apartment"
Root: "HKCR"; Subkey: "CLSID\{{BA77CFFF-0274-4d4c-BFE2-64A5731BAD35}\TypeLib"; ValueType: string; ValueData: "{{26E9A3B0-6567-4857-AABB-E09AC4A7A8A5}"

[Dirs]
Name: "{userdocs}\RebarPos"

[Code]
function GetCOMDirPath(Param: String): String;
begin
  Result := ExpandFileName(AddBackslash(ExpandConstant('{#SourcePath}')) + '..\COMRebarPos\Debug-2015\');
end;

function GetCOMLibPath(Param: String): String;
begin
  Result := AddBackslash(GetCOMDirPath('')) + 'COMRebarPos.dbx';
end;
