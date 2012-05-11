//-----------------------------------------------------------------------------
//----- PosShape.cpp : Implementation of CPosShape
//-----------------------------------------------------------------------------

#define WIN32_LEAN_AND_MEAN
#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include <windows.h>
#include <objbase.h>

#include "rxregsvc.h"

#include "assert.h"
#include "math.h"

#include "gepnt3d.h"
#include "gevec3d.h"
#include "gelnsg3d.h"
#include "gearc3d.h"

#include "dbents.h"
#include "dbsymtb.h"
#include "dbcfilrs.h"
#include "dbspline.h"
#include "dbproxy.h"
#include "dbxutil.h"
#include "acutmem.h"

#include "acdb.h"
#include "dbidmap.h"
#include "adesk.h"

#include "dbapserv.h"
#include "appinfo.h"
#include "tchar.h"

#include "PosShape.h"
#include "Utility.h"

//-----------------------------------------------------------------------------
Adesk::UInt32 CPosShape::kCurrentVersionNumber = 1;

ACHAR* CPosShape::Table_Name = _T("OZOZ_REBAR_SHAPES");

//-----------------------------------------------------------------------------
ACRX_DXF_DEFINE_MEMBERS(CPosShape, AcDbObject,
	AcDb::kDHL_CURRENT, AcDb::kMReleaseCurrent,
	AcDbProxyEntity::kAllAllowedBits, POSSHAPE,
	"RebarPos2.0\
	|Product Desc:     PosShape Entity\
	|Company:          OZOZ");

//-----------------------------------------------------------------------------
CPosShape::CPosShape () : m_Name(NULL), m_Fields(1), m_Formula(NULL), m_FormulaBending(NULL), m_Priority(0)
{ }

CPosShape::~CPosShape () 
{ 
	acutDelString(m_Name);
	acutDelString(m_Formula);
	acutDelString(m_FormulaBending);

	ClearShapes();
}

//*************************************************************************
// Properties
//*************************************************************************
const ACHAR* CPosShape::Name(void) const
{
	assertReadEnabled();
	return m_Name;
}
Acad::ErrorStatus CPosShape::setName(const ACHAR* newVal)
{
	assertWriteEnabled();
    acutDelString(m_Name);
    m_Name = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Name);
    }
	return Acad::eOk;
}

const Adesk::Int32 CPosShape::Fields(void) const
{
	assertReadEnabled();
	return m_Fields;
}

Acad::ErrorStatus CPosShape::setFields(const Adesk::Int32 newVal)
{
	assertWriteEnabled();
	m_Fields = newVal;
	return Acad::eOk;
}

const ACHAR* CPosShape::Formula(void) const
{
	assertReadEnabled();
	return m_Formula;
}

Acad::ErrorStatus CPosShape::setFormula(const ACHAR* newVal)
{
	assertWriteEnabled();

    acutDelString(m_Formula);
    m_Formula = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_Formula);
    }

	return Acad::eOk;
}

const ACHAR* CPosShape::FormulaBending(void) const
{
	assertReadEnabled();
	return m_FormulaBending;
}

Acad::ErrorStatus CPosShape::setFormulaBending(const ACHAR* newVal)
{
	assertWriteEnabled();

    acutDelString(m_FormulaBending);
    m_FormulaBending = NULL;
    if(newVal != NULL)
    {
        acutUpdString(newVal, m_FormulaBending);
    }

	return Acad::eOk;
}

const Adesk::Int32 CPosShape::Priority(void) const
{
	assertReadEnabled();
	return m_Priority;
}

Acad::ErrorStatus CPosShape::setPriority(const Adesk::Int32 newVal)
{
	assertWriteEnabled();
    m_Priority = newVal;
	return Acad::eOk;
}

//*************************************************************************
// Methods
//*************************************************************************
void CPosShape::AddShape(CShape* const shape)
{
	assertWriteEnabled();
	m_List.push_back(shape);
}

const CShape* CPosShape::GetShape(const ShapeSize index) const
{
	assertReadEnabled();
	return m_List.at(index);
}

