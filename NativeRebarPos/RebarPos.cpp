//-----------------------------------------------------------------------------
//----- RebarPos.cpp : Implementation of CRebarPos
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include <sstream>

#include "dbelipse.h"

#include "Utility.h"
#include "Calculator.h"
#include "RebarPos.h"
#include "PosShape.h"
#include "PosGroup.h"

Adesk::UInt32 CRebarPos::kCurrentVersionNumber = 1;

//*************************************************************************
// Code for the Class Body. 
//*************************************************************************

ACRX_DXF_DEFINE_MEMBERS(CRebarPos, AcDbEntity,
	AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent,
	AcDbProxyEntity::kAllAllowedBits, REBARPOS,
	"OZOZRebarPos\
	|Product Desc:     RebarPos Entity\
	|Company:          OZOZ");

//*************************************************************************
// Constructors and destructors 
//*************************************************************************

CRebarPos::CRebarPos() :
	m_BasePoint(0, 0, 0), geomInit(false), ucs(AcGeMatrix3d::kIdentity), 
	m_Direction(1, 0, 0), m_Up(0, 1, 0), m_NoteGrip(0, 2.0, 0), m_LengthGrip(0, -2.0, 0),
	m_DisplayStyle(CRebarPos::ALL), m_Length(NULL), m_Key(NULL),
	m_Pos(NULL), m_Count(NULL), m_Diameter(NULL), m_Spacing(NULL), m_Note(NULL), 
	m_IncludeInBOQ(Adesk::kTrue), m_Multiplier(1), m_DisplayedSpacing(NULL),
	m_Shape(NULL), m_A(NULL), m_B(NULL), m_C(NULL), m_D(NULL), m_E(NULL), m_F(NULL), 
	circleRadius(0.9), partSpacing(0.15), tauSize(0.6),
	defpointsLayer(AcDbObjectId::kNull), m_Detached(Adesk::kFalse), m_GroupForDisplay(NULL),
	m_CalcProps()
{
}

CRebarPos::~CRebarPos()
{
    acutDelString(m_Key);
    acutDelString(m_Length);
    acutDelString(m_Pos);
    acutDelString(m_Count);
    acutDelString(m_Diameter);
    acutDelString(m_DisplayedSpacing);
	acutDelString(m_F);
    acutDelString(m_Note);
	acutDelString(m_Shape);
    acutDelString(m_A);
    acutDelString(m_B);
    acutDelString(m_C);
    acutDelString(m_D);
    acutDelString(m_E);
    acutDelString(m_F);
}

//*************************************************************************
// Properties
//*************************************************************************
const AcGeVector3d& CRebarPos::DirectionVector(void) const
{
	assertReadEnabled();
	return m_Direction;
}

const AcGeVector3d& CRebarPos::UpVector(void) const
{
	assertReadEnabled();
	return m_Up;
}

const AcGeVector3d CRebarPos::NormalVector(void) const
{
	assertReadEnabled();
	AcGeVector3d norm = m_Direction.crossProduct(m_Up);
	norm = norm.normal() * m_Direction.length();
	return norm;
}

const double CRebarPos::Scale(void) const
{
	assertReadEnabled();
	return m_Direction.length();
}

Acad::ErrorStatus CRebarPos::setScale(const double newVal)
{
	assertWriteEnabled();
	AcGeMatrix3d xform = AcGeMatrix3d::scaling(newVal / m_Direction.length(), m_BasePoint);
	return transformBy(xform);
}

const double CRebarPos::Width(void) const
{
    assertReadEnabled();

	if(lastDrawList.empty())
	{
		return 0.0;
	}

	double w = 0.0;
	CDrawParams p;
	for(DrawListSize i = 0; i < lastDrawList.size(); i++)
	{
		p = lastDrawList[i];
		w = max(w, p.x + p.w);
		if(p.hasCircle)
		{
			w = max(w, p.x + p.w / 2 + circleRadius);
		}
	}

	return w * m_Direction.length();
}

const double CRebarPos::Height(void) const
{
    assertReadEnabled();

	if(lastDrawList.empty())
	{
		return 0.0;
	}

	double h = 0.0;
	CDrawParams p;
	for(DrawListSize i = 0; i < lastDrawList.size(); i++)
	{
		p = lastDrawList[i];
		h = max(h, p.y + p.h);
		if(p.hasCircle)
		{
			h = max(h, p.y + p.h / 2 + circleRadius);
		}
	}

	return h * m_Direction.length();
}

const void CRebarPos::TextBox(AcGePoint3d& ptmin, AcGePoint3d& ptmax)
{
    assertReadEnabled();

	if(lastDrawList.empty())
	{
		return;
	}

	double xmin = 0.0, xmax = 0.0, ymin = 0.0, ymax = 0.0;
	CDrawParams p;
	for(DrawListSize i = 0; i < lastDrawList.size(); i++)
	{
		p = lastDrawList[i];
		xmin = min(xmin, p.x); xmax = max(xmax, p.x + p.w);
		ymin = min(ymin, p.y); ymax = max(ymax, p.y + p.h);
		if(p.hasCircle)
		{
			xmin = min(xmin, p.x + p.w / 2 - circleRadius); xmax = max(xmax, p.x + p.w / 2 + circleRadius);
			ymin = min(ymin, p.y + p.h / 2 - circleRadius); ymax = max(ymax, p.y + p.h / 2 + circleRadius);
		}
	}

	double sc = m_Direction.length();
	ptmin.set(xmin * sc, ymin * sc, 0.0);
	ptmax.set(xmax * sc, ymax * sc, 0.0);
}

const AcGePoint3d& CRebarPos::BasePoint(void) const
{
	assertReadEnabled();
	return m_BasePoint;
}

Acad::ErrorStatus CRebarPos::setBasePoint(const AcGePoint3d& newVal)
{
	assertWriteEnabled();
	m_BasePoint = newVal;
	return Acad::eOk;
}

const AcGePoint3d& CRebarPos::NoteGrip(void) const
{
	assertReadEnabled();
	return m_NoteGrip;
}

Acad::ErrorStatus CRebarPos::setNoteGrip(const AcGePoint3d& newVal)
{
	assertWriteEnabled();
	m_NoteGrip = newVal;
	return Acad::eOk;
}

const AcGePoint3d& CRebarPos::LengthGrip(void) const
{
	assertReadEnabled();
	return m_LengthGrip;
}

Acad::ErrorStatus CRebarPos::setLengthGrip(const AcGePoint3d& newVal)
{
	assertWriteEnabled();
	m_LengthGrip = newVal;
	return Acad::eOk;
}

const ACHAR* CRebarPos::Pos(void) const
{
	assertReadEnabled();
	return m_Pos;
}

Acad::ErrorStatus CRebarPos::setPos(const ACHAR* newVal)
{
	assertWriteEnabled();

    acutDelString(m_Pos);
    m_Pos = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Pos);
    }

	Calculate();
	return Acad::eOk;
}

const ACHAR* CRebarPos::Note(void) const
{
	assertReadEnabled();
	return m_Note;
}

Acad::ErrorStatus CRebarPos::setNote(const ACHAR* newVal)
{
	assertWriteEnabled();

	acutDelString(m_Note);
    m_Note = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Note);
    }

	Calculate();
	return Acad::eOk;
}

const ACHAR* CRebarPos::Count(void) const
{
	assertReadEnabled();
	return m_Count;
}

Acad::ErrorStatus CRebarPos::setCount(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_Count);
    m_Count = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Count);
    }
	Calculate();
	return Acad::eOk;
}

const ACHAR* CRebarPos::Diameter(void) const
{
	assertReadEnabled();
	return m_Diameter;
}

Acad::ErrorStatus CRebarPos::setDiameter(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_Diameter);
    m_Diameter = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Diameter);
    }
	Calculate();
	return Acad::eOk;
}

const ACHAR* CRebarPos::Spacing(void) const
{
	assertReadEnabled();
	return m_Spacing;
}

Acad::ErrorStatus CRebarPos::setSpacing(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_Spacing);
    m_Spacing = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Spacing);
    }
	Calculate();
	return Acad::eOk;
}

const Adesk::Boolean CRebarPos::IncludeInBOQ(void) const
{
	assertReadEnabled();
	return m_IncludeInBOQ;
}

Acad::ErrorStatus CRebarPos::setIncludeInBOQ(const Adesk::Boolean newVal)
{
	assertWriteEnabled();
	m_IncludeInBOQ = newVal;
	Calculate();
	return Acad::eOk;
}

const Adesk::Int32 CRebarPos::Multiplier(void) const
{
	assertReadEnabled();
	return m_Multiplier;
}

Acad::ErrorStatus CRebarPos::setMultiplier(const Adesk::Int32 newVal)
{
	assertWriteEnabled();
	m_Multiplier = newVal;
	Calculate();
	return Acad::eOk;
}

const CRebarPos::DisplayStyle CRebarPos::Display(void) const
{
	assertReadEnabled();
	return m_DisplayStyle;
}

Acad::ErrorStatus CRebarPos::setDisplay(const CRebarPos::DisplayStyle newVal)
{
	assertWriteEnabled();
	m_DisplayStyle = newVal;

	// Do not include in BOQ if only marker is shown.
	if(m_DisplayStyle == CRebarPos::MARKERONLY)
		m_IncludeInBOQ = Adesk::kFalse;

	Calculate();
	return Acad::eOk;
}

const ACHAR* CRebarPos::Shape(void) const
{
	assertReadEnabled();
	return m_Shape;
}

Acad::ErrorStatus CRebarPos::setShape(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_Shape);
    m_Shape = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Shape);
    }
	Calculate();
	return Acad::eOk;
}

const ACHAR* CRebarPos::A(void) const
{
	assertReadEnabled();
	return m_A;
}

Acad::ErrorStatus CRebarPos::setA(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_A);
    m_A = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_A);
    }
	Calculate();
	return Acad::eOk;
}

const ACHAR* CRebarPos::B(void) const
{
	assertReadEnabled();
	return m_B;
}

Acad::ErrorStatus CRebarPos::setB(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_B);
    m_B = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_B);
    }
	Calculate();
	return Acad::eOk;
}

const ACHAR* CRebarPos::C(void) const
{
	assertReadEnabled();
	return m_C;
}

Acad::ErrorStatus CRebarPos::setC(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_C);
    m_C = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_C);
    }
	Calculate();
	return Acad::eOk;
}

const ACHAR* CRebarPos::D(void) const
{
	assertReadEnabled();
	return m_D;
}

Acad::ErrorStatus CRebarPos::setD(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_D);
    m_D = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_D);
    }
	Calculate();
	return Acad::eOk;
}

const ACHAR* CRebarPos::E(void) const
{
	assertReadEnabled();
	return m_E;
}

Acad::ErrorStatus CRebarPos::setE(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_E);
    m_E = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_E);
    }
	Calculate();
	return Acad::eOk;
}

const ACHAR* CRebarPos::F(void) const
{
	assertReadEnabled();
	return m_F;
}

