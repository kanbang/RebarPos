//-----------------------------------------------------------------------------
//----- PosGroup.h : Declaration of the CPosGroup
//-----------------------------------------------------------------------------
#pragma once

#include "dbmain.h"
#include "dbcurve.h"
#include "geassign.h"
#include "gegbl.h"
#include "gegblabb.h"
#include "gemat3d.h"
#include "acestext.h"
#include "gept2dar.h"
#include "windows.h"
#include "dbxutil.h"

// The following is part of the code used to export an API
// and/or use the exported API.
//
#pragma warning( disable: 4275 4251 )
#ifdef REBARPOS_MODULE
#define DLLIMPEXP __declspec( dllexport )
#else
#define DLLIMPEXP
#endif

/// ---------------------------------------------------------------------------
/// The CPosGroup represents the settings for groups of rebar markers.
/// ---------------------------------------------------------------------------
class DLLIMPEXP CPosGroup : public AcDbObject
{
public:
	/// Define additional RTT information for AcRxObject base type.
	ACRX_DECLARE_MEMBERS(CPosGroup);

protected:
	/// Object version number serialized in the drawing database.
	static Adesk::UInt32 kCurrentVersionNumber;

public:
	CPosGroup ();
	virtual ~CPosGroup ();

public:
	enum DrawingUnits
	{ 
		MM = 0,
		CM = 1,
		DM = 2,
		M = 3,
	};

public:
	/// AcDbEntity overrides: database
	virtual Acad::ErrorStatus dwgOutFields(AcDbDwgFiler *pFiler) const;
	virtual Acad::ErrorStatus dwgInFields(AcDbDwgFiler *pFiler);

	virtual Acad::ErrorStatus dxfOutFields(AcDbDxfFiler *pFiler) const;
	virtual Acad::ErrorStatus dxfInFields(AcDbDxfFiler *pFiler);

protected:
	/// Property backing fields
	Adesk::Boolean m_Bending;
	double m_MaxBarLength;

	DrawingUnits m_DrawingUnit;
	DrawingUnits m_DisplayUnit;

public:
	/// Gets or sets the bending option.
    const Adesk::Boolean Bending(void) const;
	Acad::ErrorStatus setBending(const Adesk::Boolean newVal);

	/// Gets or sets the maximum bar length
    const double MaxBarLength(void) const;
	Acad::ErrorStatus setMaxBarLength(const double newVal);

	/// Gets or sets the drawing unit
    const DrawingUnits DrawingUnit(void) const;
	Acad::ErrorStatus setDrawingUnit(const DrawingUnits newVal);

	/// Gets or sets the display unit
    const DrawingUnits DisplayUnit(void) const;
	Acad::ErrorStatus setDisplayUnit(const DrawingUnits newVal);

private:
	static ACHAR* Table_Name;

public:
	/// Gets the table name
	static ACHAR* GetTableName();

private:
    // These are here because otherwise dllexport tries to export the
    // private methods of AcDbObject.  They're private in AcDbObject
    // because vc5 and vc6 do not properly support array new and delete.
    // The "vector deleting dtor" gets optimized into a scalar deleting
    // dtor by the linker if the server dll does not call vector delete.
    // The linker shouldn't do that, because it doesn't know that the
    // object won't be passed to some other dll which *does* do vector
    // delete.
    //
#ifdef MEM_DEBUG
#undef new
#undef delete
#endif
    void *operator new[](size_t nSize) { return 0;}
    void operator delete[](void *p) {};
    void *operator new[](size_t nSize, const TCHAR *file, int line) { return 0;}
#ifdef MEM_DEBUG
#define new DEBUG_NEW
#define delete DEBUG_DELETE
#endif
};

//- This line allows us to get rid of the .def file in ARX projects
#ifndef NO_ARX_DEF
#define NO_ARX_DEF
#ifndef _WIN64
#pragma comment(linker, "/export:_acrxGetApiVersion,PRIVATE")
#else
#pragma comment(linker, "/export:acrxGetApiVersion,PRIVATE")
#endif
#endif
