//-----------------------------------------------------------------------------
//----- DrawParams.h : Declaration of the CDrawParams
//-----------------------------------------------------------------------------
#pragma once

#include <string>
#include "Shape.h"

struct CDrawParams
{
public:
	int type;
	std::wstring text;
	bool hasCircle;
	bool hasTau;
	unsigned short color;
	double x;
	double y;
	double w;
	double h;
	double widthFactor;

public:
	CDrawParams() : type(0), text(L""), hasCircle(false), hasTau(false), color(0), x(0), y(0), w(0), h(0), widthFactor(1.0)
	{ }

	CDrawParams(int _type, std::wstring _text, bool _hasCircle) : type(_type), text(_text), hasCircle(_hasCircle), hasTau(false), color(0), x(0), y(0), w(0), h(0), widthFactor(1.0)
	{ }

	CDrawParams(int _type, std::wstring _text) : type(_type), text(_text), hasCircle(false), hasTau(false), color(0), x(0), y(0), w(0), h(0), widthFactor(1.0)
	{ }
};

struct CDrawTextParams
{
public:
	enum Alignment
	{
		eNEAR = 0,
		eCENTER = 1,
		eFAR = 2
	};

public:
	std::wstring text;
	unsigned short color;
	AcDbObjectId styleId;
	double x;
	double y;
	double w;
	double h;
	double height;
	Alignment halign;
	Alignment valign;

public:
	CDrawTextParams() : text(L""), color(0), x(0), y(0), w(0), h(0), styleId(AcDbObjectId::kNull), height(0), halign(eNEAR), valign(eNEAR)
	{ }

	CDrawTextParams(std::wstring _text) : text(_text), color(0), x(0), y(0), w(0), h(0), styleId(AcDbObjectId::kNull), height(0), halign(eNEAR), valign(eNEAR)
	{ }
};

struct CDrawLineParams
{
public:
	unsigned short color;
	double x1;
	double y1;
	double x2;
	double y2;

public:
	CDrawLineParams() : color(0), x1(0), y1(0), x2(0), y2(0)
	{ }
};

struct CDrawShapeParams
{
public:
	double x;
	double y;
	double xmin;
	double ymin;
	double xmax;
	double ymax;
	double scale;
	std::vector<CShape*> shapes;

public:
	CDrawShapeParams() : x(0), y(0), scale(1.0)
	{ }

	~CDrawShapeParams()
	{
		for(std::vector<CShape*>::iterator it = shapes.begin(); it != shapes.end(); it++)
		{
			delete *it;
		}
		shapes.clear();
	}
};