Acad::ErrorStatus CRebarPos::setF(const ACHAR* newVal)
{
	assertWriteEnabled();
	acutDelString(m_F);
    m_F = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_F);
    }
	Calculate();
	return Acad::eOk;
}

const ACHAR* CRebarPos::Length(void) const
{
	assertReadEnabled();
	return m_Length;
}

const Adesk::Boolean CRebarPos::Detached(void) const
{
	assertReadEnabled();
	return m_Detached;
}

Acad::ErrorStatus CRebarPos::setDetached(const Adesk::Boolean newVal)
{
	assertWriteEnabled();
	m_Detached = newVal;
	if(newVal == Adesk::kTrue)
	{
		acutUpdString(_T(""), m_Key);
		acutUpdString(_T(""), m_Length);
		acutUpdString(_T(""), m_DisplayedSpacing);

		acutUpdString(_T(""), m_Count);
		acutUpdString(_T(""), m_Diameter);
		acutUpdString(_T(""), m_Spacing);

		m_IncludeInBOQ = Adesk::kFalse;
		m_Multiplier = 0;

		acutUpdString(_T(""), m_Note);
		acutUpdString(_T(""), m_Shape);
		acutUpdString(_T(""), m_A);
		acutUpdString(_T(""), m_B);
		acutUpdString(_T(""), m_C);
		acutUpdString(_T(""), m_D);
		acutUpdString(_T(""), m_E);
		acutUpdString(_T(""), m_F);

		m_DisplayStyle = CRebarPos::MARKERONLY;
	}
	Calculate();
	return Acad::eOk;
}

const ACHAR* CRebarPos::PosKey() const
{
	assertReadEnabled();
	return m_Key;
}

const CRebarPos::CCalculatedProperties& CRebarPos::CalcProps() const
{
	assertReadEnabled();
	return m_CalcProps;
}

Acad::ErrorStatus CRebarPos::setGroupForDisplay(const CPosGroup* newVal)
{
	assertWriteEnabled();
	m_GroupForDisplay = const_cast<CPosGroup*>(newVal);
	Calculate();
	return Acad::eOk;
}

//*************************************************************************
// Class Methods
//*************************************************************************

/// Determines which part is under the given point
const CRebarPos::PosSubEntityType CRebarPos::HitTest(const AcGePoint3d& pt0) const
{
	// Transform to text coordinate system
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, NormalVector());
	if(trans.isSingular())
	{
		return CRebarPos::NONE;
	}
	trans.invert();
	AcGePoint3d pt(pt0);
	pt.transformBy(trans);
		
	CDrawParams p;
	if(lastDrawList.size() != 0)
	{
		for(DrawListSize i = 0; i < lastDrawList.size(); i++)
		{
			p = lastDrawList[i];
			if(pt.x > p.x && pt.x <= p.x + p.w && pt.y > p.y && pt.y < p.y + p.h)
			{
				return (CRebarPos::PosSubEntityType)p.type;
			}
		}
	}
	// Check multiplier text
	p = lastMultiplierDraw;
	if(pt.x > p.x && pt.x <= p.x + p.w && pt.y > p.y && pt.y < p.y + p.h)
	{
		return CRebarPos::MULTIPLIER;
	}

	// Transform to note coordinate system
	trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_NoteGrip, m_Direction, m_Up, NormalVector());
	if(trans.isSingular())
	{
		return CRebarPos::NONE;
	}
	trans.invert();
	pt = pt0;
	pt.transformBy(trans);
	p = lastNoteDraw;
	if(pt.x > p.x && pt.x <= p.x + p.w && pt.y > p.y && pt.y < p.y + p.h)
	{
		return CRebarPos::NOTE;
	}

	// Transform to length coordinate system
	trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_LengthGrip, m_Direction, m_Up, NormalVector());
	if(trans.isSingular())
	{
		return CRebarPos::NONE;
	}
	trans.invert();
	pt = pt0;
	pt.transformBy(trans);
	p = lastLengthDraw;
	if(pt.x > p.x && pt.x <= p.x + p.w && pt.y > p.y && pt.y < p.y + p.h)
	{
		return CRebarPos::LENGTH;
	}

	return CRebarPos::NONE;
}

const void CRebarPos::Update(void)
{
	assertWriteEnabled();

	Calculate();
}

//*************************************************************************
// Overridden methods from AcDbEntity
//*************************************************************************

Acad::ErrorStatus CRebarPos::subGetOsnapPoints(
    AcDb::OsnapMode       osnapMode,
    Adesk::GsMarker       gsSelectionMark,
    const AcGePoint3d&    pickPoint,
    const AcGePoint3d&    lastPoint,
    const AcGeMatrix3d&   viewXform,
    AcGePoint3dArray&     snapPoints,
    AcDbIntArray&         /*geomIds*/) const
{
	assertReadEnabled();

	if(gsSelectionMark == 0)
        return Acad::eOk;

	if(osnapMode == AcDb::kOsModeIns)
		snapPoints.append(m_BasePoint);

	return Acad::eOk;
}

Acad::ErrorStatus CRebarPos::subGetGripPoints(
    AcGePoint3dArray& gripPoints,
    AcDbIntArray& osnapModes,
    AcDbIntArray& geomIds) const
{
	assertReadEnabled();
	gripPoints.append(m_BasePoint);
	if(m_Detached == Adesk::kFalse && m_DisplayStyle != CRebarPos::MARKERONLY) gripPoints.append(m_NoteGrip);
	if(m_Detached == Adesk::kFalse && m_DisplayStyle == CRebarPos::ALL) gripPoints.append(m_LengthGrip);
	return Acad::eOk;
}

Acad::ErrorStatus CRebarPos::subMoveGripPointsAt(
    const AcDbIntArray& indices,
    const AcGeVector3d& offset)
{
    if(indices.length() == 0 || offset.isZeroLength())
        return Acad::eOk;

	assertWriteEnabled();

	// If there are more than one hot points or base point is hot, transform the entire entity
	if(indices.length() > 1 || (indices.length() == 1 && indices[0] == 0))
		transformBy(AcGeMatrix3d::translation(offset));

	// Transform the note grip
	if(indices.length() == 1 && indices[0] == 1)
		m_NoteGrip.transformBy(AcGeMatrix3d::translation(offset));

	// Transform the length grip
	if(indices.length() == 1 && indices[0] == 2)
		m_LengthGrip.transformBy(AcGeMatrix3d::translation(offset));

	return Acad::eOk;
}

Acad::ErrorStatus CRebarPos::subTransformBy(const AcGeMatrix3d& xform)
{
	assertWriteEnabled();
	
	m_BasePoint.transformBy(xform);
	m_NoteGrip.transformBy(xform);
	m_LengthGrip.transformBy(xform);
	m_Direction.transformBy(xform);
	m_Up.transformBy(xform);

	// Get UCS vectors
	AcGeVector3d ucsdir(m_Direction);
	ucsdir.transformBy(ucs);
	AcGeVector3d ucsup(m_Up);
	ucsup.transformBy(ucs);

	// Set mirror base point to middle of entity
	AcGePoint3d pt;
	if(lastDrawList.size() != 0)
	{
		CDrawParams p = lastDrawList.at(lastDrawList.size() - 1);
		pt.x = (p.x + p.w) / 2.0;
		pt.y = (p.y + p.h) / 2.0;
		// Transform to WCS
		AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
		trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, NormalVector());
		pt.transformBy(trans);
	}

	// Text always left to right
	if(ucsdir.x < 0)
	{
		// Mirror around vertical axis
		AcGeMatrix3d mirror = AcGeMatrix3d::kIdentity;
		mirror.setToMirroring(AcGeLine3d(pt, m_Up));
		m_BasePoint.transformBy(mirror);
		m_NoteGrip.transformBy(mirror);
		m_LengthGrip.transformBy(mirror);
		m_Direction.transformBy(mirror);
		m_Up.transformBy(mirror);
	}

	// Text always upright
	if(ucsup.y < 0)
	{
		// Mirror around horizontal axis
		AcGeMatrix3d mirror = AcGeMatrix3d::kIdentity;
		mirror.setToMirroring(AcGeLine3d(pt, m_Direction));
		m_BasePoint.transformBy(mirror);
		m_NoteGrip.transformBy(mirror);
		m_LengthGrip.transformBy(mirror);
		m_Direction.transformBy(mirror);
		m_Up.transformBy(mirror);
	}

	// Correct direction vectors
	double scale = m_Direction.length();
	m_Direction = m_Direction.normal() * scale;
	m_Up = m_Up.normal() * scale;

	Calculate();

	return Acad::eOk;
}

void CRebarPos::subList() const
{
    assertReadEnabled();

	// Call parent first
    AcDbEntity::subList();

	// Base point in UCS
    acutPrintf(_T("%18s%16s "), _T(/*MSG0*/""), _T("Base Point:"));
    AcGePoint3d pt(m_BasePoint);
    acdbWcs2Ucs(asDblArray(pt), asDblArray(pt), false);
    acutPrintf(_T("X = %-9.16q0, Y = %-9.16q0, Z = %-9.16q0\n"), pt.x, pt.y, pt.z);

	// Scale
    acutPrintf(_T("%18s%16s %-9.16q0\n"), _T(/*MSG0*/""), _T("Scale:"), m_Direction.length());    

	// List all properties
	if (m_Pos != NULL)
		acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("Pos Marker:"), m_Pos);

	if(m_Detached == Adesk::kFalse)
	{
		if (m_Count != NULL)
			acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("Count:"), m_Count);
		if (m_Diameter != NULL)
			acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("Diameter:"), m_Diameter);
		if (m_Spacing != NULL)
			acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("Spacing:"), m_Spacing);

		if(m_IncludeInBOQ == Adesk::kTrue)
			acutPrintf(_T("%18s%16s %i\n"), _T(/*MSG0*/""), _T("Multiplier:"), m_Multiplier);
		else
			acutPrintf(_T("%18s%16s %i (Not Included in BOQ)\n"), _T(/*MSG0*/""), _T("Multiplier:"), m_Multiplier);

		// Shape
		if ((m_Shape != NULL) && (m_Shape[0] != _T('\0')))
		{
			acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("Shape:"), m_Shape);
		}

		// Lengths
		if ((m_A != NULL) && (m_A[0] != _T('\0')))
			acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("A Length:"), m_A);
		if ((m_B != NULL) && (m_B[0] != _T('\0')))
			acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("B Length:"), m_B);
		if ((m_C != NULL) && (m_C[0] != _T('\0')))
			acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("C Length:"), m_C);
		if ((m_D != NULL) && (m_D[0] != _T('\0')))
			acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("D Length:"), m_D);
		if ((m_E != NULL) && (m_E[0] != _T('\0')))
			acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("E Length:"), m_E);
		if ((m_F != NULL) && (m_F[0] != _T('\0')))
			acutPrintf(_T("%18s%16s %s\n"), _T(/*MSG0*/""), _T("F Length:"), m_F);
	}
}

