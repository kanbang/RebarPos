//-----------------------------------------------------------------------------
//----- Utility.cpp : Implementation of utility functions
//-----------------------------------------------------------------------------

#include "StdAfx.h"

#include <sstream>
#include <iomanip>
#include <locale>
#include <fstream>
#include <algorithm>

#include "acgiutil.h"

#include "Utility.h"

#include <boost/filesystem.hpp>
#include <boost/locale.hpp>

Acad::ErrorStatus Utility::CreateHiddenLayer(AcDbObjectId& id)
{
	Acad::ErrorStatus es;
	AcDbLayerTablePointer pLayerTbl(acdbHostApplicationServices()->workingDatabase()->layerTableId(), AcDb::kForRead);
	if ((es = pLayerTbl.openStatus()) != Acad::eOk)
		return es;

	if (pLayerTbl->getAt(_T("Defpoints"), id, AcDb::kForRead) == Acad::eKeyNotFound)
	{
		if (pLayerTbl->upgradeOpen() != Acad::eOk)
			return es;

		AcDbLayerTableRecord* pLayer = new AcDbLayerTableRecord();
		pLayer->setName(_T("Defpoints"));
		pLayerTbl->add(id, pLayer);
		pLayer->close();

		pLayerTbl->downgradeOpen();
	}
	pLayerTbl->close();

	return Acad::eOk;
}

Acad::ErrorStatus Utility::CreateTextStyle(AcDbObjectId& id, const ACHAR* name, const ACHAR* filename, const double scale)
{
	Acad::ErrorStatus es;
	AcDbTextStyleTablePointer pStyleTbl(acdbHostApplicationServices()->workingDatabase()->textStyleTableId(), AcDb::kForRead);
	if ((es = pStyleTbl.openStatus()) != Acad::eOk)
		return es;

	if (pStyleTbl->getAt(name, id, AcDb::kForRead) == Acad::eKeyNotFound)
	{
		if (pStyleTbl->upgradeOpen() != Acad::eOk)
			return es;

		AcDbTextStyleTableRecord* pText = new AcDbTextStyleTableRecord();
		pText->setName(name);
		pText->setFileName(filename);
		pText->setXScale(scale);
		pStyleTbl->add(id, pText);
		pText->close();

		pStyleTbl->downgradeOpen();
	}
	pStyleTbl->close();

	return Acad::eOk;
}

