//-----------------------------------------------------------------------------
//----- TableCell.cpp : Implementation of CTableCell
//-----------------------------------------------------------------------------
#include "StdAfx.h"

#include "TableCell.h"
#include "PosShape.h"
#include "Utility.h"

//*************************************************************************
// Constructors and destructors 
//*************************************************************************
CTableCell::CTableCell(void) : m_BasePoint(0, 0, 0), m_Direction(1, 0, 0), m_Up(0, 1, 0), m_Normal(0, 0, 1),
	m_Text(NULL), m_TextColor(0), m_ShapeTextColor(0), m_ShapeLineColor(0),
	m_TopBorderColor(0), m_LeftBorderColor(0), m_BottomBorderColor(0), m_RightBorderColor(0),
	m_TopBorder(Adesk::kFalse), m_LeftBorder(Adesk::kFalse), m_BottomBorder(Adesk::kFalse), m_RightBorder(Adesk::kFalse),
	m_TopBorderDouble(Adesk::kFalse), m_LeftBorderDouble(Adesk::kFalse), m_BottomBorderDouble(Adesk::kFalse), m_RightBorderDouble(Adesk::kFalse),
	m_MergeRight(0), m_MergeDown(0),
	m_TextStyleId(AcDbObjectId::kNull), m_Shape(NULL),
	m_TextHeight(1.0), m_A(NULL), m_B(NULL), m_C(NULL), m_D(NULL), m_E(NULL), m_F(NULL),
	m_HorizontalAlignment(CTableCell::eNEAR), m_VerticalAlignment(CTableCell::eNEAR),
	m_Width(0), m_Height(0), m_Margin(0)
{
}

