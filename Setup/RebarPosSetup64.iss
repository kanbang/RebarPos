[Setup]
ShowLanguageDialog=no
AppName=RebarPos x64
AppVersion=1.0
DefaultDirName={pf}\SahinEng\RebarPos
ArchitecturesInstallIn64BitMode=x64
AppCopyright=Þahin Inþaat
DisableProgramGroupPage=yes
AppPublisher=Þahin Ýnþaat
AppPublisherURL=http://www.sahin-ins.com
AppSupportPhone=+90 (212) 210 45 04
OutputBaseFilename=RebarPosSetup64
OutputDir=Bin

[Files]
Source: "..\x64\Release\NativeRebarPos.dbx"; DestDir: "{app}\Bin"; Flags: ignoreversion
Source: "..\x64\Release\COMRebarPos.dbx"; DestDir: "{app}\Bin"; Flags: ignoreversion
Source: "..\x64\Release\ManagedRebarPos.dll"; DestDir: "{app}\Bin"; Flags: ignoreversion
Source: "..\RebarPosCommands\bin\Release\RebarPos.dll"; DestDir: "{app}\Bin"; Flags: ignoreversion
Source: "..\Menu\RebarPos.cuix"; DestDir: "{app}\Menu";
Source: "..\Menu\RebarPos.mnr"; DestDir: "{app}\Menu";

[Registry]
Root: "HKLM"; Subkey: "Software\SahinEng\RebarPos"; ValueType: string; ValueName: "InstallPath"; ValueData: "{app}"; Flags: uninsdeletekey

[Code]
var
  wpAcadSelect:  TInputOptionWizardPage;
  Opts: TArrayOfString;
  Paths: TArrayOfString;
  ComPaths: TArrayOfString;

// Chooses a string from the given options
function ChooseString(const Value: string; const StringArray: array of string; const ResultArray: array of string): String;
var
  I: Integer;
begin
  Result := 'Unknown';
  for I:=Low(StringArray) to High(StringArray) do begin
    if Value = StringArray[I] then begin
      Result := ResultArray[I];
      Break;
    end;
  end;
end;

// Returns human readable AutoCAD installation name 
function GetAcadName(ProductID: String): String;
var
  S: String;
  AcadVer: String;
  Release: String;
  Vertical: String;
begin
  S := 'AutoCAD ';
  AcadVer  := Copy(ProductID, 6, 1);
  S := S + ChooseString(AcadVer, ['1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B'], ['2002', '2004', '2005', '2006', '2007', '2008', '2009', '2010', '2011', '2012', '2013']);
  Release  := Copy(ProductID, 7, 1);
  Vertical := Copy(ProductID, 8, 2);
  S := S + ' (';
  S := S + ChooseString(Vertical, ['00', '01', '01', '0A', '02', '03', '04', '05', '06', '07', '08', '09', '12', '13', '14', '15', '16', '17', '18', '22', '23', '28'], ['Autodesk Civil 3D', 'AutoCAD', 'DwgViewer', 'AutoCAD OEM', 'Map', 'Inventor Series (AIS)', 'Architectural Desktop', 'AutoCAD Mechanical (ACADM)', 'Building Systems', 'AutoCAD Electrical (ACADE)', 'Land Desktop', 'AutoCAD LT', 'ADT for Raster Design', 'Inventor Professional (AIP)', 'Inventor Professional for Routed Systems (AIPRS)', 'Inventor Professional for Simulation (AIPSIM)', 'AutoCAD P & ID - 2D', 'AutoCAD Plant 3D', 'Civil 3D Land Desktop Companion', 'Autodesk Utility Design', 'Autodesk Topobase Client', 'DWG TrueView']);
  S := S + ')';
  Result := S;
end;
  
procedure InitializeWizard();
var
  Names: TArrayOfString;
  SubNames: TArrayOfString;
  I: Integer;
  J: Integer;
  N: Integer;
