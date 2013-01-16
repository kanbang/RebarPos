//-----------------------------------------------------------------------------
//----- TableCell.h : Declaration of the CTableCell
//-----------------------------------------------------------------------------
#pragma once

#pragma warning( push )
#pragma warning( disable : 4005 )
#pragma warning( disable : 4100 )
#pragma warning( disable : 4201 )
#pragma warning( disable : 4512 )
#include "dbents.h"
#pragma warning( pop )

#include <vector>
#include <string>

/// ---------------------------------------------------------------------------
/// The CTableCell represents a cell in the CGenericTable. It contains
/// cell contents with formatting and borders.
/// ---------------------------------------------------------------------------
class CTableCell
{
public:
	/// Constructors and destructor
	CTableCell(void);
	~CTableCell(void);

public:
	enum Alignment
	{
		eNEAR = 0,
		eCENTER = 1,
		eFAR = 2
	};

private:
	/// Property backing fields
	AcGeVector3d m_Direction, m_Up, m_Normal;
	AcGePoint3d m_BasePoint;

	ACHAR* m_Text;
	ACHAR* m_Shape;
	ACHAR* m_A;
	ACHAR* m_B;
	ACHAR* m_C;
	ACHAR* m_D;
	ACHAR* m_E;
	ACHAR* m_F;

	Adesk::UInt16 m_TextColor;
	Adesk::UInt16 m_ShapeTextColor;
	Adesk::UInt16 m_ShapeLineColor;
	Adesk::UInt16 m_TopBorderColor;
	Adesk::UInt16 m_LeftBorderColor;
	Adesk::UInt16 m_BottomBorderColor;
	Adesk::UInt16 m_RightBorderColor;

	Adesk::Boolean m_TopBorder;
	Adesk::Boolean m_LeftBorder;
	Adesk::Boolean m_BottomBorder;
	Adesk::Boolean m_RightBorder;

	Adesk::Boolean m_TopBorderDouble;
	Adesk::Boolean m_LeftBorderDouble;
	Adesk::Boolean m_BottomBorderDouble;
	Adesk::Boolean m_RightBorderDouble;

	Adesk::Int32 m_MergeRight;
	Adesk::Int32 m_MergeDown;

	AcDbHardPointerId m_TextStyleId;

	double m_TextHeight;

	Alignment m_HorizontalAlignment;
	Alignment m_VerticalAlignment;

	double m_Width;
	double m_Height;

	double m_Margin;

public:
	/// Get direction vector
	const AcGeVector3d& DirectionVector(void) const;
	/// Get up vector
	const AcGeVector3d& UpVector(void) const;
	/// Get normal vector
	const AcGeVector3d& NormalVector(void) const;

	/// Properties
	const AcGePoint3d& BasePoint(void) const;
	Acad::ErrorStatus setBasePoint(const AcGePoint3d& newVal);

	const ACHAR* Text() const;
	Acad::ErrorStatus setText(const ACHAR* newVal);

	const ACHAR* Shape() const;
	Acad::ErrorStatus setShape(const ACHAR* newVal);

	Acad::ErrorStatus setShapeText(const ACHAR* a, const ACHAR* b, const ACHAR* c, const ACHAR* d, const ACHAR* e, const ACHAR* f);

	const Adesk::UInt16 TextColor() const;
	Acad::ErrorStatus setTextColor(const Adesk::UInt16 newVal);

	const Adesk::UInt16 ShapeTextColor() const;
	Acad::ErrorStatus setShapeTextColor(const Adesk::UInt16 newVal);

	const Adesk::UInt16 ShapeLineColor() const;
	Acad::ErrorStatus setShapeLineColor(const Adesk::UInt16 newVal);

	const Adesk::UInt16 TopBorderColor() const;
	Acad::ErrorStatus setTopBorderColor(const Adesk::UInt16 newVal);

	const Adesk::UInt16 LeftBorderColor() const;
	Acad::ErrorStatus setLeftBorderColor(const Adesk::UInt16 newVal);

