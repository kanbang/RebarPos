//-----------------------------------------------------------------------------
//----- Utility.h : Declaration of utility functions
//-----------------------------------------------------------------------------
#pragma once

#pragma warning( push )
#pragma warning( disable : 4100 )
#include "acgi.h"
#pragma warning( pop )

#include <string>
#include <sstream>
#include <vector>

// The following is part of the code used to export an API
// and/or use the exported API.
//
#pragma warning( disable: 4275 4251 )
#ifdef REBARPOS_MODULE
#define DLLIMPEXP __declspec( dllexport )
#else
#define DLLIMPEXP
#endif

class DLLIMPEXP Utility
{
private:
    Utility();
    Utility(Utility const&);
    void operator=(Utility const&);

public:
	/// Creates a hidden (non-plotted) layer
	static AcDbObjectId CreateHiddenLayer();

	/// Creates a text style
	static AcDbObjectId CreateTextStyle(const ACHAR* name, const ACHAR* filename, const double scale);

	/// Create an AcGiTextStyle from the db id of an AcDbTextStyleTableRecord
	static Acad::ErrorStatus MakeGiTextStyle(AcGiTextStyle &ts, const AcDbObjectId  styleId);

	/// Replace all occurences of string
	static  void ReplaceString(std::wstring& str, const std::wstring& oldStr, const std::wstring& newStr);

	/// Convert double to string
	static  void DoubleToStr(const double val, const int digits, std::wstring& str);
	static  void DoubleToStr(const double val, std::wstring& str);

	/// Convert string to double
	static  double StrToDouble(const std::wstring& str);
	static  double StrToDouble(const wchar_t* str);

	/// Convert int to string
	static  void IntToStr(const int val, std::wstring& str);

	/// Convert double to int
	static  int DoubleToInt(const double val);

	/// Convert string to int
	static  int StrToInt(const std::wstring& str);
	static  int StrToInt(const wchar_t* str);

	// Read DXF vals
	static  Acad::ErrorStatus ReadDXFItem(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, resbuf* val);
	static  Acad::ErrorStatus ReadDXFInt(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, short& val);
	static  Acad::ErrorStatus ReadDXFUInt(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned short& val);
	static  Acad::ErrorStatus ReadDXFLong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, int& val);
	static  Acad::ErrorStatus ReadDXFULong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned int& val);
	static  Acad::ErrorStatus ReadDXFLong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, long& val);
	static  Acad::ErrorStatus ReadDXFULong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned long& val);
	static  Acad::ErrorStatus ReadDXFReal(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, double& val);
	static  Acad::ErrorStatus ReadDXFString(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, ACHAR*& val);
	static  Acad::ErrorStatus ReadDXFPoint(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGePoint2d& val);
	static  Acad::ErrorStatus ReadDXFPoint(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGePoint3d& val);
	static  Acad::ErrorStatus ReadDXFVector(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGeVector2d& val);
	static  Acad::ErrorStatus ReadDXFVector(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGeVector3d& val);
	static  Acad::ErrorStatus ReadDXFBool(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, Adesk::Boolean& val);
	static  Acad::ErrorStatus ReadDXFBool(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, bool& val);
	static  Acad::ErrorStatus ReadDXFObjectId(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcDbObjectId& val);

	// Split string
	static std::vector<std::wstring> SplitString(const std::wstring& s, wchar_t delim);
	static std::vector<std::wstring> SplitString(const std::wstring& s);
};
