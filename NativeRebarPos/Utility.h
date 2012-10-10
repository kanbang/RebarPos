//-----------------------------------------------------------------------------
//----- Utility.h : Declaration of utility functions
//-----------------------------------------------------------------------------
#pragma once

#include <string>
#include <sstream>
#include <vector>

namespace Utility
{
	/// Creates a hidden (non-plotted) layer
	AcDbObjectId CreateHiddenLayer(const ACHAR* name, const AcCmColor& color);

	/// Creates a text style
	AcDbObjectId CreateTextStyle(const ACHAR* name, const ACHAR* filename, const double scale);

	/// Create an AcGiTextStyle from the db id of an AcDbTextStyleTableRecord
	Acad::ErrorStatus MakeGiTextStyle(AcGiTextStyle &ts, const AcDbObjectId  styleId);

	/// Replace all occurences of string
	const void ReplaceString(std::wstring& str, const std::wstring& oldStr, const std::wstring& newStr);

	/// Convert double to string
	const void DoubleToStr(const double val, const int digits, std::wstring& str);
	const void DoubleToStr(const double val, std::wstring& str);

	/// Convert string to double
	const double StrToDouble(const std::wstring& str);
	const double StrToDouble(const wchar_t* str);

	/// Convert int to string
	const void IntToStr(const int val, std::wstring& str);

	/// Convert double to int
	const int DoubleToInt(const double val);

	/// Convert string to int
	const int StrToInt(const std::wstring& str);
	const int StrToInt(const wchar_t* str);

	// Read DXF vals
	const Acad::ErrorStatus ReadDXFItem(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, resbuf* val);
	const Acad::ErrorStatus ReadDXFInt(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, short& val);
	const Acad::ErrorStatus ReadDXFUInt(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned short& val);
	const Acad::ErrorStatus ReadDXFLong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, int& val);
	const Acad::ErrorStatus ReadDXFULong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned int& val);
	const Acad::ErrorStatus ReadDXFLong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, long& val);
	const Acad::ErrorStatus ReadDXFULong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned long& val);
	const Acad::ErrorStatus ReadDXFReal(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, double& val);
	const Acad::ErrorStatus ReadDXFString(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, ACHAR*& val);
	const Acad::ErrorStatus ReadDXFPoint(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGePoint2d& val);
	const Acad::ErrorStatus ReadDXFPoint(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGePoint3d& val);
	const Acad::ErrorStatus ReadDXFVector(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGeVector2d& val);
	const Acad::ErrorStatus ReadDXFVector(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGeVector3d& val);
	const Acad::ErrorStatus ReadDXFBool(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, Adesk::Boolean& val);
	const Acad::ErrorStatus ReadDXFBool(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, bool& val);
	const Acad::ErrorStatus ReadDXFObjectId(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcDbObjectId& val);

	// Split string
	std::vector<std::wstring> SplitString(const std::wstring& s, wchar_t delim);
	std::vector<std::wstring> SplitString(const std::wstring& s);
}