CTableCell::~CTableCell(void)
{
	acutDelString(m_Text);
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
const AcGeVector3d& CTableCell::DirectionVector(void) const { return m_Direction; }
const AcGeVector3d& CTableCell::UpVector(void) const { return m_Up; }
const AcGeVector3d& CTableCell::NormalVector(void) const { return m_Normal; }

const AcGePoint3d& CTableCell::BasePoint(void) const { return m_BasePoint; }
Acad::ErrorStatus CTableCell::setBasePoint(const AcGePoint3d& newVal) { m_BasePoint = newVal; return Acad::eOk; }

const ACHAR* CTableCell::Text() const         { return m_Text; }
Acad::ErrorStatus CTableCell::setText(const ACHAR* newVal) { acutUpdString(newVal, m_Text); return Acad::eOk; }

const Adesk::UInt16 CTableCell::TextColor() const         { return m_TextColor; }
Acad::ErrorStatus CTableCell::setTextColor(const Adesk::UInt16 newVal) { m_TextColor = newVal; return Acad::eOk; }

const Adesk::UInt16 CTableCell::ShapeTextColor() const         { return m_ShapeTextColor; }
Acad::ErrorStatus CTableCell::setShapeTextColor(const Adesk::UInt16 newVal) { m_ShapeTextColor = newVal; return Acad::eOk; }

const Adesk::UInt16 CTableCell::ShapeLineColor() const         { return m_ShapeLineColor; }
Acad::ErrorStatus CTableCell::setShapeLineColor(const Adesk::UInt16 newVal) { m_ShapeLineColor = newVal; return Acad::eOk; }

const Adesk::UInt16 CTableCell::TopBorderColor() const         { return m_TopBorderColor; }
Acad::ErrorStatus CTableCell::setTopBorderColor(const Adesk::UInt16 newVal) { m_TopBorderColor = newVal; return Acad::eOk; }

const Adesk::UInt16 CTableCell::LeftBorderColor() const         { return m_LeftBorderColor; }
Acad::ErrorStatus CTableCell::setLeftBorderColor(const Adesk::UInt16 newVal) { m_LeftBorderColor = newVal; return Acad::eOk; }

const Adesk::UInt16 CTableCell::BottomBorderColor() const         { return m_BottomBorderColor; }
Acad::ErrorStatus CTableCell::setBottomBorderColor(const Adesk::UInt16 newVal) { m_BottomBorderColor = newVal; return Acad::eOk; }

const Adesk::UInt16 CTableCell::RightBorderColor() const         { return m_RightBorderColor; }
Acad::ErrorStatus CTableCell::setRightBorderColor(const Adesk::UInt16 newVal) { m_RightBorderColor = newVal; return Acad::eOk; }

const Adesk::Boolean CTableCell::TopBorder() const         { return m_TopBorder; }
Acad::ErrorStatus CTableCell::setTopBorder(const Adesk::Boolean newVal) { m_TopBorder = newVal; return Acad::eOk; }

const Adesk::Boolean CTableCell::LeftBorder() const         { return m_LeftBorder; }
Acad::ErrorStatus CTableCell::setLeftBorder(const Adesk::Boolean newVal) { m_LeftBorder = newVal; return Acad::eOk; }

const Adesk::Boolean CTableCell::BottomBorder() const         { return m_BottomBorder; }
Acad::ErrorStatus CTableCell::setBottomBorder(const Adesk::Boolean newVal) { m_BottomBorder = newVal; return Acad::eOk; }

const Adesk::Boolean CTableCell::RightBorder() const         { return m_RightBorder; }
Acad::ErrorStatus CTableCell::setRightBorder(const Adesk::Boolean newVal) { m_RightBorder = newVal; return Acad::eOk; }

const Adesk::Boolean CTableCell::TopBorderDouble() const         { return m_TopBorderDouble; }
Acad::ErrorStatus CTableCell::setTopBorderDouble(const Adesk::Boolean newVal) { m_TopBorderDouble = newVal; return Acad::eOk; }

const Adesk::Boolean CTableCell::LeftBorderDouble() const         { return m_LeftBorderDouble; }
Acad::ErrorStatus CTableCell::setLeftBorderDouble(const Adesk::Boolean newVal) { m_LeftBorderDouble = newVal; return Acad::eOk; }

const Adesk::Boolean CTableCell::BottomBorderDouble() const         { return m_BottomBorderDouble; }
Acad::ErrorStatus CTableCell::setBottomBorderDouble(const Adesk::Boolean newVal) { m_BottomBorderDouble = newVal; return Acad::eOk; }

const Adesk::Boolean CTableCell::RightBorderDouble() const         { return m_RightBorderDouble; }
Acad::ErrorStatus CTableCell::setRightBorderDouble(const Adesk::Boolean newVal) { m_RightBorderDouble = newVal; return Acad::eOk; }

const Adesk::Int32 CTableCell::MergeRight() const         { return m_MergeRight; }
Acad::ErrorStatus CTableCell::setMergeRight(const Adesk::Int32 newVal) { m_MergeRight = newVal; return Acad::eOk; }

const Adesk::Int32 CTableCell::MergeDown() const         { return m_MergeDown; }
Acad::ErrorStatus CTableCell::setMergeDown(const Adesk::Int32 newVal) { m_MergeDown = newVal; return Acad::eOk; }

const AcDbObjectId& CTableCell::TextStyleId() const          { return m_TextStyleId; }
Acad::ErrorStatus CTableCell::setTextStyleId(const AcDbObjectId& newVal)  { m_TextStyleId = newVal; return Acad::eOk; }

const ACHAR* CTableCell::Shape() const         { return m_Shape; }
Acad::ErrorStatus CTableCell::setShape(const ACHAR* newVal) { acutUpdString(newVal, m_Shape); return Acad::eOk; }

Acad::ErrorStatus CTableCell::setShapeText(const ACHAR* a, const ACHAR* b, const ACHAR* c, const ACHAR* d, const ACHAR* e, const ACHAR* f)
{
	acutUpdString(a, m_A);
	acutUpdString(b, m_B);
	acutUpdString(c, m_C);
	acutUpdString(d, m_D);
	acutUpdString(e, m_E);
	acutUpdString(f, m_F);

	return Acad::eOk;
}

const double CTableCell::TextHeight() const         { return m_TextHeight; }
Acad::ErrorStatus CTableCell::setTextHeight(const double newVal) { m_TextHeight = newVal; return Acad::eOk; }

const CTableCell::Alignment CTableCell::HorizontalAlignment() const         { return m_HorizontalAlignment; }
Acad::ErrorStatus CTableCell::setHorizontalAlignment(const CTableCell::Alignment newVal) { m_HorizontalAlignment = newVal; return Acad::eOk; }

const CTableCell::Alignment CTableCell::VerticalAlignment() const         { return m_VerticalAlignment; }
Acad::ErrorStatus CTableCell::setVerticalAlignment(const CTableCell::Alignment newVal) { m_VerticalAlignment = newVal; return Acad::eOk; }

const double CTableCell::Width() const         { return m_Width; }
Acad::ErrorStatus CTableCell::setWidth(const double newVal) { m_Width = newVal; return Acad::eOk; }

const double CTableCell::Height() const         { return m_Height; }
Acad::ErrorStatus CTableCell::setHeight(const double newVal) { m_Height = newVal; return Acad::eOk; }

const double CTableCell::Margin() const         { return m_Height; }
Acad::ErrorStatus CTableCell::setMargin(const double newVal) { m_Margin = newVal; return Acad::eOk; }

//*************************************************************************
// Methods
//*************************************************************************
const bool CTableCell::HasText() const 
{
	return m_Text != NULL && m_Text[0] != _T('\0'); 
}
const bool CTableCell::HasShape() const 
{
	return m_Shape != NULL && m_Shape[0] != _T('\0'); 
}

const AcGePoint2d CTableCell::MeasureContents() const
{
	AcGePoint2d pt(0, 0);

	if(HasText())
	{
		AcGiTextStyle textStyle;
		Utility::MakeGiTextStyle(textStyle, m_TextStyleId);
		textStyle.setTextSize(m_TextHeight);
		textStyle.loadStyleRec();

		pt = textStyle.extents(m_Text, Adesk::kTrue, -1, Adesk::kFalse);
	}
	else if(HasShape())
	{
		CPosShape* pShape = CPosShape::GetPosShape(m_Shape);
		AcDbExtents ext;
		if(pShape->bounds(ext))
		{
			double w = (ext.maxPoint().x - ext.minPoint().x) * m_TextHeight / 25.0;
			double h = (ext.maxPoint().y - ext.minPoint().y) * m_TextHeight / 25.0;
			pt.set(w, h);
		}
	}

	pt += AcGeVector2d(2.0 * m_Margin, 2.0 * m_Margin);

	return pt;
}

//*************************************************************************
// Helper Methods
//*************************************************************************
const std::vector<AcDbMText*> CTableCell::GetTexts() const
{
	std::vector<AcDbMText*> texts;

	// Transform to match orientation
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, m_Normal);

	if(HasText())
	{
		AcDbMText* mtext = new AcDbMText();

		// Text style
		mtext->setTextStyle(m_TextStyleId);
		mtext->setTextHeight(m_TextHeight);
		mtext->setColorIndex(m_TextColor);

		// Text attachment
		if(m_HorizontalAlignment == CTableCell::eNEAR)
		{
			if(m_VerticalAlignment == CTableCell::eNEAR)
				mtext->setAttachment(AcDbMText::kTopLeft);
			else if(m_VerticalAlignment == CTableCell::eCENTER)
				mtext->setAttachment(AcDbMText::kMiddleLeft);
			else if(m_VerticalAlignment == CTableCell::eFAR)
				mtext->setAttachment(AcDbMText::kBottomLeft);
		}
		else if(m_HorizontalAlignment == CTableCell::eCENTER)
		{
			if(m_VerticalAlignment == CTableCell::eNEAR)
				mtext->setAttachment(AcDbMText::kTopCenter);
			else if(m_VerticalAlignment == CTableCell::eCENTER)
				mtext->setAttachment(AcDbMText::kMiddleCenter);
			else if(m_VerticalAlignment == CTableCell::eFAR)
				mtext->setAttachment(AcDbMText::kBottomCenter);
		}
		else if(m_HorizontalAlignment == CTableCell::eFAR)
		{
			if(m_VerticalAlignment == CTableCell::eNEAR)
				mtext->setAttachment(AcDbMText::kTopRight);
			else if(m_VerticalAlignment == CTableCell::eCENTER)
				mtext->setAttachment(AcDbMText::kMiddleRight);
			else if(m_VerticalAlignment == CTableCell::eFAR)
				mtext->setAttachment(AcDbMText::kBottomRight);
		}

		// Location
		double cx = 0;
		double cy = 0;
		if(m_HorizontalAlignment == CTableCell::eNEAR)
		{
			cx = m_Margin;
		}
		else if(m_HorizontalAlignment == CTableCell::eFAR)
		{
			cx = m_Width - m_Margin;
		}
		else if(m_HorizontalAlignment == CTableCell::eCENTER)
		{
			cx = m_Width / 2.0;
		}
		if(m_VerticalAlignment == CTableCell::eNEAR)
		{
			cy = m_Margin;
		}
		else if(m_VerticalAlignment == CTableCell::eFAR)
		{
			cy = m_Height - m_Margin;
		}
		else if(m_VerticalAlignment == CTableCell::eCENTER)
		{
			cy = m_Height / 2.0;
		}
		mtext->setLocation(AcGePoint3d(cx, -cy, 0));

		// Contents
		mtext->setContents(m_Text);

		mtext->transformBy(trans);
		texts.push_back(mtext);
	}
	else if(HasShape())
	{
		CPosShape* pShape = CPosShape::GetPosShape(m_Shape);
		AcDbExtents ext;
		pShape->bounds(ext);
		double maxwidth = (ext.maxPoint().x - ext.minPoint().x);
		double maxheight = (ext.maxPoint().y - ext.minPoint().y);
		double scale = m_TextHeight / 25.0;
        double xoff = 0;
        double yoff = 0;
		if(m_HorizontalAlignment == CTableCell::eNEAR)
		{
			xoff = m_Margin;
		}
		else if(m_HorizontalAlignment == CTableCell::eFAR)
		{
			xoff = m_Width - m_Margin - maxwidth * scale;
		}
		else if(m_HorizontalAlignment == CTableCell::eCENTER)
		{
			xoff = (m_Width - maxwidth * scale) / 2.0;
		}
		if(m_VerticalAlignment == CTableCell::eNEAR)
		{
			yoff = m_Margin;
		}
		else if(m_VerticalAlignment == CTableCell::eFAR)
		{
			yoff = m_Height - m_Margin - maxheight * scale;
		}
		else if(m_VerticalAlignment == CTableCell::eCENTER)
		{
			yoff = (m_Height - maxheight * scale) / 2.0;
		}

		for(ShapeSize i = 0; i < pShape->GetShapeCount(); i++)
		{
			const CShape* shape = pShape->GetShape(i);
			if(shape->type == CShape::Text && shape->visible == Adesk::kTrue)
			{
				const CShapeText* text = dynamic_cast<const CShapeText*>(shape);

				AcDbMText* mtext = new AcDbMText();

				// Text style
				mtext->setTextStyle(m_TextStyleId);
				mtext->setTextHeight(text->height * scale);
				if(m_ShapeTextColor == 0)
					mtext->setColorIndex(text->color);
				else
					mtext->setColorIndex(m_ShapeTextColor);

				// Location
				double x = xoff + (text->x  - ext.minPoint().x) * scale;
				double y = yoff + (text->y  - ext.minPoint().y) * scale;
				mtext->setLocation(AcGePoint3d(x, -m_Height + y, 0));

				// Text attachment
				if(text->horizontalAlignment == AcDb::kTextLeft)
				{
					if(text->verticalAlignment == AcDb::kTextTop)
						mtext->setAttachment(AcDbMText::kTopLeft);
					else if(text->verticalAlignment == AcDb::kTextBase || text->verticalAlignment == AcDb::kTextBottom)
						mtext->setAttachment(AcDbMText::kBottomLeft);
					else
						mtext->setAttachment(AcDbMText::kMiddleLeft);
				}
				else if(text->horizontalAlignment == AcDb::kTextRight)
				{
					if(text->verticalAlignment == AcDb::kTextTop)
						mtext->setAttachment(AcDbMText::kTopRight);
					else if(text->verticalAlignment == AcDb::kTextBase || text->verticalAlignment == AcDb::kTextBottom)
						mtext->setAttachment(AcDbMText::kBottomRight);
					else
						mtext->setAttachment(AcDbMText::kMiddleRight);
				}
				else
				{
					if(text->verticalAlignment == AcDb::kTextTop)
						mtext->setAttachment(AcDbMText::kTopCenter);
					else if(text->verticalAlignment == AcDb::kTextBase || text->verticalAlignment == AcDb::kTextBottom)
						mtext->setAttachment(AcDbMText::kBottomCenter);
					else
						mtext->setAttachment(AcDbMText::kMiddleCenter);
				}

				// Contents
				std::wstring txt(text->text);
				if(m_A != NULL) Utility::ReplaceString(txt, L"A", m_A);
				if(m_B != NULL) Utility::ReplaceString(txt, L"B", m_B);
				if(m_C != NULL) Utility::ReplaceString(txt, L"C", m_C);
				if(m_D != NULL) Utility::ReplaceString(txt, L"D", m_D);
				if(m_E != NULL) Utility::ReplaceString(txt, L"E", m_E);
				if(m_F != NULL) Utility::ReplaceString(txt, L"F", m_F);
				mtext->setContents(txt.c_str());

				mtext->transformBy(trans);
				texts.push_back(mtext);
			}
		}
	}

	return texts;
}

