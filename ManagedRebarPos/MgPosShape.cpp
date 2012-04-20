//-----------------------------------------------------------------------------
//----- PosShape.cpp : Implementation of PosShape
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#if defined(_DEBUG) && !defined(AC_FULL_DEBUG)
#error _DEBUG should not be defined except in internal Adesk debug builds
#endif

#include <gcroot.h>
#include "mgdinterop.h"

using namespace OZOZ::RebarPosWrapper;

//*************************************************************************
// Constructors and destructors 
//*************************************************************************
PosShape::PosShape() 
:Autodesk::AutoCAD::DatabaseServices::DBObject(IntPtr(new CPosShape()), true)
{
}

PosShape::PosShape(System::IntPtr unmanagedPointer, bool autoDelete)
: Autodesk::AutoCAD::DatabaseServices::DBObject(unmanagedPointer,autoDelete)
{
}

//*************************************************************************
// Properties
//*************************************************************************
void PosShape::Fields::set(int value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFields(value));
}
int PosShape::Fields::get()
{
    return GetImpObj()->Fields();
}

void PosShape::Formula::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFormula(StringToWchar(value)));
}
String^ PosShape::Formula::get()
{
    return WcharToString(GetImpObj()->Formula());
}

void PosShape::FormulaBending::set(String^ value)
{
    Autodesk::AutoCAD::Runtime::Interop::Check(GetImpObj()->setFormulaBending(StringToWchar(value)));
}
String^ PosShape::FormulaBending::get()
{
    return WcharToString(GetImpObj()->FormulaBending());
}

PosShape::ShapeCollection^ PosShape::Items::get()
{
	return m_Shapes;
}

//*************************************************************************
// Shape Collection
//*************************************************************************
PosShape::ShapeCollection::ShapeCollection(PosShape^ parent)
{
	m_Parent = parent;
}

void PosShape::ShapeCollection::AddLine(double x1, double y1, double x2, double y2, Autodesk::AutoCAD::Colors::Color^ color)
{
	CShapeLine* line = new CShapeLine();
	line->color = color->ColorIndex;
	line->x1 = x1;
	line->x2 = x2;
	line->y1 = y1;
	line->y2 = y2;
	m_Parent->GetImpObj()->AddShape(line);
}

void PosShape::ShapeCollection::AddArc(double x, double y, double r, double startAngle, double endAngle, Autodesk::AutoCAD::Colors::Color^ color)
{
	CShapeArc* arc = new CShapeArc();
	arc->color = color->ColorIndex;
	arc->x = x;
	arc->y = y;
	arc->r = r;
	arc->startAngle = startAngle;
	arc->endAngle = endAngle;
	m_Parent->GetImpObj()->AddShape(arc);
}

void PosShape::ShapeCollection::AddText(double x, double y, double height, String^ str, Autodesk::AutoCAD::Colors::Color^ color)
{
	CShapeText* text = new CShapeText();
	text->color = color->ColorIndex;
	text->x = x;
	text->y = y;
	text->height = height;
	acutUpdString(StringToWchar(str), text->text);
	m_Parent->GetImpObj()->AddShape(text);
}

int PosShape::ShapeCollection::Count::get()
{
	return (int)m_Parent->GetImpObj()->GetShapeCount();
}

void PosShape::ShapeCollection::Remove(int index)
{
	m_Parent->GetImpObj()->RemoveShape((int)index);
}

void PosShape::ShapeCollection::Clear()
{
	m_Parent->GetImpObj()->ClearShapes();
}