Acad::ErrorStatus CRebarPos::subExplode(AcDbVoidPtrArray& entitySet) const
{
	const double PI = 4.0 * atan(1.0);

    assertReadEnabled();

	if(lastDrawList.size() == 0)
	{
		return Acad::eInvalidInput;
	}

	Acad::ErrorStatus es;

	// Open the one and only group
	AcDbObjectId groupId;
	CPosGroup::GetGroupId(groupId);
	AcDbObjectPointer<CPosGroup> pGroup (groupId, AcDb::kForRead);
	if((es = pGroup.openStatus()) != Acad::eOk)
	{
		return es;
	}
	// Open text styles
	AcDbObjectId textStyle = pGroup->TextStyleId();
	AcDbObjectId noteStyle = pGroup->NoteStyleId();
	AcDbObjectPointer<AcDbTextStyleTableRecord> pTextStyle (textStyle, AcDb::kForRead);
	if((es = pTextStyle.openStatus()) != Acad::eOk)
	{
		return es;
	}
	AcDbObjectPointer<AcDbTextStyleTableRecord> pNoteStyle (noteStyle, AcDb::kForRead);
	if((es = pNoteStyle.openStatus()) != Acad::eOk)
	{
		return es;
	}

	// Transformations
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, NormalVector());
	AcGeMatrix3d noteTrans = AcGeMatrix3d::kIdentity;
	noteTrans.setCoordSystem(m_NoteGrip, m_Direction, m_Up, NormalVector());
	AcGeMatrix3d lengthTrans = AcGeMatrix3d::kIdentity;
	lengthTrans.setCoordSystem(m_LengthGrip, m_Direction, m_Up, NormalVector());
	
    AcDbText* text;
	CDrawParams p;
	for(DrawListSize i = 0; i < lastDrawList.size(); i++)
	{
		p = lastDrawList[i];
		if(!p.text.empty())
		{
			text = new AcDbText(AcGePoint3d(p.x, p.y, 0), p.text.c_str(), textStyle, 1.0);
			text->setColorIndex(p.color);
			text->setWidthFactor(p.widthFactor);
			if((es = text->transformBy(trans)) != Acad::eOk)
			{
				return es;
			}
			entitySet.append(text);
		}
		// Circle
		if(p.hasCircle)
		{
			AcDbCircle* circle;
			circle = new AcDbCircle(AcGePoint3d(p.x + p.w / 2, p.y + p.h / 2, 0), AcGeVector3d::kZAxis, circleRadius);
			circle->setColorIndex(lastCircleColor);
			if((es = circle->transformBy(trans)) != Acad::eOk)
			{
				return es;
			}
			entitySet.append(circle);
		}
		// Tau sign
		if(p.hasTau)
		{			
			// ellipse
			AcDbEllipse* ellipse;
			ellipse = new AcDbEllipse();
			AcGePoint3d centerpt(p.x - partSpacing / 2.0 - tauSize / 2.0, p.y + p.h / 2.0, 0);
			ellipse->set(centerpt, AcGeVector3d::kZAxis, AcGeVector3d::kYAxis * 0.35, tauSize / 0.8);
			ellipse->setColorIndex(p.color);
			if((es = ellipse->transformBy(trans)) != Acad::eOk)
			{
				return es;
			}
			entitySet.append(ellipse);
			// vertical line
			AcDbLine* line;
			AcGePoint3d linept[2];
			linept[0] = AcGePoint3d(p.x - partSpacing / 2.0 - tauSize / 2.0, p.y + p.h / 2.0 - 0.5, 0);
			linept[1] = AcGePoint3d(p.x - partSpacing / 2.0 - tauSize / 2.0, p.y + p.h / 2.0 + 0.5, 0);
			line = new AcDbLine(linept[0], linept[1]);
			line->setColorIndex(p.color);
			if((es = line->transformBy(trans)) != Acad::eOk)
			{
				return es;
			}
			entitySet.append(line);
			// horizontal line
			AcDbLine* cline;
			AcGePoint3d clinept[2];
			clinept[0] = AcGePoint3d(p.x - partSpacing / 2.0 - tauSize / 2.0 - 0.2, p.y + p.h / 2.0 + 0.5, 0);
			clinept[1] = AcGePoint3d(p.x - partSpacing / 2.0 - tauSize / 2.0 + 0.2, p.y + p.h / 2.0 + 0.5, 0);
			cline = new AcDbLine(clinept[0], clinept[1]);
			cline->setColorIndex(p.color);
			if((es = cline->transformBy(trans)) != Acad::eOk)
			{
				return es;
			}
			entitySet.append(cline);
		}
	}
	p = lastNoteDraw;
	if(!p.text.empty())
	{
		text = new AcDbText(AcGePoint3d(p.x, p.y, 0), p.text.c_str(), noteStyle, 1.0 * pGroup->NoteScale());
		text->setColorIndex(p.color);
		text->setWidthFactor(p.widthFactor);
		if((es = text->transformBy(noteTrans)) != Acad::eOk)
		{
			return es;
		}
		entitySet.append(text);
	}
	p = lastLengthDraw;
	if(!p.text.empty())
	{
		text = new AcDbText(AcGePoint3d(p.x, p.y, 0), p.text.c_str(), textStyle, 1.0);
		text->setColorIndex(p.color);
		text->setWidthFactor(p.widthFactor);
		if((es = text->transformBy(lengthTrans)) != Acad::eOk)
		{
			return es;
		}
		entitySet.append(text);
	}

    return Acad::eOk;
}

Adesk::Boolean CRebarPos::subWorldDraw(AcGiWorldDraw* worldDraw)
{
	const double PI = 4.0 * atan(1.0);

    assertReadEnabled();

    if(worldDraw->regenAbort())
	{
        return Adesk::kTrue;
    }

	// Quit if there is nothing to draw
	if(lastDrawList.empty())
	{
		return Adesk::kTrue;
	}

	// Transformations
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, NormalVector());
	AcGeMatrix3d noteTrans = AcGeMatrix3d::kIdentity;
	noteTrans.setCoordSystem(m_NoteGrip, m_Direction, m_Up, NormalVector());
	AcGeMatrix3d lengthTrans = AcGeMatrix3d::kIdentity;
	lengthTrans.setCoordSystem(m_LengthGrip, m_Direction, m_Up, NormalVector());

	// Transform to match text orientation
	worldDraw->geometry().pushModelTransform(trans);
	// Draw items
	worldDraw->subEntityTraits().setSelectionMarker(1);
	lastTextStyle.setTextSize(1.0);
	lastTextStyle.loadStyleRec();
	for(DrawListSize i = 0; i < lastDrawList.size(); i++)
	{
		CDrawParams p = lastDrawList.at(i);

		// Circle
		if(p.hasCircle)
		{
			Utility::DrawCircle(worldDraw, AcGePoint3d(p.x + p.w / 2.0, p.y + p.h / 2.0, 0), circleRadius, lastCircleColor);
		}

		// Tau sign
		if(p.hasTau)
		{
			Utility::DrawEllipticalArc(worldDraw, 
				AcGePoint3d(p.x - partSpacing / 2.0 - tauSize / 2.0, p.y + p.h / 2.0, 0),
				0.35, tauSize * 0.5, 2.0 * PI, 0.5 * PI, p.color);

			Utility::DrawLine(worldDraw,
				AcGePoint3d(p.x - partSpacing / 2.0 - tauSize / 2.0, p.y + p.h / 2.0 - 0.5, 0),
				AcGePoint3d(p.x - partSpacing / 2.0 - tauSize / 2.0, p.y + p.h / 2.0 + 0.5, 0),
				p.color);

			Utility::DrawLine(worldDraw,
				AcGePoint3d(p.x - partSpacing / 2.0 - tauSize / 2.0 - 0.2, p.y + p.h / 2.0 + 0.5, 0),
				AcGePoint3d(p.x - partSpacing / 2.0 - tauSize / 2.0 + 0.2, p.y + p.h / 2.0 + 0.5, 0),
				p.color);
		}

		// Text
		if(p.widthFactor != lastTextStyle.xScale())
		{
			lastTextStyle.setXScale(p.widthFactor);
			lastTextStyle.loadStyleRec();
		}
		Utility::DrawText(worldDraw, AcGePoint3d(p.x, p.y, 0), p.text, lastTextStyle, p.color);
	}

	// Reset transform
	worldDraw->geometry().popModelTransform();

	// Transform to match note orientation
	worldDraw->geometry().pushModelTransform(noteTrans);
	// Draw note text
	lastNoteStyle.setTextSize(lastNoteScale);
	lastNoteStyle.setXScale(lastNoteDraw.widthFactor);
	lastNoteStyle.loadStyleRec();
	worldDraw->subEntityTraits().setSelectionMarker(2);
	Utility::DrawText(worldDraw, AcGePoint3d(lastNoteDraw.x, lastNoteDraw.y, 0), lastNoteDraw.text, lastNoteStyle, lastNoteDraw.color);
	// Reset transform
	worldDraw->geometry().popModelTransform();

	// Transform to match length orientation
	worldDraw->geometry().pushModelTransform(lengthTrans);
	// Draw length text
	lastTextStyle.setTextSize(lastLengthDraw.h);
	lastTextStyle.setXScale(lastLengthDraw.widthFactor);
	lastTextStyle.loadStyleRec();
	worldDraw->subEntityTraits().setSelectionMarker(3);
	Utility::DrawText(worldDraw, AcGePoint3d(lastLengthDraw.x, lastLengthDraw.y, 0), lastLengthDraw.text, lastTextStyle, lastLengthDraw.color);
	// Reset transform
	worldDraw->geometry().popModelTransform();

	// Do not call viewportDraw()
    return Adesk::kTrue; 
}

void CRebarPos::saveAs(AcGiWorldDraw *worldDraw, AcDb::SaveType saveType)
{
    assertReadEnabled();

    if(worldDraw->regenAbort())
	{
        return;
    }

	AcDbVoidPtrArray entitySet;
	explode(entitySet);
	for(int i = 0; i < entitySet.length(); ++i)
	{
		AcDbEntity* ent = static_cast<AcDbEntity*>(entitySet[i]);
		worldDraw->geometry().draw(ent);
		delete ent;
	}
	entitySet.removeAll();
}