const std::vector<AcDbLine*> CTableCell::GetLines() const
{
	std::vector<AcDbLine*> lines;

	// Transform to match orientation
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, m_Normal);

	double doublelineoffset = 0.125;

	if(m_TopBorder)
	{
		if(m_TopBorderDouble)
		{
			AcDbLine* line1 = new AcDbLine(AcGePoint3d(0, -doublelineoffset, 0), AcGePoint3d(m_Width, -doublelineoffset, 0));
			line1->setColorIndex(m_TopBorderColor);
			line1->transformBy(trans);
			lines.push_back(line1);
			AcDbLine* line2 = new AcDbLine(AcGePoint3d(0, doublelineoffset, 0), AcGePoint3d(m_Width, doublelineoffset, 0));
			line2->setColorIndex(m_TopBorderColor);
			line2->transformBy(trans);
			lines.push_back(line2);
		}
		else
		{
			AcDbLine* line = new AcDbLine(AcGePoint3d(0, 0, 0), AcGePoint3d(m_Width, 0, 0));
			line->setColorIndex(m_TopBorderColor);
			line->transformBy(trans);
			lines.push_back(line);
		}
	}
	if(m_BottomBorder)
	{
		if(m_BottomBorderDouble)
		{
			AcDbLine* line1 = new AcDbLine(AcGePoint3d(0, - m_Height - doublelineoffset, 0), AcGePoint3d(m_Width, -m_Height - doublelineoffset, 0));
			line1->setColorIndex(m_BottomBorderColor);
			line1->transformBy(trans);
			lines.push_back(line1);
			AcDbLine* line2 = new AcDbLine(AcGePoint3d(0, - m_Height + doublelineoffset, 0), AcGePoint3d(m_Width, -m_Height + doublelineoffset, 0));
			line2->setColorIndex(m_BottomBorderColor);
			line2->transformBy(trans);
			lines.push_back(line2);
		}
		else
		{
			AcDbLine* line = new AcDbLine(AcGePoint3d(0, -m_Height, 0), AcGePoint3d(m_Width, -m_Height, 0));
			line->setColorIndex(m_BottomBorderColor);
			line->transformBy(trans);
			lines.push_back(line);
		}
	}
	if(m_LeftBorder)
	{
		if(m_LeftBorderDouble)
		{
			AcDbLine* line1 = new AcDbLine(AcGePoint3d(- doublelineoffset, 0, 0), AcGePoint3d(- doublelineoffset, -m_Height, 0));
			line1->setColorIndex(m_LeftBorderColor);
			line1->transformBy(trans);
			lines.push_back(line1);
			AcDbLine* line2 = new AcDbLine(AcGePoint3d(doublelineoffset, 0, 0), AcGePoint3d(doublelineoffset, -m_Height, 0));
			line2->setColorIndex(m_LeftBorderColor);
			line2->transformBy(trans);
			lines.push_back(line2);
		}
		else
		{
			AcDbLine* line = new AcDbLine(AcGePoint3d(0, 0, 0), AcGePoint3d(0, - m_Height, 0));
			line->setColorIndex(m_LeftBorderColor);
			line->transformBy(trans);
			lines.push_back(line);
		}
	}
	if(m_RightBorder)
	{
		if(m_RightBorderDouble)
		{
			AcDbLine* line1 = new AcDbLine(AcGePoint3d(m_Width - doublelineoffset, 0, 0), AcGePoint3d(m_Width - doublelineoffset, -m_Height, 0));
			line1->setColorIndex(m_RightBorderColor);
			line1->transformBy(trans);
			lines.push_back(line1);
			AcDbLine* line2 = new AcDbLine(AcGePoint3d(m_Width + doublelineoffset, 0, 0), AcGePoint3d(m_Width + doublelineoffset, -m_Height, 0));
			line2->setColorIndex(m_RightBorderColor);
			line2->transformBy(trans);
			lines.push_back(line2);
		}
		else
		{
			AcDbLine* line = new AcDbLine(AcGePoint3d(m_Width, 0, 0), AcGePoint3d(m_Width, -m_Height, 0));
			line->setColorIndex(m_RightBorderColor);
			line->transformBy(trans);
			lines.push_back(line);
		}
	}

	if(HasShape())
	{
		CPosShape* pShape = CPosShape::GetPosShape(m_Shape);
		AcDbExtents ext;
		pShape->bounds(ext);
		double maxwidth = (ext.maxPoint().x - ext.minPoint().x);
		double maxheight = (ext.maxPoint().y - ext.minPoint().y);
		double scale = m_TextHeight / 25.0;
        double xoff = 0;
        double yoff = 0;
		if(m_HorizontalAlignment == CTableCell::eNEAR)
		{
			xoff = m_Margin;
		}
		else if(m_HorizontalAlignment == CTableCell::eFAR)
		{
			xoff = m_Width - m_Margin - maxwidth * scale;
		}
		else if(m_HorizontalAlignment == CTableCell::eCENTER)
		{
			xoff = (m_Width - maxwidth * scale) / 2.0;
		}
		if(m_VerticalAlignment == CTableCell::eNEAR)
		{
			yoff = m_Margin;
		}
		else if(m_VerticalAlignment == CTableCell::eFAR)
		{
			yoff = m_Height - m_Margin - maxheight * scale;
		}
		else if(m_VerticalAlignment == CTableCell::eCENTER)
		{
			yoff = (m_Height - maxheight * scale) / 2.0;
		}

		for(ShapeSize i = 0; i < pShape->GetShapeCount(); i++)
		{
			const CShape* shape = pShape->GetShape(i);
			if(shape->type == CShape::Line && shape->visible == Adesk::kTrue)
			{
				const CShapeLine* line = dynamic_cast<const CShapeLine*>(shape);

				AcDbLine* mline = new AcDbLine();

				if(m_ShapeLineColor == 0)
					mline->setColorIndex(line->color);
				else
					mline->setColorIndex(m_ShapeLineColor);

				// Location
				double x1 = xoff + (line->x1 - ext.minPoint().x) * scale;
				double y1 = yoff + (line->y1 - ext.minPoint().y) * scale;
				double x2 = xoff + (line->x2 - ext.minPoint().x) * scale;
				double y2 = yoff + (line->y2 - ext.minPoint().y) * scale;
				mline->setStartPoint(AcGePoint3d(x1, -m_Height + y1, 0));
				mline->setEndPoint(AcGePoint3d(x2, -m_Height + y2, 0));

				mline->transformBy(trans);
				lines.push_back(mline);
			}
		}
	}

	return lines;
}

