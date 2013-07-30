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
	AcGeVector3d m_Direction, m_Up;
	AcGePoint3d m_BasePoint;

	double m_CellMargin;

	int m_Rows;
	int m_Columns;
	std::vector<CTableCell*> m_Cells;

	double m_Width;
	double m_Height;

	/// Locals
	std::vector<double> columnWidths;
	std::vector<double> rowHeights;
	std::vector<double> minColumnWidths;
	std::vector<double> minRowHeights;

	bool geomInit;
	AcGeMatrix3d ucs;

private:
	int suspendCount;
	bool needsUpdate;

protected:
	/// Calculates draw params when the entity is modified.
	void Calculate(void);

	// Resets last draw parameters
	void ResetDrawParams(void);

public:
	/// Methods
	virtual void SuspendUpdate(void);
	virtual void ResumeUpdate(void);

	void SetSize(int rows, int columns);
	void Clear();

	void setCellText(const int i, const int j, const ACHAR* newVal);
	void setCellShape(const int i, const int j, const ACHAR* newVal);
	void setCellShapeText(const int i, const int j, const ACHAR* a, const ACHAR* b, const ACHAR* c, const ACHAR* d, const ACHAR* e, const ACHAR* f);
	void setCellTextColor(const int i, const int j, const unsigned short newVal);
	void setCellShapeTextColor(const int i, const int j, const unsigned short newVal);
	void setCellShapeLineColor(const int i, const int j, const unsigned short newVal);
	void setCellTextStyleId(const int i, const int j, const AcDbObjectId& newVal);
	void setCellTextHeight(const int i, const int j, const double newVal);

	void setCellTextColor(const unsigned short newVal);
	void setCellTextStyleId(const AcDbObjectId& newVal);
	void setCellTextHeight(const double newVal);

	void setCellLeftBorder(const int i, const int j, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);
	void setCellRightBorder(const int i, const int j, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);
	void setCellTopBorder(const int i, const int j, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);
	void setCellBottomBorder(const int i, const int j, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);

	void setCellLeftBorder(const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);
	void setCellRightBorder(const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);
	void setCellTopBorder(const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);
	void setCellBottomBorder(const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);

	void setRowTopBorder(const int i, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);
	void setRowBottomBorder(const int i, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);

	void setColumnLeftBorder(const int j, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);
	void setColumnRightBorder(const int j, const bool hasBorder, const unsigned short borderColor = 1, bool isdouble = false);

	void setCellHorizontalAlignment(const int i, const int j, const CTableCell::Alignment newVal);
	void setCellVerticalAlignment(const int i, const int j, const CTableCell::Alignment newVal);

	void setCellHorizontalAlignment(const CTableCell::Alignment newVal);
	void setCellVerticalAlignment(const CTableCell::Alignment newVal);

	void setCellMargin(const int i, const int j, const double newVal);
	void setCellMargin(const double newVal);

	void MergeAcross(const int i, const int j, const int span = 0);
	void MergeDown(const int i, const int j, const int span = 0);

	void setMinimumColumnWidth(const int j, const double newVal);
	void setMinimumRowHeight(const int i, const double newVal);

	void setMinimumColumnWidth(const double newVal);
	void setMinimumRowHeight(const double newVal);

public:
	/// Get direction vector
	const AcGeVector3d& DirectionVector(void) const;
	/// Get up vector
	const AcGeVector3d& UpVector(void) const;
	/// Get normal vector
	const AcGeVector3d NormalVector(void) const;

	/// Table size
	const int Columns(void) const;
	const int Rows(void) const;

	/// Object scale
	const double Scale(void) const;
	Acad::ErrorStatus setScale(const double newVal);

	/// Gets or sets the base grip point
	const AcGePoint3d& BasePoint(void) const;
	Acad::ErrorStatus setBasePoint(const AcGePoint3d& newVal);

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
};