Acad::ErrorStatus CRebarPos::subGetGeomExtents(AcDbExtents& extents) const
{
    assertReadEnabled();

	if(lastDrawList.empty())
	{
		return Acad::eInvalidExtents;
	}

	// Get ECS extents
	CDrawParams p;
	p = lastDrawList.at(lastDrawList.size() - 1);
	AcGePoint3d pt1(0, 0, 0);
	AcGePoint3d pt2(p.x + p.w, 0, 0);
	AcGePoint3d pt3(p.x + p.w, p.y + p.h, 0);
	AcGePoint3d pt4(0, p.y + p.h, 0);
	p = lastNoteDraw;
	AcGePoint3d pt5(0, 0, 0);
	AcGePoint3d pt6(p.x + p.w, 0, 0);
	AcGePoint3d pt7(p.x + p.w, p.y + p.h, 0);
	AcGePoint3d pt8(0, p.y + p.h, 0);
	p = lastLengthDraw;
	AcGePoint3d pt9(0, 0, 0);
	AcGePoint3d pt10(p.x + p.w, 0, 0);
	AcGePoint3d pt11(p.x + p.w, p.y + p.h, 0);
	AcGePoint3d pt12(0, p.y + p.h, 0);

	// Transform to WCS
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, NormalVector());
	pt1.transformBy(trans);
	pt2.transformBy(trans);
	pt3.transformBy(trans);
	pt4.transformBy(trans);
	AcGeMatrix3d noteTrans = AcGeMatrix3d::kIdentity;
	noteTrans.setCoordSystem(m_NoteGrip, m_Direction, m_Up, NormalVector());
	pt5.transformBy(noteTrans);
	pt6.transformBy(noteTrans);
	pt7.transformBy(noteTrans);
	pt8.transformBy(noteTrans);
	AcGeMatrix3d lengthTrans = AcGeMatrix3d::kIdentity;
	lengthTrans.setCoordSystem(m_LengthGrip, m_Direction, m_Up, NormalVector());
	pt9.transformBy(lengthTrans);
	pt10.transformBy(lengthTrans);
	pt11.transformBy(lengthTrans);
	pt12.transformBy(lengthTrans);

	extents.addPoint(pt1);
	extents.addPoint(pt2);
	extents.addPoint(pt3);
	extents.addPoint(pt4);
	extents.addPoint(pt5);
	extents.addPoint(pt6);
	extents.addPoint(pt7);
	extents.addPoint(pt8);
	extents.addPoint(pt9);
	extents.addPoint(pt10);
	extents.addPoint(pt11);
	extents.addPoint(pt12);
	
	return Acad::eOk;
}

//*************************************************************************
// Overridden methods from AcDbObject
//*************************************************************************

Acad::ErrorStatus CRebarPos::dwgOutFields(AcDbDwgFiler* pFiler) const
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbEntity::dwgOutFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number
	pFiler->writeItem(CRebarPos::kCurrentVersionNumber);

	pFiler->writePoint3d(m_BasePoint);
	pFiler->writePoint3d(m_NoteGrip);
	pFiler->writePoint3d(m_LengthGrip);
	pFiler->writeVector3d(m_Direction);
	pFiler->writeVector3d(m_Up);

	if (m_Pos)
		pFiler->writeString(m_Pos);
	else
		pFiler->writeString(_T(""));
	if(m_Note)
		pFiler->writeString(m_Note);
	else
		pFiler->writeString(_T(""));
	if(m_Count)
		pFiler->writeString(m_Count);
	else
		pFiler->writeString(_T(""));
	if(m_Diameter)
		pFiler->writeString(m_Diameter);
	else
		pFiler->writeString(_T(""));
	if(m_Spacing)
		pFiler->writeString(m_Spacing);
	else
		pFiler->writeString(_T(""));
	pFiler->writeBoolean(m_IncludeInBOQ);
	pFiler->writeInt32(m_Multiplier);
	pFiler->writeInt32(m_DisplayStyle);

	if(m_Shape)
		pFiler->writeString(m_Shape);

	if(m_A)
		pFiler->writeString(m_A);
	else
		pFiler->writeString(_T(""));
	if(m_B)
		pFiler->writeString(m_B);
	else
		pFiler->writeString(_T(""));
	if(m_C)
		pFiler->writeString(m_C);
	else
		pFiler->writeString(_T(""));
	if(m_D)
		pFiler->writeString(m_D);
	else
		pFiler->writeString(_T(""));
	if(m_E)
		pFiler->writeString(m_E);
	else
		pFiler->writeString(_T(""));
	if(m_F)
		pFiler->writeString(m_F);
	else
		pFiler->writeString(_T(""));

	pFiler->writeBoolean(m_Detached);

	return pFiler->filerStatus();
}

Acad::ErrorStatus CRebarPos::dwgInFields(AcDbDwgFiler* pFiler)
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbEntity::dwgInFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number needs to be read first
	Adesk::UInt32 version = 0;
	pFiler->readItem(&version);
	if (version > CRebarPos::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Read params
	if (version >= 1)
	{
		pFiler->readPoint3d(&m_BasePoint);
		pFiler->readPoint3d(&m_NoteGrip);
		pFiler->readPoint3d(&m_LengthGrip);
		pFiler->readVector3d(&m_Direction);
		pFiler->readVector3d(&m_Up);
		
		acutDelString(m_Pos);
		acutDelString(m_Note);
		acutDelString(m_Count);
		acutDelString(m_Diameter);
		acutDelString(m_Spacing);
		acutDelString(m_A);
		acutDelString(m_B);
		acutDelString(m_C);
		acutDelString(m_D);
		acutDelString(m_E);
		acutDelString(m_F);

		pFiler->readString(&m_Pos);
		pFiler->readString(&m_Note);
		pFiler->readString(&m_Count);
		pFiler->readString(&m_Diameter);
		pFiler->readString(&m_Spacing);
		pFiler->readBoolean(&m_IncludeInBOQ);
		pFiler->readInt32(&m_Multiplier);
		Adesk::Int32 display = 0;
		pFiler->readInt32(&display);
		m_DisplayStyle = (CRebarPos::DisplayStyle)display;
		pFiler->readString(&m_Shape);
		pFiler->readString(&m_A);
		pFiler->readString(&m_B);
		pFiler->readString(&m_C);
		pFiler->readString(&m_D);
		pFiler->readString(&m_E);
		pFiler->readString(&m_F);
		pFiler->readBoolean(&m_Detached);
	}

	Calculate();

	return pFiler->filerStatus();
}

Acad::ErrorStatus CRebarPos::dxfOutFields(AcDbDxfFiler* pFiler) const
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbEntity::dxfOutFields(pFiler)) != Acad::eOk)
		return es;

	// Subclass
	pFiler->writeItem(AcDb::kDxfSubclass, _T("RebarPos"));

	// Object version number
	pFiler->writeItem(AcDb::kDxfInt32, CRebarPos::kCurrentVersionNumber);

	// Geometry
	pFiler->writePoint3d(AcDb::kDxfXCoord, m_BasePoint);
	pFiler->writePoint3d(AcDb::kDxfXCoord + 1, m_NoteGrip);
	pFiler->writePoint3d(AcDb::kDxfXCoord + 2, m_LengthGrip);
	// Use max precision when writing out direction vectors
	pFiler->writeVector3d(AcDb::kDxfXCoord + 3, m_Direction, 16);
	pFiler->writeVector3d(AcDb::kDxfXCoord + 4, m_Up, 16);

	// Properties
	if(m_Pos)
		pFiler->writeString(AcDb::kDxfText, m_Pos);
	else
		pFiler->writeString(AcDb::kDxfText, _T(""));
	if(m_Note)
		pFiler->writeString(AcDb::kDxfXTextString, m_Note);
	else
		pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	if(m_Count)
		pFiler->writeString(AcDb::kDxfXTextString + 1, m_Count);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 1, _T(""));
	if(m_Diameter)
		pFiler->writeString(AcDb::kDxfXTextString + 2, m_Diameter);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 2, _T(""));
	if(m_Spacing)
		pFiler->writeString(AcDb::kDxfXTextString + 3, m_Spacing);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 3, _T(""));
	pFiler->writeBoolean(AcDb::kDxfBool, m_IncludeInBOQ);
	pFiler->writeInt32(AcDb::kDxfInt32 + 1, m_Multiplier);
	pFiler->writeInt32(AcDb::kDxfInt32 + 2, m_DisplayStyle);
	if(m_Shape)
		pFiler->writeString(AcDb::kDxfText + 1, m_Shape);
	else
		pFiler->writeString(AcDb::kDxfText + 1, _T(""));
	if(m_A)
		pFiler->writeString(AcDb::kDxfXTextString + 4, m_A);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 4, _T(""));
	if(m_B)
		pFiler->writeString(AcDb::kDxfXTextString + 5, m_B);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 5, _T(""));
	if(m_C)
		pFiler->writeString(AcDb::kDxfXTextString + 6, m_C);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 6, _T(""));
	if(m_D)
		pFiler->writeString(AcDb::kDxfXTextString + 7, m_D);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 7, _T(""));
	if(m_E)
		pFiler->writeString(AcDb::kDxfXTextString + 8, m_E);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 8, _T(""));
	if(m_F)
		pFiler->writeString(AcDb::kDxfXTextString + 9, m_F);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 9, _T(""));

	pFiler->writeBoolean(AcDb::kDxfBool + 1, m_Detached);

	return pFiler->filerStatus();
}