const std::vector<AcDbArc*> CTableCell::GetArcs() const
{
	std::vector<AcDbArc*> arcs;

	// Transform to match orientation
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, m_Normal);

	if(HasShape())
	{
		CPosShape* pShape = CPosShape::GetPosShape(m_Shape);
		AcDbExtents ext;
		pShape->bounds(ext);
		double maxwidth = (ext.maxPoint().x - ext.minPoint().x);
		double maxheight = (ext.maxPoint().y - ext.minPoint().y);
		double scale = m_TextHeight / 25.0;
        double xoff = 0;
        double yoff = 0;
		if(m_HorizontalAlignment == CTableCell::eNEAR)
		{
			xoff = m_Margin;
		}
		else if(m_HorizontalAlignment == CTableCell::eFAR)
		{
			xoff = m_Width - m_Margin - maxwidth * scale;
		}
		else if(m_HorizontalAlignment == CTableCell::eCENTER)
		{
			xoff = (m_Width - maxwidth * scale) / 2.0;
		}
		if(m_VerticalAlignment == CTableCell::eNEAR)
		{
			yoff = m_Margin;
		}
		else if(m_VerticalAlignment == CTableCell::eFAR)
		{
			yoff = m_Height - m_Margin - maxheight * scale;
		}
		else if(m_VerticalAlignment == CTableCell::eCENTER)
		{
			yoff = (m_Height - maxheight * scale) / 2.0;
		}

		for(ShapeSize i = 0; i < pShape->GetShapeCount(); i++)
		{
			const CShape* shape = pShape->GetShape(i);
			if(shape->type == CShape::Arc && shape->visible == Adesk::kTrue)
			{
				const CShapeArc* arc = dynamic_cast<const CShapeArc*>(shape);

				AcDbArc* marc = new AcDbArc();

				if(m_ShapeLineColor == 0)
					marc->setColorIndex(arc->color);
				else
					marc->setColorIndex(m_ShapeLineColor);

				// Location
				double x = xoff + (arc->x - ext.minPoint().x) * scale;
				double y = yoff + (arc->y - ext.minPoint().y) * scale;
				double r = arc->r * scale;
				marc->setCenter(AcGePoint3d(x, -m_Height + y, 0));
				marc->setRadius(r);
				marc->setStartAngle(arc->startAngle);
				marc->setEndAngle(arc->endAngle);

				marc->transformBy(trans);
				arcs.push_back(marc);
			}
		}
	}

	return arcs;
}

