(defun C:LL (/ path)
  (setq path "C:/ObjectARX 2010/samples/entity/RebarPos/Win32/Debug")
  (LoadArx path "NativeRebarPos.dbx")
  (LoadArx path "COMRebarPos.dbx")

  (LoadNet path "ManagedRebarPos.dll")
  
  (setq path "C:/ObjectARX 2010/samples/entity/RebarPos/RebarPosCommands/bin/Debug")
  (LoadNet path "RebarPos.dll")
  
  (princ)
)

(defun C:LLN (/ path)
  (setq path "C:/ObjectARX 2010/samples/entity/RebarPos/Win32/Debug")
  (LoadArx path "NativeRebarPos.dbx")

  (LoadNet path "ManagedRebarPos.dll")
  
  (setq path "C:/ObjectARX 2010/samples/entity/RebarPos/RebarPosCommands/bin/Debug")
  (LoadNet path "RebarPos.dll")
  
  (princ)
)

(defun LoadArx (path name)
  (setq path (strcat path "/" name))
  (if (null (findfile path)) (progn (alert "Could not find arx file.") (exit)))  
  (if (null (member (strcase name t) (arx))) (arxload path))
)

(defun LoadNet (path name)
  (setq path (strcat path "/" name))
  (if (null (findfile path)) (progn (alert "Could not find dll file.") (exit)))  
  (command "netload" path)
)

(princ "/nLL to load libraries.")
(princ)
