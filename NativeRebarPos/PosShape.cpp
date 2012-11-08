//-----------------------------------------------------------------------------
//----- PosShape.cpp : Implementation of CPosShape
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include "PosShape.h"
#include "Utility.h"
#include "resource.h"

std::map<std::wstring, CPosShape*> CPosShape::m_PosShapes;

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
	return m_Name;
}
Acad::ErrorStatus CPosShape::setName(const ACHAR* newVal)
{
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
	return m_Fields;
}

Acad::ErrorStatus CPosShape::setFields(const Adesk::Int32 newVal)
{
	m_Fields = newVal;
	return Acad::eOk;
}

const ACHAR* CPosShape::Formula(void) const
{
	return m_Formula;
}

Acad::ErrorStatus CPosShape::setFormula(const ACHAR* newVal)
{
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
	return m_FormulaBending;
}

Acad::ErrorStatus CPosShape::setFormulaBending(const ACHAR* newVal)
{
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
	return m_Priority;
}

Acad::ErrorStatus CPosShape::setPriority(const Adesk::Int32 newVal)
{
    m_Priority = newVal;
	return Acad::eOk;
}

//*************************************************************************
// Class Methods
//*************************************************************************
void CPosShape::AddShape(CShape* const shape)
{
	m_List.push_back(shape);
}

const CShape* CPosShape::GetShape(const ShapeSize index) const
{
	return m_List.at(index);
}

void CPosShape::SetShape(const ShapeSize index, CShape* const shape)
{
	delete m_List[index];
	m_List[index] = shape;
}

void CPosShape::RemoveShape(const ShapeSize index)
{
	delete m_List[index];
	ShapeListIt it = m_List.begin();
	m_List.erase(it + index);
}

void CPosShape::ClearShapes()
{
	for(ShapeListIt it = m_List.begin(); it != m_List.end(); it++)
	{
		delete *it;
	}
	m_List.clear();
}

const ShapeSize CPosShape::GetShapeCount() const
{
	return m_List.size();
}

const AcDbExtents CPosShape::GetShapeExtents() const
{
	AcDbExtents ext;
	for(ShapeListConstIt it = m_List.begin(); it != m_List.end(); it++)
	{
		CShape* shape = *it;
		switch(shape->type)
		{
		case CShape::Line:
			{
				CShapeLine* line = dynamic_cast<CShapeLine*>(shape);
				ext.addPoint(AcGePoint3d(line->x1, line->y1, 0));
				ext.addPoint(AcGePoint3d(line->x2, line->y2, 0));
			}
			break;
		case CShape::Arc:
			{
				CShapeArc* arc = dynamic_cast<CShapeArc*>(shape);
				double da = (arc->endAngle - arc->startAngle) / 10.0;
				for(double a = arc->startAngle; a <= arc->endAngle; a+= da)
				{
					double x = arc->x + cos(a) * arc->r;
					double y = arc->y + sin(a) * arc->r;
					ext.addPoint(AcGePoint3d(x, y, 0));
				}
			}
			break;
		case CShape::Text:
			{
				CShapeText* text = dynamic_cast<CShapeText*>(shape);
				ext.addPoint(AcGePoint3d(text->x - 2.0 * text->height, text->y, 0));
				ext.addPoint(AcGePoint3d(text->x + 2.0 * text->height, text->y + text->height, 0));
			}
			break;
		}
	}
	return ext;
}


//*************************************************************************
// Common static dictionary methods
//*************************************************************************
CPosShape* CPosShape::GetPosShape(std::wstring name)
{
	assert(m_PosShapes.find(name) != m_PosShapes.end());

	return m_PosShapes[name];
}

std::map<std::wstring, CPosShape*>::size_type CPosShape::GetPosShapeCount()
{
	return m_PosShapes.size();
}

std::map<std::wstring, CPosShape*> CPosShape::GetMap()
{
	return m_PosShapes;
}