//*************************************************************************
// Overridden methods from AcDbObject
//*************************************************************************

Acad::ErrorStatus CTableCell::dwgOutFields(AcDbDwgFiler* pFiler) const
{
	pFiler->writePoint3d(m_BasePoint);
	pFiler->writeVector3d(m_Direction);
	pFiler->writeVector3d(m_Up);

	if(HasText())
		pFiler->writeString(m_Text);
	else
		pFiler->writeString(_T(""));

	if(HasShape())
		pFiler->writeString(m_Shape);
	else
		pFiler->writeString(_T(""));

	if(m_A != NULL && m_A[0] != _T('\0'))
		pFiler->writeString(m_A);
	else
		pFiler->writeString(_T(""));
	if(m_B != NULL && m_B[0] != _T('\0'))
		pFiler->writeString(m_B);
	else
		pFiler->writeString(_T(""));
	if(m_C != NULL && m_C[0] != _T('\0'))
		pFiler->writeString(m_C);
	else
		pFiler->writeString(_T(""));
	if(m_D != NULL && m_D[0] != _T('\0'))
		pFiler->writeString(m_D);
	else
		pFiler->writeString(_T(""));
	if(m_E != NULL && m_E[0] != _T('\0'))
		pFiler->writeString(m_E);
	else
		pFiler->writeString(_T(""));
	if(m_F != NULL && m_F[0] != _T('\0'))
		pFiler->writeString(m_F);
	else
		pFiler->writeString(_T(""));

	pFiler->writeUInt16(m_TextColor);
	pFiler->writeUInt16(m_ShapeTextColor);
	pFiler->writeUInt16(m_ShapeLineColor);
	pFiler->writeUInt16(m_TopBorderColor);
	pFiler->writeUInt16(m_LeftBorderColor);
	pFiler->writeUInt16(m_BottomBorderColor);
	pFiler->writeUInt16(m_RightBorderColor);

	pFiler->writeBoolean(m_TopBorder);
	pFiler->writeBoolean(m_LeftBorder);
	pFiler->writeBoolean(m_BottomBorder);
	pFiler->writeBoolean(m_RightBorder);

	pFiler->writeBoolean(m_TopBorderDouble);
	pFiler->writeBoolean(m_LeftBorderDouble);
	pFiler->writeBoolean(m_BottomBorderDouble);
	pFiler->writeBoolean(m_RightBorderDouble);

	pFiler->writeInt32(m_MergeRight);
	pFiler->writeInt32(m_MergeDown);

	pFiler->writeHardPointerId(m_TextStyleId);

	pFiler->writeDouble(m_TextHeight);

	pFiler->writeInt32(m_HorizontalAlignment);
	pFiler->writeInt32(m_VerticalAlignment);

	pFiler->writeDouble(m_Width);
	pFiler->writeDouble(m_Height);

	pFiler->writeDouble(m_Margin);

	return Acad::eOk;
}

Acad::ErrorStatus CTableCell::dwgInFields(AcDbDwgFiler* pFiler)
{
	pFiler->readPoint3d(&m_BasePoint);
	pFiler->readVector3d(&m_Direction);
	pFiler->readVector3d(&m_Up);
	m_Normal = m_Direction.crossProduct(m_Up);

	acutDelString(m_Text);
	acutDelString(m_Shape);

	acutDelString(m_A);
	acutDelString(m_B);
	acutDelString(m_C);
	acutDelString(m_D);
	acutDelString(m_E);
	acutDelString(m_F);

	pFiler->readString(&m_Text);
	pFiler->readString(&m_Shape);

	pFiler->readString(&m_A);
	pFiler->readString(&m_B);
	pFiler->readString(&m_C);
	pFiler->readString(&m_D);
	pFiler->readString(&m_E);
	pFiler->readString(&m_F);

	pFiler->readUInt16(&m_TextColor);
	pFiler->readUInt16(&m_ShapeTextColor);
	pFiler->readUInt16(&m_ShapeLineColor);
	pFiler->readUInt16(&m_TopBorderColor);
	pFiler->readUInt16(&m_LeftBorderColor);
	pFiler->readUInt16(&m_BottomBorderColor);
	pFiler->readUInt16(&m_RightBorderColor);

	pFiler->readBoolean(&m_TopBorder);
	pFiler->readBoolean(&m_LeftBorder);
	pFiler->readBoolean(&m_BottomBorder);
	pFiler->readBoolean(&m_RightBorder);

	pFiler->readBoolean(&m_TopBorderDouble);
	pFiler->readBoolean(&m_LeftBorderDouble);
	pFiler->readBoolean(&m_BottomBorderDouble);
	pFiler->readBoolean(&m_RightBorderDouble);

	pFiler->readInt32(&m_MergeRight);
	pFiler->readInt32(&m_MergeDown);

	pFiler->readHardPointerId(&m_TextStyleId);

	pFiler->readDouble(&m_TextHeight);

	Adesk::Int32 t_HorizontalAlignment;
	Adesk::Int32 t_VerticalAlignment;
	pFiler->readInt32(&t_HorizontalAlignment);
	pFiler->readInt32(&t_VerticalAlignment);
	m_HorizontalAlignment =  (CTableCell::Alignment)t_HorizontalAlignment;
	m_VerticalAlignment =  (CTableCell::Alignment)t_VerticalAlignment;

	pFiler->readDouble(&m_Width);
	pFiler->readDouble(&m_Height);

	pFiler->readDouble(&m_Margin);

	return Acad::eOk;
}

