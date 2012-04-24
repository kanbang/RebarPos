//-----------------------------------------------------------------------------
//----- Utility.h : Declaration of utility functions
//-----------------------------------------------------------------------------
#pragma once

#include "acgi.h"

namespace Utility
{
	/// Creates a text style
	AcDbObjectId CreateTextStyle(const ACHAR* name, const ACHAR* filename, const double scale);

	/// Create an AcGiTextStyle from the db id of an AcDbTextStyleTableRecord
	Acad::ErrorStatus MakeGiTextStyle(AcGiTextStyle &ts, AcDbObjectId  styleId);
}