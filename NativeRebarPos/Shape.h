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
};

struct CShapeText : CShape
{
	ads_real x;
	ads_real y;
	ads_real height;
	ACHAR* text;

	CShapeText()
		: CShape(CShape::Text), x(0), y(0), height(0), text(NULL)
	{ }

	~CShapeText()
	{
		acutDelString(text);
	}
};