Acad::ErrorStatus CRebarPos::dxfInFields(AcDbDxfFiler* pFiler)
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if(((es = AcDbEntity::dxfInFields(pFiler)) != Acad::eOk) || !pFiler->atSubclassData(_T("RebarPos")))
		return es;

	resbuf rb;
	// Object version number
    Adesk::UInt32 version;
    pFiler->readItem(&rb);
    if (rb.restype != AcDb::kDxfInt32) 
    {
        pFiler->pushBackItem();
        pFiler->setError(Acad::eInvalidDxfCode, _T("\nError: expected group code %d (version)"), AcDb::kDxfInt32);
        return pFiler->filerStatus();
    }
    version = rb.resval.rlong;
	if (version > CRebarPos::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Read params
	AcGePoint3d t_BasePoint, t_NoteGrip, t_LengthGrip;
	AcGeVector3d t_Direction, t_Up;
	ACHAR* t_Pos = NULL;
	ACHAR* t_Note = NULL;
	ACHAR* t_Count = NULL;
	ACHAR* t_Diameter = NULL;
	ACHAR* t_Spacing = NULL;
	Adesk::Boolean t_IncludeInBOQ = Adesk::kTrue;
	Adesk::Int32 t_Multiplier = 0;
	int t_Display = 0;
	ACHAR* t_Shape = NULL;
	ACHAR* t_A = NULL;
	ACHAR* t_B = NULL;
	ACHAR* t_C = NULL;
	ACHAR* t_D = NULL;
	ACHAR* t_E = NULL;
	ACHAR* t_F = NULL;
	Adesk::Boolean t_Detached = Adesk::kFalse;

    while ((es == Acad::eOk) && ((es = pFiler->readResBuf(&rb)) == Acad::eOk))
    {
        switch (rb.restype) 
		{
        case AcDb::kDxfXCoord:
            t_BasePoint = asPnt3d(rb.resval.rpoint);
            break;
        case AcDb::kDxfXCoord + 1:
            t_NoteGrip = asPnt3d(rb.resval.rpoint);
            break;
        case AcDb::kDxfXCoord + 2:
            t_LengthGrip = asPnt3d(rb.resval.rpoint);
            break;
        case AcDb::kDxfXCoord + 3:
			t_Direction = asVec3d(rb.resval.rpoint);
            break;
        case AcDb::kDxfXCoord + 4:
            t_Up = asVec3d(rb.resval.rpoint);
            break;

        case AcDb::kDxfText:
            acutUpdString(rb.resval.rstring, t_Pos);
            break;
        case AcDb::kDxfXTextString:
            acutUpdString(rb.resval.rstring, t_Note);
            break;
        case AcDb::kDxfXTextString + 1:
            acutUpdString(rb.resval.rstring, t_Count);
            break;
        case AcDb::kDxfXTextString + 2:
            acutUpdString(rb.resval.rstring, t_Diameter);
            break;
        case AcDb::kDxfXTextString + 3:
            acutUpdString(rb.resval.rstring, t_Spacing);
            break;

        case AcDb::kDxfBool:
			t_IncludeInBOQ = (rb.resval.rint == 0) ? Adesk::kFalse : Adesk::kTrue;
            break;
        case AcDb::kDxfInt32 + 1:
            t_Multiplier = rb.resval.rlong;
            break;

        case AcDb::kDxfInt32 + 2:
            t_Display = rb.resval.rlong;
            break;

        case AcDb::kDxfText + 1:
            acutUpdString(rb.resval.rstring, t_Shape);
            break;
        case AcDb::kDxfXTextString + 4:
            acutUpdString(rb.resval.rstring, t_A);
            break;
        case AcDb::kDxfXTextString + 5:
            acutUpdString(rb.resval.rstring, t_B);
            break;
        case AcDb::kDxfXTextString + 6:
            acutUpdString(rb.resval.rstring, t_C);
            break;
        case AcDb::kDxfXTextString + 7:
            acutUpdString(rb.resval.rstring, t_D);
            break;
        case AcDb::kDxfXTextString + 8:
            acutUpdString(rb.resval.rstring, t_E);
            break;
        case AcDb::kDxfXTextString + 9:
            acutUpdString(rb.resval.rstring, t_F);
            break;

        case AcDb::kDxfBool + 1:
			t_Detached = (rb.resval.rint == 0) ? Adesk::kFalse : Adesk::kTrue;
            break;

        default:
            // An unrecognized group. Push it back so that
            // the subclass can read it again.
            pFiler->pushBackItem();
            es = Acad::eEndOfFile;
            break;
        }
    }

    // At this point the es variable must contain eEndOfFile
    // - either from readResBuf() or from pushback. If not,
    // it indicates that an error happened and we should
    // return immediately.
    //
    if (es != Acad::eEndOfFile)
        return Acad::eInvalidResBuf;

	// Successfully read DXF codes; set object properties.
	m_BasePoint = t_BasePoint;
	m_NoteGrip = t_NoteGrip;
	m_LengthGrip = t_LengthGrip;
	m_Direction = t_Direction;
	m_Up = t_Up;

	setPos(t_Pos);
	setNote(t_Note);
	setCount(t_Count);
	setDiameter(t_Diameter);
	setSpacing(t_Spacing);
	m_IncludeInBOQ = t_IncludeInBOQ;
	m_Multiplier = t_Multiplier;
	m_DisplayStyle = (CRebarPos::DisplayStyle)t_Display;
	setShape(t_Shape);
	setA(t_A);
	setB(t_B);
	setC(t_C);
	setD(t_D);
	setE(t_E);
	setF(t_F);
	setDetached(t_Detached);

	acutDelString(t_Pos);
	acutDelString(t_Note);
	acutDelString(t_Count);
	acutDelString(t_Diameter);
	acutDelString(t_Spacing);
	acutDelString(t_Shape);
	acutDelString(t_A);
	acutDelString(t_B);
	acutDelString(t_C);
	acutDelString(t_D);
	acutDelString(t_E);
	acutDelString(t_F);

	Calculate();

    return es;
}

Acad::ErrorStatus CRebarPos::subDeepClone(AcDbObject*    pOwner,
                    AcDbObject*&   pClonedObject,
                    AcDbIdMapping& idMap,
                    Adesk::Boolean isPrimary) const
{
    // You should always pass back pClonedObject == NULL
    // if, for any reason, you do not actually clone it
    // during this call.  The caller should pass it in
    // as NULL, but to be safe, we set it here as well.
    //
    pClonedObject = NULL;

    // If this object is in the idMap and is already
    // cloned, then return.
    //
    bool isPrim = false;
    if (isPrimary)
        isPrim = true;
    AcDbIdPair idPair(objectId(), (AcDbObjectId)NULL, false, isPrim);
    if (idMap.compute(idPair) && (idPair.value() != NULL))
        return Acad::eOk;    

    // Create the clone
    //
    CRebarPos *pClone = (CRebarPos*)isA()->create();
    if (pClone != NULL)
        pClonedObject = pClone;    // set the return value
    else
        return Acad::eOutOfMemory;

    AcDbDeepCloneFiler filer;
    dwgOut(&filer);

    filer.seek(0L, AcDb::kSeekFromStart);
    pClone->dwgIn(&filer);
    bool bOwnerXlated = false;
    if (isPrimary)
    {
        AcDbBlockTableRecord *pBTR = AcDbBlockTableRecord::cast(pOwner);
        if (pBTR != NULL)
        {
            pBTR->appendAcDbEntity(pClone);
            bOwnerXlated = true;
        }
        else
        {
            pOwner->database()->addAcDbObject(pClone);
        }
    } 
	else 
	{
        pOwner->database()->addAcDbObject(pClone);
        pClone->setOwnerId(pOwner->objectId());
        bOwnerXlated = true;
    }

    // This must be called for all newly created objects
    // in deepClone.  It is turned off by endDeepClone()
    // after it has translated the references to their
    // new values.
    //
    pClone->setAcDbObjectIdsInFlux();
    pClone->disableUndoRecording(true);


    // Add the new information to the idMap.  We can use
    // the idPair started above.
    //
    idPair.setValue(pClonedObject->objectId());
    idPair.setIsCloned(Adesk::kTrue);
    idPair.setIsOwnerXlated(bOwnerXlated);
    idMap.assign(idPair);

    // Using the filer list created above, find and clone
    // any owned objects.
    //
    AcDbObjectId id;
    while (filer.getNextOwnedObject(id)) 
	{
        AcDbObject *pSubObject;
        AcDbObject *pClonedSubObject;

        // Some object's references may be set to NULL, 
        // so don't try to clone them.
        //
        if (id == NULL)
            continue;

        // Open the object and clone it.  Note that we now
        // set "isPrimary" to kFalse here because the object
        // is being cloned, not as part of the primary set,
        // but because it is owned by something in the
        // primary set.
        //
        acdbOpenAcDbObject(pSubObject, id, AcDb::kForRead);
        pClonedSubObject = NULL;
        pSubObject->deepClone(pClonedObject, pClonedSubObject, idMap, Adesk::kFalse);

        // If this is a kDcInsert context, the objects
        // may be "cheapCloned".  In this case, they are
        // "moved" instead of cloned.  The result is that
        // pSubObject and pClonedSubObject will point to
        // the same object.  So, we only want to close
        // pSubObject if it really is a different object
        // than its clone.
        //
        if (pSubObject != pClonedSubObject)
            pSubObject->close();
        
        // The pSubObject may either already have been
        // cloned, or for some reason has chosen not to be
        // cloned.  In that case, the returned pointer will
        // be NULL.  Otherwise, since we have no immediate
        // use for it now, we can close the clone.
        //
        if (pClonedSubObject != NULL)
            pClonedSubObject->close();
    }

    // Leave pClonedObject open for the caller
    //
    return Acad::eOk;
}


Acad::ErrorStatus CRebarPos::subWblockClone(AcRxObject*    pOwner,
                      AcDbObject*&   pClonedObject,
                      AcDbIdMapping& idMap,
                      Adesk::Boolean isPrimary) const
{
    // You should always pass back pClonedObject == NULL
    // if, for any reason, you do not actually clone it
    // during this call.  The caller should pass it in
    // as NULL, but to be safe, we set it here as well.
    //
    pClonedObject = NULL;

    // If this is a fast wblock operation then no cloning
    // should take place, so we simply call the base class's
    // wblockClone() and return whatever it returns.
    //
    // For fast wblock, the source and destination databases
    // are the same, so we can use that as the test to see
    // if a fast wblock is in progress.
    //
    AcDbDatabase *pDest, *pOrig;
    idMap.destDb(pDest);
    idMap.origDb(pOrig);
    if (pDest == pOrig)
        return AcDbEntity::subWblockClone(pOwner, pClonedObject, idMap, isPrimary);

    // If this is an Xref bind operation and this 
    // entity is in Paper Space,  then we don't want to
    // clone because Xref bind doesn't support cloning
    // entities in Paper Space.  So we simply return
    // Acad::eOk
    //
    AcDbObjectId pspace;
    AcDbBlockTable *pTable;
    database()->getSymbolTable(pTable, AcDb::kForRead);
    pTable->getAt(ACDB_PAPER_SPACE, pspace);
    pTable->close(); 

    if (idMap.deepCloneContext() == AcDb::kDcXrefBind && ownerId() == pspace)
        return Acad::eOk;
    
    // If this object is in the idMap and is already
    // cloned, then return.
    //
    bool isPrim = false;
    if (isPrimary)
        isPrim = true;

    AcDbIdPair idPair(objectId(), (AcDbObjectId)NULL, false, isPrim);
    if (idMap.compute(idPair) && (idPair.value() != NULL))
        return Acad::eOk;    

    // The owner object can be either an AcDbObject, or an
    // AcDbDatabase.  AcDbDatabase is used if the caller is
    // not the owner of the object being cloned (because it
    // is being cloned as part of an AcDbHardPointerId
    // reference).  In this case, the correct ownership
    // will be set during reference translation.  So, if
    // the owner is an AcDbDatabase, then pOwn will be left
    // NULL here, and is used as a "flag" later.
    //

    AcDbObject   *pOwn = AcDbObject::cast(pOwner);
    AcDbDatabase *pDb = AcDbDatabase::cast(pOwner);
    if (pDb == NULL) 
        pDb = pOwn->database();

    // STEP 1:
    // Create the clone
    //
    CRebarPos *pClone = (CRebarPos*)isA()->create();
    if (pClone != NULL)
        pClonedObject = pClone;    // set the return value
    else
        return Acad::eOutOfMemory;

    // STEP 2:
    // If the owner is an AcDbBlockTableRecord, go ahead
    // and append the clone.  If not, but we know who the
    // owner is, set the clone's ownerId to it.  Otherwise,
    // we set the clone's ownerId to our own ownerId (in
    // other words, the original owner Id).  This Id will
    // then be used later, in reference translation, as
    // a key to finding who the new owner should be.  This
    // means that the original owner must also be cloned at
    // some point during the wblock operation. 
    // EndDeepClone's reference translation aborts if the
    // owner is not found in the idMap.
    //
    // The most common situation where this happens is
    // AcDbEntity references to Symbol Table Records, such
    // as the Layer an Entity is on.  This is when you will
    // have to pass in the destination database as the owner
    // of the Layer Table Record.  Since all Symbol Tables
    // are always cloned in Wblock, you do not need to make
    // sure that Symbol Table Record owners are cloned. 
    //
    // However, if the owner is one of your own classes,
    // then it is up to you to make sure that it gets
    // cloned.  This is probably easiest to do at the end
    // of this function.  Otherwise you may have problems
    // with recursion when the owner, in turn, attempts
    // to clone this object as one of its subObjects.
    // 
    AcDbBlockTableRecord *pBTR = NULL;
    if (pOwn != NULL)
        pBTR = AcDbBlockTableRecord::cast(pOwn);
    if (pBTR != NULL && isPrimary) 
	{
        pBTR->appendAcDbEntity(pClone);
    } 
	else 
	{
        pDb->addAcDbObject(pClonedObject);
    }

    // STEP 3:
    // The AcDbWblockCloneFiler makes a list of
    // AcDbHardOwnershipIds and AcDbHardPointerIds.  These
    // are the references which must be cloned during a
    // Wblock operation.
    //
    AcDbWblockCloneFiler filer;
    dwgOut(&filer);

    // STEP 4:
    // Rewind the filer and read the data into the clone.
    //
    filer.seek(0L, AcDb::kSeekFromStart);
    pClone->dwgIn(&filer);

    idMap.assign(AcDbIdPair(objectId(), pClonedObject->objectId(), Adesk::kTrue, isPrim, (Adesk::Boolean)(pOwn != NULL)));

    pClonedObject->setOwnerId((pOwn != NULL) ? pOwn->objectId() : ownerId());

    // STEP 5:
    // This must be called for all newly created objects
    // in wblockClone.  It is turned off by endDeepClone()
    // after it has translated the references to their
    // new values.
    //
    pClone->setAcDbObjectIdsInFlux();

    // STEP 6:
    // Add the new information to the idMap.  We can use
    // the idPair started above.  We must also let the
    // idMap entry know whether the clone's owner is
    // correct, or needs to be translated later.
    //

    // STEP 7:
    // Using the filer list created above, find and clone
    // any hard references.
    //
    AcDbObjectId id;
    while (filer.getNextHardObject(id)) 
	{
        AcDbObject *pSubObject;
        AcDbObject *pClonedSubObject;

        // Some object's references may be set to NULL, 
        // so don't try to clone them.
        //
        if (id == NULL)
            continue;

        // If the referenced object is from a different
        // database, such as an xref, do not clone it.
        //
        acdbOpenAcDbObject(pSubObject, id, AcDb::kForRead);
        if (pSubObject->database() != database()) 
		{
            pSubObject->close();
            continue;
        }

        // We can find out if this is an AcDbHardPointerId
        // verses an AcDbHardOwnershipId, by seeing if we
        // are the owner of the pSubObject.  If we are not,
        // then we cannot pass our clone in as the owner
        // for the pSubObject's clone.  In that case, we
        // pass in our clone's database (the destination
        // database).
        // 
        // Note that we now set "isPrimary" to kFalse here
        // because the object is being cloned, not as part
        // of the primary set, but because it is owned by
        // something in the primary set.
        //
        pClonedSubObject = NULL;
        if (pSubObject->ownerId() == objectId()) 
		{
            pSubObject->wblockClone(pClone, pClonedSubObject, idMap, Adesk::kFalse);
        }
		else 
		{
            pSubObject->wblockClone(pClone->database(), pClonedSubObject, idMap, Adesk::kFalse);
        }
        pSubObject->close();
        
        // The pSubObject may either already have been
        // cloned, or for some reason has chosen not to be
        // cloned.  In that case, the returned pointer will
        // be NULL.  Otherwise, since we have no immediate
        // use for it now, we can close the clone.
        //
        if (pClonedSubObject != NULL)
            pClonedSubObject->close();
    }

    // Leave pClonedObject open for the caller.
    //
    return Acad::eOk;
}