void CPosShape::SetShape(const ShapeSize index, CShape* const shape)
{
	assertWriteEnabled();
	m_List[index] = shape;
}

void CPosShape::RemoveShape(const ShapeSize index)
{
	assertWriteEnabled();
	ShapeListIt it = m_List.begin();
	m_List.erase(it + index);
}

void CPosShape::ClearShapes()
{
	assertWriteEnabled();
	for(ShapeListIt it = m_List.begin(); it != m_List.end(); it++)
	{
		delete *it;
	}
	m_List.clear();
}

const ShapeSize CPosShape::GetShapeCount() const
{
	assertReadEnabled();
	return m_List.size();
}

//*************************************************************************
// Overrides
//*************************************************************************
//- Dwg Filing protocol
Acad::ErrorStatus CPosShape::dwgOutFields(AcDbDwgFiler *pFiler) const 
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbObject::dwgOutFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number
	pFiler->writeItem(CPosShape::kCurrentVersionNumber);

	// Properties
	if(m_Name)
		pFiler->writeItem(m_Name);
	else
		pFiler->writeString(_T(""));
	pFiler->writeInt32(m_Fields);
	if (m_Formula)
		pFiler->writeString(m_Formula);
	else
		pFiler->writeString(_T(""));
	if (m_FormulaBending)
		pFiler->writeString(m_FormulaBending);
	else
		pFiler->writeString(_T(""));
	pFiler->writeInt32(m_Priority);

    // Segments
	pFiler->writeInt32((int)m_List.size());
	for(ShapeListConstIt it = m_List.begin(); it != m_List.end(); it++)
	{
		CShape* shape = *it;
		pFiler->writeInt32(shape->type);
		pFiler->writeUInt16(shape->color);
		switch(shape->type)
		{
		case CShape::Line:
			{
				CShapeLine* line = dynamic_cast<CShapeLine*>(shape);
				pFiler->writeDouble(line->x1);
				pFiler->writeDouble(line->y1);
				pFiler->writeDouble(line->x2);
				pFiler->writeDouble(line->y2);
			}
			break;
		case CShape::Arc:
			{
				CShapeArc* arc = dynamic_cast<CShapeArc*>(shape);
				pFiler->writeDouble(arc->x);
				pFiler->writeDouble(arc->y);
				pFiler->writeDouble(arc->r);
				pFiler->writeDouble(arc->startAngle);
				pFiler->writeDouble(arc->endAngle);
			}
			break;
		case CShape::Text:
			{
				CShapeText* text = dynamic_cast<CShapeText*>(shape);
				pFiler->writeDouble(text->x);
				pFiler->writeDouble(text->y);
				pFiler->writeDouble(text->height);
				pFiler->writeString(text->text);
			}
			break;
		}
	}

	return pFiler->filerStatus();
}

Acad::ErrorStatus CPosShape::dwgInFields(AcDbDwgFiler *pFiler) 
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbObject::dwgInFields(pFiler)) != Acad::eOk)
		return es;

	// Object version number needs to be read first
	Adesk::UInt32 version = 0;
	pFiler->readItem(&version);
	if (version > CPosShape::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Read params
	if (version >= 1)
	{
		acutDelString(m_Name);
		acutDelString(m_Formula);
		acutDelString(m_FormulaBending);

		// Properties
		pFiler->readString(&m_Name);
        pFiler->readInt32(&m_Fields);
		pFiler->readString(&m_Formula);
		pFiler->readString(&m_FormulaBending);
        pFiler->readInt32(&m_Priority);

		// Segments
		long count;
		pFiler->readInt32(&count);
		for(long i = 0; i < count; i++)
		{
			Adesk::Int32 type;
			pFiler->readInt32(&type);
			Adesk::UInt16 color;
			pFiler->readUInt16(&color);
			switch(type)
			{
			case CShape::Line:
				{
					CShapeLine* line = new CShapeLine();
					line->color = color;
					pFiler->readDouble(&line->x1);
					pFiler->readDouble(&line->y1);
					pFiler->readDouble(&line->x2);
					pFiler->readDouble(&line->y2);
					m_List.push_back(line);
				}
				break;
			case CShape::Arc:
				{
					CShapeArc* arc = new CShapeArc();
					arc->color = color;
					pFiler->readDouble(&arc->x);
					pFiler->readDouble(&arc->y);
					pFiler->readDouble(&arc->r);
					pFiler->readDouble(&arc->startAngle);
					pFiler->readDouble(&arc->endAngle);
					m_List.push_back(arc);
				}
				break;
			case CShape::Text:
				{
					CShapeText* text = new CShapeText();
					text->color = color;
					pFiler->readDouble(&text->x);
					pFiler->readDouble(&text->y);
					pFiler->readDouble(&text->height);
					pFiler->readString(&text->text);
					m_List.push_back(text);
				}
				break;
			}
		}
	}

	return pFiler->filerStatus();
}

