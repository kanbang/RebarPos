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

public:
	CDrawParams(int _type, AcString _text, bool _hasCircle) : type(_type), text(_text), hasCircle(_hasCircle)
	{ }

	CDrawParams(int _type, AcString _text) : type(_type), text(_text), hasCircle(false)
	{ }
};