// Return the CLSID of the class here
Acad::ErrorStatus CRebarPos::subGetClassID(CLSID* pClsid) const
{
    assertReadEnabled();
	// See the interface definition file for the CLASS ID
	CLSID clsid = {0x97CAC17D, 0xB1C7, 0x49ca, { 0x8D, 0x57, 0xD3, 0xFF, 0x49, 0x18, 0x60, 0xFF}};
    *pClsid = clsid;
    return Acad::eOk;
}

//*************************************************************************
// Helper methods
//*************************************************************************

void CRebarPos::Calculate(void)
{
	assertWriteEnabled();

	// Current UCS
	if(!geomInit)
	{
		acdbUcsMatrix(ucs);
		geomInit = true;
	}

	// Open group and shape
	Acad::ErrorStatus es;
	const CPosGroup* pGroup = NULL;
	AcDbObjectPointer<CPosGroup> pGroupPointer;
	if(m_GroupForDisplay != NULL)
	{
		pGroup = m_GroupForDisplay;
	}
	else
	{
		AcDbObjectId groupId;
		CPosGroup::GetGroupId(groupId);
		pGroupPointer.open(groupId, AcDb::kForRead);
		if((es = pGroupPointer.openStatus()) != Acad::eOk)
		{
			return;
		}
		pGroup = pGroupPointer;
	}
	if(pGroup == NULL) return;

	// Reset calculated properties
	m_CalcProps.Reset();
	m_CalcProps.Generation = m_CalcProps.Generation + 1;

	// Create text styles
	if (!pGroup->TextStyleId().isNull())
		Utility::MakeGiTextStyle(lastTextStyle, pGroup->TextStyleId());
	if (!pGroup->NoteStyleId().isNull())
		Utility::MakeGiTextStyle(lastNoteStyle, pGroup->NoteStyleId());
	lastNoteScale = pGroup->NoteScale();

	if(m_Detached)
	{
		acutUpdString(_T(""), m_Length);
		acutUpdString(_T(""), m_DisplayedSpacing);
		acutUpdString(_T(""), m_Key);

		// Rebuild draw lists
		lastDrawList.clear();
		if(pGroup->FormulaPosOnly() != NULL)
		{
			lastDrawList = ParseFormula(pGroup->FormulaPosOnly());
		}

		lastNoteDraw.text = L"";
		lastLengthDraw.text = L"";
		lastMultiplierDraw.text = L"-";
	}
	else
	{
		m_CalcProps.Bending = (pGroup->Bending() == Adesk::kTrue);
		m_CalcProps.Precision = pGroup->Precision();
		m_CalcProps.DrawingUnit = pGroup->DrawingUnit();
		m_CalcProps.DisplayUnit = pGroup->DisplayUnit();
		if(m_Shape == NULL || m_Shape[0] == _T('\0'))
		{
			return;
		}
		CPosShape* pShape = CPosShape::GetPosShape(m_Shape);
		const ACHAR* formula;
		if(m_CalcProps.Bending)
		{
			formula = pShape->FormulaBending();
		}
		else
		{
			formula = pShape->Formula();
		}
		m_CalcProps.FieldCount = pShape->Fields();

		// Calculate count
		if(m_Count != NULL && m_Count[0] != _T('\0'))
		{
			std::wstring countstring(m_Count);
			std::vector<std::wstring> countlist;
			int minCount = 100000; int maxCount = 0; int totalCount = 0;
			int currCount = 0;
			try
			{
				Utility::ReplaceString(countstring, L"x", L"*");
				Utility::ReplaceString(countstring, L"X", L"*");
				totalCount = Utility::DoubleToInt(Calculator::Evaluate(countstring));
				countlist = Utility::SplitString(countstring, L'*');
				for(std::vector<std::wstring>::iterator cit = countlist.begin(); cit != countlist.end(); cit++)
				{
					currCount = Utility::DoubleToInt(Calculator::Evaluate(*cit));
					if(currCount > maxCount) maxCount = currCount;
					if(currCount < minCount) minCount = currCount;
				}

				m_CalcProps.Count = totalCount;
				m_CalcProps.MinCount = minCount;
				m_CalcProps.MaxCount = maxCount;
				m_CalcProps.IsMultiCount = (countlist.size() > 1);
			}
			catch(...)
			{
				m_CalcProps.Count = 0;
				m_CalcProps.MinCount = 0;
				m_CalcProps.MaxCount = 0;
				m_CalcProps.IsMultiCount = false;
			}
		}

		// Calculate diameter
		if(m_Diameter != NULL && m_Diameter[0] != _T('\0'))
			m_CalcProps.Diameter = Utility::StrToDouble(m_Diameter);

		// Calculate piece lengths
		if(m_CalcProps.FieldCount >= 1)
			GetLengths(m_A, m_CalcProps.Diameter, m_CalcProps.DrawingUnit, m_CalcProps.MinA, m_CalcProps.MaxA, m_CalcProps.IsVarA);
		if(m_CalcProps.FieldCount >= 2)
			GetLengths(m_B, m_CalcProps.Diameter, m_CalcProps.DrawingUnit, m_CalcProps.MinB, m_CalcProps.MaxB, m_CalcProps.IsVarB);
		if(m_CalcProps.FieldCount >= 3)
			GetLengths(m_C, m_CalcProps.Diameter, m_CalcProps.DrawingUnit, m_CalcProps.MinC, m_CalcProps.MaxC, m_CalcProps.IsVarC);
		if(m_CalcProps.FieldCount >= 4)
			GetLengths(m_D, m_CalcProps.Diameter, m_CalcProps.DrawingUnit, m_CalcProps.MinD, m_CalcProps.MaxD, m_CalcProps.IsVarD);
		if(m_CalcProps.FieldCount >= 5)
			GetLengths(m_E, m_CalcProps.Diameter, m_CalcProps.DrawingUnit, m_CalcProps.MinE, m_CalcProps.MaxE, m_CalcProps.IsVarE);
		if(m_CalcProps.FieldCount >= 6)
			GetLengths(m_F, m_CalcProps.Diameter, m_CalcProps.DrawingUnit, m_CalcProps.MinF, m_CalcProps.MaxF, m_CalcProps.IsVarF);

		// Calculate total length
		m_CalcProps.IsVarLength = m_CalcProps.IsVarA || m_CalcProps.IsVarB || m_CalcProps.IsVarC || m_CalcProps.IsVarD || m_CalcProps.IsVarE || m_CalcProps.IsVarF;
		GetTotalLengths(formula, m_CalcProps);

		// Calculate spacing
		GetLengths(m_Spacing, 0.0, m_CalcProps.DrawingUnit, m_CalcProps.MinSpacing, m_CalcProps.MaxSpacing, m_CalcProps.IsVarSpacing);

		// Scale from MM to display units
		double scale = ConvertLength(1.0, CPosGroup::MM, m_CalcProps.DisplayUnit);

		// Set length text
		std::wstring strL1;
		Utility::DoubleToStr(m_CalcProps.MinLength * scale, m_CalcProps.Precision, strL1);
		std::wstring strL2;
		Utility::DoubleToStr(m_CalcProps.MaxLength * scale, m_CalcProps.Precision, strL2);
		std::wstring strL;
		if(m_CalcProps.IsVarLength)
		{
			strL = strL1 + L"~" + strL2;
		}
		else
		{
			strL = strL1;
		}
		acutUpdString(strL.c_str(), m_Length);

		// Set spacing text
		std::wstring strS1;
		Utility::DoubleToStr(m_CalcProps.MinSpacing * scale, m_CalcProps.Precision, strS1);
		std::wstring strS2;
		Utility::DoubleToStr(m_CalcProps.MaxSpacing * scale, m_CalcProps.Precision, strS2);
		std::wstring strS;
		if(m_CalcProps.IsVarSpacing)
		{
			strS = strS1 + L"~" + strS2;
		}
		else
		{
			strS = strS1;
		}
		acutUpdString(strS.c_str(), m_DisplayedSpacing);

		// Shape code
		std::wstringstream oss;
		if(m_Diameter != NULL && m_Diameter[0] != _T('\0'))
			oss << L"T" << m_Diameter;
		if(m_Shape != NULL && m_Shape[0] != _T('\0'))
			oss << L":S" << m_Shape;
	
		if(m_CalcProps.FieldCount >= 1 && m_A != NULL && m_A[0] != _T('\0'))
			oss << L":A" << m_A;
		if(m_CalcProps.FieldCount >= 2 && m_B != NULL && m_B[0] != _T('\0'))
			oss << L":B" << m_B;
		if(m_CalcProps.FieldCount >= 3 && m_C != NULL && m_C[0] != _T('\0'))
			oss << L":C" << m_C;
		if(m_CalcProps.FieldCount >= 4 && m_D != NULL && m_D[0] != _T('\0'))
			oss << L":D" << m_D;
		if(m_CalcProps.FieldCount >= 5 && m_E != NULL && m_E[0] != _T('\0'))
			oss << L":E" << m_E;
		if(m_CalcProps.FieldCount >= 6 && m_F != NULL && m_F[0] != _T('\0'))
			oss << L":F" << m_F;
		acutUpdString(oss.str().c_str(), m_Key);

		// Rebuild draw lists
		lastDrawList.clear();
		if((m_DisplayStyle == CRebarPos::ALL || m_DisplayStyle == CRebarPos::WITHOUTLENGTH) && m_CalcProps.IsVarLength && pGroup->FormulaVariableLength() != NULL)
		{
			lastDrawList = ParseFormula(pGroup->FormulaVariableLength());
		}
		else if((m_DisplayStyle == CRebarPos::ALL || m_DisplayStyle == CRebarPos::WITHOUTLENGTH) && pGroup->Formula() != NULL)
		{
			lastDrawList = ParseFormula(pGroup->Formula());
		}
		else if(m_DisplayStyle == CRebarPos::MARKERONLY && pGroup->FormulaPosOnly() != NULL)
		{
			lastDrawList = ParseFormula(pGroup->FormulaPosOnly());
		}

		if(m_DisplayStyle != CRebarPos::MARKERONLY && m_Note != NULL && m_Note[0] != _T('\0'))
			lastNoteDraw.text = m_Note;
		else
			lastNoteDraw.text = L"";

		if(m_DisplayStyle == CRebarPos::ALL && pGroup->FormulaLengthOnly() != NULL)
		{
			DrawList lengthList = ParseFormula(pGroup->FormulaLengthOnly());
			if(lengthList.size() == 0)
				lastLengthDraw.text = L"";
			else
				lastLengthDraw = lengthList[0];
		}
		else
		{
			lastLengthDraw.text = L"";
		}

		if(m_Multiplier == 0)
			lastMultiplierDraw.text = L"-";
		else
			Utility::IntToStr(m_Multiplier, lastMultiplierDraw.text);
	}

	// Set width factors
	for(DrawListSize i = 0; i < lastDrawList.size(); i++)
	{
		CDrawParams p = lastDrawList[i];
		if(p.type == CRebarPos::POS && p.hasCircle && p.text.length() > 2)
		{
			p.widthFactor = 0.5;
		}
		else
		{
			p.widthFactor = lastTextStyle.xScale();
		}
		lastDrawList[i] = p;
	}
	lastNoteDraw.widthFactor = lastNoteStyle.xScale();
	lastLengthDraw.widthFactor = lastTextStyle.xScale();

	// Set colors
	Utility::CreateHiddenLayer(defpointsLayer);
	lastCircleColor = pGroup->CircleColor();
	lastMultiplierDraw.color = pGroup->MultiplierColor();
	for(DrawListSize i = 0; i < lastDrawList.size(); i++)
	{
		CDrawParams p = lastDrawList[i];
		switch(p.type)
		{
		case CRebarPos::POS:
			p.color = pGroup->PosColor();
			break;
		case CRebarPos::MULTIPLIER:
			p.color = pGroup->MultiplierColor();
			break;
		default:
			p.color = pGroup->TextColor();
		}
		lastDrawList[i] = p;
	}
	lastNoteDraw.color = pGroup->NoteColor();
	lastLengthDraw.color = pGroup->TextColor();

	// Measure items
	lastTextStyle.setTextSize(1.0);
	lastTextStyle.loadStyleRec();
	lastNoteStyle.setTextSize(1.0 * lastNoteScale);
	lastNoteStyle.loadStyleRec();
	double x = 0;
	double y = 0;
	for(DrawListSize i = 0; i < lastDrawList.size(); i++)
	{
		CDrawParams p = lastDrawList[i];
		if(p.widthFactor != lastTextStyle.xScale())
		{
			lastTextStyle.setXScale(p.widthFactor);
			lastTextStyle.loadStyleRec();
		}
		AcGePoint2d ext = lastTextStyle.extents(p.text.c_str(), Adesk::kTrue, -1, Adesk::kFalse);
		if(p.hasCircle)
		{
			p.x = x + (2.0 * circleRadius - ext.x) / 2.0;
		}
		else if(p.hasTau)
		{
			p.x = x + tauSize + partSpacing;
		}
		else
		{
			p.x = x;
		}
		p.y = y;
		p.w = ext.x;
		p.h = lastTextStyle.textSize();
		if(p.hasCircle)
		{
			x += 2.0 * circleRadius + partSpacing;
		}
		else if(p.hasTau)
		{
			x += ext.x + tauSize + partSpacing;
		}
		else
		{
			x += ext.x + partSpacing;
		}
		lastDrawList[i] = p;
	}
	// Measure multiplier text
	AcGePoint2d mext = lastTextStyle.extents(lastMultiplierDraw.text.c_str(), Adesk::kTrue, -1, Adesk::kFalse);
	lastMultiplierDraw.x = 0;
	lastMultiplierDraw.y = 1.4;
	lastMultiplierDraw.w = mext.x;
	lastMultiplierDraw.h = lastTextStyle.textSize();

	// Measure note text
	AcGePoint2d noteExt = lastNoteStyle.extents(lastNoteDraw.text.c_str(), Adesk::kTrue, -1, Adesk::kFalse);
	lastNoteDraw.x = 0;
	lastNoteDraw.y = 0;
	lastNoteDraw.w = noteExt.x;
	lastNoteDraw.h = lastNoteStyle.textSize();

	// Measure length text
	lastTextStyle.setXScale(lastLengthDraw.widthFactor);
	lastTextStyle.loadStyleRec();
	AcGePoint2d lengthExt = lastTextStyle.extents(lastLengthDraw.text.c_str(), Adesk::kTrue, -1, Adesk::kFalse);
	lastLengthDraw.x = 0;
	lastLengthDraw.y = 0;
	lastLengthDraw.w = lengthExt.x;
	lastLengthDraw.h = lastTextStyle.textSize();
}

