//-----------------------------------------------------------------------------
//----- GenericTable.h : Declaration of the CGenericTable
//-----------------------------------------------------------------------------
#pragma once

#include <vector>
#include <map>

#pragma warning( push )
#pragma warning( disable : 4005 )
#pragma warning( disable : 4100 )
#pragma warning( disable : 4201 )
#pragma warning( disable : 4512 )
#include "dbents.h"
#pragma warning( pop )

#include "TableCell.h"
#include "DrawParams.h"

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
/// The CGenericTable represents a table in the drawing. It contains
/// cells with formatting and borders.
/// ---------------------------------------------------------------------------
class DLLIMPEXP CGenericTable : public  AcDbEntity
{
public:
	/// Define additional RTT information for AcRxObject base type.
    ACRX_DECLARE_MEMBERS(CGenericTable);
    
protected:
	/// Object version number serialized in the drawing database.
	static Adesk::UInt32 kCurrentVersionNumber;

public:
    /// Constructors and destructor
	CGenericTable(void);
	~CGenericTable(void);

private:
	/// Property backing fields
	AcGeVector3d m_Direction, m_Up, m_Normal;
	AcGePoint3d m_BasePoint;

	double m_CellMargin;

	int m_Rows;
	int m_Columns;
	std::vector<CTableCell*> m_Cells;

	mutable double m_Width;
	mutable double m_Height;

	double m_MaxHeight;
	double m_TableSpacing;

	/// Locals
	mutable std::vector<double> columnWidths;
	mutable std::vector<double> rowHeights;
	mutable std::vector<double> minColumnWidths;
	mutable std::vector<double> minRowHeights;

	mutable bool geomInit;
	mutable AcGeMatrix3d ucs;

	mutable bool isModified;

protected:
	/// Calculates draw params when the entity is modified.
	const void Calculate(void) const;

	// Resets last draw parameters
	const void ResetDrawParams(void) const;

	// Returns the row index around the given height
	// before which the table can be divided
	const int DivideAt(double& y) const;

public:
	/// Methods
	void SetSize(int rows, int columns);
	void Clear();

	void setCellText(const int i, const int j, const ACHAR* newVal);
	void setCellShapeId(const int i, const int j, const AcDbObjectId& newVal);
	void setCellShapeText(const int i, const int j, const ACHAR* a, const ACHAR* b, const ACHAR* c, const ACHAR* d, const ACHAR* e, const ACHAR* f);
	void setCellTextColor(const int i, const int j, const unsigned short newVal);
	void setCellTextStyleId(const int i, const int j, const AcDbObjectId& newVal);
	void setCellTextHeight(const int i, const int j, const double newVal);

	void setCellLeftBorder(const int i, const int j, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);
	void setCellRightBorder(const int i, const int j, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);
	void setCellTopBorder(const int i, const int j, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);
	void setCellBottomBorder(const int i, const int j, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);

	void setRowTopBorder(const int i, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);
	void setRowBottomBorder(const int i, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);

	void setColumnLeftBorder(const int j, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);
	void setColumnRightBorder(const int j, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);

	void setCellHorizontalAlignment(const int i, const int j, const CTableCell::Alignment newVal);
	void setCellVerticalAlignment(const int i, const int j, const CTableCell::Alignment newVal);

	void MergeAcross(const int i, const int j, const int span = 0);
	void MergeDown(const int i, const int j, const int span = 0);

	void setMinimumColumnWidth(const int j, const double newVal);
	void setMinimumRowHeight(const int i, const double newVal);

public:
	/// Get direction vector
	const AcGeVector3d& DirectionVector(void) const;
	/// Get up vector
	const AcGeVector3d& UpVector(void) const;
	/// Get normal vector
	const AcGeVector3d& NormalVector(void) const;

	/// Table size
	const int Columns(void) const;
	const int Rows(void) const;

	/// Divide table at max height
	const double MaxHeight(void) const;
	Acad::ErrorStatus setMaxHeight(const double newVal);

	/// Spacing between divided tables
	const double TableSpacing(void) const;
	Acad::ErrorStatus setTableSpacing(const double newVal);

	/// Object scale
	const double Scale(void) const;
	Acad::ErrorStatus setScale(const double newVal);

	/// Gets or sets the base grip point
	const AcGePoint3d& BasePoint(void) const;
	Acad::ErrorStatus setBasePoint(const AcGePoint3d& newVal);

	/// Cell margins
	const double CellMargin(void) const;
	Acad::ErrorStatus setCellMargin(const double newVal);

	/// Get extents
	const double Width(void) const;
	const double Height(void) const;

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

    virtual Acad::ErrorStatus	subExplode(AcDbVoidPtrArray& entitySet) const;

    virtual Adesk::Boolean      subWorldDraw(AcGiWorldDraw*	mode);
    
	virtual Acad::ErrorStatus   subGetGeomExtents(AcDbExtents& extents) const;

private:
    // These are here because otherwise dllexport tries to export the
    // private methods of AcDbObject. They're private in AcDbObject
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