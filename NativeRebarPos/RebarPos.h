//-----------------------------------------------------------------------------
//----- RebarPos.h : Declaration of the CRebarPos
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

#include "DrawParams.h"
#include <vector>

// The following is part of the code used to export an API
// and/or use the exported API.
//
#pragma warning( disable: 4275 4251 )
#ifdef REBARPOS_MODULE
#define DLLIMPEXP __declspec( dllexport )
#else
#define DLLIMPEXP
#endif

typedef std::vector<CDrawParams> DrawList;
typedef std::vector<CDrawParams>::size_type DrawListSize;
typedef std::vector<CDrawParams>::iterator DrawListIt;
typedef std::vector<CDrawParams>::const_iterator DrawListConstIt;

/// ---------------------------------------------------------------------------
/// The CRebarPos represents a rebar marker in the drawing. It contains
/// marker number, rebar shape, diameter, spacing, piece lengths and some
/// metadata as well.
/// ---------------------------------------------------------------------------
class DLLIMPEXP CRebarPos : public  AcDbEntity
{
public:
	/// Define additional RTT information for AcRxObject base type.
    ACRX_DECLARE_MEMBERS(CRebarPos);
    
protected:
	/// Object version number serialized in the drawing database.
	static Adesk::UInt32 kCurrentVersionNumber;

public:
    /// Constructors and destructor    
    CRebarPos();
    virtual ~CRebarPos();
       
public:
	enum PosSubEntityType
	{ 
		NONE = 0,
		POS = 1,
		COUNT = 2,
		DIAMETER = 3,
		SPACING = 4,
		GROUP = 5,
		MULTIPLIER = 6,
		LENGTH = 7,
		NOTE = 8
	};
	enum DisplayStyle
	{ 
		ALL = 0,
		WITHOUTLENGTH = 1,
		MARKERONLY = 2,
	};

private:
	/// Used to cache last draw params
	AcString lastGroupName;
	DrawList lastDrawList;
	CDrawParams lastNoteDraw;
    AcGiTextStyle lastTextStyle;
    AcGiTextStyle lastNoteStyle;
	Adesk::UInt16 lastCircleColor;
	Adesk::UInt16 lastGroupHighlightColor;
	Adesk::UInt16 lastGroupColor;
	Adesk::UInt16 lastMultiplierColor;
	Adesk::Boolean lastCurrentGroup;

private:
	/// Property backing fields
	AcGePoint3d m_BasePoint;
	AcGePoint3d m_NoteGrip;
	mutable ACHAR* m_Key;
	mutable ACHAR* m_Length;
	ACHAR* m_Pos;
	ACHAR* m_Count;
	ACHAR* m_Diameter;
	ACHAR* m_Spacing;
	Adesk::Int32 m_Multiplier;
	ACHAR* m_Note;
	ACHAR* m_A;
	ACHAR* m_B;
	ACHAR* m_C;
	ACHAR* m_D;
	ACHAR* m_E;
	ACHAR* m_F;

	DisplayStyle m_DisplayStyle;

	AcDbHardPointerId m_ShapeID;
	AcDbHardPointerId m_GroupID;

	/// Locals
	AcGeVector3d direction, up, norm;
	mutable bool isModified;

protected:
	/// Calculates pos text when the pos is modified.
	const void Calculate(void) const;

	/// Parses formula text and creates the draw list
	const DrawList ParseFormula(const ACHAR* formula) const;

public:
	/// Determines which part is under the given point
	const CRebarPos::PosSubEntityType HitTest(const AcGePoint3d& pt0) const;

	/// Gets or sets the base grip point
	const AcGePoint3d& BasePoint(void) const;
	Acad::ErrorStatus setBasePoint(const AcGePoint3d& newVal);

	/// Gets or sets the note grip point
	const AcGePoint3d& NoteGrip(void) const;
	Acad::ErrorStatus setNoteGrip(const AcGePoint3d& newVal);

	/// Gets or sets pos marker number
	const ACHAR* Pos(void) const;
	Acad::ErrorStatus setPos(const ACHAR* newVal);

	/// Gets or sets the note text
	const ACHAR* Note(void) const;
	Acad::ErrorStatus setNote(const ACHAR* newVal);

	/// Gets or sets the count. Simple arithmetic allowed, ie. 4x2, 2x3x4
	const ACHAR* Count(void) const;
	Acad::ErrorStatus setCount(const ACHAR* newVal);

