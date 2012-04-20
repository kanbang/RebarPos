//-----------------------------------------------------------------------------
//----- PosStyle.h : Declaration of the CPosStyle
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
/// The CPosStyle represents the style settings of rebar markers.
/// ---------------------------------------------------------------------------
class DLLIMPEXP CPosStyle : public AcDbObject
{

public:
	/// Define additional RTT information for AcRxObject base type.
	ACRX_DECLARE_MEMBERS(CPosStyle);

protected:
	/// Object version number serialized in the drawing database.
	static Adesk::UInt32 kCurrentVersionNumber;

public:
	CPosStyle ();
	virtual ~CPosStyle ();

public:
	/// AcDbEntity overrides: database
	virtual Acad::ErrorStatus dwgOutFields(AcDbDwgFiler *pFiler) const;
	virtual Acad::ErrorStatus dwgInFields(AcDbDwgFiler *pFiler);

	virtual Acad::ErrorStatus dxfOutFields(AcDbDxfFiler *pFiler) const;
	virtual Acad::ErrorStatus dxfInFields(AcDbDxfFiler *pFiler);

protected:
	/// Property backing fields
	ACHAR* m_Formula;
	ACHAR* m_FormulaWithoutLength;
	ACHAR* m_FormulaPosOnly;

	Adesk::UInt16 m_TextColor;
	Adesk::UInt16 m_PosColor;
	Adesk::UInt16 m_CircleColor;
	Adesk::UInt16 m_MultiplierColor;
	Adesk::UInt16 m_GroupColor;
	Adesk::UInt16 m_NoteColor;
	Adesk::UInt16 m_CurrentGroupHighlightColor;

    double m_NoteScale;

    AcDbHardPointerId m_TextStyleID;
    AcDbHardPointerId m_NoteStyleID;

protected:
	/// Creates a text style
	static AcDbObjectId CreateTextStyle(const ACHAR* name, const ACHAR* filename, const double scale);

public:
	/// Gets or sets the formula text.
    const ACHAR* Formula(void) const;
	Acad::ErrorStatus setFormula(const ACHAR* newVal);

	/// Gets or sets the formula text without length displayed.
    const ACHAR* FormulaWithoutLength(void) const;
	Acad::ErrorStatus setFormulaWithoutLength(const ACHAR* newVal);

	/// Gets or sets the formula text with only pos marker displayed.
    const ACHAR* FormulaPosOnly(void) const;
	Acad::ErrorStatus setFormulaPosOnly(const ACHAR* newVal);

	/// Gets or sets the text color.
	const Adesk::UInt16 TextColor(void) const;
	Acad::ErrorStatus setTextColor(const Adesk::UInt16 newVal);

	/// Gets or sets the pos text color.
	const Adesk::UInt16 PosColor(void) const;
	Acad::ErrorStatus setPosColor(const Adesk::UInt16 newVal);

	/// Gets or sets the pos circle color.
	const Adesk::UInt16 CircleColor(void) const;
	Acad::ErrorStatus setCircleColor(const Adesk::UInt16 newVal);

	/// Gets or sets the multiplier text color.
	const Adesk::UInt16 MultiplierColor(void) const;
	Acad::ErrorStatus setMultiplierColor(const Adesk::UInt16 newVal);

	/// Gets or sets the group text color.
	const Adesk::UInt16 GroupColor(void) const;
	Acad::ErrorStatus setGroupColor(const Adesk::UInt16 newVal);

	/// Gets or sets the note text color.
	const Adesk::UInt16 NoteColor(void) const;
	Acad::ErrorStatus setNoteColor(const Adesk::UInt16 newVal);

	/// Gets or sets the highlight color of the pos objects in the current group.
	const Adesk::UInt16 CurrentGroupHighlightColor(void) const;
	Acad::ErrorStatus setCurrentGroupHighlightColor(const Adesk::UInt16 newVal);

	/// Gets or sets the note height relative to text height.
	const double NoteScale(void) const;
	Acad::ErrorStatus setNoteScale(const double newVal);

	/// Gets or sets pointer to the text style.
	const AcDbObjectId& TextStyleId(void) const;
	Acad::ErrorStatus setTextStyleId(const AcDbObjectId& newVal);

	/// Gets or sets pointer to the note style.
	const AcDbObjectId& NoteStyleId(void) const;
	Acad::ErrorStatus setNoteStyleId(const AcDbObjectId& newVal);

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
