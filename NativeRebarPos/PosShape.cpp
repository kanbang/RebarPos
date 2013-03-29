//-----------------------------------------------------------------------------
//----- PosShape.cpp : Implementation of CPosShape
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include "PosShape.h"
#include "Utility.h"

#include <fstream>
#include <algorithm>

std::map<std::wstring, CPosShape*> CPosShape::m_BuiltInPosShapes;
std::map<std::wstring, CPosShape*> CPosShape::m_InternalPosShapes;
std::map<std::wstring, CPosShape*> CPosShape::m_CustomPosShapes;

//*************************************************************************
// Code for the Class Body. 
//*************************************************************************

ACRX_CONS_DEFINE_MEMBERS(CPosShape, AcGiDrawable, 0);

//*************************************************************************
// Constructors and destructors 
//*************************************************************************
CPosShape::CPosShape () : m_Name(NULL), m_Fields(1), m_Formula(NULL), m_FormulaBending(NULL), 
	m_Priority(0), m_IsBuiltIn(Adesk::kFalse), m_IsUnknown(Adesk::kFalse), m_IsInternal(Adesk::kFalse),
	m_A(NULL), m_B(NULL), m_C(NULL), m_D(NULL), m_E(NULL), m_F(NULL), m_Style(),
	m_GsNode(NULL)
{ }

