//-----------------------------------------------------------------------------
//----- DrawParams.h : Declaration of the CDrawParams
//-----------------------------------------------------------------------------
#pragma once

#include <string>

struct CDrawParams
{
public:
	int type;
	std::wstring text;
	bool hasCircle;
	unsigned short color;
	double x;
	double y;
	double w;
	double h;

public:
	CDrawParams() : type(0), text(L""), hasCircle(false), color(0), x(0), y(0), w(0), h(0)
	{ }

	CDrawParams(int _type, std::wstring _text, bool _hasCircle) : type(_type), text(_text), hasCircle(_hasCircle), color(0), x(0), y(0), w(0), h(0)
	{ }

	CDrawParams(int _type, std::wstring _text) : type(_type), text(_text), hasCircle(false), color(0), x(0), y(0), w(0), h(0)
	{ }
};