void CPosShape::MakePosShapesFromResource(HINSTANCE hInstance)
{
	CPosShape::ClearPosShapes();

	HRSRC hResource = FindResource(hInstance, MAKEINTRESOURCE(IDR_SHAPELIST1), L"SHAPELIST");
	if (!hResource)
	{
		return;
	}

	HGLOBAL hLoadedResource = LoadResource(hInstance, hResource);
	if (!hLoadedResource)
	{
		return;
	}

	LPVOID pLockedResource = LockResource(hLoadedResource);
	if (!pLockedResource)
	{
		return;
	}

	DWORD dwResourceSize = SizeofResource(hInstance, hResource);
	if (dwResourceSize == 0)
	{
		return;
	}

	std::string casted_memory(static_cast<char*>(pLockedResource), dwResourceSize);
	std::istringstream stream(casted_memory);
	std::string   line;
	
	while(std::getline(stream, line))
	{
		while(!stream.eof() && line.compare(0, 5, "BEGIN") != 0)
			std::getline(stream, line);

		if(stream.eof())
			break;

		std::wstring wline;
		std::wstringstream linestream;
		std::wstring fieldname;
		std::wstring name;
		int fields;
		std::wstring formula;
		std::wstring formulabending;
		int count;

		// Name
		std::getline(stream, line);
		linestream.clear(); linestream.str(std::wstring());
		wline.assign(line.begin(), line.end());
		linestream << wline;
		std::getline(linestream, fieldname, L'\t');
		linestream >> name;
		// Fields
		std::getline(stream, line);
		linestream.clear(); linestream.str(std::wstring());
		wline.assign(line.begin(), line.end());
		linestream << wline;
		std::getline(linestream, fieldname, L'\t');
		linestream >> fields;
		// Formula
		std::getline(stream, line);
		linestream.clear(); linestream.str(std::wstring());
		wline.assign(line.begin(), line.end());
		linestream << wline;
		std::getline(linestream, fieldname, L'\t');
		linestream >> formula;
		// FormulaBending
		std::getline(stream, line);
		linestream.clear(); linestream.str(std::wstring());
		wline.assign(line.begin(), line.end());
		linestream << wline;
		std::getline(linestream, fieldname, L'\t');
		linestream >> formulabending;
		// Count
		std::getline(stream, line);
		linestream.clear(); linestream.str(std::wstring());
		wline.assign(line.begin(), line.end());
		linestream << wline;
		std::getline(linestream, fieldname, L'\t');
		linestream >> count;

		// Create the shape
		CPosShape *shape = new CPosShape();
		shape->setName(name.c_str());
		shape->setFields(fields);
		shape->setFormula(formula.c_str());
		shape->setFormulaBending(formulabending.c_str());
		
		// Read shapes
		for(int i = 0; i < count; i++)
		{
			std::getline(stream, line);
			linestream.clear(); linestream.str(std::wstring());
			wline.assign(line.begin(), line.end());
			linestream << wline;
			std::getline(linestream, fieldname, L'\t');

			if(fieldname.compare(L"LINE") == 0)
			{
				double x1, y1, x2, y2;
				unsigned short color;
				std::wstring visible;
				linestream >> x1 >> y1 >> x2 >> y2 >> color >> visible;

				CShapeLine *line = new CShapeLine(color, x1, y1, x2, y2, (visible.compare(L"V") == 0 ? Adesk::kTrue : Adesk::kFalse));
				shape->AddShape(line);
			}
			else if(fieldname.compare(L"ARC") == 0)
			{
				double x, y, r, a1, a2;
				unsigned short color;
				std::wstring visible;
				linestream >> x >> y >> r >> a1 >> a2 >> color >> visible;

				CShapeArc *arc = new CShapeArc(color, x, y, r, a1, a2, (visible.compare(L"V") == 0 ? Adesk::kTrue : Adesk::kFalse));
				shape->AddShape(arc);
			}
			else if(fieldname.compare(L"TEXT") == 0)
			{
				double x, y, h;
				std::wstring str;
				int ha, va;
				unsigned short color;
				std::wstring visible;
				linestream >> x >> y >> h >> str >> ha >> va >> color >> visible;

				CShapeText *text = new CShapeText(color, x, y, h, str.c_str(), static_cast<AcDb::TextHorzMode>(ha), static_cast<AcDb::TextVertMode>(va), (visible.compare(L"V") == 0 ? Adesk::kTrue : Adesk::kFalse));
				shape->AddShape(text);
			}
		}

		// Add the shape to the dictionary
		m_PosShapes[name] = shape;
	}
}

void CPosShape::ClearPosShapes()
{
	for(std::map<std::wstring, CPosShape*>::iterator it = m_PosShapes.begin(); it != m_PosShapes.end(); it++)
	{
		delete it->second;
	}
	m_PosShapes.clear();
}
