#include "StdAfx.h"

#include "Shape.h"

CShapeLine::CShapeLine() : CShape(CShape::Line), x1(0), y1(0), x2(0), y2(0) 
{ }

CShapeLine::CShapeLine(const Adesk::UInt16 Color, const double X1, const double Y1, const double X2, const double Y2)
	: CShape(CShape::Line, Color), x1(X1), y1(Y1), x2(X2), y2(Y2) 
{ }

CShapeLine* CShapeLine::clone() const
{
	return new CShapeLine(*this);
}

CShapeArc::CShapeArc() : CShape(CShape::Arc), x(0), y(0), r(0), startAngle(0), endAngle(0)
{ }

CShapeArc::CShapeArc(const Adesk::UInt16 Color, const double X, const double Y, const double R, const double StartAngle, const double EndAngle)
	: CShape(CShape::Arc, Color), x(X), y(Y), r(R), startAngle(StartAngle), endAngle(EndAngle)
{ }

CShapeArc* CShapeArc::clone() const
{
	return new CShapeArc(*this);
}

CShapeText::CShapeText() : CShape(CShape::Text), x(0), y(0), height(0), text(L""), 
	horizontalAlignment(AcDb::kTextLeft), verticalAlignment(AcDb::kTextBase)
{ }

CShapeText::CShapeText(const Adesk::UInt16 Color, const double X, const double Y, const double Height, const std::wstring& Text, const AcDb::TextHorzMode HorizontalAlignment, const AcDb::TextVertMode VerticalAlignment)
	: CShape(CShape::Text, Color), x(X), y(Y), height(Height), text(L""), horizontalAlignment(HorizontalAlignment), verticalAlignment(VerticalAlignment)
{
	text = Text;
}

void CShapeText::SetText(const std::wstring& Text)
{
	text.assign(Text);
}

void CShapeText::SetText(const wchar_t* Text)
{
	text.assign(Text);
}

CShapeText* CShapeText::clone() const
{
	return new CShapeText(*this);
}