//-----------------------------------------------------------------------------
//----- AcDbObject protocols
//- Dxf Filing protocol
Acad::ErrorStatus CPosShape::dxfOutFields(AcDbDxfFiler *pFiler) const 
{
	assertReadEnabled();

	// Save parent class information first.
	Acad::ErrorStatus es;
	if((es = AcDbObject::dxfOutFields(pFiler)) != Acad::eOk)
		return es;

	// Subclass
	pFiler->writeItem(AcDb::kDxfSubclass, _T("PosShape"));

	// Object version number
	pFiler->writeItem(AcDb::kDxfInt32, CPosShape::kCurrentVersionNumber);

	// Properties
	if(m_Name)
		pFiler->writeString(AcDb::kDxfXTextString, m_Name);
	else
		pFiler->writeString(AcDb::kDxfXTextString, _T(""));
	pFiler->writeInt32(AcDb::kDxfInt32 + 1, m_Fields);
	if(m_Formula)
		pFiler->writeString(AcDb::kDxfXTextString + 1, m_Formula);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 1, _T(""));
	if(m_FormulaBending)
		pFiler->writeString(AcDb::kDxfXTextString + 2, m_FormulaBending);
	else
		pFiler->writeString(AcDb::kDxfXTextString + 2, _T(""));
	pFiler->writeInt32(AcDb::kDxfInt32 + 2, m_Priority);

    // Segments
	pFiler->writeInt32(AcDb::kDxfInt32 + 3, (int)m_List.size());
	for(ShapeListConstIt it = m_List.begin(); it != m_List.end(); it++)
	{
		CShape* shape = *it;
		pFiler->writeInt32(AcDb::kDxfInt32, shape->type);
		pFiler->writeUInt16(AcDb::kDxfColor, shape->color);
		switch(shape->type)
		{
		case CShape::Line:
			{
				CShapeLine* line = dynamic_cast<CShapeLine*>(shape);
				pFiler->writeDouble(AcDb::kDxfXCoord, line->x1);
				pFiler->writeDouble(AcDb::kDxfYCoord, line->y1);
				pFiler->writeDouble(AcDb::kDxfXCoord + 1, line->x2);
				pFiler->writeDouble(AcDb::kDxfYCoord + 1, line->y2);
			}
			break;
		case CShape::Arc:
			{
				CShapeArc* arc = dynamic_cast<CShapeArc*>(shape);
				pFiler->writeDouble(AcDb::kDxfXCoord, arc->x);
				pFiler->writeDouble(AcDb::kDxfYCoord, arc->y);
				pFiler->writeDouble(AcDb::kDxfReal, arc->r);
				pFiler->writeDouble(AcDb::kDxfAngle, arc->startAngle);
				pFiler->writeDouble(AcDb::kDxfAngle + 1, arc->endAngle);
			}
			break;
		case CShape::Text:
			{
				CShapeText* text = dynamic_cast<CShapeText*>(shape);
				pFiler->writeDouble(AcDb::kDxfXCoord, text->x);
				pFiler->writeDouble(AcDb::kDxfYCoord, text->y);
				pFiler->writeDouble(AcDb::kDxfTxtSize, text->height);
				pFiler->writeString(AcDb::kDxfText, text->text);
			}
			break;
		}
	}

	return pFiler->filerStatus();
}