begin
  // Read AutoCAD registry entries and build a list of all installations
  N := 0;
  if RegGetSubkeyNames(HKEY_LOCAL_MACHINE, 'SOFTWARE\Autodesk\AutoCAD', Names) then begin
    for I := 0 to GetArrayLength(Names)-1 do begin
      if RegGetSubkeyNames(HKEY_LOCAL_MACHINE, 'SOFTWARE\Autodesk\AutoCAD\' + Names[I], SubNames) then begin
        for J := 0 to GetArrayLength(SubNames)-1 do begin
          N := N + 1;
          SetArrayLength(Opts, N);
          SetArrayLength(Paths, N);
          SetArrayLength(ComPaths, N);
          Opts[N - 1] := GetAcadName(SubNames[J]);
          Paths[N - 1] := 'SOFTWARE\Autodesk\AutoCAD\' + Names[I] + '\' + SubNames[J];
          ComPaths[N - 1] := 'SOFTWARE\Autodesk\ObjectDBX\' + Names[I] + '\ActiveXCLSID';
        end;
      end;
    end;
  end;
    
  // Create custom wizard page to choose AutoCAD installations
  wpAcadSelect := CreateInputOptionPage(wpWelcome,
    'Choose AutoCAD Installation', 'Which AutoCAD installations should have RebarPos enabled?',
    'Select AutoCAD installations on your computer to apply RebarPos settings, then click Next.',
    False, True);
    
  for I := 0 to GetArrayLength(Opts)-1 do
    wpAcadSelect.Add(Opts[I]);
end;

function NextButtonClick(CurPageID: Integer): Boolean;
var
  CheckedCount: Integer;
  I: Integer;
begin
  Result := True;
  if CurPageID = wpWelcome then begin
    // Check if AutoCAD is installed
    if GetArrayLength(Opts) = 0 then begin
      MsgBox('No AutoCAD installations were found. Setup cannot continue.', mbCriticalError, MB_OK);
      Result := False;
      WizardForm.Close;
    end;
  end else if CurPageID = wpAcadSelect.ID then begin
    // Make sure at least one AutoCAD installation is checked
    CheckedCount := 0;
    for I := 0 to wpAcadSelect.CheckListBox.Items.Count-1 do
      if wpAcadSelect.CheckListBox.Checked[I] then CheckedCount := CheckedCount + 1;
      
    if CheckedCount = 0 then begin
      MsgBox('Please select an AutoCAD installation from the list.', mbError, MB_OK);
      Result := False;
    end;
  end;
end;

function UpdateReadyMemo(Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo, MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
var
  I: Integer;
  S: String;
begin
  // Fill the 'Ready Memo' with selected AutoCAD installations 
  S := '';
  S := S + 'Selected AutoCAD installations:' + NewLine;
  for I := 0 to wpAcadSelect.CheckListBox.Items.Count-1 do begin
    if wpAcadSelect.CheckListBox.Checked[I] then
      S := S + Space + Opts[I] + NewLine;
  end;
  
  // Add path
  S := S + NewLine;
  S := S + MemoDirInfo + NewLine;

  Result := S;  
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  I: Integer;
  RegPath: String;
begin
  if CurStep = ssPostInstall then begin
    // Set registry keys
    
    // COM classes
    RegPath := 'CLSID\{97CAC17D-B1C7-49CA-8D57-D3FF491860FF}';
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath, '', 'ComRebarPos Class');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\Programmable', '', '');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\InprocServer32', '', ExpandConstant('{app}\Bin\COMRebarPos.dbx'));
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\InprocServer32', 'ThreadingModel', 'Apartment');
    
    RegPath := 'CLSID\{BA77CFFF-0274-4D4C-BFE2-64A5731BAD37}';
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath, '', 'ComBOQTable Class');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\Programmable', '', '');
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\InprocServer32', '', ExpandConstant('{app}\Bin\COMRebarPos.dbx'));
    RegWriteStringValue(HKEY_CLASSES_ROOT, RegPath + '\InprocServer32', 'ThreadingModel', 'Apartment');
    
    for I := 0 to wpAcadSelect.CheckListBox.Items.Count-1 do begin
      if wpAcadSelect.CheckListBox.Checked[I] then begin
        // NativeRebarPos.dbx
        RegPath := Paths[I] + '\Applications\OZOZRebarPosNative';
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'DESCRIPTION', 'RebarPos Module');
        RegWriteDWordValue(HKEY_LOCAL_MACHINE, RegPath, 'LOADCTRLS', 9);
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'LOADER', ExpandConstant('{app}\Bin\NativeRebarPos.dbx'));
        
        // COMRebarPos.dbx
        RegPath := Paths[I] + '\Applications\OZOZRebarPosCOM';
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'DESCRIPTION', 'RebarPos COM Module');
        RegWriteDWordValue(HKEY_LOCAL_MACHINE, RegPath, 'LOADCTRLS', 9);
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'LOADER', ExpandConstant('{app}\Bin\COMRebarPos.dbx'));
        
        // COM Classes
        RegPath := ComPaths[I];
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'RebarPos', '{97CAC17D-B1C7-49CA-8D57-D3FF491860FF}');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'BOQTable', '{BA77CFFF-0274-4D4C-BFE2-64A5731BAD37}');
        
        // ManagedRebarPos.dll
        RegPath := Paths[I] + '\Applications\OZOZRebarPosManaged';
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'DESCRIPTION', 'RebarPos Managed Module');
        RegWriteDWordValue(HKEY_LOCAL_MACHINE, RegPath, 'LOADCTRLS', 9);
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'LOADER', ExpandConstant('{app}\Bin\ManagedRebarPos.dll'));
        RegWriteDWordValue(HKEY_LOCAL_MACHINE, RegPath, 'MANAGED', 1);
        
        // RebarPos.dll
        RegPath := Paths[I] + '\Applications\OZOZRebarPos';
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'DESCRIPTION', 'RebarPos Commands');
        RegWriteDWordValue(HKEY_LOCAL_MACHINE, RegPath, 'LOADCTRLS', 12);
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'LOADER', ExpandConstant('{app}\Bin\RebarPos.dll'));
        RegWriteDWordValue(HKEY_LOCAL_MACHINE, RegPath, 'MANAGED', 1);
        RegPath := Paths[I] + '\Applications\OZOZRebarPos\Groups';
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'RebarPos', 'RebarPos');
        RegPath := Paths[I] + '\Applications\OZOZRebarPos\Commands';
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'POS', 'POZ');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'POSEDIT', 'POZEDIT');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'BOQEDIT', 'METRAJEDIT');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'NEWPOS', 'POZEKLE');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'NUMBERPOS', 'POZNUM');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'EMPTYPOS', 'NUMARASIL');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'POSCHECK', 'POZKONTROL');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'COPYPOS', 'POZKOPYALA');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'COPYPOSDETAIL', 'POZKOPYALADETAY');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'COPYPOSNUMBER', 'POZBENZET');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'BOQ', 'METRAJ');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'POSFIND', 'POZBUL');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'POSSHAPES', 'ACILIMLAR');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'TOGGLESHAPES', 'SEKILGOSTERGIZLE');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'SHOWSHAPES', 'SEKILGOSTER');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'HIDESHAPES', 'SEKILSIL');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'TABLESTYLE', 'TABLOSTILI');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'POSLENGTH', 'BOYGOSTER');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'INCLUDEPOS', 'POZKATKI');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'LASTPOSNUMBER', 'SONPOZ');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'POSHELP', 'METRAJYARDIM');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'POSSETTINGS', 'POZAYAR');
        RegWriteStringValue(HKEY_LOCAL_MACHINE, RegPath, 'POSMENU', 'POZMENU');
      end;
    end;
  end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
  I: Integer;
  J: Integer;
  RegPath: String;
  Names: TArrayOfString;
  SubNames: TArrayOfString;
