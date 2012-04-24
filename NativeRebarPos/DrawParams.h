//-----------------------------------------------------------------------------
//----- DrawParams.h : Declaration of the CDrawParams
//-----------------------------------------------------------------------------
#pragma once

#include "AcString.h"

struct CDrawParams
{
public:
	int type;
	AcString text;
	bool hasCircle;
	Adesk::UInt16 color;
	double x;
	double y;
	double w;
	double h;

public:
	CDrawParams() : type(0), text(""), hasCircle(false), color(0), x(0), y(0), w(0), h(0)
	{ }

	CDrawParams(int _type, AcString _text, bool _hasCircle) : type(_type), text(_text), hasCircle(_hasCircle), color(0), x(0), y(0), w(0), h(0)
	{ }

	CDrawParams(int _type, AcString _text) : type(_type), text(_text), hasCircle(false), color(0), x(0), y(0), w(0), h(0)
	{ }
};
