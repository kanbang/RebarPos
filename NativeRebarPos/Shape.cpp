#include "StdAfx.h"

#include "Shape.h"

// LINE
CShapeLine::CShapeLine() : CShape(CShape::Line), x1(0), y1(0), x2(0), y2(0) 
{ }

CShapeLine::CShapeLine(const Adesk::UInt16 Color, const double X1, const double Y1, const double X2, const double Y2, const Adesk::Boolean Visible)
	: CShape(CShape::Line, Color, Visible), x1(X1), y1(Y1), x2(X2), y2(Y2) 
{ }

CShapeLine* CShapeLine::clone() const
{
	return new CShapeLine(*this);
}

// ARC
CShapeArc::CShapeArc() : CShape(CShape::Arc), x(0), y(0), r(0), startAngle(0), endAngle(0)
{ }

CShapeArc::CShapeArc(const Adesk::UInt16 Color, const double X, const double Y, const double R, const double StartAngle, const double EndAngle, const Adesk::Boolean Visible)
	: CShape(CShape::Arc, Color, Visible), x(X), y(Y), r(R), startAngle(StartAngle), endAngle(EndAngle)
{ }

CShapeArc* CShapeArc::clone() const
{
	return new CShapeArc(*this);
}

// CIRCLE
CShapeCircle::CShapeCircle() : CShape(CShape::Circle), x(0), y(0), r(0)
{ }

CShapeCircle::CShapeCircle(const Adesk::UInt16 Color, const double X, const double Y, const double R, const Adesk::Boolean Visible)
	: CShape(CShape::Circle, Color, Visible), x(X), y(Y), r(R)
{ }

CShapeCircle* CShapeCircle::clone() const
{
	return new CShapeCircle(*this);
}

// ELLIPSE
CShapeEllipse::CShapeEllipse() : CShape(CShape::Ellipse), x(0), y(0), r(0), width(0), height(0)
{ }

CShapeEllipse::CShapeEllipse(const Adesk::UInt16 Color, const double X, const double Y, const double Width, const double Height, const Adesk::Boolean Visible)
	: CShape(CShape::Ellipse, Color, Visible), x(X), y(Y), width(Width), height(Height)
{ }

CShapeEllipse* CShapeEllipse::clone() const
{
	return new CShapeEllipse(*this);
}

// TEXT
CShapeText::CShapeText() : CShape(CShape::Text), x(0), y(0), height(0), text(L""), 
	horizontalAlignment(AcDb::kTextLeft), verticalAlignment(AcDb::kTextBase)
{ }

CShapeText::CShapeText(const Adesk::UInt16 Color, const double X, const double Y, const double Height, const double Width, const std::wstring& Text, const std::wstring& Font, const AcDb::TextHorzMode HorizontalAlignment, const AcDb::TextVertMode VerticalAlignment, const Adesk::Boolean Visible)
	: CShape(CShape::Text, Color, Visible), x(X), y(Y), height(Height), width(Width), text(L""), font(L""), horizontalAlignment(HorizontalAlignment), verticalAlignment(VerticalAlignment)
{
	text = Text;
	font = Font;
}

void CShapeText::SetText(const std::wstring& Text)
{
	text.assign(Text);
}

void CShapeText::SetText(const wchar_t* Text)
{
	text.assign(Text);
}

void CShapeText::SetFont(const std::wstring& Font)
{
	font.assign(Font);
}

void CShapeText::SetFont(const wchar_t* Font)
{
	font.assign(Font);
}

CShapeText* CShapeText::clone() const
{
	return new CShapeText(*this);
}