Acad::ErrorStatus Utility::MakeGiTextStyle(AcGiTextStyle &newStyle, const AcDbObjectId& styleId)
{
	Acad::ErrorStatus es;
	es = fromAcDbTextStyle(newStyle, styleId);
	if (es == Acad::eOk)
		newStyle.loadStyleRec();
	return es;
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
	while ((pos = str.find(oldStr, pos)) != std::string::npos)
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

std::wstring Utility::StringFromResource(const HINSTANCE hInstance, const std::wstring& resourceType, const int resid)
{
	HRSRC hResource = FindResource(hInstance, MAKEINTRESOURCE(resid), resourceType.c_str());
	if (!hResource) return L"";

	HGLOBAL hLoadedResource = LoadResource(hInstance, hResource);
	if (!hLoadedResource) return L"";

	DWORD dwResourceSize = SizeofResource(hInstance, hResource);
	if (dwResourceSize == 0) return L"";

	LPVOID pLockedResource = LockResource(hLoadedResource);
	if (!pLockedResource) return L"";

	std::wstring source(static_cast<wchar_t*>(pLockedResource), dwResourceSize / sizeof(wchar_t));

	// Skip UNICODE BOM UTF-16 (LE) 255 254
	if (source[0] == 0xFEFF) source.erase(0, 1);


	FreeResource(hLoadedResource);

	return source;
}

std::wstring Utility::StringFromFile(const std::wstring& filename)
{
	std::wifstream ifs(filename.c_str());

	boost::locale::generator gen;
	std::locale utf8_locale = gen("en_US.UTF-8");
	ifs.imbue(utf8_locale);

	std::wstring source((std::istreambuf_iterator<wchar_t>(ifs)),
		(std::istreambuf_iterator<wchar_t>()));

	return source;
}

void Utility::StringToFile(const std::wstring& filename, const std::wstring& str)
{
	std::wofstream ofs(filename.c_str());

	boost::locale::generator gen;
	std::locale utf8_locale = gen("en_US.UTF-8");
	ofs.imbue(utf8_locale);

	ofs << str;
	ofs << std::endl;

	ofs.flush();
}

void Utility::ClearLog(const std::wstring& filename)
{
	std::wofstream ofs(filename.c_str(), std::wofstream::trunc);
	ofs.close();
}

void Utility::Log(const std::wstring& filename, const std::wstring& message)
{
	std::wofstream ofs(filename.c_str(), std::wofstream::app);
	ofs << message << std::endl;
	ofs.close();
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
	else if (rb->restype != code)
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
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rint;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFUInt(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned short& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rint;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFLong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, int& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rlong;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFULong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned int& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rlong;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFLong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, long& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rlong;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFULong(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, unsigned long& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rlong;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFReal(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, double& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = rb.resval.rreal;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFString(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, ACHAR*& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		acutDelString(val);
		val = NULL;
		if (rb.resval.rstring != NULL)
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
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
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
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
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
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
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
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
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
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		val = (rb.resval.rint == 0) ? Adesk::kFalse : Adesk::kTrue;
	}
	return es;
}

Acad::ErrorStatus Utility::ReadDXFObjectId(AcDbDxfFiler* pFiler, const short code, const ACHAR* name, AcDbObjectId& val)
{
	Acad::ErrorStatus es;
	resbuf rb;
	if ((es = ReadDXFItem(pFiler, code, name, &rb)) == Acad::eOk)
	{
		if ((es = acdbGetObjectId(val, rb.resval.rlname)) != Acad::eOk)
		{
			val = AcDbObjectId::kNull;
		}
	}
	return es;
}

std::vector<std::wstring> Utility::SplitString(const std::wstring& s, const std::wstring& delim, const bool skipEmpty)
{
	std::vector<std::wstring> elems;

	size_t start = 0, end = 0;

	while (end != std::wstring::npos)
	{
		end = s.find(delim, start);

		std::wstring item;
		if (end == std::wstring::npos)
			item = s.substr(start, std::wstring::npos);
		else
			item = s.substr(start, end - start);

		if (!skipEmpty || item.size() > 0)
			elems.push_back(item);

		if (end > std::wstring::npos - delim.size())
			start = std::wstring::npos;
		else
			start = end + delim.size();
	}

	return elems;
}

std::vector<std::wstring> Utility::SplitString(const std::wstring& s, const wchar_t delim, const bool skipEmpty)
{
	std::vector<std::wstring> elems;

	std::wstringstream ss(s);
	std::wstring item;
	while (std::getline(ss, item, delim))
	{
		if (!skipEmpty || item.size() > 0)
			elems.push_back(item);
	}

	return elems;
}

std::vector<std::wstring> Utility::SplitString(const std::wstring& s, const bool skipEmpty)
{
	return SplitString(s, L'\n', skipEmpty);
}

void Utility::DrawLine(const AcGiWorldDraw* worldDraw, const AcGePoint3d& pt1, const AcGePoint3d& pt2, const Adesk::UInt16 color)
{
	worldDraw->subEntityTraits().setColor(color);
	AcGePoint3d line[2];
	line[0] = pt1;
	line[1] = pt2;
	worldDraw->geometry().polyline(2, line);
}

void Utility::DrawDoubleLine(const AcGiWorldDraw* worldDraw, const AcGePoint3d& pt1, const AcGePoint3d& pt2, const double offset, const Adesk::UInt16 color)
{
	AcGeVector3d offVec = (pt2 - pt1).perpVector().normalize() * offset / 2.0;
	DrawLine(worldDraw, pt1 + offVec, pt2 + offVec, color);
	DrawLine(worldDraw, pt1 - offVec, pt2 - offVec, color);
}

void Utility::DrawCircle(const AcGiWorldDraw* worldDraw, const AcGePoint3d& center, const double radius, const Adesk::UInt16 color)
{
	worldDraw->subEntityTraits().setColor(color);
	worldDraw->geometry().circle(center, radius, AcGeVector3d::kZAxis);
}

void Utility::DrawEllipse(const AcGiWorldDraw* worldDraw, const AcGePoint3d& center, const double majorAxisLength, const double minorAxisLength, const Adesk::UInt16 color)
{
	worldDraw->subEntityTraits().setColor(color);
	worldDraw->geometry().ellipticalArc(center, AcGeVector3d::kZAxis, majorAxisLength, minorAxisLength, 0.0, 6.28318530717958647692, 0);
}

void Utility::DrawArc(const AcGiWorldDraw* worldDraw, const AcGePoint3d& center, const double radius,
	const double startAngle, const double endAngle, const Adesk::UInt16 color)
{
	worldDraw->subEntityTraits().setColor(color);
	worldDraw->geometry().ellipticalArc(center, AcGeVector3d::kZAxis, radius, radius, startAngle, endAngle, 0);
}

void Utility::DrawEllipticalArc(const AcGiWorldDraw* worldDraw, const AcGePoint3d& center, const double majorAxisLength, const double minorAxisLength, const double startAngle, const double endAngle, const Adesk::UInt16 color)
{
	worldDraw->subEntityTraits().setColor(color);
	worldDraw->geometry().ellipticalArc(center, AcGeVector3d::kZAxis, majorAxisLength, minorAxisLength, startAngle, endAngle, 0);
}

void Utility::DrawText(const AcGiWorldDraw* worldDraw, const AcGePoint3d& position, const std::wstring& string,
	const AcGiTextStyle& textStyle, const Adesk::UInt16 color)
{
	DrawText(worldDraw, position, string, textStyle, AcDb::kTextLeft, AcDb::kTextBottom, color);
}

void Utility::DrawText(const AcGiWorldDraw* worldDraw, const AcGePoint3d& position, const std::wstring& string,
	const AcGiTextStyle& textStyle, const AcDb::TextHorzMode horizontalAlignment, const AcDb::TextVertMode verticalAlignment,
	const Adesk::UInt16 color)
{
	// Split into lines
	std::vector<std::wstring> lines = SplitString(string, std::wstring(L"\\P"));
	if (lines.size() == 0) return;

	// Measure text lines
	std::vector<double> widths;
	std::vector<double> heights;
	double totalHeight = 0.0;
	double lineSpacing = 0.0;
	double totalWidth = 0.0;
	double lineHeight = 0.0;
	for (std::vector<std::wstring>::iterator it = lines.begin(); it != lines.end(); ++it)
	{
		std::wstring line = (*it);
		AcGePoint2d ext = textStyle.extents(line.c_str(), Adesk::kTrue, -1, Adesk::kFalse);
		widths.push_back(ext.x);
		heights.push_back(ext.y);
		totalWidth = max(totalWidth, ext.x);
	}
	lineHeight = 1.333 * textStyle.textSize();
	lineSpacing = 0.333 * lineHeight;
	totalHeight = lineHeight * (double)lines.size() + lineSpacing * (double)(lines.size() - 1);

	// Vertical location of first line
	double y = 0.0;
	if (verticalAlignment == AcDb::kTextTop)
		y = 0.0;
	else if (verticalAlignment == AcDb::kTextBase || verticalAlignment == AcDb::kTextBottom)
		y = totalHeight;
	else // vertical middle
		y = totalHeight / 2.0;

	// Draw lines
	worldDraw->subEntityTraits().setColor(color);
	size_t i = 0;
	for (std::vector<std::wstring>::iterator it = lines.begin(); it != lines.end(); ++it)
	{
		std::wstring line = (*it);

		y -= lineHeight;
		double x = 0.0;
		if (horizontalAlignment == AcDb::kTextLeft)
			x = 0.0;
		else if (horizontalAlignment == AcDb::kTextRight)
			x = -widths[i];
		else // horizontal center
			x = -widths[i] / 2.0;

		worldDraw->geometry().text(AcGePoint3d(position.x + x, position.y + y, 0), AcGeVector3d::kZAxis, AcGeVector3d::kXAxis, line.c_str(), -1, Adesk::kFalse, textStyle);

		// Next line
		y -= lineSpacing;
		i++;
	}
}

AcGePoint2d Utility::MeasureText(const std::wstring& string, const AcGiTextStyle& textStyle)
{
	// Split into lines
	std::vector<std::wstring> lines = SplitString(string, std::wstring(L"\\P"));
	if (lines.size() == 0) return AcGePoint2d(0, 0);

	// Measure text lines
	double totalWidth = 0.0;
	double totalHeight = 0.0;
	double lineSpacing = 0.0;
	double lineHeight = 0.0;
	for (std::vector<std::wstring>::iterator it = lines.begin(); it != lines.end(); ++it)
	{
		std::wstring line = (*it);
		AcGePoint2d ext = textStyle.extents(line.c_str(), Adesk::kTrue, -1, Adesk::kFalse);
		totalWidth = max(totalWidth, ext.x);
	}
	lineHeight = 1.333 * textStyle.textSize();
	lineSpacing = 0.333 * lineHeight;
	totalHeight = lineHeight * (double)lines.size() + lineSpacing * (double)(lines.size() - 1);

	return AcGePoint2d(totalWidth, totalHeight);
}