bool CRebarPos::GetLengths(const ACHAR* str, const double diameter, const CPosGroup::DrawingUnits inputUnit, double& minLength, double& maxLength, bool& isVar)
{
	std::wstring length;
	if(str != NULL)
		length.assign(str);
	std::wstring length1, length2;
	
	// Get variable lengths
	size_t i = length.find(L'~');
	if(i != -1)
	{
		length1 = length.substr(0, i);
		length2 = length.substr(i + 1, length.length() - i - 1);
		isVar = true;
	}
	else
	{
		length1 = length;
		length2 = length;
		isVar = false;
	}
	
	// Calculate lengths
	bool check1 = CalcConsLength(length1.c_str(), diameter, inputUnit, minLength);
	bool check2 = CalcConsLength(length2.c_str(), diameter, inputUnit, maxLength);
	if(check1 && check2)
	{
		return true;
	}
	else
	{
		minLength = 0.0;
		maxLength = 0.0;
		return false;
	}
}

bool CRebarPos::CalcConsLength(const ACHAR* str, const double diameter, const CPosGroup::DrawingUnits inputUnit, double& value)
{
	std::wstring length;
	if(str != NULL)
		length.assign(str);

	double scale = ConvertLength(1.0, inputUnit, CPosGroup::MM);

	// Replace diameter and radius
	double d = diameter;
	double r = BendingRadius(d);
	// Convert units
	std::wstring strD, strR;
	d /= scale;
	r /= scale;
	Utility::DoubleToStr(d, strD);
	Utility::DoubleToStr(r, strR);
	Utility::ReplaceString(length, L"d", strD);
	Utility::ReplaceString(length, L"r", strR);
	Utility::ReplaceString(length, L"D", strD);
	Utility::ReplaceString(length, L"R", strR);
	
	// Calculate length
	try
	{
		value = Calculator::Evaluate(length) * scale;
		return true;
	}
	catch(...)
	{
		value = 0.0;
		return false;
	}
}

bool CRebarPos::GetTotalLengths(const ACHAR* formula, CRebarPos::CCalculatedProperties& props)
{
	std::wstring length1, length2;
	if(formula != NULL)
	{
		length1.assign(formula);
		length2.assign(formula);
	}

	// Replace lengths
	std::wstring strA1, strA2, strB1, strB2, strC1, strC2;
	std::wstring strD1, strD2, strE1, strE2, strF1, strF2;
	Utility::DoubleToStr(props.MinA, strA1);
	Utility::DoubleToStr(props.MaxA, strA2);
	Utility::DoubleToStr(props.MinB, strB1);
	Utility::DoubleToStr(props.MaxB, strB2);
	Utility::DoubleToStr(props.MinC, strC1);
	Utility::DoubleToStr(props.MaxC, strC2);
	Utility::DoubleToStr(props.MinD, strD1);
	Utility::DoubleToStr(props.MaxD, strD2);
	Utility::DoubleToStr(props.MinE, strE1);
	Utility::DoubleToStr(props.MaxE, strE2);
	Utility::DoubleToStr(props.MinF, strF1);
	Utility::DoubleToStr(props.MaxF, strF2);
	Utility::ReplaceString(length1, L"A", strA1);
	Utility::ReplaceString(length2, L"A", strA2);
	Utility::ReplaceString(length1, L"B", strB1);
	Utility::ReplaceString(length2, L"B", strB2);
	Utility::ReplaceString(length1, L"C", strC1);
	Utility::ReplaceString(length2, L"C", strC2);
	Utility::ReplaceString(length1, L"D", strD1);
	Utility::ReplaceString(length2, L"D", strD2);
	Utility::ReplaceString(length1, L"E", strE1);
	Utility::ReplaceString(length2, L"E", strE2);
	Utility::ReplaceString(length1, L"F", strF1);
	Utility::ReplaceString(length2, L"F", strF2);

	// Replace diameter and radius
	double dia = props.Diameter;
	double rad = BendingRadius(dia);
	std::wstring strD, strR;
	Utility::DoubleToStr(dia, strD);
	Utility::DoubleToStr(rad, strR);
	Utility::ReplaceString(length1, L"d", strD);
	Utility::ReplaceString(length2, L"d", strD);
	Utility::ReplaceString(length1, L"r", strR);
	Utility::ReplaceString(length2, L"r", strR);
	Utility::ReplaceString(length1, L"D", strD);
	Utility::ReplaceString(length2, L"D", strD);
	Utility::ReplaceString(length1, L"R", strR);
	Utility::ReplaceString(length2, L"R", strR);

	// Calculate lengths
	try
	{
		props.MinLength = Calculator::Evaluate(length1);
		props.MaxLength = Calculator::Evaluate(length2);
		return true;
	}
	catch(...)
	{
		props.MinLength = 0;
		props.MaxLength = 0;
		return false;
	}
}