Acad::ErrorStatus CTableCell::dxfOutFields(AcDbDxfFiler* pFiler) const
{
	pFiler->writePoint3d(AcDb::kDxfXCoord, m_BasePoint);
	// Use max precision when writing out direction vectors
	pFiler->writeVector3d(AcDb::kDxfXCoord + 1, m_Direction, 16);
	pFiler->writeVector3d(AcDb::kDxfXCoord + 2, m_Up, 16);;

	if(HasText())
		pFiler->writeString(AcDb::kDxfText, m_Text);
	else
		pFiler->writeString(AcDb::kDxfText, _T(""));
	if(HasShape())
		pFiler->writeString(AcDb::kDxfText, m_Shape);
	else
		pFiler->writeString(AcDb::kDxfText, _T(""));

	if(m_A != NULL && m_A[0] != _T('\0'))
		pFiler->writeString(AcDb::kDxfText, m_A);
	else
		pFiler->writeString(AcDb::kDxfText, _T(""));
	if(m_B != NULL && m_B[0] != _T('\0'))
		pFiler->writeString(AcDb::kDxfText, m_B);
	else
		pFiler->writeString(AcDb::kDxfText, _T(""));
	if(m_C != NULL && m_C[0] != _T('\0'))
		pFiler->writeString(AcDb::kDxfText, m_C);
	else
		pFiler->writeString(AcDb::kDxfText, _T(""));
	if(m_D != NULL && m_D[0] != _T('\0'))
		pFiler->writeString(AcDb::kDxfText, m_D);
	else
		pFiler->writeString(AcDb::kDxfText, _T(""));
	if(m_E != NULL && m_E[0] != _T('\0'))
		pFiler->writeString(AcDb::kDxfText, m_E);
	else
		pFiler->writeString(AcDb::kDxfText, _T(""));
	if(m_F != NULL && m_F[0] != _T('\0'))
		pFiler->writeString(AcDb::kDxfText, m_F);
	else
		pFiler->writeString(AcDb::kDxfText, _T(""));

	pFiler->writeUInt16(AcDb::kDxfXInt16, m_TextColor);
	pFiler->writeUInt16(AcDb::kDxfXInt16, m_ShapeTextColor);
	pFiler->writeUInt16(AcDb::kDxfXInt16, m_ShapeLineColor);
	pFiler->writeUInt16(AcDb::kDxfXInt16, m_TopBorderColor);
	pFiler->writeUInt16(AcDb::kDxfXInt16, m_LeftBorderColor);
	pFiler->writeUInt16(AcDb::kDxfXInt16, m_BottomBorderColor);
	pFiler->writeUInt16(AcDb::kDxfXInt16, m_RightBorderColor);

	pFiler->writeBoolean(AcDb::kDxfBool, m_TopBorder);
	pFiler->writeBoolean(AcDb::kDxfBool, m_LeftBorder);
	pFiler->writeBoolean(AcDb::kDxfBool, m_BottomBorder);
	pFiler->writeBoolean(AcDb::kDxfBool, m_RightBorder);

	pFiler->writeBoolean(AcDb::kDxfBool, m_TopBorderDouble);
	pFiler->writeBoolean(AcDb::kDxfBool, m_LeftBorderDouble);
	pFiler->writeBoolean(AcDb::kDxfBool, m_BottomBorderDouble);
	pFiler->writeBoolean(AcDb::kDxfBool, m_RightBorderDouble);

	pFiler->writeInt32(AcDb::kDxfInt32, m_MergeRight);
	pFiler->writeInt32(AcDb::kDxfInt32, m_MergeDown);

	pFiler->writeObjectId(AcDb::kDxfHardPointerId, m_TextStyleId);

	pFiler->writeDouble(AcDb::kDxfReal, m_TextHeight);

	pFiler->writeInt32(AcDb::kDxfInt32, m_HorizontalAlignment);
	pFiler->writeInt32(AcDb::kDxfInt32, m_VerticalAlignment);

	pFiler->writeDouble(AcDb::kDxfReal, m_Width);
	pFiler->writeDouble(AcDb::kDxfReal, m_Height);

	pFiler->writeDouble(AcDb::kDxfReal, m_Margin);

	return Acad::eOk;
}