begin
  if CurUninstallStep = usPostUninstall then begin
    // Remove registry keys
    
    // COM classes
    RegDeleteKeyIncludingSubkeys(HKEY_CLASSES_ROOT, 'CLSID\{97CAC17D-B1C7-49CA-8D57-D3FF491860FF}');
    RegDeleteKeyIncludingSubkeys(HKEY_CLASSES_ROOT, 'CLSID\{BA77CFFF-0274-4D4C-BFE2-64A5731BAD37}');
    
    // Read AutoCAD registry entries and build a list of all installations
    if RegGetSubkeyNames(HKEY_LOCAL_MACHINE, 'SOFTWARE\Autodesk\AutoCAD', Names) then begin
      for I := 0 to GetArrayLength(Names)-1 do begin
        if RegGetSubkeyNames(HKEY_LOCAL_MACHINE, 'SOFTWARE\Autodesk\AutoCAD\' + Names[I], SubNames) then begin
          for J := 0 to GetArrayLength(SubNames)-1 do begin
            RegPath := 'SOFTWARE\Autodesk\AutoCAD\' + Names[I] + '\' + SubNames[J];
            // NativeRebarPos.dbx
            RegDeleteKeyIncludingSubkeys(HKEY_LOCAL_MACHINE, RegPath + '\Applications\OZOZRebarPosNative');
            // COMRebarPos.dbx
            RegDeleteKeyIncludingSubkeys(HKEY_LOCAL_MACHINE, RegPath + '\Applications\OZOZRebarPosCOM');
            // ManagedRebarPos.dll
            RegDeleteKeyIncludingSubkeys(HKEY_LOCAL_MACHINE, RegPath + '\Applications\OZOZRebarPosManaged');
            // RebarPos.dll
            RegDeleteKeyIncludingSubkeys(HKEY_LOCAL_MACHINE, RegPath + '\Applications\OZOZRebarPos');
          end;
        end;
        // COM Classes
        RegPath := 'SOFTWARE\Autodesk\ObjectDBX\' + Names[I] + '\ActiveXCLSID';
        RegDeleteValue(HKEY_LOCAL_MACHINE, RegPath, 'RebarPos');
        RegDeleteValue(HKEY_LOCAL_MACHINE, RegPath, 'BOQTable');        
      end;
    end;
  end;
end;
