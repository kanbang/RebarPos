//-----------------------------------------------------------------------------
//----- BOQTable.h : Declaration of the CBOQTable
//-----------------------------------------------------------------------------
#pragma once

#pragma warning( push )
#pragma warning( disable : 4100 )
#include "acgi.h"
#pragma warning( pop )

#include <vector>
#include "BOQRow.h"

// The following is part of the code used to export an API
// and/or use the exported API.
//
#pragma warning( disable: 4275 4251 )
#ifdef REBARPOS_MODULE
#define DLLIMPEXP __declspec( dllexport )
#else
#define DLLIMPEXP
#endif

typedef std::vector<CBOQRow*> RowList;
typedef std::vector<CBOQRow*>::size_type RowListSize;
typedef std::vector<CBOQRow*>::iterator RowListIt;
typedef std::vector<CBOQRow*>::const_iterator RowListConstIt;

/// ---------------------------------------------------------------------------
/// The CBOQTable represents the BOQ list of rebar marker in the drawing.
/// ---------------------------------------------------------------------------
class DLLIMPEXP CBOQTable : public  AcDbEntity
{
public:
	/// Define additional RTT information for AcRxObject base type.
    ACRX_DECLARE_MEMBERS(CBOQTable);
    
protected:
	/// Object version number serialized in the drawing database.
	static Adesk::UInt32 kCurrentVersionNumber;

public:
    /// Constructors and destructor    
    CBOQTable();
    virtual ~CBOQTable();
       
private:
	/// Property backing fields
	AcGeVector3d m_Direction, m_Up, m_Normal;
	AcGePoint3d m_BasePoint;
	Adesk::Int32 m_Multiplier;

	AcDbHardPointerId m_StyleID;

	RowList m_List;

public:
	/// Get direction vector
	const AcGeVector3d& DirectionVector(void) const;
	/// Get up vector
	const AcGeVector3d& UpVector(void) const;
	/// Get normal vector
	const AcGeVector3d& NormalVector(void) const;

	/// Object scale
	const double Scale(void) const;
	Acad::ErrorStatus setScale(const double newVal);

	/// Get extents
	const double Width(void) const;
	const double Height(void) const;

	/// Gets or sets the base grip point
	const AcGePoint3d& BasePoint(void) const;
	Acad::ErrorStatus setBasePoint(const AcGePoint3d& newVal);

	/// Gets or sets the BOQ multiplier
	const Adesk::Int32 Multiplier(void) const;
	Acad::ErrorStatus setMultiplier(const Adesk::Int32 newVal);

	/// Gets or sets the BOQ style
	const AcDbObjectId& StyleId(void) const;
	Acad::ErrorStatus setStyleId(const AcDbObjectId& newVal);

public:
	/// Adds a row.
	void AddRow(CBOQRow* const row);

	/// Gets the row at the given index.
	const CBOQRow* GetRow(const RowListSize index) const;

	/// Sets the row at the given index.
	void SetRow(const RowListSize index, CBOQRow* const row);

	/// Gets the count of rows.
	const RowListSize GetRowCount() const;

	/// Removes the row at the given index.
	void RemoveRow(const RowListSize index);

	/// Clears all rows.
	void ClearRows();

public:
	/// AcDbEntity overrides: database    
    virtual Acad::ErrorStatus	dwgInFields(AcDbDwgFiler* filer);
    virtual Acad::ErrorStatus	dwgOutFields(AcDbDwgFiler* filer) const;
    
    virtual Acad::ErrorStatus	dxfInFields(AcDbDxfFiler* filer);
    virtual Acad::ErrorStatus	dxfOutFields(AcDbDxfFiler* filer) const;

	virtual void saveAs(AcGiWorldDraw *pWd, AcDb::SaveType saveType);

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
    
	virtual Acad::ErrorStatus   subGetGeomExtents(AcDbExtents& extents) const;

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
    void *operator new[](size_t /* nSize */) { return 0;}
    void operator delete[](void* /* p */) {};
    void *operator new[](size_t /* nSize */, const TCHAR* /* file*/ , int /* line */) { return 0;}
#ifdef MEM_DEBUG
#define new DEBUG_NEW
#define delete DEBUG_DELETE
#endif
};