CPosShape::~CPosShape () 
{ 
	acutDelString(m_Name);
	acutDelString(m_Formula);
	acutDelString(m_FormulaBending);

	acutDelString(m_A);
	acutDelString(m_B);
	acutDelString(m_C);
	acutDelString(m_D);
	acutDelString(m_E);
	acutDelString(m_F);

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

const Adesk::Boolean CPosShape::IsBuiltIn(void) const
{
	return m_IsBuiltIn;
}

Acad::ErrorStatus CPosShape::setIsBuiltIn(const Adesk::Boolean newVal)
{
	m_IsBuiltIn = newVal;
	return Acad::eOk;
}

const Adesk::Boolean CPosShape::IsUnknown(void) const
{
	return m_IsUnknown;
}

Acad::ErrorStatus CPosShape::setIsUnknown(const Adesk::Boolean newVal)
{
	m_IsUnknown = newVal;
	return Acad::eOk;
}

const Adesk::Boolean CPosShape::IsInternal(void) const
{
	return m_IsInternal;
}

Acad::ErrorStatus CPosShape::setIsInternal(const Adesk::Boolean newVal)
{
	m_IsInternal = newVal;
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
				int i = 0;
				double a = arc->startAngle;
				for(i = 0; i < 10; i++)
				{
					double x = arc->x + cos(a) * arc->r;
					double y = arc->y + sin(a) * arc->r;
					ext.addPoint(AcGePoint3d(x, y, 0));
					a += da;
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

Acad::ErrorStatus CPosShape::setShapeTexts(const ACHAR* a, const ACHAR* b, const ACHAR* c, const ACHAR* d, const ACHAR* e, const ACHAR* f)
{
	acutUpdString(a, m_A);
	acutUpdString(b, m_B);
	acutUpdString(c, m_C);
	acutUpdString(d, m_D);
	acutUpdString(e, m_E);
	acutUpdString(f, m_F);

	return Acad::eOk;
}
Acad::ErrorStatus CPosShape::clearShapeTexts(void)
{
	acutDelString(m_A);
	acutDelString(m_B);
	acutDelString(m_C);
	acutDelString(m_D);
	acutDelString(m_E);
	acutDelString(m_F);

	m_A = NULL;
	m_B = NULL;
	m_C = NULL;
	m_D = NULL;
	m_E = NULL;
	m_F = NULL;

	return Acad::eOk;
}

//*************************************************************************
// Common static dictionary methods
//*************************************************************************
void CPosShape::AddPosShape(CPosShape* shape)
{
	if(shape->IsInternal())
		m_InternalPosShapes[shape->Name()] = shape;
	else if(shape->IsBuiltIn())
		m_BuiltInPosShapes[shape->Name()] = shape;
	else
		m_CustomPosShapes[shape->Name()] = shape;
}

CPosShape* CPosShape::GetPosShape(const std::wstring name)
{
	std::map<std::wstring, CPosShape*>::iterator it;
	if((it = m_InternalPosShapes.find(name)) != m_InternalPosShapes.end())
		return (*it).second;
	else if((it = m_BuiltInPosShapes.find(name)) != m_BuiltInPosShapes.end())
		return (*it).second;
	else if((it = m_CustomPosShapes.find(name)) != m_CustomPosShapes.end())
		return (*it).second;

	CPosShape* shape = CPosShape::GetUnknownPosShape();
	shape->setName(name.c_str());
	return shape;
}

CPosShape* CPosShape::GetUnknownPosShape()
{
	CPosShape* shape = new CPosShape();

	shape->setName(_T("HATA"));
	shape->setFields(1);
	shape->setFormula(_T("A"));
	shape->setFormulaBending(_T("A"));
	shape->setIsBuiltIn(Adesk::kTrue);
	shape->setIsUnknown(Adesk::kTrue);

	return shape;
}

int CPosShape::GetPosShapeCount(const bool builtin, const bool isinternal, const bool custom)
{
	int count = 0;

	if(isinternal) count += (int)m_InternalPosShapes.size();
	if(builtin) count += (int)m_BuiltInPosShapes.size();
	if(custom) count += (int)m_CustomPosShapes.size();

	return count;
}

void CPosShape::ClearPosShapes(const bool builtin, const bool isinternal, const bool custom)
{
	if(isinternal)
	{
		for(std::map<std::wstring, CPosShape*>::iterator it = m_InternalPosShapes.begin(); it != m_InternalPosShapes.end(); it++)
		{
			std::map<std::wstring, CPosShape*>::iterator current = it;
			it++;

			CPosShape* shape = (*current).second;
			delete shape;
			m_InternalPosShapes.erase(current);
		}
	}
	if(builtin)
	{
		for(std::map<std::wstring, CPosShape*>::iterator it = m_BuiltInPosShapes.begin(); it != m_BuiltInPosShapes.end(); it++)
		{
			std::map<std::wstring, CPosShape*>::iterator current = it;
			it++;

			CPosShape* shape = (*current).second;
			delete shape;
			m_BuiltInPosShapes.erase(current);
		}
	}
	if(custom)
	{
		for(std::map<std::wstring, CPosShape*>::iterator it = m_CustomPosShapes.begin(); it != m_CustomPosShapes.end(); it++)
		{
			std::map<std::wstring, CPosShape*>::iterator current = it;
			it++;

			CPosShape* shape = (*current).second;
			delete shape;
			m_CustomPosShapes.erase(current);
		}
	}
}

std::vector<std::wstring> CPosShape::GetAllShapes(const bool builtin, const bool isinternal, const bool custom)
{
	std::vector<std::wstring> names;
	if(isinternal)
	{
		for(std::map<std::wstring, CPosShape*>::iterator it = m_InternalPosShapes.begin(); it != m_InternalPosShapes.end(); it++)
		{
			names.push_back(std::wstring(it->first));
		}
	}
	if(builtin)
	{
		for(std::map<std::wstring, CPosShape*>::iterator it = m_BuiltInPosShapes.begin(); it != m_BuiltInPosShapes.end(); it++)
		{
			names.push_back(std::wstring(it->first));
		}
	}
	if(custom)
	{
		for(std::map<std::wstring, CPosShape*>::iterator it = m_CustomPosShapes.begin(); it != m_CustomPosShapes.end(); it++)
		{
			names.push_back(std::wstring(it->first));
		}
	}

	std::sort(names.begin(), names.end(), CPosShape::SortShapeNames);
	return names;
}

bool CPosShape::SortShapeNames(const std::wstring p1, const std::wstring p2)
{
	if (p1 == p2) return true;

	if (p1 == L"GENEL")
		return true;
	else if (p2 == L"GENEL")
		return false;
	else
	{
		if(Utility::IsNumeric(p1) && Utility::IsNumeric(p2))
		{
			int n1 = Utility::StrToInt(p1);
			int n2 = Utility::StrToInt(p2);
			return n1 < n2;
		}
		else
		{
			return p1.compare(p2) < 0;
		}
	}
}

void CPosShape::ReadPosShapesFromResource(HINSTANCE hInstance, const int resid, const bool isinternal)
{
	HRSRC hResource = FindResource(hInstance, MAKEINTRESOURCE(resid), L"SHAPELIST");
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
	std::wstring source;
	source.assign(casted_memory.begin(), casted_memory.end());

	ReadPosShapesFromString(source, true, isinternal);
}

void CPosShape::ReadPosShapesFromFile(const std::wstring filename)
{
	std::wifstream ifs(filename.c_str());
	std::wstring content( (std::istreambuf_iterator<wchar_t>(ifs) ),
                          (std::istreambuf_iterator<wchar_t>()    ) );
	ReadPosShapesFromString(content, false, false);
}

void CPosShape::ReadPosShapesFromString(const std::wstring source, const bool builtin, const bool isinternal)
{
	std::wistringstream stream(source);
	std::wstring   line;
	
	while(std::getline(stream, line))
	{
		while(!stream.eof() && line.compare(0, 5, L"BEGIN") != 0)
			std::getline(stream, line);

		if(stream.eof())
			break;

		std::wstringstream linestream;
		std::wstring fieldname;
		std::wstring name;
		int fields;
		std::wstring formula;
		std::wstring formulabending;
		int priority;
		int count;

		// Name
		std::getline(stream, line);
		linestream.clear(); linestream.str(std::wstring());
		linestream << line;
		std::getline(linestream, fieldname, L'\t');
		linestream >> name;
		// Fields
		std::getline(stream, line);
		linestream.clear(); linestream.str(std::wstring());
		linestream << line;
		std::getline(linestream, fieldname, L'\t');
		linestream >> fields;
		// Formula
		std::getline(stream, line);
		linestream.clear(); linestream.str(std::wstring());
		linestream << line;
		std::getline(linestream, fieldname, L'\t');
		linestream >> formula;
		// FormulaBending
		std::getline(stream, line);
		linestream.clear(); linestream.str(std::wstring());
		linestream << line;
		std::getline(linestream, fieldname, L'\t');
		linestream >> formulabending;
		// Priority
		std::getline(stream, line);
		linestream.clear(); linestream.str(std::wstring());
		linestream << line;
		std::getline(linestream, fieldname, L'\t');
		linestream >> priority;
		// Count
		std::getline(stream, line);
		linestream.clear(); linestream.str(std::wstring());
		linestream << line;
		std::getline(linestream, fieldname, L'\t');
		linestream >> count;

		// Create the shape
		CPosShape *shape = new CPosShape();
		shape->setName(name.c_str());
		shape->setFields(fields);
		shape->setFormula(formula.c_str());
		shape->setFormulaBending(formulabending.c_str());
		shape->setIsBuiltIn(builtin ? Adesk::kTrue : Adesk::kFalse);
		shape->setIsInternal(isinternal ? Adesk::kTrue : Adesk::kFalse);
		
		// Read shapes
		for(int i = 0; i < count; i++)
		{
			std::getline(stream, line);
			linestream.clear(); linestream.str(std::wstring());
			linestream << line;
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
				double x, y, h, w;
				std::wstring font;
				std::wstring str;
				int ha, va;
				unsigned short color;
				std::wstring visible;
				linestream >> x >> y >> h >> w >> str >> font >> ha >> va >> color >> visible;

				CShapeText *text = new CShapeText(color, x, y, h, w, str.c_str(), font.c_str(), static_cast<AcDb::TextHorzMode>(ha), static_cast<AcDb::TextVertMode>(va), (visible.compare(L"V") == 0 ? Adesk::kTrue : Adesk::kFalse));
				shape->AddShape(text);
			}
		}

		// Add the shape to the dictionary
		if(isinternal)
			m_InternalPosShapes[name] = shape;
		else if(builtin)
			m_BuiltInPosShapes[name] = shape;
		else
			m_CustomPosShapes[name] = shape;
	}
}


void CPosShape::SavePosShapesToFile(const std::wstring filename)
{
	std::wofstream ofs(filename.c_str());

	for(std::map<std::wstring, CPosShape*>::iterator it = m_CustomPosShapes.begin(); it != m_CustomPosShapes.end(); it++)
	{
		CPosShape* posShape = (*it).second;

		ofs << L"BEGIN" << std::endl;

		ofs << L"Name"           << L'\t' << posShape->Name()             << std::endl;
		ofs << L"Fields"         << L'\t' << posShape->Fields()           << std::endl;
		ofs << L"Formula"        << L'\t' << posShape->Formula()          << std::endl;
		ofs << L"FormulaBending" << L'\t' << posShape->FormulaBending()   << std::endl;
		ofs << L"Priority"       << L'\t' << posShape->Priority()         << std::endl;
		ofs << L"Count"          << L'\t' << posShape->GetShapeCount()    << std::endl;

		// Write shapes
		for(int i = 0; i < posShape->GetShapeCount(); i++)
		{
			const CShape* shape = posShape->GetShape(i);
			if(shape->type == CShape::Line)
			{
				const CShapeLine* line = dynamic_cast<const CShapeLine*>(shape);
				ofs << L"LINE" << L'\t' << 
					line->x1 << L'\t' << line->y1 << L'\t' << line->x2 << L'\t' << line->y2 << L'\t' << 
					line->color << L'\t' << (line->visible == Adesk::kTrue ? L'V' : L'I') << std::endl;
			}
			else if(shape->type == CShape::Arc)
			{
				const CShapeArc* arc = dynamic_cast<const CShapeArc*>(shape);
				ofs << L"ARC" << L'\t' << 
					arc->x << L'\t' << arc->y << L'\t' << arc->r << L'\t' << 
					arc->startAngle << L'\t' << arc->endAngle << L'\t' <<
					arc->color << L'\t' << (arc->visible == Adesk::kTrue ? L'V' : L'I') << std::endl;
			}
			else if(shape->type == CShape::Text)
			{
				const CShapeText* text = dynamic_cast<const CShapeText*>(shape);
				ofs << L"TEXT" << L'\t' << 
					text->x << L'\t' << text->y << L'\t' << text->height << L'\t' << text->width << L'\t' << 
					text->text << L'\t' << text->font << L'\t' << 
					text->horizontalAlignment << L'\t' << text->verticalAlignment << L'\t' <<
					text->color << L'\t' << (text->visible == Adesk::kTrue ? L'V' : L'I') << std::endl;
			}
		}

		ofs << L"END" << std::endl;
		ofs << std::endl;
	}
}

//*************************************************************************
// Drawable implementation
//*************************************************************************
Adesk::Boolean CPosShape::isPersistent(void) const
{
	return Adesk::kFalse;
}

AcDbObjectId CPosShape::id(void) const
{
	return AcDbObjectId::kNull;
}

void CPosShape::setGsNode(AcGsNode* gsnode)
{
	m_GsNode = gsnode;
}

AcGsNode* CPosShape::gsNode(void) const
{
	return m_GsNode;
}

bool CPosShape::bounds(AcDbExtents& bounds) const
{
	if(m_List.empty())
	{
		return false;
	}
	else
	{
		bounds = GetShapeExtents();
		return true;
	}
}

Adesk::UInt32 CPosShape::subSetAttributes(AcGiDrawableTraits* traits)
{
	return AcGiDrawable::kDrawableNone;
}

Adesk::Boolean CPosShape::subWorldDraw(AcGiWorldDraw* worldDraw)
{
    if(worldDraw->regenAbort())
	{
        return Adesk::kTrue;
    }

	for(ShapeListConstIt it = m_List.begin(); it != m_List.end(); it++)
	{
		CShape* shape = *it;
		if(shape->visible == Adesk::kFalse) continue;

		switch(shape->type)
		{
		case CShape::Line:
			{
				CShapeLine* line = dynamic_cast<CShapeLine*>(shape);
				Utility::DrawLine(worldDraw, AcGePoint3d(line->x1, line->y1, 0), AcGePoint3d(line->x2, line->y2, 0), line->color);
			}
			break;
		case CShape::Arc:
			{
				CShapeArc* arc = dynamic_cast<CShapeArc*>(shape);
				Utility::DrawArc(worldDraw, AcGePoint3d(arc->x, arc->y, 0), arc->r, arc->startAngle, arc->endAngle, arc->color);
			}
			break;
		case CShape::Text:
			{
				CShapeText* text = dynamic_cast<CShapeText*>(shape);

				std::wstring txt(text->text);
				if(m_A != NULL) Utility::ReplaceString(txt, L"A", m_A);
				if(m_B != NULL) Utility::ReplaceString(txt, L"B", m_B);
				if(m_C != NULL) Utility::ReplaceString(txt, L"C", m_C);
				if(m_D != NULL) Utility::ReplaceString(txt, L"D", m_D);
				if(m_E != NULL) Utility::ReplaceString(txt, L"E", m_E);
				if(m_F != NULL) Utility::ReplaceString(txt, L"F", m_F);

				if(text->height != m_Style.textSize() || text->width != m_Style.xScale() || text->font.compare(m_Style.fileName()) != 0)
				{
					m_Style.setFileName(text->font.c_str());
					m_Style.setTextSize(text->height);
					m_Style.setXScale(text->width);
					m_Style.loadStyleRec();
				}
				Utility::DrawText(worldDraw, AcGePoint3d(text->x, text->y, 0), txt.c_str(), m_Style, text->horizontalAlignment, text->verticalAlignment, text->color);
			}
			break;
		}
	}

	// Do not call viewportDraw()
    return Adesk::kTrue; 
}

void CPosShape::subViewportDraw(AcGiViewportDraw* vd)
{
	;
}

Adesk::UInt32 CPosShape::subViewportDrawLogicalFlags(AcGiViewportDraw* vd)
{
	return 0;
}