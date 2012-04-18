//-----------------------------------------------------------------------------
//----- PosShape.h : Declaration of the CPosShape
//-----------------------------------------------------------------------------
#pragma once

#include "dbmain.h"
#include <vector>
#include "Shape.h"
#include "DictEntry.h"

// The following is part of the code used to export an API
// and/or use the exported API.
//
#pragma warning( disable: 4275 4251 )
#ifdef REBARPOS_MODULE
#define DLLIMPEXP __declspec( dllexport )
#else
#define DLLIMPEXP
#endif

typedef std::vector<CShape*> ShapeList;
typedef std::vector<CShape*>::size_type ShapeSize;
typedef std::vector<CShape*>::iterator ShapeListIt;
typedef std::vector<CShape*>::const_iterator ShapeListConstIt;

/// ---------------------------------------------------------------------------
/// The CPosShape represents the shape definitions of rebar markers.
/// ---------------------------------------------------------------------------
class DLLIMPEXP CPosShape : public CDictEntry<CPosShape>
{

public:
	/// Define additional RTT information for AcRxObject base type.
	ACRX_DECLARE_MEMBERS(CPosShape);

protected:
	/// Object version number serialized in the drawing database.
	static Adesk::UInt32 kCurrentVersionNumber;

public:
	CPosShape ();
	virtual ~CPosShape ();

public:
	/// AcDbObject overrides: database
	virtual Acad::ErrorStatus dwgOutFields(AcDbDwgFiler *pFiler) const;
	virtual Acad::ErrorStatus dwgInFields(AcDbDwgFiler *pFiler);

	virtual Acad::ErrorStatus dxfOutFields(AcDbDxfFiler *pFiler) const;
	virtual Acad::ErrorStatus dxfInFields(AcDbDxfFiler *pFiler);

protected:
	/// Property backing fields
	Adesk::UInt16 m_Fields;

	ACHAR* m_Formula;
	ACHAR* m_FormulaBending;

	ShapeList m_List;
public:
	/// Creates a default entry or returns the first entry in the table.
	static AcDbObjectId CreateDefault(void);

public:
	/// Gets or sets the field count.
    const Adesk::UInt16 Fields(void) const;
	Acad::ErrorStatus setFields(const Adesk::UInt16 newVal);

	/// Gets or sets the formula used to calculate the total length from piece lengths.
	const ACHAR* Formula(void) const;
	Acad::ErrorStatus setFormula(const ACHAR* newVal);

	/// Gets or sets the formula used to calculate the total length from piece lengths
	/// including bending allowance.
	const ACHAR* FormulaBending(void) const;
	Acad::ErrorStatus setFormulaBending(const ACHAR* newVal);

	/// Adds a shape.
	void AddShape(CShape* shape);

	/// Gets the shape at the given index.
	CShape* GetShape(ShapeSize index);

	/// Sets the shape at the given index.
	void SetShape(ShapeSize index, CShape* shape);

	/// Gets the count of shapes.
	ShapeSize GetShapeCount();

	/// Removes the shape at the given index.
	void RemoveShape(ShapeSize index);

	/// Clears all shapes.
	void ClearShapes();

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