Acad::ErrorStatus CTableCell::dxfInFields(AcDbDxfFiler* pFiler)
{
	Acad::ErrorStatus es;

	AcGePoint3d t_BasePoint;
	AcGeVector3d t_Direction, t_Up;

	ACHAR* t_Text = NULL;
	ACHAR* t_Shape = NULL;
	ACHAR* t_A = NULL;
	ACHAR* t_B = NULL;
	ACHAR* t_C = NULL;
	ACHAR* t_D = NULL;
	ACHAR* t_E = NULL;
	ACHAR* t_F = NULL;
	
	Adesk::UInt16 t_TextColor;
	Adesk::UInt16 t_ShapeTextColor;
	Adesk::UInt16 t_ShapeLineColor;
	Adesk::UInt16 t_TopBorderColor;
	Adesk::UInt16 t_LeftBorderColor;
	Adesk::UInt16 t_BottomBorderColor;
	Adesk::UInt16 t_RightBorderColor;

	Adesk::Boolean t_TopBorder;
	Adesk::Boolean t_LeftBorder;
	Adesk::Boolean t_BottomBorder;
	Adesk::Boolean t_RightBorder;

	Adesk::Boolean t_TopBorderDouble;
	Adesk::Boolean t_LeftBorderDouble;
	Adesk::Boolean t_BottomBorderDouble;
	Adesk::Boolean t_RightBorderDouble;

	Adesk::Int32 t_MergeRight;
	Adesk::Int32 t_MergeDown;

	AcDbHardPointerId t_TextStyleId;

	double t_TextHeight;

	int t_HorizontalAlignment;
	int t_VerticalAlignment;

	double t_Width;
	double t_Height;
	double t_Margin;

	if((es = Utility::ReadDXFPoint(pFiler, AcDb::kDxfXCoord, _T("base point"), t_BasePoint)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFVector(pFiler, AcDb::kDxfXCoord + 1, _T("direction vector"), t_Direction)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFVector(pFiler, AcDb::kDxfXCoord + 2, _T("up vector"), t_Up)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("cell text"), t_Text)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("cell shape"), t_Shape)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("text replacement A"), t_A)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("text replacement B"), t_B)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("text replacement C"), t_C)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("text replacement D"), t_D)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("text replacement E"), t_E)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFString(pFiler, AcDb::kDxfText, _T("text replacement F"), t_F)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("cell text color"), t_TextColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("cell shape text color"), t_ShapeTextColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("cell shape line color"), t_ShapeLineColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("cell top border color"), t_TopBorderColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("cell left border color"), t_LeftBorderColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("cell bottom border color"), t_BottomBorderColor)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFUInt(pFiler, AcDb::kDxfXInt16, _T("cell right border color"), t_RightBorderColor)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFBool(pFiler, AcDb::kDxfBool, _T("cell top border"), t_TopBorder)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFBool(pFiler, AcDb::kDxfBool, _T("cell left border"), t_LeftBorder)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFBool(pFiler, AcDb::kDxfBool, _T("cell bottom border"), t_BottomBorder)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFBool(pFiler, AcDb::kDxfBool, _T("cell right border"), t_RightBorder)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFBool(pFiler, AcDb::kDxfBool, _T("cell top border double"), t_TopBorderDouble)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFBool(pFiler, AcDb::kDxfBool, _T("cell left border double"), t_LeftBorderDouble)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFBool(pFiler, AcDb::kDxfBool, _T("cell bottom border double"), t_BottomBorderDouble)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFBool(pFiler, AcDb::kDxfBool, _T("cell right border double"), t_RightBorderDouble)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32, _T("cell merge right"), t_MergeRight)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32, _T("cell merge down"), t_MergeDown)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFObjectId(pFiler, AcDb::kDxfHardPointerId, _T("cell text style"), t_TextStyleId)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfReal, _T("cell text height"), t_TextHeight)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32, _T("cell horizontal alignment"), t_HorizontalAlignment)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFLong(pFiler, AcDb::kDxfInt32, _T("cell vertical alignment"), t_VerticalAlignment)) != Acad::eOk) return es;

	if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfReal, _T("table width"), t_Width)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfReal, _T("table height"), t_Height)) != Acad::eOk) return es;
	if((es = Utility::ReadDXFReal(pFiler, AcDb::kDxfReal, _T("cell margin"), t_Margin)) != Acad::eOk) return es;

	m_BasePoint = t_BasePoint;
	m_Direction = t_Direction;
	m_Up = t_Up;
	m_Normal = m_Direction.crossProduct(m_Up);

	setText(t_Text);
	setShape(t_Shape);
	setShapeText(t_A, t_B, t_C, t_D, t_E, t_F);

	m_TextColor = t_TextColor;
	m_ShapeTextColor = t_ShapeTextColor;
	m_ShapeLineColor = t_ShapeLineColor;
	m_TopBorderColor = t_TopBorderColor;
	m_LeftBorderColor = t_LeftBorderColor;
	m_BottomBorderColor = t_BottomBorderColor;
	m_RightBorderColor = t_RightBorderColor;

	m_TopBorder = t_TopBorder;
	m_LeftBorder = t_LeftBorder;
	m_BottomBorder = t_BottomBorder;
	m_RightBorder = t_RightBorder;

	m_TopBorder = t_TopBorderDouble;
	m_LeftBorder = t_LeftBorderDouble;
	m_BottomBorder = t_BottomBorderDouble;
	m_RightBorder = t_RightBorderDouble;

	m_MergeRight = t_MergeRight;
	m_MergeDown = t_MergeDown;

	m_TextStyleId = t_TextStyleId;

	m_TextHeight = t_TextHeight;

	m_HorizontalAlignment =  (CTableCell::Alignment)t_HorizontalAlignment;
	m_VerticalAlignment =  (CTableCell::Alignment)t_VerticalAlignment;

	m_Width = t_Width;
	m_Height = t_Height;
	m_Margin = t_Margin;

	acutDelString(t_Text);
	acutDelString(t_A);
	acutDelString(t_B);
	acutDelString(t_C);
	acutDelString(t_D);
	acutDelString(t_E);
	acutDelString(t_F);

	return Acad::eOk;
}

//*************************************************************************
// Overridden methods from AcDbEntity
//*************************************************************************
Acad::ErrorStatus CTableCell::getOsnapPoints(
    AcDb::OsnapMode       osnapMode,
    Adesk::GsMarker       gsSelectionMark,
    const AcGePoint3d&    pickPoint,
    const AcGePoint3d&    lastPoint,
    const AcGeMatrix3d&   viewXform,
    AcGePoint3dArray&     snapPoints,
    AcDbIntArray&         /*geomIds*/) const
{
	if(osnapMode == AcDb::kOsModeEnd)
	{
		AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
		trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, m_Normal);

		if(m_TopBorder == Adesk::kTrue || m_LeftBorder == Adesk::kTrue)
		{
			AcGePoint3d pt(0, 0, 0);
			pt.transformBy(trans);
			snapPoints.append(pt);
		}
		if(m_TopBorder == Adesk::kTrue || m_RightBorder == Adesk::kTrue)
		{
			AcGePoint3d pt(m_Width, 0, 0);
			pt.transformBy(trans);
			snapPoints.append(pt);
		}
		if(m_BottomBorder == Adesk::kTrue || m_LeftBorder == Adesk::kTrue)
		{
			AcGePoint3d pt(0, -m_Height, 0);
			pt.transformBy(trans);
			snapPoints.append(pt);
		}
		if(m_BottomBorder == Adesk::kTrue || m_RightBorder == Adesk::kTrue)
		{
			AcGePoint3d pt(m_Width, -m_Height, 0);
			pt.transformBy(trans);
			snapPoints.append(pt);
		}
	}

	return Acad::eOk;
}

Acad::ErrorStatus CTableCell::resetTransform()
{
	m_BasePoint.set(0, 0, 0);
	m_Direction.set(1, 0, 0);
	m_Up.set(0, 1, 0);
	m_Normal.set(0, 0, 1);

	return Acad::eOk;
}

Acad::ErrorStatus CTableCell::transformBy(const AcGeMatrix3d& xform)
{
	m_BasePoint.transformBy(xform);
	m_Direction.transformBy(xform);
	m_Up.transformBy(xform);
	m_Normal = m_Direction.crossProduct(m_Up);

	// Correct direction vectors
	double scale = m_Direction.length();
	m_Direction = m_Direction.normal() * scale;
	m_Up = m_Up.normal() * scale;
	m_Normal = m_Normal.normal() * scale;

	return Acad::eOk;
}