	/// Gets or sets the rebar diameter
	const ACHAR* Diameter(void) const;
	Acad::ErrorStatus setDiameter(const ACHAR* newVal);

	/// Gets or sets the bar spacing
	const ACHAR* Spacing(void) const;
	Acad::ErrorStatus setSpacing(const ACHAR* newVal);

	/// Gets or sets the BOQ multiplier
	const Adesk::Int32 Multiplier(void) const;
	Acad::ErrorStatus setMultiplier(const Adesk::Int32 newVal);

	/// Gets or sets whether length text is shown
	const DisplayStyle Display(void) const;
	Acad::ErrorStatus setDisplay(const DisplayStyle newVal);

	/// Gets or sets the A piece length
	const ACHAR* A(void) const;
	Acad::ErrorStatus setA(const ACHAR* newVal);

	/// Gets or sets the B piece length
	const ACHAR* B(void) const;
	Acad::ErrorStatus setB(const ACHAR* newVal);

	/// Gets or sets the C piece length
	const ACHAR* C(void) const;
	Acad::ErrorStatus setC(const ACHAR* newVal);

	/// Gets or sets the D piece length
	const ACHAR* D(void) const;
	Acad::ErrorStatus setD(const ACHAR* newVal);

	/// Gets or sets the E piece length
	const ACHAR* E(void) const;
	Acad::ErrorStatus setE(const ACHAR* newVal);

	/// Gets or sets the F piece length
	const ACHAR* F(void) const;
	Acad::ErrorStatus setF(const ACHAR* newVal);

	/// Gets the total length
	const ACHAR* Length(void) const;

	/// Gets or sets the pos shape
	const AcDbObjectId& ShapeId(void) const;
	Acad::ErrorStatus setShapeId(const AcDbObjectId& newVal);

	/// Gets or sets the pos group
	const AcDbObjectId& GroupId(void) const;
	Acad::ErrorStatus setGroupId(const AcDbObjectId& newVal);

public:
	/// Gets a string key identifying a pos with a certain diameter,
	/// shape and piece lengths
	const ACHAR* PosKey() const;
 
public:
	/// AcDbEntity overrides: database    
    virtual Acad::ErrorStatus	dwgInFields(AcDbDwgFiler* filer);
    virtual Acad::ErrorStatus	dwgOutFields(AcDbDwgFiler* filer) const;
    
    virtual Acad::ErrorStatus	dxfInFields(AcDbDxfFiler* filer);
    virtual Acad::ErrorStatus	dxfOutFields(AcDbDxfFiler* filer) const;

protected:
	/// AcDbEntity overrides: geometry
    virtual Acad::ErrorStatus subGetOsnapPoints(
        AcDb::OsnapMode       osnapMode,
        Adesk::GsMarker       gsSelectionMark,
        const AcGePoint3d&    pickPoint,
        const AcGePoint3d&    lastPoint,
        const AcGeMatrix3d&   viewXform,
        AcGePoint3dArray&     snapPoints,
        AcDbIntArray&         geomIds) const;

    virtual Acad::ErrorStatus   subGetGripPoints(AcGePoint3dArray&     gripPoints,
        AcDbIntArray&  osnapModes,
        AcDbIntArray&  geomIds) const;

    virtual Acad::ErrorStatus   subMoveGripPointsAt(const AcDbIntArray& indices,
        const AcGeVector3d&     offset);

    virtual Acad::ErrorStatus   subTransformBy(const AcGeMatrix3d& xform);

    virtual void                subList() const;

    virtual Acad::ErrorStatus	subExplode(AcDbVoidPtrArray& entitySet) const;

    virtual Adesk::Boolean      subWorldDraw(AcGiWorldDraw*	mode);
    
    /// Overridden methods from AcDbObject    
    virtual Acad::ErrorStatus subDeepClone(AcDbObject* pOwnerObject,
        AcDbObject*& pClonedObject,
        AcDbIdMapping& idMap,
        Adesk::Boolean isPrimary
        = Adesk::kTrue) const;
    
    virtual Acad::ErrorStatus subWblockClone(AcRxObject* pOwnerObject,
        AcDbObject*& pClonedObject,
        AcDbIdMapping& idMap,
        Adesk::Boolean isPrimary
        = Adesk::kTrue) const;
    
    virtual Acad::ErrorStatus subGetClassID(CLSID* pClsid) const;

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