	const Adesk::UInt16 BottomBorderColor() const;
	Acad::ErrorStatus setBottomBorderColor(const Adesk::UInt16 newVal);

	const Adesk::UInt16 RightBorderColor() const;
	Acad::ErrorStatus setRightBorderColor(const Adesk::UInt16 newVal);

	const Adesk::Boolean TopBorder() const;
	Acad::ErrorStatus setTopBorder(const Adesk::Boolean newVal);

	const Adesk::Boolean LeftBorder() const;
	Acad::ErrorStatus setLeftBorder(const Adesk::Boolean newVal);

	const Adesk::Boolean BottomBorder() const;
	Acad::ErrorStatus setBottomBorder(const Adesk::Boolean newVal);

	const Adesk::Boolean RightBorder() const;
	Acad::ErrorStatus setRightBorder(const Adesk::Boolean newVal);

	const Adesk::Boolean TopBorderDouble() const;
	Acad::ErrorStatus setTopBorderDouble(const Adesk::Boolean newVal);

	const Adesk::Boolean LeftBorderDouble() const;
	Acad::ErrorStatus setLeftBorderDouble(const Adesk::Boolean newVal);

	const Adesk::Boolean BottomBorderDouble() const;
	Acad::ErrorStatus setBottomBorderDouble(const Adesk::Boolean newVal);

	const Adesk::Boolean RightBorderDouble() const;
	Acad::ErrorStatus setRightBorderDouble(const Adesk::Boolean newVal);

	const Adesk::Int32 MergeRight() const;
	Acad::ErrorStatus setMergeRight(const Adesk::Int32 newVal);

	const Adesk::Int32 MergeDown() const;
	Acad::ErrorStatus setMergeDown(const Adesk::Int32 newVal);

	const AcDbObjectId& TextStyleId() const;
	Acad::ErrorStatus setTextStyleId(const AcDbObjectId& newVal);

	const double TextHeight() const;
	Acad::ErrorStatus setTextHeight(const double newVal);

	const Alignment HorizontalAlignment() const;
	Acad::ErrorStatus setHorizontalAlignment(const Alignment newVal);

	const Alignment VerticalAlignment() const;
	Acad::ErrorStatus setVerticalAlignment(const Alignment newVal);

	const double Width() const;
	Acad::ErrorStatus setWidth(const double newVal);

	const double Height() const;
	Acad::ErrorStatus setHeight(const double newVal);

	const double Margin(void) const;
	Acad::ErrorStatus setMargin(const double newVal);

public:
	/// Methods
	const bool HasText() const;
	const bool HasShape() const;

	const AcGePoint2d MeasureContents() const;

private:
	/// Helper methods
	const std::vector<AcDbMText*> GetTexts() const;
	const std::vector<AcDbLine*> GetLines() const;
	const std::vector<AcDbArc*> GetArcs() const;

public:
	/// AcDbEntity overrides: database    
    Acad::ErrorStatus	dwgInFields(AcDbDwgFiler* filer);
    Acad::ErrorStatus	dwgOutFields(AcDbDwgFiler* filer) const;
    
    Acad::ErrorStatus	dxfInFields(AcDbDxfFiler* filer);
    Acad::ErrorStatus	dxfOutFields(AcDbDxfFiler* filer) const;

	void saveAs(AcGiWorldDraw *pWd, AcDb::SaveType saveType);

public:
	/// AcDbEntity overrides: geometry
    Acad::ErrorStatus getOsnapPoints(
        AcDb::OsnapMode       osnapMode,
        Adesk::GsMarker       gsSelectionMark,
        const AcGePoint3d&    pickPoint,
        const AcGePoint3d&    lastPoint,
        const AcGeMatrix3d&   viewXform,
        AcGePoint3dArray&     snapPoints,
        AcDbIntArray&         geomIds) const;

    Acad::ErrorStatus   resetTransform();
    Acad::ErrorStatus   transformBy(const AcGeMatrix3d& xform);

    Acad::ErrorStatus	explode(AcDbVoidPtrArray& entitySet) const;

    Adesk::Boolean      worldDraw(AcGiWorldDraw* mode);
};