Acad::ErrorStatus CPosShape::dxfInFields(AcDbDxfFiler *pFiler) 
{
	assertWriteEnabled();

	// Read parent class information first.
	Acad::ErrorStatus es;
	if(((es = AcDbObject::dxfInFields(pFiler)) != Acad::eOk) || !pFiler->atSubclassData(_T("PosShape")))
		return es;

	// Object version number
    Adesk::UInt32 version;
	if((es = Utility::ReadDXFULong(pFiler, AcDb::kDxfInt32, _T("version"), version)) != Acad::eOk) return es;
	if (version > CPosShape::kCurrentVersionNumber)
		return Acad::eMakeMeProxy;

	// Properties
	ACHAR* t_Name = NULL;
	Adesk::Int32 t_Fields;
	ACHAR* t_Formula = NULL;
	ACHAR* t_FormulaBending = NULL;
	ShapeList t_List;
	Adesk::Int32 t_Priority;

	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString, _T("name"), t_Name)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32 + 1, _T("fields"), t_Fields)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString + 1, _T("formula"), t_Formula)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfXTextString + 2, _T("formula bending"), t_FormulaBending)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32 + 2, _T("priority"), t_Priority)) != Acad::eOk) return es;

	// Segments
	int count;
	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32 + 3, _T("segment count"), count)) != Acad::eOk) return es;
	for(int i = 0; i < count; i++)
	{
		Adesk::Int32 type;
		Adesk::UInt16 color;
		if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32, _T("segment type code"), type)) != Acad::eOk) return es;
		if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfColor, _T("segment color"), color)) != Acad::eOk) return es;

		switch(type)
		{
		case CShape::Line:
			{
				CShapeLine* line = new CShapeLine();
				line->color = color;
				AcGePoint2d p1, p2;
				if((es = Utility::ReadDXFPoint(pFiler, AcDb::kDxfXCoord, _T("segment x1"), p1)) != Acad::eOk) return es;
				if((es = Utility::ReadDXFPoint(pFiler, AcDb::kDxfXCoord + 1, _T("segment x2"), p2)) != Acad::eOk) return es;
				line->x1 = p1.x; line->y1 = p1.y;
				line->x2 = p2.x; line->y2 = p2.y;
				t_List.push_back(line);
			}
			break;
		case CShape::Arc:
			{
				CShapeArc* arc = new CShapeArc();
				arc->color = color;
				AcGePoint2d p;
				if((es = Utility::ReadDXFPoint(pFiler, AcDb::kDxfXCoord, _T("arc x"), p)) != Acad::eOk) return es;
				arc->x = p.x; arc->y = p.y;
				if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfReal, _T("arc r"), arc->r)) != Acad::eOk) return es;
				if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfAngle, _T("arc start angle"), arc->startAngle)) != Acad::eOk) return es;
				if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfAngle + 1, _T("arc end angle"), arc->endAngle)) != Acad::eOk) return es;
				t_List.push_back(arc);
			}
			break;
		case CShape::Text:
			{
				CShapeText* text = new CShapeText();
				text->color = color;
				AcGePoint2d p;
				if((es = Utility::ReadDXFPoint(pFiler, AcDb::kDxfXCoord, _T("text x"), p)) != Acad::eOk) return es;
				text->x = p.x; text->y = p.y;
				if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfTxtSize, _T("text height"), text->height)) != Acad::eOk) return es;
				if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("text contents"), text->text)) != Acad::eOk) return es;
				t_List.push_back(text);
			}
			break;
		}
	}

	setName(t_Name);
	m_Fields = t_Fields;
	setFormula(t_Formula);
	setFormulaBending(t_FormulaBending);
	m_List = t_List;
	m_Priority = t_Priority;

	acutDelString(t_Name);
	acutDelString(t_Formula);
	acutDelString(t_FormulaBending);

	return pFiler->filerStatus();
}

//*************************************************************************
// Common static dictionary methods
//*************************************************************************

ACHAR* CPosShape::GetTableName()
{
	return Table_Name;
}