PosShape::Shape^ PosShape::ShapeCollection::default::get(int index)
{
	const CShape* shape = m_Parent->GetImpObj()->GetShape(index);

	switch(shape->type)
	{
	case CShape::Line:
		{
			const CShapeLine* line = static_cast<const CShapeLine*>(shape);
			ShapeLine^ rshape = gcnew ShapeLine();
			rshape->Color = Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, shape->color);
			rshape->X1 = line->x1;
			rshape->Y1 = line->y1;
			rshape->X2 = line->x2;
			rshape->Y2 = line->y2;
			return rshape;
		}
		break;
	case CShape::Arc:
		{
			const CShapeArc* arc = static_cast<const CShapeArc*>(shape);
			ShapeArc^ rshape = gcnew ShapeArc();
			rshape->Color = Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, shape->color);
			rshape->X = arc->x;
			rshape->Y = arc->y;
			rshape->R = arc->r;
			rshape->StartAngle = arc->startAngle;
			rshape->EndAngle = arc->endAngle;
			return rshape;
		}
		break;
	case CShape::Text:
		{
			const CShapeText* text = static_cast<const CShapeText*>(shape);
			ShapeText^ rshape = gcnew ShapeText();
			rshape->Color = Autodesk::AutoCAD::Colors::Color::FromColorIndex(Autodesk::AutoCAD::Colors::ColorMethod::ByAci, shape->color);
			rshape->X = text->x;
			rshape->Y = text->y;
			rshape->Height = text->height;
			rshape->Text = WcharToString(text->text);
			return rshape;
		}
		break;
	default:
		throw gcnew Exception("Unknown shape type");
	}
}

void PosShape::ShapeCollection::default::set(int index, PosShape::Shape^ value)
{
	if(value->GetType() == ShapeLine::typeid)
	{
		ShapeLine^ rshape = static_cast<ShapeLine^>(value);
		CShapeLine* line = new CShapeLine();
		line->color = rshape->Color->ColorIndex;
		line->x1 = rshape->X1;
		line->y1 = rshape->Y1;
		line->x2 = rshape->X2;
		line->y2 = rshape->Y1;
		m_Parent->GetImpObj()->SetShape(index, line);
	}
	else if(value->GetType() == ShapeArc::typeid)
	{
		ShapeArc^ rshape = static_cast<ShapeArc^>(value);
		CShapeArc* arc = new CShapeArc();
		arc->color = rshape->Color->ColorIndex;
		arc->x = rshape->X;
		arc->y = rshape->Y;
		arc->r = rshape->R;
		arc->startAngle = rshape->StartAngle;
		arc->endAngle = rshape->EndAngle;
		m_Parent->GetImpObj()->SetShape(index, arc);
	}
	else if(value->GetType() == ShapeText::typeid)
	{
		ShapeText^ rshape = static_cast<ShapeText^>(value);
		CShapeText* text = new CShapeText();
		text->color = rshape->Color->ColorIndex;
		text->x = rshape->X;
		text->y = rshape->Y;
		text->height = rshape->Height;
		acutUpdString(StringToWchar(rshape->Text), text->text);
		m_Parent->GetImpObj()->SetShape(index, text);
	}
	else
	{
		throw gcnew Exception("Unknown shape type");
	}
}

//*************************************************************************
// Static Properties
//*************************************************************************
String^ PosShape::TableName::get()
{
	return WcharToString(CPosShape::GetTableName());
}

Autodesk::AutoCAD::DatabaseServices::ObjectId PosShape::Dictionary::get()
{
	Autodesk::AutoCAD::DatabaseServices::ObjectId id = Autodesk::AutoCAD::DatabaseServices::ObjectId::Null;

	Autodesk::AutoCAD::DatabaseServices::Database^ db = Autodesk::AutoCAD::DatabaseServices::HostApplicationServices::WorkingDatabase;
	Autodesk::AutoCAD::DatabaseServices::Transaction^ tr = db->TransactionManager->StartTransaction();

	DBDictionary^ nod = (DBDictionary^)tr->GetObject(db->NamedObjectsDictionaryId, Autodesk::AutoCAD::DatabaseServices::OpenMode::ForRead);
	if(!nod->Contains(TableName))
	{
		DBDictionary^ pDict = gcnew DBDictionary();
		nod->UpgradeOpen();
		nod->SetAt(TableName, pDict);
		tr->AddNewlyCreatedDBObject(pDict, true);
		nod->DowngradeOpen();
		id = pDict->Id;
	}
	else
	{
		id = nod->GetAt(TableName);
	}
	tr->Commit();

	return id;
}

//*************************************************************************
// Static Methods
//*************************************************************************
Autodesk::AutoCAD::DatabaseServices::ObjectId PosShape::CreateDefault()
{
	return ToObjectId(CPosShape::CreateDefault());
}