Acad::ErrorStatus CTableCell::explode(AcDbVoidPtrArray& entitySet) const
{
	// Texts
	std::vector<AcDbMText*> texts = GetTexts();
	for(std::vector<AcDbMText*>::iterator it = texts.begin(); it != texts.end(); it++)
	{
		AcDbMText* text = (*it);
		entitySet.append(text);
	}
	texts.clear();

	// Lines
	std::vector<AcDbLine*> lines = GetLines();
	for(std::vector<AcDbLine*>::iterator it = lines.begin(); it != lines.end(); it++)
	{
		AcDbLine* line = (*it);
		entitySet.append(line);
	}
	lines.clear();

	// Arcs
	std::vector<AcDbArc*> arcs = GetArcs();
	for(std::vector<AcDbArc*>::iterator it = arcs.begin(); it != arcs.end(); it++)
	{
		AcDbArc* arc = (*it);
		entitySet.append(arc);
	}
	arcs.clear();

	return Acad::eOk;
}

Adesk::Boolean CTableCell::worldDraw(AcGiWorldDraw* worldDraw)
{
    if(worldDraw->regenAbort())
	{
        return Adesk::kTrue;
    }

	// Transform to match orientation
	AcGeMatrix3d trans = AcGeMatrix3d::kIdentity;
	trans.setCoordSystem(m_BasePoint, m_Direction, m_Up, m_Normal);
	worldDraw->geometry().pushModelTransform(trans);

	// Draw texts
	if(HasText())
	{
		AcGiTextStyle textStyle;
		Utility::MakeGiTextStyle(textStyle, m_TextStyleId);
		textStyle.setTextSize(m_TextHeight);
		textStyle.loadStyleRec();

		double cx = 0;
		double cy = 0;
		AcDb::TextHorzMode horz = AcDb::kTextLeft;
		AcDb::TextVertMode vert = AcDb::kTextBottom;
		if(m_HorizontalAlignment == CTableCell::eNEAR)
		{
			cx = m_Margin;
			horz = AcDb::kTextLeft;
		}
		else if(m_HorizontalAlignment == CTableCell::eFAR)
		{
			cx = m_Width - m_Margin;
			horz = AcDb::kTextRight;
		}
		else if(m_HorizontalAlignment == CTableCell::eCENTER)
		{
			cx = m_Width / 2.0;
			horz = AcDb::kTextCenter;
		}
		if(m_VerticalAlignment == CTableCell::eNEAR)
		{
			cy = -m_Height + m_Margin;
			vert = AcDb::kTextBottom;
		}
		else if(m_VerticalAlignment == CTableCell::eFAR)
		{
			cy = m_Margin;
			vert = AcDb::kTextTop;
		}
		else if(m_VerticalAlignment == CTableCell::eCENTER)
		{
			cy = -m_Height / 2.0;
			vert = AcDb::kTextVertMid;
		}

		Utility::DrawText(worldDraw, AcGePoint3d(cx, cy, 0.0), m_Text, textStyle, horz, vert, m_TextColor);
	}

	// Draw lines
	double doublelineoffset = 0.125;

	if(m_TopBorder)
	{
		if(m_TopBorderDouble)
			Utility::DrawDoubleLine(worldDraw, AcGePoint3d(0, 0, 0), AcGePoint3d(m_Width, 0, 0), doublelineoffset, m_TopBorderColor);
		else
			Utility::DrawLine(worldDraw, AcGePoint3d(0, 0, 0), AcGePoint3d(m_Width, 0, 0), m_TopBorderColor);
	}
	if(m_BottomBorder)
	{
		if(m_BottomBorderDouble)
			Utility::DrawDoubleLine(worldDraw, AcGePoint3d(0, -m_Height, 0), AcGePoint3d(m_Width, -m_Height, 0), doublelineoffset, m_TopBorderColor);
		else
			Utility::DrawLine(worldDraw, AcGePoint3d(0, -m_Height, 0), AcGePoint3d(m_Width, -m_Height, 0), m_BottomBorderColor);
	}
	if(m_LeftBorder)
	{
		if(m_LeftBorderDouble)
			Utility::DrawDoubleLine(worldDraw, AcGePoint3d(0, 0, 0), AcGePoint3d(0, -m_Height, 0), doublelineoffset, m_TopBorderColor);
		else
			Utility::DrawLine(worldDraw, AcGePoint3d(0, 0, 0), AcGePoint3d(0, -m_Height, 0), m_LeftBorderColor);
	}
	if(m_RightBorder)
	{
		if(m_RightBorderDouble)
			Utility::DrawDoubleLine(worldDraw, AcGePoint3d(m_Width, 0, 0), AcGePoint3d(m_Width, -m_Height, 0), doublelineoffset, m_TopBorderColor);
		else
			Utility::DrawLine(worldDraw, AcGePoint3d(m_Width, 0, 0), AcGePoint3d(m_Width, -m_Height, 0), m_RightBorderColor);
	}

	// Draw shape
	if(HasShape())
	{
		CPosShape* pShape = CPosShape::GetPosShape(m_Shape);
		AcDbExtents ext;
		if(pShape->bounds(ext))
		{
			pShape->setShapeTexts(m_A, m_B, m_C, m_D, m_E, m_F);

			double maxwidth = (ext.maxPoint().x - ext.minPoint().x);
			double maxheight = (ext.maxPoint().y - ext.minPoint().y);
			double scale = m_TextHeight / 25.0;

			double xoff = 0;
			double yoff = 0;
			if(m_HorizontalAlignment == CTableCell::eNEAR)
				xoff = m_Margin;
			else if(m_HorizontalAlignment == CTableCell::eFAR)
				xoff = m_Width - m_Margin - maxwidth * scale;
			else if(m_HorizontalAlignment == CTableCell::eCENTER)
				xoff = (m_Width - maxwidth * scale) / 2.0;

			if(m_VerticalAlignment == CTableCell::eNEAR)
				yoff = m_Margin;
			else if(m_VerticalAlignment == CTableCell::eFAR)
				yoff = m_Height - m_Margin - maxheight * scale;
			else if(m_VerticalAlignment == CTableCell::eCENTER)
				yoff = (m_Height - maxheight * scale) / 2.0;

			AcGeMatrix3d shapeTrans = AcGeMatrix3d::kIdentity;
			shapeTrans.setCoordSystem(AcGePoint3d(xoff - ext.minPoint().x * scale, -m_Height + yoff - ext.minPoint().y * scale, 0.0), AcGeVector3d::kXAxis * scale, AcGeVector3d::kYAxis * scale, AcGeVector3d::kZAxis * scale);
			worldDraw->geometry().pushModelTransform(shapeTrans);
			worldDraw->geometry().draw(pShape);
			worldDraw->geometry().popModelTransform();

			pShape->clearShapeTexts();
		}
	}

	// Reset transform
	worldDraw->geometry().popModelTransform();

    return Adesk::kTrue; // Don't call viewportDraw()
}