bool CRebarPos::GetTotalLengths(const ACHAR* formula, const int fieldCount, const CPosGroup::DrawingUnits inputUnit, const ACHAR* a, const ACHAR* b, const ACHAR* c, const ACHAR* d, const ACHAR* e, const ACHAR* f, const ACHAR* diameter, double& minLength, double& maxLength, bool& isVar)
{
	std::wstring length1, length2;
	if(formula != NULL)
	{
		length1.assign(formula);
		length2.assign(formula);
	}

	double dia = 0.0;
	if(diameter != NULL && diameter[0] != _T('\0'))
		dia = Utility::StrToDouble(diameter);
	double rad = BendingRadius(dia);

	// Calculate piece lengths
	double A1 = 0.0, A2 = 0.0, B1 = 0.0, B2 = 0.0, C1 = 0.0, C2 = 0.0, D1 = 0.0, D2 = 0.0, E1 = 0.0, E2 = 0.0, F1 = 0.0, F2 = 0.0;
	bool Avar = false, Bvar = false, Cvar = false, Dvar = false, Evar = false, Fvar = false;
	std::wstring strA1, strA2, strB1, strB2, strC1, strC2;
	std::wstring strD1, strD2, strE1, strE2, strF1, strF2;
	if(fieldCount >= 1)
		GetLengths(a, dia, inputUnit, A1, A2, Avar);
	if(fieldCount >= 2)
		GetLengths(b, dia, inputUnit, B1, B2, Bvar);
	if(fieldCount >= 3)
		GetLengths(c, dia, inputUnit, C1, C2, Cvar);
	if(fieldCount >= 4)
		GetLengths(d, dia, inputUnit, D1, D2, Dvar);
	if(fieldCount >= 5)
		GetLengths(e, dia, inputUnit, E1, E2, Evar);
	if(fieldCount >= 6)
		GetLengths(f, dia, inputUnit, F1, F2, Fvar);

	isVar = Avar || Bvar || Cvar || Dvar || Evar || Fvar;

	// Replace lengths
	Utility::DoubleToStr(A1, strA1);
	Utility::DoubleToStr(A2, strA2);
	Utility::DoubleToStr(B1, strB1);
	Utility::DoubleToStr(B2, strB2);
	Utility::DoubleToStr(C1, strC1);
	Utility::DoubleToStr(C2, strC2);
	Utility::DoubleToStr(D1, strD1);
	Utility::DoubleToStr(D2, strD2);
	Utility::DoubleToStr(E1, strE1);
	Utility::DoubleToStr(E2, strE2);
	Utility::DoubleToStr(F1, strF1);
	Utility::DoubleToStr(F2, strF2);
	Utility::ReplaceString(length1, L"A", strA1);
	Utility::ReplaceString(length2, L"A", strA2);
	Utility::ReplaceString(length1, L"B", strB1);
	Utility::ReplaceString(length2, L"B", strB2);
	Utility::ReplaceString(length1, L"C", strC1);
	Utility::ReplaceString(length2, L"C", strC2);
	Utility::ReplaceString(length1, L"D", strD1);
	Utility::ReplaceString(length2, L"D", strD2);
	Utility::ReplaceString(length1, L"E", strE1);
	Utility::ReplaceString(length2, L"E", strE2);
	Utility::ReplaceString(length1, L"F", strF1);
	Utility::ReplaceString(length2, L"F", strF2);

	// Replace diameter and radius
	std::wstring strD, strR;
	Utility::DoubleToStr(dia, strD);
	Utility::DoubleToStr(rad, strR);
	Utility::ReplaceString(length1, L"d", strD);
	Utility::ReplaceString(length2, L"d", strD);
	Utility::ReplaceString(length1, L"r", strR);
	Utility::ReplaceString(length2, L"r", strR);
	Utility::ReplaceString(length1, L"D", strD);
	Utility::ReplaceString(length2, L"D", strD);
	Utility::ReplaceString(length1, L"R", strR);
	Utility::ReplaceString(length2, L"R", strR);

	// Calculate lengths
	try
	{
		minLength = Calculator::Evaluate(length1);
		maxLength = Calculator::Evaluate(length2);
		return true;
	}
	catch(...)
	{
		minLength = 0;
		maxLength = 0;
		return false;
	}
}

double CRebarPos::ConvertLength(const double length, const CPosGroup::DrawingUnits fromUnit, const CPosGroup::DrawingUnits toUnit)
{
	if(fromUnit == toUnit) return length;

	double scale = 1.0;

	// Convert from fromUnit to MM
	switch(fromUnit)
	{
	case CPosGroup::MM:
		scale *= 1.0;
		break;
	case CPosGroup::CM:
		scale *= 10.0;
		break;
	}

	// Convert from MM to toUnit
	switch(toUnit)
	{
	case CPosGroup::MM:
		scale /= 1.0;
		break;
	case CPosGroup::CM:
		scale /= 10.0;
		break;
	}

	return length * scale;
}

double CRebarPos::BendingRadius(const double d)
{
	if(d <= 16.0)
		return (2.0 * d);
	else
		return (3.5 * d);
}

const DrawList CRebarPos::ParseFormula(const ACHAR* formula) const
{
	assertReadEnabled();

	// Formula text
	// [M] Pos marker
	// [MM] Pos marker with double digits
	// [N] Bar count
	// [D] Bar diameter
	// [TD] Bar diameter with Tau sign prepended
	// [S] Spacing
	// [L] Total length
	// [M:C] Draw a circle around the item
	// Anything else is considered plain text and printed as-is
	// ["T":D] Prints the letter T before the diameter
	// e.g. [M:C][N]["T":D]["/":S] ["L=":L]

	DrawList list;
	list.clear();

	// First pass: separate parts
	std::wstring str;
	if(formula != NULL)
		str.assign(formula);
	std::wstring part;
	bool indeco = false;
	for(unsigned int i = 0; i < str.length(); i++)
	{
		wchar_t c = str.at(i);
		if((!indeco && c == L'[') || (indeco && (c == L']')) || (i == str.length() - 1))
		{
			if((i == str.length() - 1) && (c != L'[') && (c != L']'))
			{
				part += c;
			}
			if(!indeco && !part.empty())
			{
				CDrawParams p(0, part);
				list.push_back(p);
			}
			else if(indeco && !part.empty())
			{
				CDrawParams p(1, part);
				list.push_back(p);
			}
			indeco = (c == L'[');

			part.clear();
		}
		else
		{
			part += c;
		}
	}

	// Second pass: separate format strings and decorators
	DrawList finallist;
	finallist.clear();
	for(DrawListSize j = 0; j < list.size(); j++)
	{
		CDrawParams p = list[j];
		if(p.type == 0)
		{
			p.type = CRebarPos::NONE;
			p.hasCircle = false;
			finallist.push_back(p);
		}
		else if(p.type == 1)
		{
			p.type = CRebarPos::NONE;
			part.clear();
			std::vector<std::wstring> parts;
			bool inliteral = false;
			for(unsigned int i = 0; i < p.text.length(); i++)
			{
				wchar_t c = p.text.at(i);;
				if(!inliteral && (c == L'"'))
				{
					inliteral = true;
				}
				else if(inliteral && (c == L'"'))
				{
					parts.push_back(part);
					part.clear();
					inliteral = false;
				}
				else if(c == L':' || (i == p.text.length() - 1))
				{
					if(i == p.text.length() - 1)
					{
						part += c;
					}
					if(part == L"M")
					{
						p.type = CRebarPos::POS;
						std::wstring pos(m_Pos);
						if(pos.length() == 0) pos = _T(" ");
						parts.push_back(pos);
					}
					else if(part == L"MM")
					{
						p.type = CRebarPos::POS;
						std::wstring pos(m_Pos);
						if(pos.length() == 0) pos = L"  ";
						if(pos.length() == 1) pos = std::wstring(L"0").append(pos);
						parts.push_back(pos);
					}
					else if(part == L"N" && m_Count != NULL && m_Count[0] != _T('\0'))
					{
						p.type = CRebarPos::COUNT;
						parts.push_back(m_Count);
					}
					else if(part == L"NM" && m_Count != NULL && m_Count[0] != _T('\0'))
					{
						p.type = CRebarPos::COUNT;
						std::wstring countstr;
						Utility::IntToStr(m_CalcProps.MinCount, countstr);
						parts.push_back(countstr);
					}
					else if(part == L"NX" && m_Count != NULL && m_Count[0] != _T('\0'))
					{
						p.type = CRebarPos::COUNT;
						std::wstring countstr;
						Utility::IntToStr(m_CalcProps.MaxCount, countstr);
						parts.push_back(countstr);
					}
					else if(part == L"D" && m_Diameter != NULL && m_Diameter[0] != _T('\0'))
					{
						p.type = CRebarPos::DIAMETER;
						parts.push_back(m_Diameter);
					}
					else if(part == L"TD" && m_Diameter != NULL && m_Diameter[0] != _T('\0'))
					{
						p.type = CRebarPos::DIAMETER;
						p.hasTau = true;
						parts.push_back(m_Diameter);
					}
					else if(part == L"S" && m_DisplayedSpacing != NULL && m_DisplayedSpacing[0] != _T('\0') && m_DisplayedSpacing[0] != _T('0'))
					{
						p.type = CRebarPos::SPACING;
						parts.push_back(m_DisplayedSpacing);
					}
					else if(part == L"L" && m_Length != NULL && m_Length[0] != _T('\0') && m_Length[0] != _T('0'))
					{
						p.type = CRebarPos::LENGTH;
						parts.push_back(m_Length);
					}
					else if(part == L"C")
					{
						p.hasCircle = true;
					}
					part.clear();
				}
				else
				{
					part += c;
				}
			}
			if(p.type != CRebarPos::NONE)
			{
				p.text.clear();
				for(std::vector<std::wstring>::size_type k = 0; k < parts.size(); k++)
				{
					p.text.append(parts[k]);
				}
				finallist.push_back(p);
			}
		}
	}

	return finallist;
}
