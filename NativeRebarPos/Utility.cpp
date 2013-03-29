//-----------------------------------------------------------------------------
//----- Utility.cpp : Implementation of utility functions
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include <sstream>
#include <iomanip>
#include <locale>

#include "Utility.h"

AcDbObjectId Utility::CreateHiddenLayer()
{
    AcDbObjectId id;

	AcDbLayerTable *pLayerTbl = NULL;
	AcDbDatabase *pDb = acdbHostApplicationServices()->workingDatabase();
	pDb->getSymbolTable(pLayerTbl, AcDb::kForRead);
	if (pLayerTbl->getAt(_T("Defpoints"), id, AcDb::kForRead) == Acad::eKeyNotFound)
	{
		pLayerTbl->upgradeOpen();
		AcDbLayerTableRecord* pLayer = new AcDbLayerTableRecord();
		pLayer->setName(_T("Defpoints"));
		pLayerTbl->add(id, pLayer);
		pLayer->close();
		pLayerTbl->downgradeOpen();
	}
	pLayerTbl->close();

	return id;
}

AcDbObjectId Utility::CreateTextStyle(const ACHAR* name, const ACHAR* filename, const double scale)
{
	AcDbObjectId id;

	AcDbTextStyleTable *pStyleTbl = NULL;
	AcDbDatabase *pDb = acdbHostApplicationServices()->workingDatabase();
	pDb->getSymbolTable(pStyleTbl, AcDb::kForRead);
	if (pStyleTbl->getAt(name, id, AcDb::kForRead) == Acad::eKeyNotFound)
	{
		pStyleTbl->upgradeOpen();
		AcDbTextStyleTableRecord* pText = new AcDbTextStyleTableRecord();
		pText->setName(name);
		pText->setFileName(filename);
		pText->setXScale(scale);
		pStyleTbl->add(id, pText);
		pText->close();
		pStyleTbl->downgradeOpen();
	}
	pStyleTbl->close();

	return id;
}

Acad::ErrorStatus Utility::MakeGiTextStyle(AcGiTextStyle &newStyle, const AcDbObjectId styleId)
{
	Acad::ErrorStatus es;
	AcDbObjectPointer<AcDbTextStyleTableRecord> oldStyle (styleId, AcDb::kForRead);
	if((es = oldStyle.openStatus()) != Acad::eOk)
		return es;

    ACHAR* filename = NULL;
    if ((es = oldStyle->fileName(filename)) != Acad::eOk) 
        return es;
    ACHAR* bigfilename = NULL;
    if ((es = oldStyle->bigFontFileName(bigfilename)) != Acad::eOk) 
        return es;

	Utility::MakeGiTextStyle(newStyle, filename, bigfilename, oldStyle->textSize(), oldStyle->xScale(), oldStyle->obliquingAngle());

	acutDelString(filename);
	acutDelString(bigfilename);

	return Acad::eOk;
}

void Utility::MakeGiTextStyle(AcGiTextStyle &newStyle, const ACHAR* filename, const ACHAR* bigFontFilename, const double textSize, const double widthFactor, const double obliquingAngle)
{
    newStyle.setFileName(filename);
    newStyle.setBigFontFileName(bigFontFilename);

    newStyle.setTextSize(textSize);
    newStyle.setXScale(widthFactor);
    newStyle.setObliquingAngle(obliquingAngle);

    newStyle.loadStyleRec();
}

void Utility::ReplaceString(std::wstring& str, const std::wstring& oldStr, const std::wstring& newStr)
{
	size_t pos = 0;
	while((pos = str.find(oldStr, pos)) != std::string::npos)
	{
		str.replace(pos, oldStr.length(), newStr);
		pos += newStr.length();
	}
}

bool Utility::IsNumeric(const std::wstring& str)
{
    std::wstring::const_iterator it = str.begin();
	std::locale loc = std::locale::classic();
	while (it != str.end() && std::isdigit(*it, loc)) ++it;
    return !str.empty() && it == str.end();
}

void Utility::IntToStr(const int val, std::wstring& str)
{
	std::wstringstream s;
	s << val;
	str = s.str();
}

int Utility::DoubleToInt(const double val)
{
	 return (int)(val >= 0 ? val + 0.5 : val - 0.5);
}

int Utility::StrToInt(const std::wstring& str)
{
	return Utility::StrToInt(str.c_str());
}

int Utility::StrToInt(const wchar_t* str)
{
	return _wtoi(str);
}

void Utility::DoubleToStr(const double val, const int digits, std::wstring& str)
{
	std::wstringstream s;
	s << std::fixed << std::setprecision(digits) << val;
	str = s.str();
}

void Utility::DoubleToStr(const double val, std::wstring& str)
{
	std::wstringstream s;
	s << val;
	str = s.str();
}

double Utility::StrToDouble(const std::wstring& str)
{
	return Utility::StrToDouble(str.c_str());
}

double Utility::StrToDouble(const wchar_t* str)
{
	return _wtof(str);
}

