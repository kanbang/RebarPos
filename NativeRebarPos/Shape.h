#pragma once

#include "dbmain.h"

struct CShape
{
	enum ShapeType
	{ 
		Line = 0,
		Arc = 1,
		Text = 2,
	};

	CShape::ShapeType type;
	Adesk::UInt16 color;

	virtual CShape* clone() const = 0;

protected:
	CShape(const CShape::ShapeType Type, const Adesk::UInt16 Color = 0) : type(Type), color(Color)
	{ }
};

struct CShapeLine : CShape
{
	ads_real x1;
	ads_real y1;
	ads_real x2;
	ads_real y2;

	CShapeLine()
		: CShape(CShape::Line), x1(0), y1(0), x2(0), y2(0) 
	{ }

	CShapeLine(const Adesk::UInt16 Color, ads_real X1, ads_real Y1, ads_real X2, ads_real Y2)
		: CShape(CShape::Line, Color), x1(X1), y1(Y1), x2(X2), y2(Y2) 
	{ }

	virtual CShapeLine* clone() const
	{
		return new CShapeLine(*this);
	}
};

struct CShapeArc : CShape
{
	ads_real x;
	ads_real y;
	ads_real r;
	ads_real startAngle;
	ads_real endAngle;

	CShapeArc()
		: CShape(CShape::Arc), x(0), y(0), r(0), startAngle(0), endAngle(0)
	{ }

	CShapeArc(const Adesk::UInt16 Color, ads_real X, ads_real Y, ads_real R, ads_real StartAngle, ads_real EndAngle)
		: CShape(CShape::Arc, Color), x(X), y(Y), r(R), startAngle(StartAngle), endAngle(EndAngle)
	{ }

	virtual CShapeArc* clone() const
	{
		return new CShapeArc(*this);
	}
};

struct CShapeText : CShape
{
	ads_real x;
	ads_real y;
	ads_real height;
	ACHAR* text;
	AcDb::TextHorzMode horizontalAlignment;
	AcDb::TextVertMode verticalAlignment;

	CShapeText()
		: CShape(CShape::Text), x(0), y(0), height(0), text(NULL), horizontalAlignment(AcDb::kTextLeft), verticalAlignment(AcDb::kTextBase)
	{ }

	CShapeText(const Adesk::UInt16 Color, ads_real X, ads_real Y, ads_real Height, ACHAR* Text, AcDb::TextHorzMode HorizontalAlignment, AcDb::TextVertMode VerticalAlignment)
		: CShape(CShape::Text, Color), x(X), y(Y), height(Height), text(NULL), horizontalAlignment(HorizontalAlignment), verticalAlignment(VerticalAlignment)
	{ 
		acutUpdString(Text, text);
	}

	~CShapeText()
	{
		acutDelString(text);
	}

	virtual CShapeText* clone() const
	{
		return new CShapeText(*this);
	}
};