Acad::ErrorStatus Utility::ReadDXFItem(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, resbuf* rb)
{
	Acad::ErrorStatus es;
	if ((es = pFiler->readItem(rb)) != Acad::eOk)
	{
		pFiler->pushBackItem();
		pFiler->setError(Acad::eInvalidDxfCode, _T("\nError: unable to read group code %d for %s"), code, name);
		return pFiler->filerStatus();
	}
	else if(rb->restype != code)
	{
		pFiler->pushBackItem();
		pFiler->setError(Acad::eInvalidDxfCode, _T("\nError: expected group code %d for %s, but got %d"), code, name, rb->restype);
		return Acad::eMissingDxfField;
	}

	return pFiler->filerStatus();
}

Acad::ErrorStatus Utility::ReadDXFInt(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, short& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rint;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFUInt(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned short& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rint;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFLong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, int& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rlong;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFULong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned int& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rlong;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFLong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, long& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rlong;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFULong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned long& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rlong;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFReal(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, double& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rreal;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFString(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, ACHAR*& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		acutDelString(val);
		val = NULL;
		if(rb.resval.rstring != NULL)
		{
			acutUpdString(rb.resval.rstring, val);
		}
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFPoint(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGePoint2d& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val.x = rb.resval.rpoint[0];
		val.y = rb.resval.rpoint[1];
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFPoint(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGePoint3d& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val.x = rb.resval.rpoint[0];
		val.y = rb.resval.rpoint[1];
		val.z = rb.resval.rpoint[2];
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFVector(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGeVector2d& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val.x = rb.resval.rpoint[0];
		val.y = rb.resval.rpoint[1];
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFVector(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcGeVector3d& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val.x = rb.resval.rpoint[0];
		val.y = rb.resval.rpoint[1];
		val.z = rb.resval.rpoint[2];
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFBool(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, Adesk::Boolean& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = (rb.resval.rint == 0) ? Adesk::kFalse : Adesk::kTrue;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFBool(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, bool& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = (rb.resval.rint != 0);
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFObjectId(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcDbObjectId& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		if((es = acdbGetObjectId(val, rb.resval.rlname)) != Acad::eOk)
		{
			val = AcDbObjectId::kNull;
		}
	}
	return es;
}

std::vector<std::wstring> Utility::SplitString(const std::wstring& s, wchar_t delim)
{
	std::vector<std::wstring> elems;

	std::wstringstream ss(s);
	std::wstring item;
	while(std::getline(ss, item, delim)) 
	{
		elems.push_back(item);
	}

	return elems;
}

std::vector<std::wstring> Utility::SplitString(const std::wstring& s)
{
	return SplitString(s, L'\n');
}

void Utility::DrawLine(const AcGiWorldDraw* worldDraw, const AcGePoint3d& pt1, const AcGePoint3d& pt2, const Adesk::UInt16 color)
{
	worldDraw->subEntityTraits().setColor(color);
	AcGePoint3d line[2];
	line[0] = pt1;
	line[1] = pt2;
	worldDraw->geometry().polyline(2, line);
}

void Utility::DrawCircle(const AcGiWorldDraw* worldDraw, const AcGePoint3d& center, const double radius, const Adesk::UInt16 color)
{
	const double PI = 4.0 * atan(1.0);

	worldDraw->subEntityTraits().setColor(color);
	worldDraw->geometry().circle(center, radius, AcGeVector3d::kZAxis);
}

void Utility::DrawArc(const AcGiWorldDraw* worldDraw, const AcGePoint3d& center, const double radius, 
					  const double startAngle, const double endAngle, const Adesk::UInt16 color)
{
	const double PI = 4.0 * atan(1.0);

	worldDraw->subEntityTraits().setColor(color);
	worldDraw->geometry().ellipticalArc(center, AcGeVector3d::kZAxis, radius, radius, startAngle, endAngle, 0);
}

void Utility::DrawText(const AcGiWorldDraw* worldDraw, const AcGePoint3d& position, const std::wstring& string,
					   const AcGiTextStyle& textStyle, const Adesk::UInt16 color)
{
	worldDraw->subEntityTraits().setColor(color);
	worldDraw->geometry().text(position, AcGeVector3d::kZAxis, AcGeVector3d::kXAxis, string.c_str(), -1, Adesk::kFalse, textStyle);
}

void Utility::DrawText(const AcGiWorldDraw* worldDraw, const AcGePoint3d& position, const std::wstring& string, 
					   const AcGiTextStyle& textStyle, const AcDb::TextHorzMode horizontalAlignment, const AcDb::TextVertMode verticalAlignment, 
					   const Adesk::UInt16 color)
{
	AcGePoint2d ext = textStyle.extents(string.c_str(), Adesk::kTrue, -1, Adesk::kFalse);

	double xoff = 0.0; 
	if(horizontalAlignment == AcDb::kTextLeft)
		xoff = 0.0;
	else if(horizontalAlignment == AcDb::kTextRight)
		xoff = -ext.x;
	else // horizontal center
		xoff = -ext.x / 2.0;

	double yoff = 0.0;
	if(verticalAlignment == AcDb::kTextTop) 
		yoff = -ext.y;
	else if(verticalAlignment == AcDb::kTextBase || verticalAlignment == AcDb::kTextBottom)
		yoff = 0.0;
	else // vertical middle
		yoff = -ext.y / 2.0;

	DrawText(worldDraw, AcGePoint3d(position.x + xoff, position.y + yoff, 0), string, textStyle, color);
}
